using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSliderController : MonoBehaviour
{
    
    public Button PriceButton, PlayButton;
    public Image CharacterHolder, ProfileImage, CharacterLockImage;
    //public PlayerVariables playerVariables;
    public List<CharacterModel> Characters;

    CharacterModel Current;

    private int SelectedCharacterIndex = 0;

    public static CharacterSliderController Inistance;

    private void Awake()
    {
        Inistance = this;
    }

    private void Start()
    {
        //get last selected char from playfav
        //save last selected on select
        CharacterHolder.gameObject.SetActive(true);
        if (PlayerVariables.Inistance.getCharacterIndex() != null)
            SelectedCharacterIndex = int.Parse(PlayerVariables.Inistance.getCharacterIndex());
        else
            SelectedCharacterIndex = 0;
        setCurrentCharacterOnScreen(SelectedCharacterIndex);
    }

    public void OnNextClick()
    {
        SelectedCharacterIndex++;
        if (SelectedCharacterIndex == Characters.Count)
            SelectedCharacterIndex = 0;

        setCurrentCharacterOnScreen(SelectedCharacterIndex);
    }

    public void OnPreviousClick()
    {
        SelectedCharacterIndex--;
        if (SelectedCharacterIndex < 0)
            SelectedCharacterIndex = Characters.Count - 1;

        setCurrentCharacterOnScreen(SelectedCharacterIndex);
    }

    private void setCurrentCharacterOnScreen(int Index)
    {
        Current = Characters[Index];
        PlayerVariables.Inistance.setCurrentCharacter(Current);
        PlayerVariables.Inistance.setCharacterIndex(Index.ToString());


        CharacterHolder.sprite = Current.CharacterImage;
        if (SettingsController.Inistance.UserFacebookImage!= null)
            ProfileImage.sprite = SettingsController.Inistance.UserFacebookImage;
        else
            ProfileImage.sprite = Current.ProfileImage;

        if (Current.IsFree || Current.IsOpenToUser)
        {
            CharacterLockImage.gameObject.SetActive(false);
            PriceButton.gameObject.SetActive(false);
            PlayButton.interactable = true;
        }
        else
        {
            CharacterLockImage.gameObject.SetActive(true);
            PriceButton.gameObject.SetActive(true);
            PlayButton.interactable = false;
            PriceButton.transform.GetChild(0).GetComponent<Text>().text = Current.CharacterPrice.ToString();

            if (PlayerVariables.Inistance.getCurrentGems() < Current.CharacterPrice)
                PriceButton.interactable = false;
            else
                PriceButton.interactable = true;
        }
    }

    public void OnPriceClick()
    {
        if(PlayerVariables.Inistance.getCurrentGems() >= Current.CharacterPrice)
        {
            //store in playfab
            CharacterLockImage.gameObject.SetActive(false);
            Current.IsOpenToUser = true;
            PriceButton.gameObject.SetActive(false);
            PlayButton.interactable = true;
            PlayerVariables.Inistance.setOpenListCharacters(PlayfabDataSetter.Inistance.DataToStringFromList(ListType.Characters));
            PlayerVariables.Inistance.setCurrentGems(PlayerVariables.Inistance.getCurrentGems() - Current.CharacterPrice);
            GeneralController.Inistance.GemsText.text = PlayerVariables.Inistance.getCurrentGems().ToString();
        }
    }
}
