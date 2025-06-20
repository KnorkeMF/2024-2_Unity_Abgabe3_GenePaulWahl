using System.Collections;
using UnityEngine;

public class KometenSpawner : MonoBehaviour
{
    public GameObject kometPrefab;
    public Transform player;
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;
    public float spawnRangeX = 20f;
    public float spawnHeightAbovePlayer = 10f;
    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y + spawnHeightAbovePlayer, 0);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            SpawnKomet();
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnKomet()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(transform.position.x - spawnRangeX, transform.position.x + spawnRangeX),
            transform.position.y
        );

        Instantiate(kometPrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
