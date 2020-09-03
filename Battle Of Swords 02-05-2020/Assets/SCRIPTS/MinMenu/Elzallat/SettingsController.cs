using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PlayFab;
using PlayFab.ClientModels;

using Photon.Pun;
using Photon.Realtime;


public class SettingsController : MonoBehaviour
{
    //private string _message = "";
    //public GameObject LoadingIndecator;
    public Image UserImage;
    public Text UserName, CharacterName, BotAttack, BotDefense, PlayerAttack;
    public Slider PlayerAttackSlider;
    public ObjectLocalizedWithImage SoundOnObject, SoundOffObject, FacebookOnObject, FacebookOffObject;

    private float CurrentSoundLevel;

    public static SettingsController Inistance;
    public Sprite UserFacebookImage;
    public string UserFacebookName = null;

    public GameObject PanelEditName;
    public InputField FieldNewName;

    private void Awake()
    {
        Inistance = this;
        CurrentSoundLevel = PlayerPrefs.GetFloat("SoundLevel", 1.0f);
        AudioListener.volume = CurrentSoundLevel;
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            FacebookOffObject.MainObject.sprite = FacebookOffObject.ImageInEnglish;
        else
            FacebookOffObject.MainObject.sprite = FacebookOffObject.ImageInArabic;
    }

    public void setDataOnScreen()
    {
        if (UserFacebookImage != null)
            UserImage.sprite = UserFacebookImage;
        else
            UserImage.sprite = PlayerVariables.Inistance.getCurrentCharacter().ProfileImage;

        CharacterName.text = PlayerVariables.Inistance.getCurrentCharacter().CharacterName;

        //set name
        if(PlayerVariables.Inistance.getUserDisplayName() == null || PlayerVariables.Inistance.getUserDisplayName().Equals(""))
        {
            if (UserFacebookName != null && !UserFacebookName.Equals(""))
            {
                UserName.text = UserFacebookName;
                GeneralController.Inistance.PlayerName.text = UserFacebookName;
            }
            else
            {
                UserName.text = PlayerVariables.Inistance.getUserPlayFabID();
                GeneralController.Inistance.PlayerName.text = PlayerVariables.Inistance.getUserPlayFabID();
            }
        }
        else
        {
            UserName.text = PlayerVariables.Inistance.getUserDisplayName();
            GeneralController.Inistance.PlayerName.text = PlayerVariables.Inistance.getUserDisplayName();
        }

        

        BotAttack.text = PlayerVariables.Inistance.CurrentBotAttack.ToString();
        BotDefense.text = PlayerVariables.Inistance.CurrentBotDefese.ToString();
        PlayerAttack.text = PlayerVariables.Inistance.getCurrentPlayerAttack().ToString() + "/" + PlayerVariables.Inistance.MaxPlayerAttack.ToString();

        PlayerAttackSlider.value = (PlayerVariables.Inistance.getCurrentPlayerAttack() / PlayerVariables.Inistance.MaxPlayerAttack);


        if (AudioListener.volume > 0.0f)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                SoundOnObject.MainObject.sprite = SoundOnObject.ImageInEnglish;
            else
                SoundOnObject.MainObject.sprite = SoundOnObject.ImageInArabic;
            //SoundButton.image.sprite = SoundOn;
        }
        else
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                SoundOffObject.MainObject.sprite = SoundOffObject.ImageInEnglish;
            else
                SoundOffObject.MainObject.sprite = SoundOffObject.ImageInArabic;
            //SoundButton.image.sprite = SoundOff;
        }

     
    }


    public void OnIncreastSwordValueClickOk(int IncreaseValue)
    {
        if (PlayerVariables.Inistance.getCurrentGold() >= IncreaseValue)
        {
            //store in playfab
            PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() - IncreaseValue);
            GeneralController.Inistance.GoldText.text = PlayerVariables.Inistance.getCurrentGold().ToString();


            if (PlayerVariables.Inistance.getCurrentPlayerAttack() < PlayerVariables.Inistance.MaxPlayerAttack)
            {
                PlayerVariables.Inistance.setCurrentPlayerAttack(PlayerVariables.Inistance.getCurrentPlayerAttack() + PlayerVariables.Inistance.IncreaseAttackByValue);
                if (PlayerVariables.Inistance.getCurrentPlayerAttack() > PlayerVariables.Inistance.MaxPlayerAttack)
                    PlayerVariables.Inistance.setCurrentPlayerAttack(PlayerVariables.Inistance.MaxPlayerAttack);
                //save to playfab

                PlayerAttack.text = PlayerVariables.Inistance.getCurrentPlayerAttack().ToString() + "/" + PlayerVariables.Inistance.MaxPlayerAttack.ToString();
                PlayerAttackSlider.value = (PlayerVariables.Inistance.getCurrentPlayerAttack() / PlayerVariables.Inistance.MaxPlayerAttack);
            }
        }
        else
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                StartCoroutine(GeneralController.Inistance.ToastDisplayer("You Don't have Enough Gold"));
            else
                StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﻲﻓﺎﻛ ﺐﻫﺫ ﻚﻳﺪﻟ ﺲﻴﻟ"));
        }
    }

    public void SoundButtonClick()
    {
        if (AudioListener.volume > 0.0f)
        {
            AudioListener.volume = 0.0f;
            PlayerPrefs.SetFloat("SoundLevel", 0.0f);
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                SoundOffObject.MainObject.sprite = SoundOffObject.ImageInEnglish;
            else
                SoundOffObject.MainObject.sprite = SoundOffObject.ImageInArabic;
            //SoundButton.image.sprite = SoundOff;
        }
        else
        {
            AudioListener.volume = 1.0f;
            PlayerPrefs.SetFloat("SoundLevel", 1.0f);
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                SoundOnObject.MainObject.sprite = SoundOnObject.ImageInEnglish;
            else
                SoundOnObject.MainObject.sprite = SoundOnObject.ImageInArabic;
            //SoundButton.image.sprite = SoundOn;
        }

    }

    /*
     * 
     * 
     * 
     *    Facebook
     * 
     * 
     * 
     * 
     */


    public void OnFacebookConnected()
    {
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            FacebookOnObject.MainObject.sprite = FacebookOnObject.ImageInEnglish;
        else
            FacebookOnObject.MainObject.sprite = FacebookOnObject.ImageInArabic;
        FacebookOnObject.MainObject.gameObject.GetComponent<Button>().interactable = false;
    }

    public void OnFacebookFailed()
    {
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            FacebookOffObject.MainObject.sprite = FacebookOffObject.ImageInEnglish;
        else
            FacebookOffObject.MainObject.sprite = FacebookOffObject.ImageInArabic;
        FacebookOnObject.MainObject.gameObject.GetComponent<Button>().interactable = true;
    }

    ////////////////////////////////////////////////////////
    

    public void OnSaveNewName()
    {
        string NewName = FieldNewName.text;
        if (!NewName.Equals(""))
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(
                new UpdateUserTitleDisplayNameRequest
                {
                    DisplayName = NewName
                }, result =>
                {
                    PanelEditName.SetActive(false);
                    UserName.text = result.DisplayName;
                    PlayerVariables.Inistance.setUserDisplayName(result.DisplayName);
                    Debug.Log("The player's display name is now: " + result.DisplayName);
                }, DisplayPlayFabError);
        }
    }

    private void DisplayPlayFabError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        //can add a toast displayer
    }

}
