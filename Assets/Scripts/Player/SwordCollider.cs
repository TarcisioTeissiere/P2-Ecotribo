using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    private bool isAttacking = false; // Verificar se o jogador está atacando

    // Atualiza o estado de ataque com base no input do jogador
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isAttacking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isAttacking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAttacking) return; // Só executa se o jogador estiver atacando

        if (other.CompareTag("Enemy"))
        {
            // Aplica dano ao inimigo
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
            }
        }
    }
}
