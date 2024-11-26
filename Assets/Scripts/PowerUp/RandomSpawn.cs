using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 20f;

    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    private void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            // Gera uma posição aleatória dentro dos limites definidos
            Vector3 randomPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                0f // Mantém a posição no plano XY
            );
            Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
