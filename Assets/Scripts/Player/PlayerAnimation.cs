using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovementAnimation(Vector2 moveDirection)
    {
        bool isWalking = moveDirection.magnitude > 0;

        animator.SetBool("isWalking", isWalking); // Atualiza a animação de movimento
    }

    public void Attack() // Novo método para ataque
    {
        animator.SetTrigger("playerAttack"); // Chama a animação de ataque
    }

    public bool IsAttacking() // Método para verificar se o jogador está atacando
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("playerAttack");
    }
}
