using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendsNewScript : MonoBehaviourPunCallbacks, IChatClientListener
{
    [Header("Friends Listing UI")]
    [SerializeField]
    Transform ContentParent;
    [SerializeField]
    GameObject FriendListingPrefab;
    [SerializeField]
    int ScrollBarWidth = 20;

    [Header("Play Requests UI")]
    [SerializeField]
    GameObject PanelRequests;
    [SerializeField]
    GameObject LoadingImage;
    [SerializeField]
    GameObject PanelDialog;
    [SerializeField]
    Text PlayerNameUIText;

    List<PlayFab.ClientModels.FriendInfo> _friends = null;
    List<PlayFab.ClientModels.FriendInfo> myFriends;
    enum FriendIDType { PlayFabId, Username, Email, DisplayName};

    private ChatClient chatClient;
    private static string CHANNEL_NAME = "PlayChannel";

    private bool ShowLoading = false;
    private string SENDER_ID, RECIEVER_ID, ROOM_NAME;

    private string PHOTON_CHAT_ID = "49070b51-67a7-4e33-992d-3295c39a43b1";



    [Header("Room Variables")]
    public Transform PlayerFigures;
    public GameObject MainMenuCanvas, WaitForCanvas;
    public double timer = 10;
    public Text playercount, CountDownTimer;
    bool cancle, nameExist = false, startTimer = false, ExitRoom = false;
    double timerIncrementValue, startTime;
    GameObject player;
    ExitGames.Client.Photon.Hashtable CustomeValue;
    private bool OnFriends = false;


    public void OnCopyButtonClick()
    {
        string FullID = PlayerVariables.Inistance.getUserPlayFabID();
        TextEditor TempEditor = new TextEditor();
        TempEditor.text = FullID;
        TempEditor.SelectAll();
        TempEditor.Copy();

        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("Copied To Clipboard"));
        else
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﺩﻮﻜﻟﺍ ﺦﺴﻧ ﻢﺗ"));
    }

    public void OnSearchButtonClick(InputField SearchUserID)
    {
        if (SearchUserID.text.Equals(""))
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                StartCoroutine(GeneralController.Inistance.ToastDisplayer("Wrong User ID"));
            else
                StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﺊﻃﺎﺧ ﻡﺪﺨﺘﺴﻣ ﻢﺳﺍ"));
        }
        else
        {
            AddFriend(FriendIDType.PlayFabId, SearchUserID.text);
            SearchUserID.text = "";
        }
    }

    public void GetAllFriends()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeFacebookFriends = false,
            IncludeSteamFriends = false
        }, result =>
        {
            _friends = result.Friends;
            DisplayFriends(_friends);
        }, DisplayPlayFabError);
    }

    private void AddFriend(FriendIDType idType, string friendPlayFabID)
    {
        var Request = new AddFriendRequest();
        switch (idType)
        {
            case FriendIDType.PlayFabId:
                Request.FriendPlayFabId = friendPlayFabID;
                break;
            case FriendIDType.Username:
                Request.FriendUsername = friendPlayFabID;
                break;
            case FriendIDType.Email:
                Request.FriendEmail = friendPlayFabID;
                break;
            case FriendIDType.DisplayName:
                Request.FriendTitleDisplayName = friendPlayFabID;
                break;
        }

        PlayFabClientAPI.AddFriend(Request,
            result =>
            {
                StartCoroutine(GeneralController.Inistance.ToastDisplayer("Friend Added Successfully"));
            }, DisplayPlayFabError);
    }

    private void DisplayPlayFabError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        StartCoroutine(GeneralController.Inistance.ToastDisplayer(error.GenerateErrorReport()));
    }

    private void DisplayError(string error)
    {
        Debug.LogError(error);
        StartCoroutine(GeneralController.Inistance.ToastDisplayer(error));
    }

    private void DisplayFriends(List<PlayFab.ClientModels.FriendInfo> friendsCache)
    {
        foreach (PlayFab.ClientModels.FriendInfo friend in friendsCache)
        {
            bool IsFound = false;
            if(myFriends != null)
            {
                foreach (PlayFab.ClientModels.FriendInfo ExistingFriend in myFriends)
                {
                    if (friend.FriendPlayFabId.Equals(ExistingFriend.FriendPlayFabId))
                        IsFound = true;
                }
            }
            if (!IsFound)
            {
                GameObject Listing = Instantiate(FriendListingPrefab, ContentParent, false);
                if(friend.TitleDisplayName != null)
                    Listing.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = friend.TitleDisplayName;
                else
                    Listing.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = friend.FriendPlayFabId;
                Listing.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = friend.FriendPlayFabId;
                //if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                //    Listing.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "Play";
                //else
                //    Listing.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "ﺐﻌﻟ";
                Listing.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
                Listing.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(
                    () => { OnFriendButtonPlayClick(friend.FriendPlayFabId); }
                    );
                Listing.GetComponent<RectTransform>().sizeDelta =
                new Vector2(Screen.width - ScrollBarWidth, Listing.GetComponent<RectTransform>().sizeDelta.y);
            }
        }
        myFriends = friendsCache;
    }

    private IEnumerator WaitForFirend()
    {
        yield return new WaitForSeconds(2);
        GetAllFriends();
    }

    public void RunWaitForFriendsFunction()
    {
        StartCoroutine(WaitForFirend());
    }

    private void OnFriendButtonPlayClick(string FriendPlayFabID)
    {
        PhotonNetwork.FindFriends(new string[] { FriendPlayFabID });
    }

    public override void OnFriendListUpdate(List<Photon.Realtime.FriendInfo> friendList)
    {
        base.OnFriendListUpdate(friendList);

        if(friendList.Count > 0)
        {
            if (friendList[0].IsOnline)
            {
                if (!friendList[0].IsInRoom)
                {
                    SendRequestToFriend(friendList[0].UserId);
                }
                else
                {
                    if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                        StartCoroutine(GeneralController.Inistance.ToastDisplayer("Friend is in a game"));
                    else
                        StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﺎﻴﻟﺎﺣ ﻩﺯﺭﺎﺒﻣ ﻲﻗ ﻖﻳﺪﺼﻟﺍ"));
                }
            }
            else
            {
                if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                    StartCoroutine(GeneralController.Inistance.ToastDisplayer("Friend is Offline"));
                else
                    StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﻞﺼﺘﻣ ﺮﻴﻏ ﻖﻳﺪﺼﻟﺍ"));
            }
        }
    }

    ///////////////Chat

    private void Start()
    {
        InitChat();
    }

    void Update()
    {
        if (chatClient != null)
            chatClient.Service();

        if (startTimer)
            DisplayPlayersCount();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
        }
    }

    public override void OnConnected()
    {
        chatClient.Subscribe(new string[] { CHANNEL_NAME });
    }

    private void InitChat()
    {
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "EU";

        Photon.Chat.AuthenticationValues authValues = new Photon.Chat.AuthenticationValues();
        authValues.UserId = PlayerVariables.Inistance.getUserPlayFabID();
        authValues.AuthType = Photon.Chat.CustomAuthenticationType.None;

        chatClient.Connect(PHOTON_CHAT_ID, "0.01", authValues);
    }

    private void SendRequestToFriend(string FriendId)
    {
        PanelRequests.SetActive(true);
        LoadingImage.SetActive(true);
        ShowLoading = true;
        StartCoroutine(CycleTheCircle());
        StartCoroutine(CloseLoadingIfStillOpen());

        ROOM_NAME = PlayerVariables.Inistance.getUserPlayFabID() + "Friends" + FriendId;

        string[] MSG = {
            "Request",                                                                                                      //message type
            PlayerVariables.Inistance.getUserPlayFabID(),                                                                   //sender ID
            PlayerVariables.Inistance.getUserDisplayName() == null ? "" : PlayerVariables.Inistance.getUserDisplayName(),   //Sender Name
            FriendId,                                                                                                       //reciever ID
            ROOM_NAME                                                                                                        //room name
        };

        //chatClient.PublishMessage(CHANNEL_NAME, MSG);
        chatClient.SendPrivateMessage(FriendId, MSG);
    }


    //IChatClientListener 
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string[] MSG = (string[])message;

        if (MSG.Length > 0)
        {
            string MSGType = MSG[0];
            string SenderName;
            if (MSGType.Equals("Request"))
            {
                SenderName = MSG[2];

                SENDER_ID = MSG[1];
                RECIEVER_ID = MSG[3];
                ROOM_NAME = MSG[4];

                if (RECIEVER_ID.Equals(PlayerVariables.Inistance.getUserPlayFabID()))
                {
                    PanelRequests.SetActive(true);
                    PanelDialog.SetActive(true);
                    PlayerNameUIText.text = SenderName.Equals("") ? SENDER_ID : SenderName;
                }
            }
            else if (MSGType.Equals("Response"))
            {
                SenderName = MSG[2];
                SENDER_ID = MSG[1];
                RECIEVER_ID = MSG[3];
                string ResponseType = MSG[4];

                if (RECIEVER_ID.Equals(PlayerVariables.Inistance.getUserPlayFabID()))
                {
                    if (ResponseType.Equals("NO"))
                    {
                        ShowLoading = false;
                        LoadingImage.SetActive(false);
                        PanelRequests.SetActive(false);

                        if (SenderName.Equals(""))
                        {
                            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                                StartCoroutine(GeneralController.Inistance.ToastDisplayer("Player " + SENDER_ID + " Refused Your Request"));
                            else
                                StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﺐﻋﻼﻟﺍ " + SENDER_ID + " ﻚﺒﻠﻃ ﺾﻓﺭ"));
                        }
                        else
                        {
                            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                                StartCoroutine(GeneralController.Inistance.ToastDisplayer("Player " + SenderName + " Refused Your Request"));
                            else
                                StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﺐﻋﻼﻟﺍ " + SenderName + " ﻚﺒﻠﻃ ﺾﻓﺭ"));
                        }
                    }
                    else if (ResponseType.Equals("YES"))
                    {
                        CreateOrJoinRoom(true);
                    }
                }
            }
        }
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages) { }
    public void DebugReturn(DebugLevel level, string message){}
    public void OnDisconnected(){}
    public void OnChatStateChange(ChatState state){}
    public void OnSubscribed(string[] channels, bool[] results){}
    public void OnUnsubscribed(string[] channels){}
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message){}
    public void OnUserSubscribed(string channel, string user){}
    public void OnUserUnsubscribed(string channel, string user){}


    private IEnumerator CycleTheCircle()
    {
        LoadingImage.GetComponent<RectTransform>().Rotate(0f, 0f, 200f * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        if (ShowLoading)
            StartCoroutine(CycleTheCircle());
    }

    private IEnumerator CloseLoadingIfStillOpen(float Seconds = 60f)
    {
        yield return new WaitForSeconds(Seconds);
        if (PanelDialog.activeSelf)
            PanelDialog.SetActive(false);
        if (LoadingImage.activeSelf)
            LoadingImage.SetActive(false);
        if (PanelRequests.activeSelf)
            PanelRequests.SetActive(false);
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("Connection Error"));
        else
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﻝﺎﺼﺗﻹﺍ ﻲﻓ ﺄﻄﺧ"));
    }

    public void OnRequestAccepted()
    {
        PanelDialog.SetActive(false);
        LoadingImage.SetActive(true);
        ShowLoading = true;
        StartCoroutine(CycleTheCircle());
        StartCoroutine(CloseLoadingIfStillOpen());

        string[] MSG = {
            "Response",                                                                                                      //message type
            PlayerVariables.Inistance.getUserPlayFabID(),                                                                   //sender ID
            PlayerVariables.Inistance.getUserDisplayName() == null ? "" : PlayerVariables.Inistance.getUserDisplayName(),   //Sender Name
            SENDER_ID,                                                                                                       //reciever ID
            "YES"                                                                                                            //Response Type
        };

        //chatClient.PublishMessage(CHANNEL_NAME, MSG);
        chatClient.SendPrivateMessage(SENDER_ID, MSG);

        CreateOrJoinRoom(false);
    }

    public void OnRequestRefused()
    {
        PanelDialog.SetActive(false);
        PanelRequests.SetActive(false);

        string[] MSG = {
            "Response",                                                                                                      //message type
            PlayerVariables.Inistance.getUserPlayFabID(),                                                                   //sender ID
            PlayerVariables.Inistance.getUserDisplayName() == null ? "" : PlayerVariables.Inistance.getUserDisplayName(),   //Sender Name
            SENDER_ID,                                                                                                       //reciever ID
            "NO"                                                                                                            //Response Type
        };

        //chatClient.PublishMessage(CHANNEL_NAME, MSG);
        chatClient.SendPrivateMessage(SENDER_ID, MSG);

        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("You Refused The Request"));
        else
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("ﺐﻠﻄﻟﺍ ﺖﻀﻓﺭ ﺪﻘﻟ"));
    }

    //Room

    private void CreateOrJoinRoom(bool IsSender)
    {
        OnFriends = true;
        cancle = false;
        startTimer = false;

        //if (!PhotonNetwork.InLobby)
        //    PhotonNetwork.JoinLobby(TypedLobby.Default);
        LoadingImage.SetActive(false);
        PanelRequests.SetActive(false);

        cancle = false;
        MainMenuCanvas.SetActive(false);
        WaitForCanvas.SetActive(true);



        string[] ExpectedUser = new string[1];
        ExpectedUser[0] = SENDER_ID;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.PublishUserId = true;
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default, ExpectedUser);
    }

    void Awake()
    {
        cancle = false;
        startTimer = false;
    }

    void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        //  Debug.Log("Application ending after " + Time.time + " seconds");
    }

    public override void OnJoinedRoom()
    {
        if (OnFriends)
        {
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable() { { "spawn", PhotonNetwork.CurrentRoom.PlayerCount } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
            StartGame();
        }
    }

    private void DisplayPlayersCount()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            { playercount.text = "Players 1/4"; }
            else
            {
                playercount.text = "ﻦﻴﺒﻋﻼﻟﺍ 1/4";
            }
            PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(false);
            PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(false);
            PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            { playercount.text = "Players 2/4"; }
            else
            {
                playercount.text = "ﻦﻴﺒﻋﻼﻟﺍ 2/4";
            }

            PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(false);
            PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            { playercount.text = "Players 3/4"; }
            else
            {
                playercount.text = "ﻦﻴﺒﻋﻼﻟﺍ 3/4";
            }

            PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            { playercount.text = "Players 4/4"; }
            else
            {
                playercount.text = "ﻦﻴﺒﻋﻼﻟﺍ 4/4";
            }

            PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(true);
            PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(true);
        }
    }

    private void StartGame()
    {
        ExitRoom = false;
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }
        StartCoroutine(CheckForPlayersCount());
    }

    IEnumerator CheckForPlayersCount()
    {
        if (cancle == false && !ExitRoom)
        {

            timerIncrementValue = PhotonNetwork.Time - startTime;

            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                CountDownTimer.text = "Time Left: " + Math.Round(timer - timerIncrementValue);
            else
                CountDownTimer.text = Math.Round(timer - timerIncrementValue) + " :ﻲﻗﺒﺘﻤﻟﺍ ﺖﻗﻮﻟﺍ ";


            if (timerIncrementValue >= timer || PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                startTimer = false;
                OnFriends = false;
                SceneManager.LoadScene("Demo");
                PhotonNetwork.CurrentRoom.IsOpen = false;


            }
            else
            {
                // Debug.Log("Seconds = " + timerIncrementValue);
                //  Debug.Log("PlayersCount = " + PhotonNetwork.CurrentRoom.PlayerCount);
                yield return new WaitForSeconds(1);
                StartCoroutine(CheckForPlayersCount());
            }
        }
    }

    //public override void OnJoinedLobby()
    //{
    //    LoadingImage.SetActive(false);
    //    PanelRequests.SetActive(false);

    //    cancle = false;
    //    MainMenuCanvas.SetActive(false);
    //    WaitForCanvas.SetActive(true);



    //    string[] ExpectedUser = new string[1];
    //    ExpectedUser[0] = SENDER_ID;
        
    //    RoomOptions roomOptions = new RoomOptions();
    //    roomOptions.MaxPlayers = 4;
    //    roomOptions.PublishUserId = true;
    //    PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default, ExpectedUser);
    //}

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (OnFriends)
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    CreateRoom();
    //}

    //void CreateRoom()
    //{
    //    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    //}

    public void OnClickCancle()
    {
        OnFriends = false;
        if (PhotonNetwork.InRoom)
        {
            cancle = true;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom();
            MainMenuCanvas.SetActive(true);
            WaitForCanvas.SetActive(false);
        }
        else
        {
            cancle = true;
            MainMenuCanvas.SetActive(true);
            WaitForCanvas.SetActive(false);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
        }
        ExitRoom = true;


    }

    public void OnExitButtonClick()
    {
        OnFriends = false;
        ExitRoom = true;
        if (PhotonNetwork.InRoom)
        {
            // cancle = true;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
            //mainmenuCanvas.SetActive(true);
            //WaitForCanvas.SetActive(false);
        }
        Application.Quit();
    }

}
