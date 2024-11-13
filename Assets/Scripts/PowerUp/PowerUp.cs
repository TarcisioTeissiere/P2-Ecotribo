using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speedBoost = 2f;
    public float duration = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.collider.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedBoost(duration, speedBoost);
            }
            Destroy(gameObject);
        }
    }
}
