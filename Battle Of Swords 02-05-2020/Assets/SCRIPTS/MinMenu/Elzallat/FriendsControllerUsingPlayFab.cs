using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsControllerUsingPlayFab : MonoBehaviour
{

    public Text TempText;
    List<FriendInfo> _friends = null;
    List<FriendInfo> myFriends;
    string FriendSearch;
    
    IEnumerator WaitForFriend()
    {
        yield return new WaitForSeconds(2);
        GetFriends();
    }

    public void RunWaitFunction()
    {
        StartCoroutine(WaitForFriend());
    }

    public void GetFriends()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeSteamFriends = false,
            IncludeFacebookFriends = true
        }, result =>
        {
            _friends = result.Friends;
            DisplayFriends(_friends);
        }, DisplayPlayfabError);
    }

    void DisplayFriends(List<FriendInfo> friendsCashe)
    {
        TempText.text = string.Empty;
        foreach (FriendInfo friend in friendsCashe)
        {
            bool IsFound = false;
            if(myFriends != null)
            {
                foreach (FriendInfo TempFriend in myFriends)
                {
                    if (friend.FriendPlayFabId.Equals(TempFriend.FriendPlayFabId))
                        IsFound = true;
                }
            }
            if (!IsFound)
            {
                TempText.text += friend.TitleDisplayName;
            }
        }
        myFriends = friendsCashe;
    }

    enum FriendIdType
    {
        PlayFabId, Username, Email, DisplayName
    }

    void AddFriend(FriendIdType IdType, string FriendId)
    {
        var Request = new AddFriendRequest();
        switch (IdType)
        {
            case FriendIdType.PlayFabId:
                Request.FriendPlayFabId = FriendId;
                break;
            case FriendIdType.Username:
                Request.FriendUsername = FriendId;
                break;
            case FriendIdType.Email:
                Request.FriendEmail = FriendId;
                break;
            case FriendIdType.DisplayName:
                Request.FriendTitleDisplayName = FriendId;
                break;
        }

        PlayFabClientAPI.AddFriend(Request, result =>
        {
            Debug.Log("Friend Added Successfully");
        }, DisplayPlayfabError);
    }

    public void InputFriendUsernameOrID(string friendID)
    {
        FriendSearch = friendID;
    }

    public void SubmitFriendRequest()
    {
        //AddFriend(FriendIdType.Username, FriendSearch);
        AddFriend(FriendIdType.PlayFabId, FriendSearch);
    }

    void DisplayPlayfabError(PlayFabError error)
    {
        TempText.text = error.GenerateErrorReport();
        Debug.Log(error.GenerateErrorReport());
    }

    private void CopyText(string textToCopy)
    {
        TextEditor editor = new TextEditor
        {
            text = textToCopy
        };
        editor.SelectAll();
        editor.Copy();
    }
}
