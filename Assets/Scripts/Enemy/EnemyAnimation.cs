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
        if (enemyHealth.currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            return; 
        }
        bool isWalking = enemyMovement.IsMoving();
        animator.SetBool("isWalking", isWalking);
    }

    // Função para ativar a animação de ataque do Slime
    public void TriggerAttack()
    {
        animator.SetTrigger("isAttacking");
    }

    // Função para ativar a animação de dano do Slime
    public void TakeHit()
    {
        animator.SetTrigger("slimeHit");
    }
}
