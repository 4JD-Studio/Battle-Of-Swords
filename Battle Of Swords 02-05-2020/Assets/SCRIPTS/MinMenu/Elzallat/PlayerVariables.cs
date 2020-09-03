using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour
{

    public static PlayerVariables Inistance;

    public float MaxPlayerAttack = 200;
    public int IncreaseAttackByValue = 20;


    private int CurrentGold, CurrentGems, CurrentLevel;
    private CharacterModel CurrentCharacter;
    private WeaponModel CurrentWeapon;
    private ShieldsModel CurrentShield;
    private float CurrentPlayerAttack;

    private string CharacterIndex, WeaponIndex, ShieldIndex;
    private string OpenListCharacters, OpenListWeapons, OpenListShields;

    private string UserPlayFabID;
    private string UserDisplayName;

    [HideInInspector]
    public string UserName;
    [HideInInspector]
    public float CurrentBotAttack, CurrentBotDefese;

    private void Awake()
    {
        if(Inistance == null)
        {
            Inistance = this;

            //Deafult Values
            CurrentGold = 100;
            CurrentGems = 0;
            //UserName = "Jon Doe";
            CurrentPlayerAttack = 1;
            CurrentBotAttack = 5;
            CurrentBotDefese = 10;
           // MaxPlayerAttack = 200;
            CurrentLevel = 1;
        }
            
        DontDestroyOnLoad(transform.gameObject);
    }

    public void setCurrentGold(int Amount, bool UpdateData = true)
    {
        CurrentGold = Amount;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_GOLD,
                Amount.ToString());
    }

    public int getCurrentGold()
    {
        return CurrentGold;
    }

    public void setCurrentGems(int Amount, bool UpdateData = true)
    {
        CurrentGems = Amount;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_GEMS,
                Amount.ToString());
    }

    public int getCurrentGems()
    {
        return CurrentGems;
    }

    public void setCurrentLevel(int Level, bool UpdateData = true)
    {
        CurrentLevel = Level;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_LEVEL,
                Level.ToString());
    }

    public int getCurrentLevel()
    {
        return CurrentLevel;
    }

    public void setCurrentWeapon(WeaponModel Weapon, bool UpdateData = true)
    {
        CurrentWeapon = Weapon;
    }

    public WeaponModel getCurrentWeapon()
    {
        return CurrentWeapon;
    }

    public void setCurrentCharacter(CharacterModel character, bool UpdateData = true)
    {
        CurrentCharacter = character;
    }

    public CharacterModel getCurrentCharacter()
    {
        return CurrentCharacter;
    }

    public void setCurrentShield(ShieldsModel Shield, bool UpdateData = true)
    {
        CurrentShield = Shield;
    }

    public ShieldsModel getCurrentShield()
    {
        return CurrentShield;
    }

    public void setCurrentPlayerAttack(float PlayerAttack, bool UpdateData = true)
    {
        CurrentPlayerAttack = PlayerAttack;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_PLAYER_ATTACK,
                PlayerAttack.ToString());
    }

    public float getCurrentPlayerAttack()
    {
        return CurrentPlayerAttack;
    }

    public void setCharacterIndex(string Index, bool UpdateData = true)
    {
        CharacterIndex = Index;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_CHARACTER,
                Index);
    }

    public string getCharacterIndex()
    {
        return CharacterIndex;
    }

    public void setWeaponIndex(string Index, bool UpdateData = true)
    {
        WeaponIndex = Index;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_WEAPON,
                Index);
    }

    public string getWeaponIndex()
    {
        return WeaponIndex;
    }

    public void setShieldIndex(string Index, bool UpdateData = true)
    {
        ShieldIndex = Index;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_CURRENT_SHIELD,
                Index);
    }

    public string getShieldIndex()
    {
        return ShieldIndex;
    }

    public void setOpenListCharacters(string list, bool UpdateData = true)
    {
        OpenListCharacters = list;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_LIST_CHARACTERS,
                list);
    }

    public string getOpenListCharacters()
    {
        return OpenListCharacters;
    }

    public void setOpenListWeapons(string list, bool UpdateData = true)
    {
        OpenListWeapons = list;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_LIST_WEAPONS,
                list);
    }

    public string getOpenListWeapons()
    {
        return OpenListWeapons;
    }

    public void setOpenListShields(string list, bool UpdateData = true)
    {
        OpenListShields = list;
        if (UpdateData)
            PlayfabDataSetter.Inistance.SetUserData(
                PlayfabDataSetter.TAG_LIST_SHIELDS,
                list);
    }

    public string getOpenListShields()
    {
        return OpenListShields;
    }

    public void setUserPlayFabID(string ID)
    {
        UserPlayFabID = ID;
    }

    public string getUserPlayFabID()
    {
        return UserPlayFabID;
    }

    public void setUserDisplayName(string Name)
    {
        UserDisplayName = Name;
    }

    public string getUserDisplayName()
    {
        return UserDisplayName;
    }
}
