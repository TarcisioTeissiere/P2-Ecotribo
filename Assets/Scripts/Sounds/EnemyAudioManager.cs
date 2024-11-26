using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip deathClip;

    public void PlayAttackSound()
    {
        PlaySound(attackClip);
    }

    public void PlayDeathSound()
    {
        PlaySound(deathClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
