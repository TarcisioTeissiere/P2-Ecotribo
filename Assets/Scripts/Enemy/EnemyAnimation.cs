using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private EnemyHealth enemyHealth; 
    private EnemyMovement enemyMovement; 

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        // Verifica se o inimigo está morto
        if (enemyHealth.currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            return;
        }

        // Verifica se o inimigo está andando
        bool isWalking = enemyMovement.IsMoving();
        animator.SetBool("isWalking", isWalking);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("isAttacking");
    }

    // Função para ativar a animação de dano
    public void TakeHit()
    {
        animator.SetTrigger("slimeHit");
    }
}
