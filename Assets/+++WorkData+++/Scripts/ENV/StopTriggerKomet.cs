using UnityEngine;

public class StopTriggerKomet : MonoBehaviour
{
    public KometenSpawner spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.StopSpawning();
            Destroy(gameObject);
        }
    }
}