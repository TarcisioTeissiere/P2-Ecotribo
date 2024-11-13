using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public TrashTypeObject trashType; // Tipo de lixo usando o ScriptableObject

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.collider.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.PickUpTrash(trashType); // Passa o ScriptableObject como tipo de lixo
                Destroy(gameObject); // Remove o item de lixo da cena
            }
        }
    }
}