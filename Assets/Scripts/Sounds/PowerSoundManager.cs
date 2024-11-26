using UnityEngine;

public class PowerSoundManager : MonoBehaviour
{
    public AudioClip powerUpSpawnSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado! Adicione um AudioSource ao GameObject.");
        }
    }

    private void Start()
    {
        PlayPowerUpSpawnSound();
    }

    public void PlayPowerUpSpawnSound()
    {
        PlaySound(powerUpSpawnSound);
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
