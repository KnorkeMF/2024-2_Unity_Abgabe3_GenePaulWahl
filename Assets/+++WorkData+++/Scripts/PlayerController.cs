using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager;       // Reference to coin manager
    [SerializeField] private UIManager uiManager;           // Reference to UI manager

    [SerializeField] private float speed = 2.0f;             // Movement speed of the player
    [SerializeField] private float jumpForce = 2.0f;         // Force applied when jumping
    private float direction = 0f;                             // Direction input (-1 left, 1 right)

    public Rigidbody2D rb;                                   // Rigidbody2D component reference
    private SpriteRenderer spriteRenderer;                    // SpriteRenderer component reference

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;          // Position to check if grounded
    [SerializeField] private LayerMask groundLayer;           // Layer considered as ground

    public bool canMove = true;                               // Flag to enable/disable movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                     // Get Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>();      // Get SpriteRenderer component
    }

    void Update()
    {
        if (!canMove) return;                                  // Exit if movement disabled

        direction = 0f;                                        // Reset direction each frame

        if (Keyboard.current.aKey.isPressed)                  // Check if 'A' key pressed
        {
            direction = -1f;                                   // Move left
        }
        else if (Keyboard.current.dKey.isPressed)             // Check if 'D' key pressed
        {
            direction = 1f;                                    // Move right
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)    // Check if jump pressed this frame
        {
            Jump();                                            // Perform jump
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);  // Apply horizontal velocity

        if (direction > 0)                                     // If moving right
        {
            spriteRenderer.flipX = false;                      // Face right
        }
        else if (direction < 0)                                // If moving left
        {
            spriteRenderer.flipX = true;                       // Face left
        }
    }

    private void Jump()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);  // Check ground
        if (isGrounded)                                        // Only jump if grounded
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Apply jump force
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("wir sind kollidiert");                      // Log collision event

        if (other.CompareTag("coin"))                          // Check if collided with coin
        {
            Debug.Log("mit einer Münze");                      // Log coin collision
            Destroy(other.gameObject);                          // Remove coin from scene
            coinManager.AddCoin();                              // Add coin to player count
        }

        if (other.CompareTag("diamond"))                       // Check if collided with diamond
        {
            Debug.Log("diamond kollidiert");                   // Log diamond collision
            Destroy(other.gameObject);                          // Remove diamond from scene
            coinManager.AddDia();                               // Add diamond count
            uiManager.PlusCountdown();                          // Increase countdown timer
        }

        if (other.CompareTag("obstacle"))                      // Check if collided with obstacle
        {
            Debug.Log("Es war ein obstacle");                  // Log obstacle collision
            uiManager.ShowPanelLost();                          // Show lose panel
            MovementStop();                                     // Stop player movement
        }

        if (other.CompareTag("portal"))                        // Check if collided with portal
        {
            Debug.Log("Es war ein portal");                    // Log portal collision
            Destroy(other.gameObject);                          // Remove portal object
            MovementStop();                                     // Stop player movement
            gameObject.SetActive(false);                        // Disable player object
            uiManager.ShowPanelWin();                           // Show win panel
        }
    }

    public void MovementStop()
    {
        canMove = false;                                        // Disable movement flag
        rb.linearVelocity = Vector2.zero;                             // Stop player velocity
    }
}
