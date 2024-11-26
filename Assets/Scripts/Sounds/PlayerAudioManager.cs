using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioSource audioSource; 

    // Áudios que separei >=(
    public AudioClip attackClip; 
    public AudioClip hurtClip;   
    public AudioClip deathClip;   

    public void PlayAttackSound()
    {
        PlaySound(attackClip);
    }

    public void PlayHurtSound()
    {
        PlaySound(hurtClip);
    }


    public void PlayDeathSound()
    {
        PlaySound(deathClip);
    }

    // Método genérico para tocar um som
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource ou AudioClip não configurado no PlayerAudioManager.");
        }
    }
}
