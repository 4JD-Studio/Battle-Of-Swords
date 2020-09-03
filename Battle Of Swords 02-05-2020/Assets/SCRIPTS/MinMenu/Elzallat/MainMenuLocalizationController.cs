using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObjectLocalizedWithImage
{
    public string ObjectDescription;
    public Image MainObject;
    public Sprite ImageInArabic, ImageInEnglish;
}

[System.Serializable]
public class ObjectLocalizedWithText
{
    public string ObjectDescription;
    public Text MainObject;
    public string TextInArabic, TextInEnglish;
}

public class MainMenuLocalizationController : MonoBehaviour
{
    public List<ObjectLocalizedWithImage> ObjectsWithImages;
    public List<ObjectLocalizedWithText> ObjectsWithTexts;

    public ObjectLocalizedWithImage SoundOnObject, SoundOffObject, FacebookOnObject, FacebookOffObject;

    string Language;//EN , AR

    void Awake()
    {
        //DontDestroyOnLoad(this);
        Language = PlayerPrefs.GetString("Language", "EN");
        setDataOnScreen();
    }

    public void OnChangeLanguageClick()
    {
        if (Language.Equals("EN"))
        {
            PlayerPrefs.SetString("Language", "AR");
            Language = "AR";
        }
        else
        {
            PlayerPrefs.SetString("Language", "EN");
            Language = "EN";
        }
        setDataOnScreen();
    }


    private void setDataOnScreen()
    {
        foreach (ObjectLocalizedWithImage Obj in ObjectsWithImages)
        {
            if(Obj.MainObject != null){
                if (Language.Equals("EN"))
                    Obj.MainObject.sprite = Obj.ImageInEnglish;
                else
                    Obj.MainObject.sprite = Obj.ImageInArabic;
            }
            
        }
        foreach (ObjectLocalizedWithText Obj in ObjectsWithTexts)
        {
            if (Obj.MainObject != null)
            {
                if (Language.Equals("EN"))
                    Obj.MainObject.text = Obj.TextInEnglish;
                else
                    Obj.MainObject.text = Obj.TextInArabic;
            }
        }

        //set facebook and sound
        if (!FacebookOnObject.MainObject.gameObject.GetComponent<Button>().interactable)
        {
            if (Language.Equals("EN"))
                FacebookOnObject.MainObject.sprite = FacebookOnObject.ImageInEnglish;
            else
                FacebookOnObject.MainObject.sprite = FacebookOnObject.ImageInArabic;
        }
        else
        {
            if (Language.Equals("EN"))
                FacebookOffObject.MainObject.sprite = FacebookOffObject.ImageInEnglish;
            else
                FacebookOffObject.MainObject.sprite = FacebookOffObject.ImageInArabic;
        }

        if (AudioListener.volume > 0.0f)
        {
            if (Language.Equals("EN"))
                SoundOnObject.MainObject.sprite = SoundOnObject.ImageInEnglish;
            else
                SoundOnObject.MainObject.sprite = SoundOnObject.ImageInArabic;
        }
        else
        {
            if (Language.Equals("EN"))
                SoundOffObject.MainObject.sprite = SoundOffObject.ImageInEnglish;
            else
                SoundOffObject.MainObject.sprite = SoundOffObject.ImageInArabic;
        }
    }
}
