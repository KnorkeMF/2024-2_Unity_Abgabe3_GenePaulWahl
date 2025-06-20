using UnityEngine;

public class LavaPlatformScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    
    public float upSpeed = 1f;
    public float downSpeed = 5f;

    private bool movingUp = true;
    private float threshold = 0.1f;
    
    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.Lerp(transform.position, pointA.position, Time.deltaTime * upSpeed);

            if (Vector3.Distance(transform.position, pointA.position) < threshold)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, pointB.position, Time.deltaTime * downSpeed);

            if (Vector3.Distance(transform.position, pointB.position) < threshold)
            {
                movingUp = true;
            }
        }
    }
}