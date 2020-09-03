using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabDataSetter : MonoBehaviour
{
    public static PlayfabDataSetter Inistance;

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
    public static string TAG_LIST_FRIENDS = "ListFriends";

    private void Awake()
    {
        if(Inistance == null)
            Inistance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetUserData(string objectTAG, string value)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {objectTAG, value}
            }
        }, OnSetDataSuccess, OnSetDataFails);
    }

    private void OnSetDataFails(PlayFabError obj)
    {
        Debug.Log("OnSetDataFails " + obj.Error.ToString());
    }

    private void OnSetDataSuccess(UpdateUserDataResult obj)
    {
        //Debug.Log("OnSetDataSuccess");
    }

    public string DataToStringFromList(ListType listType)
    {
        string Output = "";
        if (listType == ListType.Characters)
        {
            foreach (CharacterModel item in CharacterSliderController.Inistance.Characters)
            {
                if (item.IsFree || item.IsOpenToUser)
                    Output += "1";
                else
                    Output += "0";
            }
        }
        else if (listType == ListType.Weapons)
        {
            foreach (WeaponModel item in WeaponsInShopController.Inistance.Weapons)
            {
                if (item.IsFree || item.IsOpenToUser)
                    Output += "1";
                else
                    Output += "0";
            }
        }
        else if (listType == ListType.Shields)
        {
            foreach (ShieldsModel item in ShieldsInShopController.Inistance.Shields)
            {
                if (item.IsFree || item.IsOpenToUser)
                    Output += "1";
                else
                    Output += "0";
            }
        }
        return Output;
    }

    public void StringToDataFromList(ListType listType, string Input)
    {
        for (int i = 0; i < Input.Length; i++)
        {
            if (int.Parse(Input[i].ToString()) == 1)
            {
                switch (listType)
                {
                    case ListType.Characters:
                        CharacterSliderController.Inistance.Characters[i].IsOpenToUser = true;
                        break;
                    case ListType.Weapons:
                        WeaponsInShopController.Inistance.Weapons[i].IsOpenToUser = true;
                        break;
                    case ListType.Shields:
                        ShieldsInShopController.Inistance.Shields[i].IsOpenToUser = true;
                        break;
                }
            }
            else
            {
                switch (listType)
                {
                    case ListType.Characters:
                        CharacterSliderController.Inistance.Characters[i].IsFree = false;
                        CharacterSliderController.Inistance.Characters[i].IsOpenToUser = false;
                        break;
                    case ListType.Weapons:
                        WeaponsInShopController.Inistance.Weapons[i].IsFree = false;
                        WeaponsInShopController.Inistance.Weapons[i].IsOpenToUser = false;
                        break;
                    case ListType.Shields:
                        ShieldsInShopController.Inistance.Shields[i].IsFree = false;
                        ShieldsInShopController.Inistance.Shields[i].IsOpenToUser = false;
                        break;
                }
            }
        }
    }
}


public enum ListType
{
    Characters,
    Weapons,
    Shields
}

public enum CurrentObjectType
{
    Gold,
    Gems,
    Level,
    Character,
    Weapon,
    Shield,
    PlayerAttack,
    BotAttack,
    BotDefence
}