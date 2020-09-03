using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip AttackAudio, HurtAudio, DeathAudio;
    [SerializeField]
    AudioSource audioSource;

    public static AudioPlayer Inistance;

    private void Awake()
    {
        Inistance = this;
    }

    public void OnAttack()
    {
        audioSource.clip = AttackAudio;
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.Play();
    }

    public void OnGetHit()
    {
        audioSource.clip = HurtAudio;
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.Play();
    }

    public void OnDeath()
    {
        audioSource.clip = DeathAudio;
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.Play();
    }
}
