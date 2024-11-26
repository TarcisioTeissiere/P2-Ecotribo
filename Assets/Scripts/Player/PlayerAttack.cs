using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PolygonCollider2D swordCollider;
    private Animator animator;
    private PlayerAudioManager audioManager;

    public bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (swordCollider == null)
        {
            swordCollider = GetComponentInChildren<PolygonCollider2D>(); // Localiza o Collider da espada, se não atribuído
        }
        audioManager = GetComponent<PlayerAudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("PlayerAudioManager não encontrado no Player.");
        }

        // Garante que o Collider da espada esteja desativado inicialmente
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;

        // Ativa o Collider da espada
        if (swordCollider != null)
        {
            swordCollider.enabled = true;
        }
        animator.SetTrigger("playerAttack");

        if (audioManager != null)
        {
            audioManager.PlayAttackSound();
        }

        // Reinicia o estado de ataque após a animação
        float attackDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("ResetAttack", attackDuration); // Ajuste conforme a duração da animação
    }

    private void ResetAttack()
    {
        isAttacking = false;

        // Desativa o Collider da espada
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }
}
