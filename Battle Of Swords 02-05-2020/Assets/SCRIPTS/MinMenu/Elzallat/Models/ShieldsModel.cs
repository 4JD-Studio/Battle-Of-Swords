using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShieldsModel
{
    public string ShieldName;
    public int PrefabIndex;
    public Sprite ShieldImage;
    public bool IsFree;
    public int ShieldPrice;

    [HideInInspector]
    public bool IsOpenToUser;
}
