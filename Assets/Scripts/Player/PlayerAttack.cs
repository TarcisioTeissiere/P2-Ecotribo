using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;               // Referência ao Animator
    public GameObject swordCollider;        // Referência ao objeto SwordCollider
    public bool isAttacking = false;        // Controle do estado de ataque

    private void Start()
    {
        // Certifique-se de que o collider da espada comece desativado
        swordCollider.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        // Verifica se a tecla de ataque foi pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Inicia a animação de ataque
        animator.SetTrigger("playerAttack");

        // Marca o jogador como atacando
        isAttacking = true;

        // Ativa o collider da espada para o ataque
        swordCollider.GetComponent<Collider2D>().enabled = true;

        // Desativa o collider da espada após a duração do ataque
        Invoke("ResetAttack", 0.5f); // Ajuste o tempo conforme a duração da animação de ataque
    }

    private void ResetAttack()
    {
        // Quando a animação terminar, o jogador não está mais atacando
        isAttacking = false;

        // Desativa o collider da espada após o ataque
        swordCollider.GetComponent<Collider2D>().enabled = false;
    }
}
