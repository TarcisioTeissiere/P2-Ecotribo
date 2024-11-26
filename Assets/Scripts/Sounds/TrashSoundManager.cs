using UnityEngine;

public class TrashSoundManager : MonoBehaviour
{
    public AudioClip trashDisposed;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado! Adicione um AudioSource ao GameObject.");
        }
    }

    public void PlayTrashDisposedSound()
    {
        PlaySound(trashDisposed);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Áudio ou AudioSource ausente ao tentar tocar som.");
        }
    }
}
