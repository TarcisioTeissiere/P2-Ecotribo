using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 0.5f;    // Distância mínima para atacar
    public int damage = 1;              // Dano causado ao jogador
    private Transform player;
    private Animator animator;
    private float attackCooldown = 2f;  // Tempo de cooldown entre ataques
    private float lastAttackTime = -Mathf.Infinity;  // Armazena o tempo do último ataque

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verifica se o jogador está no alcance e o cooldown foi cumprido
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;    // Atualiza o tempo do último ataque
        animator.SetTrigger("isAttacking"); // Aciona a animação de ataque
    }

    // Esse método é chamado via evento da animação ou um Collider
    public void ApplyDamage()
    {
        if (player != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);  // Aplica dano ao jogador
        }
    }
}
