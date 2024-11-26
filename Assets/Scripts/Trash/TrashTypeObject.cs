using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashType", menuName = "Trash/TrashType")]
public class TrashTypeObject : ScriptableObject
{
    public string trashName; // Nome do tipo de lixo (ex.: "Plástico", "Vidro")
    public Sprite icon; // Ícone opcional para o tipo de lixo
    public Sprite trashSprite;
}
