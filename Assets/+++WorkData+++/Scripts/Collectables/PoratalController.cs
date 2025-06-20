using UnityEngine;

public class PoratalController : MonoBehaviour
{
   public float speed;
   
   
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
