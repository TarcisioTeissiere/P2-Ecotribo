using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f; // Distância do ataque
    public LayerMask enemyLayer;   // Camada dos inimigos

    private Animator animator;
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Inicia a animação de ataque
        animator.SetTrigger("playerAttack");

        // Detecta inimigos dentro do alcance de ataque e aplica dano
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Aplica dano ao inimigo se a animação de ataque estiver acontecendo
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerAttack"))
            {
                enemy.GetComponent<EnemyHealth>()?.TakeDamage(1);
            }
        }

        // Reinicia o estado de ataque após a animação
        Invoke("ResetAttack", 0.5f); // Ajuste conforme a duração da animação
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}
