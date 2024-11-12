using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo a ser instanciado
    public Transform spawnPoint;   // Ponto de spawn dos inimigos
    public float spawnInterval = 2f; // Intervalo de spawn em segundos
    private float spawnTimer;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>(); // Encontra o PlayerInventory na cena
        spawnTimer = spawnInterval; // Inicializa o timer de spawn 

    }

/*
   private void Update()
{
    if (playerInventory != null && !playerInventory.IsInventoryFull())
    {
        spawnTimer -= Time.deltaTime;

        // Checa se spawnTimer chegou a zero ou abaixo para spawnar o inimigo
        if (spawnTimer <= 0f)
        {
            Debug.Log("Instanciando inimigo em: " + spawnPoint.position);
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnTimer = spawnInterval; // Reinicializa o timer corretamente
        }
    }
}
*/

}
