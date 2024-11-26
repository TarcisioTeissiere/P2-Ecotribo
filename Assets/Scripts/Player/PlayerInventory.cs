using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int maxItems = 3; 
    private List<TrashTypeObject> inventory = new List<TrashTypeObject>(); // Lista de itens no inventário
    public Image[] inventorySlots;

    public float discardRange = 1f;

    public void PickUpTrash(TrashTypeObject trashType)
    {
        if (inventory.Count < maxItems)
        {
            inventory.Add(trashType); // Adiciona o item ao inventário

            // Atualizar o slot correspondente com o sprite do item
            inventorySlots[inventory.Count - 1].sprite = trashType.trashSprite;
            inventorySlots[inventory.Count - 1].enabled = true;

            Debug.Log("Lixo coletado: " + trashType.trashName + ". Itens no inventário: " + inventory.Count);
        }
        else
        {
            Debug.Log("Inventário cheio! Não é possível coletar mais lixo.");
        }
    }

    public bool IsInventoryFull()
    {
        return inventory.Count >= maxItems;
    }

    public bool IsInventoryEmpty()
    {
        return inventory.Count == 0;
    }

    private void Update()
    {
        // Simulação de descarte de item ao pressionar "R"
        if (Input.GetKeyDown(KeyCode.R) && !IsInventoryEmpty())
        {
            TrashTypeObject trashToDiscard = inventory[0];
            TrashBin closestBin = FindClosestTrashBin();

            if (closestBin != null && Vector3.Distance(transform.position, closestBin.transform.position) <= discardRange)
            {
                if (closestBin.IsCorrectBin(trashToDiscard))
                {
                    Debug.Log("Lixo descartado corretamente!");
                }
                else
                {
                    Debug.Log("Lixo descartado incorretamente!");
                }

                // Remover o item do inventário
                inventory.RemoveAt(0);

                // Atualizar os slots do inventário visualmente
                UpdateInventoryUI();
            }
            else
            {
                Debug.Log("Você precisa estar mais próximo de uma lixeira para descartar o lixo!");
            }
        }
    }

    private TrashBin FindClosestTrashBin()
    {
        TrashBin[] bins = FindObjectsOfType<TrashBin>();
        TrashBin closestBin = null;
        float minDistance = Mathf.Infinity;

        foreach (TrashBin bin in bins)
        {
            float distance = Vector3.Distance(transform.position, bin.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestBin = bin;
            }
        }

        return closestBin;
    }

    private void UpdateInventoryUI()
    {
        // Limpa todas as imagens
        foreach (Image slot in inventorySlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        // Atualiza os slots com os itens restantes no inventário
        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].sprite = inventory[i].trashSprite;
            inventorySlots[i].enabled = true;
        }
    }
}
