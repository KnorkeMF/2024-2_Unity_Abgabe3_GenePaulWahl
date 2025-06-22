using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager;       // Reference to the coin manager
    [SerializeField] private UIManager uiManager;           // Reference to the UI manager
    
    public AudioSource audioSource;                         // Audio source for playing sounds
    public AudioClip coinSound;                             // Sound clip for collecting a coin
    public AudioClip diamondSound;                          // Sound clip for collecting a diamond
    public AudioClip jumpSound;                             // Sound clip for jumping

    [SerializeField] private float speed = 2.0f;            // Horizontal movement speed
    [SerializeField] private float jumpForce = 2.0f;        // Vertical jump force
    private float direction = 0f;                           // Current horizontal input direction

    public Rigidbody2D rb;                                  // Reference to the Rigidbody2D component
    private SpriteRenderer spriteRenderer;                  // Reference to the SpriteRenderer

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;         // Transform used to check if grounded
    [SerializeField] private LayerMask groundLayer;         // Layer mask identifying what counts as ground

    public bool canMove = true;                             // Flag to enable or disable player movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                   // Get Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>();    // Get SpriteRenderer component
    }

    void Update()
    {
        if (!canMove) return;                               // Exit if movement is disabled

        direction = 0f;                                      // Reset direction every frame

        if (Keyboard.current.aKey.isPressed)                // Move left if 'A' key is pressed
        {
            direction = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)           // Move right if 'D' key is pressed
        {
            direction = 1f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)  // Check if space was pressed this frame
        {
            bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

            if (isGrounded)                                 // Only jump if on the ground
            {
                Jump();                                     
                PlaySound(jumpSound);                        // Play jump sound
            }
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);  // Apply horizontal velocity

        if (direction > 0)                                  // Face right if moving right
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)                             // Face left if moving left
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Apply vertical jump force
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected");                    // Log any trigger collision

        if (other.CompareTag("coin"))                       // If collided with a coin
        {
            Debug.Log("Collided with a coin");
            PlaySound(coinSound);                           // Play coin sound
            Destroy(other.gameObject);                      // Remove coin object
            coinManager.AddCoin();                          // Increase coin count
        }

        if (other.CompareTag("diamond"))                    // If collided with a diamond
        {
            Debug.Log("Collided with a diamond");
            PlaySound(diamondSound);                        // Play diamond sound
            Destroy(other.gameObject);                      // Remove diamond object
            coinManager.AddDia();                           // Increase diamond count
            uiManager.PlusCountdown();                      // Extend countdown timer
        }

        if (other.CompareTag("obstacle"))                   // If collided with an obstacle
        {
            Debug.Log("Collided with an obstacle");
            uiManager.ShowPanelLost();                      // Show lose panel
            MovementStop();                                 // Stop player movement
        }

        if (other.CompareTag("portal"))                     // If collided with a portal
        {
            Debug.Log("Collided with a portal");
            Destroy(other.gameObject);                      // Remove portal object
            MovementStop();                                 // Stop player movement
            gameObject.SetActive(false);                    // Disable player object
            uiManager.ShowPanelWin();                       // Show win panel
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);                  // Play a one-shot sound
        }
    }

    public void MovementStop()
    {
        canMove = false;                                    // Disable movement
        rb.linearVelocity = Vector2.zero;                   // Stop all player velocity
    }
}
