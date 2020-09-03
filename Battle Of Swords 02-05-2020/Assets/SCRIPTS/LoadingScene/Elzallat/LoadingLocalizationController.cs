using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLocalizationController : MonoBehaviour
{

    public GameObject ArabicTitle, EnglishTitle;
    public Image NetworkMessage, RetryButton;
    public Sprite NetworkMessageArabic, NetworkMessageEnglish, RetryArabic, RetryEnglish;

    string Language;//EN , AR

    void Awake()
    {
        //DontDestroyOnLoad(this);
        Language = PlayerPrefs.GetString("Language", "EN");

        if (Language.Equals("EN"))
        {
            ArabicTitle.SetActive(false);
            EnglishTitle.SetActive(true);
            NetworkMessage.sprite = NetworkMessageEnglish;
            RetryButton.sprite = RetryEnglish;
        }
        else
        {
            ArabicTitle.SetActive(true);
            EnglishTitle.SetActive(false);
            NetworkMessage.sprite = NetworkMessageArabic;
            RetryButton.sprite = RetryArabic;
        }
    }
}
