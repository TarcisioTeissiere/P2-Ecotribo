using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 0.5f; // Distância mínima para atacar
    public int damage = 1;           // Dano causado ao jogador
    private Transform player;
    private PlayerAttack playerAttack; // Referência ao PlayerAttack do jogador
    private bool isAttacking = false;
    private Animator animator;       // Referência ao Animator
    private float attackCooldown = 5f; // Tempo de cooldown entre ataques
    private float lastAttackTime = 0f; // Armazena o tempo do último ataque

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAttack = player.GetComponent<PlayerAttack>(); // Obtém o PlayerAttack
        animator = GetComponent<Animator>(); // Inicializa o Animator
    }

    private void Update()
    {
        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verifica se o jogador está no alcance e não está atacando
        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            // Verifica se o cooldown foi cumprido e se o jogador não está atacando
            if (Time.time >= lastAttackTime + attackCooldown && !playerAttack.isAttacking)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time; // Atualiza o tempo do último ataque
        animator.SetTrigger("isAttacking"); // Chama a animação de ataque

        // Reduz a vida do jogador, apenas se ele não estiver atacando
        if (!playerAttack.isAttacking)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        // Reinicia o estado de ataque após um intervalo
        Invoke("ResetAttack", 1f);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}