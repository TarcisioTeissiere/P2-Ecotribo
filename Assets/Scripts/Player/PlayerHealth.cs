using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth; 
    private Animator animator;
    public Image[] hearts;
    public bool isDead = false; // Estado de morte do jogador
    private PlayerAudioManager audioManager;

    private void Start()
    {
        currentHealth = maxHealth; // Inicia com a vida máxima
        animator = GetComponent<Animator>();

        audioManager = GetComponent<PlayerAudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("PlayerAudioManager não encontrado no Player.");
        }

        UpdateHearts(); // Atualiza as vidas na interface
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isDead)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Não aplica dano se o jogador já estiver morto

        currentHealth -= damage;
        Debug.Log("Jogador recebeu dano. Vida atual: " + currentHealth);
        if (audioManager != null)
        {
            audioManager.PlayHurtSound();
        }
        animator.SetTrigger("playerHit");
        UpdateHearts(); // Atualiza interface

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private void UpdateHearts()
    {
        // Atualiza as imagens das vidas na interface
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth);
        }
    }

    private IEnumerator Die()
    {
        if (isDead) yield break; // Impede múltiplas chamadas
        isDead = true;

        Debug.Log("Jogador morreu!");

        if (audioManager != null)
        {
            audioManager.PlayDeathSound();
        }

        DisablePlayerControls();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Desativa o colisor para evitar interações adicionais
        GetComponent<Collider2D>().enabled = false;

        animator.SetTrigger("playerDeath");
        float deathAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationTime+1);
        Fade.Instance.TransitionToScene("Menu");
    }

    private void DisablePlayerControls()
    {
        // Desativa os controles do jogador (movimento e ataque)
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        if (playerAttack != null)
        {
            playerAttack.enabled = false;
        }
    }
}
