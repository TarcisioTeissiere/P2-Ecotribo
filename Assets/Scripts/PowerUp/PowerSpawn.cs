using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject powerUpPrefab; 
    private Vector2 screenBounds;
    private float powerUpWidth;
    private float powerUpHeight;

    private void Start()
    {
        // Obtém os limites da câmera principal
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        
        // Obtém o tamanho do PowerUp para evitar spawn fora da tela
        SpriteRenderer spriteRenderer = powerUpPrefab.GetComponent<SpriteRenderer>();
        powerUpWidth = spriteRenderer.bounds.size.x / 2;
        powerUpHeight = spriteRenderer.bounds.size.y / 2;
        SpawnPowerUp();
    }

    public void SpawnPowerUp()
    {
        // Gera posição aleatória dentro dos limites ajustados
        float randomX = Random.Range(-screenBounds.x + powerUpWidth, screenBounds.x - powerUpWidth);
        float randomY = Random.Range(-screenBounds.y + powerUpHeight, screenBounds.y - powerUpHeight);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
}
