using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterModel
{
    public string CharacterName, PrefabName;
    public Sprite CharacterImage, ProfileImage;
    public bool IsFree;
    public int CharacterPrice;

    [HideInInspector]
    public bool IsOpenToUser;
}
