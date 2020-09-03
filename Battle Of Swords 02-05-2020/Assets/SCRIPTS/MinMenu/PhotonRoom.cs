using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
//using NUnit.Framework;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public GameObject mainmenuCanvas, WaitForCanvas;
    public Transform PlayerFigures;
    bool cancle;
    public double timer = 10;

    GameObject player;
    bool nameExist = false;

    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    ExitGames.Client.Photon.Hashtable CustomeValue;


    public Text playercount, CountDownTimer;  
   

    public static PhotonRoom room;
    bool ExitRoom = false;
    //private PhotonView pv;
    private bool OnNormalPlay = false;



    void Awake()
    {
        //if (PhotonRoom.room==null)
        //{
        //    PhotonRoom.room = this;

        //}
        //else
        //{
        //    if (PhotonRoom.room!=this)

        //    {
        //        Destroy(PhotonRoom.room.gameObject);
        //        PhotonRoom.room = this;
        //    }
        //}
      //  DontDestroyOnLoad(this.gameObject);

        //pv=GetComponent<PhotonView>();
        cancle = false;
        startTimer = false;
    }

    void Update()
    {
        if (startTimer)
            DisplayPlayersCount();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
        }
    }

    void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        //  Debug.Log("Application ending after " + Time.time + " seconds");
    }
   
    public override void OnJoinedRoom()
    {
        if (OnNormalPlay)
        {
            //Debug.Log("we are now in room");
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable() { { "spawn", PhotonNetwork.CurrentRoom.PlayerCount } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

            // DisplayPlayersCount();
            //ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable() { { "spawn", PhotonNetwork.CurrentRoom.PlayerCount } };
            //PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
            //if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            //    playercount.text = "Players " + PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() + "/4";
            //else
            //    playercount.text = "ﻦﻴﺒﻋﻼﻟﺍ " + PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() + "/4";
            //switch (PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString())
            //{
            //    case "1":
            //        PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(false);
            //        PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(false);
            //        PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
            //        break;
            //    case "2":
            //        PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(false);
            //        PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
            //        break;
            //    case "3":
            //        PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
            //        break;
            //    case "4":
            //        PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(true);
            //        PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(true);
            //        break;
            //}

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


        //  if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
        //      playercount.text = "Players " + PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() + "/4";
        //  else
        //      playercount.text = "ﻦﻴﺒﻋﻼﻟﺍ " + PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() + "/4";

        ////  Debug.Log("Function Called: " + PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString());
        //  switch (PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString())
        //  {
        //      case "1":
        //          PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(false);
        //          PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(false);
        //          PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
        //          break;
        //      case "2":
        //          PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(false);
        //          PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
        //          break;
        //      case "3":
        //          PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(false);
        //          break;
        //      case "4":
        //          PlayerFigures.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(1).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //          PlayerFigures.GetChild(3).GetChild(0).gameObject.SetActive(true);
        //          break;
        //  }
    }

    private void StartGame()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    return;
        //}
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
                OnNormalPlay = false;
                startTimer = false;
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

    public void OnPlayClick()
    {
        //cancle = false;
        //mainmenuCanvas.SetActive(false);
        //WaitForCanvas.SetActive(true);
        //PhotonNetwork.JoinRandomRoom();

        OnNormalPlay = true;
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        if (OnNormalPlay)
        {
            cancle = false;
            mainmenuCanvas.SetActive(false);
            WaitForCanvas.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (OnNormalPlay)
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }
  
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        if (OnNormalPlay)
            CreateRoom();
    }

    void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });

    }

    public void OnClickCancle()
    {
        OnNormalPlay = false;
        if (PhotonNetwork.InRoom)
        {
            cancle = true;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom();
            mainmenuCanvas.SetActive(true);
            WaitForCanvas.SetActive(false);
        }
        else
        {
            cancle = true;
            mainmenuCanvas.SetActive(true);
            WaitForCanvas.SetActive(false);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
        }
        ExitRoom = true;
         

    }

    public void OnExitButtonClick()
    {
        OnNormalPlay = false;
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
