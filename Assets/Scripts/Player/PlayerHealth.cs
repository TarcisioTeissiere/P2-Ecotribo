using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;             // Vida máxima do jogador
    private int currentHealth;            // Vida atual do jogador
    private Animator animator;            // Referência ao Animator
    private PlayerAttack playerAttack;    // Referência ao script de ataque do jogador

    public Image[] hearts;                // Array para armazenar as imagens de coração

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        UpdateHearts();
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
            StartCoroutine(Die());
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth);
        }
    }

    private IEnumerator Die()
    {
        Debug.Log("Jogador morreu!");
        animator.SetTrigger("playerDeath");
        float deathAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        float extraTime = 0.5f; // Tempo extra que você deseja adicionar (em segundos)

        // Espera o tempo da animação de morte + o tempo extra
        yield return new WaitForSeconds(deathAnimationTime + extraTime);

        SceneManager.LoadScene("Menu");
    }
}