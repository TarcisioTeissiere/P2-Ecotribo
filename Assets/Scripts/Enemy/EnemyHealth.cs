using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 1;             // Vida inicial do inimigo
    private Animator animator;
    public GameObject[] trashItemPrefabs;      // Prefabs para o lixo que o inimigo deixa
    private float dropChance = 0.8f;           // Chance de gerar o lixo
    private PlayerHealth playerHealth;
    
    // Adicione a referência ao script de controle de vitória
    private VictoryEnemySpawn victoryEnemySpawn;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        victoryEnemySpawn = GameObject.FindObjectOfType<VictoryEnemySpawn>(); // Encontra o VictoryEnemySpawn na cena
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log("Inimigo tomou dano. Vida atual: " + currentHealth);

            StartCoroutine(PlayHitAnimation());

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator PlayHitAnimation()
    {
        animator.SetTrigger("slimeHit");
        float hitAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        float extraHitTime = 0.3f; // Tempo extra em segundos para a animação de hit
        yield return new WaitForSeconds(hitAnimationTime + extraHitTime);
    }

    private IEnumerator Die()
    {
        Debug.Log("Inimigo derrotado!");
        animator.SetTrigger("isDead");

        // Notifica o VictoryEnemySpawn que um inimigo morreu
        if (victoryEnemySpawn != null)
        {
            victoryEnemySpawn.EnemyDefeated(); // Chama o método que conta a morte
        }

        if (Random.value <= dropChance)
        {
            Debug.Log("Item de drop será instanciado.");
            int randomIndex = Random.Range(0, trashItemPrefabs.Length);
            GameObject item = Instantiate(trashItemPrefabs[randomIndex], transform.position, Quaternion.identity);

            if (item != null)
            {
                Debug.Log("Item instanciado com sucesso: " + item.name);
            }
            else
            {
                Debug.LogWarning("Falha ao instanciar o item de drop!");
            }
        }
        else
        {
            Debug.Log("Item de drop não foi instanciado desta vez.");
        }

        float deathAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        float extraDeathTime = 0.5f;
        yield return new WaitForSeconds(deathAnimationTime + extraDeathTime);

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Animator playerAnimator = collision.collider.GetComponent<Animator>();
            bool isPlayerAttacking = playerAnimator != null && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("playerAttack");

            if (isPlayerAttacking && currentHealth > 0)
            {
                TakeDamage(1);
            }
            else
            {
                // Se o jogador não estiver atacando, o inimigo não toma dano
                Debug.Log("Inimigo não tomou dano porque o jogador não estava atacando.");
            }
        }

    }
}
