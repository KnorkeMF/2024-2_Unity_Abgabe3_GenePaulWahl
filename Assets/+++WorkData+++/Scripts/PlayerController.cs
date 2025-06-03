using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private CoinManager coinManager;
    [SerializeField] UIManager uiManager;
    
    [SerializeField] private float speed = 2.0f;
    [SerializeField] float jumpForce = 2.0f;
    private float direction = 0f;
    
    Rigidbody2D rb;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    bool canMove = true; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            direction = 0f;
        
            if (Keyboard.current.aKey.isPressed)
            {
                direction = -1;
            }
        
            if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }

            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

            void Jump()
            {
                if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
                {rb.linearVelocity = new Vector2(x:0, jumpForce);}
            }
        } 
  
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log(message: "wir sind kollidiert");

        if (other.CompareTag("coin"))
        {
            Debug.Log(message: "mit einer Münze " );
            Destroy(other.gameObject);
            coinManager.AddCoin();  
        }

        else if (other.CompareTag("obstacle"))
        {
            Debug.Log(message: "Es war ein obstacle");
            uiManager.ShowPanelLost();
            rb.linearVelocity = Vector2.zero;
            canMove = false;
        }
       
    }
}
