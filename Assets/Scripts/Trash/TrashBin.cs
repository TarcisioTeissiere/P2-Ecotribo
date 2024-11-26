using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashTypeObject acceptedTrashType;
    public ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public bool IsCorrectBin(TrashTypeObject trashType)
    {
        bool isCorrect = trashType == acceptedTrashType;
        scoreManager.AddScore(isCorrect ? 100 : -100);

        // Tocar som ao descartar lixo
        TrashSoundManager soundManager = FindObjectOfType<TrashSoundManager>();
        if (soundManager != null)
        {
            soundManager.PlayTrashDisposedSound();
        }

        return isCorrect;
    }
}
