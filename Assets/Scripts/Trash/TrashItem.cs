using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public TrashTypeObject trashType; // O tipo de lixo configurado no Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.collider.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.PickUpTrash(trashType);
                Destroy(gameObject);
            }
        }
    }
}
