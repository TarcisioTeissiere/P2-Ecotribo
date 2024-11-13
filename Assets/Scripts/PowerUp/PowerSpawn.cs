using UnityEngine;
using System.Collections;

public class PowerSpawn : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 20f;

    private void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
