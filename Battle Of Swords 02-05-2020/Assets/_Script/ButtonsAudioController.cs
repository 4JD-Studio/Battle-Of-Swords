using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAudioController : MonoBehaviour
{
    public AudioClip Sound;
    public List<Button> ButtonsWithSound;

    void Start()
    {
        foreach (Button button in ButtonsWithSound)
        {
            AudioSource CurrentAudioSource = button.gameObject.AddComponent<AudioSource>();
            CurrentAudioSource.clip = Sound;
            CurrentAudioSource.playOnAwake = false;
            button.onClick.AddListener(() => { CurrentAudioSource.PlayOneShot(Sound); });
        }
    }
}
