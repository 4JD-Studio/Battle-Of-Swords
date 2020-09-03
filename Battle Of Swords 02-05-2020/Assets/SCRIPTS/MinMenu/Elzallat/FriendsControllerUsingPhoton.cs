using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Photon.Realtime;

public class FriendsControllerUsingPhoton : MonoBehaviourPunCallbacks
{
    public GameObject FriendItemHolder;
    public GameObject ContentParent;
    public float ScrollBarWidth = 20;

    public static FriendsControllerUsingPhoton Inistance;

    private void Awake()
    {
        Inistance = this;
    }

  
      

    //private void OnPlayButtonClick(string currentName, string currentID)
    //{
    //    PhotonNetwork.FindFriends(new string[] { currentID });
    //}

    //public override void OnFriendListUpdate(List<FriendInfo> friendList)
    //{
    //    base.OnFriendListUpdate(friendList);

    //    foreach (FriendInfo info in friendList)
    //    {
    //        Debug.Log("UserId: " + info.UserId);
    //        Debug.Log("IsOnline: " + info.IsOnline);
    //        Debug.Log("IsInRoom: " + info.IsInRoom);

    //        StartCoroutine(GeneralController.Inistance.ToastDisplayer(
    //            "UserId: " + info.UserId
    //            + "\n" + 
    //            "IsOnline: " + info.IsOnline
    //            + "\n" + 
    //            "IsInRoom: " + info.IsInRoom, 5));
    //    }
    //}
}
