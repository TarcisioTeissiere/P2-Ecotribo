using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    private float spawnTimer;

    private PlayerInventory playerInventory;
    private int enemyCount = 0;
    private int maxEnemies = 10;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        // Condição para parar o spawn ao atingir o máximo de inimigos
        if (playerInventory != null && !playerInventory.IsInventoryFull() && enemyCount < maxEnemies)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                Debug.Log("Instanciando inimigo em: " + spawnPoint.position);
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemyCount++;
                spawnTimer = spawnInterval;
            }
        }

        // Verifica se o inventário está vazio e o número de inimigos atingiu o máximo
        if (playerInventory != null && playerInventory.IsInventoryEmpty() && enemyCount >= maxEnemies)
        {
            LoadNextScene(); // Carrega a nova cena
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("Carregando nova cena...");
        SceneManager.LoadScene("Second"); // Substitua pelo nome da cena desejada
    }
}