using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    public EnemyHealth enemyHealth; 
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
            return; // Se estiver morto, não deve mais fazer nada além de animação de morte
        }

        // Verifica se o inimigo está andando
        bool isWalking = enemyMovement.IsMoving();
        animator.SetBool("isWalking", isWalking);
    }

    // Função para ativar a animação de ataque
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
