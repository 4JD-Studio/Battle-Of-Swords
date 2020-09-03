using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
   
   
   // public Text pallf;
    public GameObject[] playerPrefabName;
    public GameObject[] BotsPrefabsName;
    public Transform[] spawnpointsPlyers;

   
   
    //bool Startgame=true;
    public GameObject sceneCamera;
   


    public GameObject CanvasGameOver;
    public static GameSetup instance;

    bool CheckPlayers;

    private float startTime = 10;
    public  string TimeCountdown;
    string timestring;
    public Text txtTimeCountdown;
    int min, sec;
    public static int playerId = 0;

    [Header("Elzallat Variables For Win & Lose")]
    public GameObject ToastHolder;
    public GameObject[] ObjectsToDisable;
    public Sprite BackgroundWinArabic, BackgroundWinEnglish, BackgroundLoseArabic, BackgroundLoseEnglish,
                  ButtonBackArabic, ButtonBackEnglish, ImageGoldArabic, ImageGoldEnglish;
    public GameObject CanvasWinLose;
    public Text UserLevelText, UserAttackText, UserMinionsCount;
    public GameObject NotificationTextCount;
    private int OldTextCount = 0;


    bool StartGame = false;
    double StartTime = 0;
    public int GameDuration = 180;

    public bool sTartGame = true;

    public Text text;
    PhotonView pv;
    void Awake()
    {
        instance = this;
    }

    public void ShowCanvasLosser()
    {
        //CanvasGameOver.SetActive(true);
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LeaveLobby();
        //Time.timeScale = 0;
        WinLoseController.Inistance.WhenPlayerLose();
    }
    public void LoadScene()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("ManMenu");
    }


    void Start()
    {
        startTime = 180;
        playerId = 0;
        pv = GetComponent<PhotonView>();

        if (sTartGame)
        {

            UserLevelText.text = PlayerVariables.Inistance.getCurrentLevel().ToString();
            UserAttackText.text = PlayerVariables.Inistance.getCurrentPlayerAttack().ToString();
            UserMinionsCount.text = "0";


            if (PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "1")
            {
                string CharacterPrefabName = playerPrefabName[1].name;
                if (PlayerVariables.Inistance.getCurrentCharacter() != null)
                    CharacterPrefabName = PlayerVariables.Inistance.getCurrentCharacter().PrefabName;
                sceneCamera.SetActive(false);
                PhotonNetwork.Instantiate(Path.Combine("Players", CharacterPrefabName),
                                        spawnpointsPlyers[0].position,
                                        spawnpointsPlyers[0].rotation,
                                        0);


                sTartGame = false;
                playerId = 1;

                //   Debug.Log("Players in the room after start pressed " + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            }

            if (PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "2")
            {
                string CharacterPrefabName = playerPrefabName[1].name;
                if (PlayerVariables.Inistance.getCurrentCharacter() != null)
                    CharacterPrefabName = PlayerVariables.Inistance.getCurrentCharacter().PrefabName;
                sceneCamera.SetActive(false);
                PhotonNetwork.Instantiate(Path.Combine("Players", CharacterPrefabName),
                                          spawnpointsPlyers[1].position,
                                          spawnpointsPlyers[1].rotation,
                                          0);

                sTartGame = false;
                playerId = 2;
                //   Debug.Log("Players in the room after start pressed " + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            }
            if (PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "3")
            {
                string CharacterPrefabName = playerPrefabName[1].name;
                if (PlayerVariables.Inistance.getCurrentCharacter() != null)
                    CharacterPrefabName = PlayerVariables.Inistance.getCurrentCharacter().PrefabName;
                sceneCamera.SetActive(false);
                PhotonNetwork.Instantiate(Path.Combine("Players", CharacterPrefabName),
                                          spawnpointsPlyers[2].position,
                                          spawnpointsPlyers[2].rotation,
                                          0);
                sTartGame = false;

                playerId = 3;
                //  Debug.Log("Players in the room after start pressed " + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            }
            if (PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "4")
            {
                string CharacterPrefabName = playerPrefabName[1].name;
                if (PlayerVariables.Inistance.getCurrentCharacter() != null)
                    CharacterPrefabName = PlayerVariables.Inistance.getCurrentCharacter().PrefabName;
                sceneCamera.SetActive(false);
                PhotonNetwork.Instantiate(Path.Combine("Players", CharacterPrefabName),
                                         spawnpointsPlyers[3].position,
                                         spawnpointsPlyers[3].rotation,
                                         0);
                sTartGame = false;
                playerId = 4;

                //  Debug.Log("Players in the room after start pressed " + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            }
            sTartGame = false;
            CheckPlayers = true;
        }
        //PhotonNetwork.CurrentRoom.GetPlayer(ActorProperties)


        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable CustomeValue = new ExitGames.Client.Photon.Hashtable();
            StartTime = PhotonNetwork.Time;
            StartGame = true;
            CustomeValue.Add("StartTime", StartTime.ToString());
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["StartTime"] != null)
            {
                Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"]);
                StartTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
                StartGame = true;
            }
            else
                Debug.Log("Start Time  == null ");

        }

    }

    public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
        if (StartTime != 0 && propertiesThatChanged.ContainsKey("StartTime"))
        {
            StartTime = (int)propertiesThatChanged["StartTime"];
            StartGame = true;
        }
    }





    void Update()
    {

        if (StartGame)
        {
            int CurrentTime = (int)(PhotonNetwork.Time - StartTime);
            if (CurrentTime >= GameDuration)
            {
                //Time Over
                txtTimeCountdown.text = "-- : --";
                WinLoseController.Inistance.WhenTimeOver();
                StartGame = false;
            }
            else
            {
                //  Debug.Log("Time Is " + CurrentTime);
                //11 12 13
                int ReversedTime = GameDuration - CurrentTime;
                //  Debug.Log(" RT " + ReversedTime);
                int Minuts = ReversedTime / 60;
                int Seconds = ReversedTime - (Minuts * 60);
                txtTimeCountdown.text = Minuts + " : " + Seconds;
            }

            int CurrentCount = CalculateBotsCount();
            UserMinionsCount.text = CurrentCount.ToString();
            if (OldTextCount < CurrentCount)
            {
                OldTextCount = CurrentCount;
                StartCoroutine(ShowNotificationText());
            }

        }
        else
        {
            txtTimeCountdown.text = "-- : --";

        }

        //text.text = "" + PhotonNetwork.LocalPlayer.ActorNumber;


        if (CheckPlayers)
        {

            pv.RPC("ShowEnemy", RpcTarget.All);

            CheckPlayers = false;
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
        }
    }

  


    [PunRPC]
    public void ShowEnemy()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            CheckPlayers = false;
            BotsPrefabsName[0].SetActive(true);


        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            CheckPlayers = false;
            BotsPrefabsName[1].SetActive(true);


        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            CheckPlayers = false;
            BotsPrefabsName[2].SetActive(true);

        }

        ArrowsIndecatorsController.Inistance.StartAfterAllPlayersInit();
        WinLoseController.Inistance.StartCheckForWin();

    }
    void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        // Debug.Log("Application ending after " + Time.time + " seconds");
    }

    public  void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("LoadingScene");
    }
    

    private int CalculateBotsCount()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GetComponent<PhotonView>().ViewID == 1001*/)
            return GameObject.FindGameObjectWithTag("SP1").transform.childCount;
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GetComponent<PhotonView>().ViewID == 2001*/)
            return GameObject.FindGameObjectWithTag("SP2").transform.childCount;
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*GetComponent<PhotonView>().ViewID == 3001*/)
            return GameObject.FindGameObjectWithTag("SP3").transform.childCount;
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GetComponent<PhotonView>().ViewID == 4001*/)
            return GameObject.FindGameObjectWithTag("SP4").transform.childCount;
        else return 0;
    }

    public IEnumerator ShowNotificationText(float Time = 2f)
    {
        NotificationTextCount.SetActive(true);
        NotificationTextCount.transform.GetChild(0).GetComponent<Text>().text = "+" + OldTextCount;
        yield return new WaitForSeconds(Time);
        NotificationTextCount.SetActive(false);
    }

}
