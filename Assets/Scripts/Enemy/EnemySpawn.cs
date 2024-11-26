using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 5f;
    private float spawnTimer;
    private PlayerInventory playerInventory;
    private int enemyCount = 0;
    private int maxEnemies = 5;
    private ScoreManager pscore; 
    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>(); 
        pscore = FindObjectOfType<ScoreManager>();  
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
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "First" && pscore.score >= 400)
            {
                LoadNextScene();  
                enemyCount = 0;   
            }

            else if (currentScene == "Second" && pscore.score >= 600)
            {
                LoadVictoryScene();
            }
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("Carregando nova cena...");
        Fade.Instance.TransitionToScene("Second");
    }

    private void LoadVictoryScene()
    {
        Debug.Log("Condições de vitória atendidas! Carregando tela de vitória...");
        Fade.Instance.TransitionToScene("Victory");
    }
}
