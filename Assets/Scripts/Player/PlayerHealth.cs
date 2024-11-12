using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;             // Vida máxima do jogador
    private int currentHealth;            // Vida atual do jogador
    private Animator animator;            // Referência ao Animator

    public Image[] hearts;                // Array para armazenar as imagens de coração

    private void Start()
    {
        // Define a vida inicial como a vida máxima
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Inicializa o Animator

        UpdateHearts(); // Atualiza as imagens de coração inicialmente
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("playerAttack"))
            {
                TakeDamage(1); // Aplica 1 de dano ao jogador
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jogador recebeu dano. Vida atual: " + currentHealth);

        animator.SetTrigger("playerHit");
        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth); // Mostra ou esconde os corações com base na vida atual
        }
    }

    private void Die()
    {
        Debug.Log("Jogador morreu!");
        animator.SetTrigger("playerDeath");
        Destroy(gameObject, 0.5f);
        SceneManager.LoadScene("Menu");
    }
}
