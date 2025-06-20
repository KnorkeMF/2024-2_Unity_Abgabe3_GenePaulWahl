using UnityEngine;

public class KometRotator : MonoBehaviour
{
    private float rotationSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(-180f, 180f);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}