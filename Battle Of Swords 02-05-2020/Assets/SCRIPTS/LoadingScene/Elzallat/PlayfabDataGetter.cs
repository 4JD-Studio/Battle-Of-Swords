using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

public class PlayfabDataGetter : MonoBehaviour
{
    public static PlayfabDataGetter Inistance;

    public static string TAG_CURRENT_GOLD = "CurrentGold";
    public static string TAG_CURRENT_GEMS = "CurrentGems";
    public static string TAG_CURRENT_LEVEL = "CurrentLevel";
    public static string TAG_CURRENT_CHARACTER = "CurrentCharacter";
    public static string TAG_CURRENT_WEAPON = "CurrentWeapon";
    public static string TAG_CURRENT_SHIELD = "CurrentShield";
    public static string TAG_CURRENT_PLAYER_ATTACK = "CurrentPlayerAttack";
    public static string TAG_CURRENT_BOT_ATTACK = "CurrentBotAttack";
    public static string TAG_CURRENT_BOT_DEFENCE = "CurrentBotDefence";
    public static string TAG_LIST_CHARACTERS = "ListCharacters";
    public static string TAG_LIST_WEAPONS = "ListWeapons";
    public static string TAG_LIST_SHIELDS = "ListShields";

    string CurrentUserID = "";

    private void Awake()
    {
        Inistance = this;
    }

    public void GetAllData(string UserID)
    {
        CurrentUserID = UserID;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = UserID,
            Keys = null
        }, OnGetDataSuccess, OnGetDataFails);

        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = UserID,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
        result =>
        {
            PlayerVariables.Inistance.setUserDisplayName(result.PlayerProfile.DisplayName);
        },
        OnGetDataFails);
    }
    private void OnGetDataSuccess(GetUserDataResult result)
    {

        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_GOLD))
            PlayerVariables.Inistance.setCurrentGold(int.Parse(result.Data[TAG_CURRENT_GOLD].Value), false);
        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_GEMS))
            PlayerVariables.Inistance.setCurrentGems(int.Parse(result.Data[TAG_CURRENT_GEMS].Value), false);
        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_LEVEL))
            PlayerVariables.Inistance.setCurrentLevel(int.Parse(result.Data[TAG_CURRENT_LEVEL].Value), false);

        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_CHARACTER))
            PlayerVariables.Inistance.setCharacterIndex(result.Data[TAG_CURRENT_CHARACTER].Value, false);
        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_WEAPON))
            PlayerVariables.Inistance.setWeaponIndex(result.Data[TAG_CURRENT_WEAPON].Value, false);
        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_SHIELD))
            PlayerVariables.Inistance.setShieldIndex(result.Data[TAG_CURRENT_SHIELD].Value, false);

        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_PLAYER_ATTACK))
            PlayerVariables.Inistance.setCurrentPlayerAttack(int.Parse(result.Data[TAG_CURRENT_PLAYER_ATTACK].Value), false);
        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_BOT_ATTACK))
            PlayerVariables.Inistance.CurrentBotAttack = int.Parse(result.Data[TAG_CURRENT_BOT_ATTACK].Value);
        if (result.Data != null && result.Data.ContainsKey(TAG_CURRENT_BOT_DEFENCE))
            PlayerVariables.Inistance.CurrentBotDefese = int.Parse(result.Data[TAG_CURRENT_BOT_DEFENCE].Value);

        if (result.Data != null && result.Data.ContainsKey(TAG_LIST_CHARACTERS))
            PlayerVariables.Inistance.setOpenListCharacters(result.Data[TAG_LIST_CHARACTERS].Value, false);
        if (result.Data != null && result.Data.ContainsKey(TAG_LIST_WEAPONS))
            PlayerVariables.Inistance.setOpenListWeapons(result.Data[TAG_LIST_WEAPONS].Value, false);
        if (result.Data != null && result.Data.ContainsKey(TAG_LIST_SHIELDS))
            PlayerVariables.Inistance.setOpenListShields(result.Data[TAG_LIST_SHIELDS].Value, false);

    }
    private void OnGetDataFails(PlayFabError error)
    {
        Debug.Log("OnGetDataFails " + error.Error.ToString());
        SceneManager.LoadScene("LoadingScene");
    }
}