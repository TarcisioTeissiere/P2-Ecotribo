using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 1; // Vida inicial do Slime
    public GameObject[] trashItemPrefabs; // Prefabs de lixo gerado
    public float dropChance = 0.8f; // Chance de dropar item de lixo
    private bool isDead = false; // Flag para evitar múltiplos eventos de morte
    private bool hasDamagedPlayer = false; // Flag para evitar dano duplicado
    private Animator animator;
    private Collider2D enemyCollider;
    private EnemyAudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        audioManager = GetComponent<EnemyAudioManager>();

        if (audioManager == null)
        {
            Debug.LogWarning("EnemyAudioManager não encontrado no Slime.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead || hasDamagedPlayer) return; // Previne ações após a morte ou múltiplos danos

        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                hasDamagedPlayer = true; 
                Debug.Log("Slime causou dano ao jogador!");

                // Inicia o processo de dano e morte
                StartCoroutine(HandleDamageAndDeath());
            }
        }
    }

    private IEnumerator HandleDamageAndDeath()
    {
        if (isDead) yield break; // Garante que o processo não será duplicado
        isDead = true;

        DisableEnemyCollisions();
        if (animator != null)
        {
            animator.SetTrigger("slimeHit");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        // Executa a animação de morte
        if (animator != null)
        {
            animator.SetTrigger("isDead");
            if (audioManager != null) audioManager.PlayDeathSound();
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        // Dropar item de lixo
        if (Random.value <= dropChance && trashItemPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, trashItemPrefabs.Length);
            Instantiate(trashItemPrefabs[randomIndex], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void DisableEnemyCollisions()
    {
        // Desativa o colisor para evitar múltiplas interações
        if (enemyCollider != null) enemyCollider.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Slime tomou dano. Vida atual: " + currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(HandleDamageAndDeath());
        }
        else
        {
            // Toca a animação de hit
            if (animator != null)
            {
                animator.SetTrigger("slimeHit");
                if (audioManager != null) audioManager.PlayDeathSound();
            }
        }
    }
}
