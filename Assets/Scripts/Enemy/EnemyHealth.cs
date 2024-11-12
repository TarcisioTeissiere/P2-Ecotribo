using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 1;  // O inimigo tem apenas uma vida
    private Animator animator;
    public GameObject[] trashItemPrefabs;  // Array para os diferentes tipos de lixo
    private float dropChance = 0.8f; // Chance de gerar o lixo

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log("Inimigo tomou dano. Vida atual: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Inimigo derrotado!");
        animator.SetTrigger("isDead");  // Aciona a animação de morte

        // Checa se o lixo deve ser gerado
        if (Random.value <= dropChance)
        {
            // Gera um item de lixo aleatório
            int randomIndex = Random.Range(0, trashItemPrefabs.Length);
            Instantiate(trashItemPrefabs[randomIndex], transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 1f);  // Destroi o inimigo após a animação de morte
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Verifica se o jogador está atacando antes de aplicar dano
            PlayerAttack playerAttack = collision.collider.GetComponent<PlayerAttack>();
            if (playerAttack != null && playerAttack.isAttacking)
            {
                TakeDamage(1); // Aplica o dano ao inimigo
            }
            else
            {
                Debug.Log("O jogador colidiu, mas não está atacando.");
            }
        }
    }
}
