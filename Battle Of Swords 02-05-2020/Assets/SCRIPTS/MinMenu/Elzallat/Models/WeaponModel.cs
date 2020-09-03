using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponModel
{
    public string WeaponName;
    public int PrefabIndex;
    public Sprite WeaponImage;
    public bool IsFree;
    public int WeaponPrice;

    [HideInInspector]
    public bool IsOpenToUser;
}
