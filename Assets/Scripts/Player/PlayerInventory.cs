// PlayerInventory.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    private TrashTypeObject currentTrash = null; // Tipo de lixo atualmente carregado
    public int maxItems = 3; // Limite de itens no inventário
    private int currentItemCount = 0;

    public void PickUpTrash(TrashTypeObject trashType)
    {
        if (currentItemCount < maxItems)
        {
            currentTrash = trashType;
            currentItemCount++;
            Debug.Log("Lixo coletado: " + trashType.trashName + ". Itens no inventário: " + currentItemCount);
        }
        else
        {
            Debug.Log("Inventário cheio! Não é possível coletar mais lixo.");
        }
    }

    public bool IsInventoryFull()
    {
        return currentItemCount >= maxItems;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentTrash != null)
        {
            TrashBin closestBin = FindClosestTrashBin();
            if (closestBin != null && closestBin.IsCorrectBin(currentTrash))
            {
                Debug.Log("Lixo descartado corretamente!");
            }
            else
            {
                Debug.Log("Lixo descartado incorretamente!");
            }

            currentTrash = null;
            currentItemCount--;
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
}
