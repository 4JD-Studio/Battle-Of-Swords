using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour
{

    public static GeneralController Inistance;

    public GameObject ToastHolder;
    public Text GoldText, GemsText, PlayerName, LevelText;

    private void Awake()
    {
        Inistance = this;
    }

    private void Start()
    {
        GoldText.text = PlayerVariables.Inistance.getCurrentGold().ToString();
        GemsText.text = PlayerVariables.Inistance.getCurrentGems().ToString();
        //PlayerName.text = Photon.Pun.PhotonNetwork.LocalPlayer.NickName;


        if (PlayerVariables.Inistance.getUserDisplayName() == null || PlayerVariables.Inistance.getUserDisplayName().Equals(""))
        {
            if (SettingsController.Inistance.UserFacebookName != null && !SettingsController.Inistance.UserFacebookName.Equals(""))
                PlayerName.text = SettingsController.Inistance.UserFacebookName;
            else
                PlayerName.text = PlayerVariables.Inistance.getUserPlayFabID();
        }
        else
        {
            PlayerName.text = PlayerVariables.Inistance.getUserDisplayName();
        }
        




        LevelText.text = PlayerVariables.Inistance.getCurrentLevel().ToString() + "/100";

        if (PlayerVariables.Inistance.getOpenListCharacters() != null)
            PlayfabDataSetter.Inistance.StringToDataFromList(ListType.Characters, PlayerVariables.Inistance.getOpenListCharacters());
        if (PlayerVariables.Inistance.getOpenListWeapons() != null)
            PlayfabDataSetter.Inistance.StringToDataFromList(ListType.Weapons, PlayerVariables.Inistance.getOpenListWeapons());
        if (PlayerVariables.Inistance.getOpenListShields() != null)
            PlayfabDataSetter.Inistance.StringToDataFromList(ListType.Shields, PlayerVariables.Inistance.getOpenListShields());
    }

    public IEnumerator ToastDisplayer(string Message, float TimeDisplay = 2f)
    {
        ToastHolder.SetActive(true);
        ToastHolder.transform.GetChild(0).GetComponent<Text>().text = Message;
        yield return new WaitForSeconds(TimeDisplay);
        ToastHolder.SetActive(false);
    }

}
