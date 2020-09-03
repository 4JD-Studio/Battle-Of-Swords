using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class WinLoseController : MonoBehaviour
{
    [HideInInspector]
    public GameObject ToastHolder;
    [HideInInspector]
    public GameObject[] ObjectsToDisable;
    public int BotValue = 50;
    public int KillAllEnemies = 500;
    public int TenLevelsOpenPrice = 1000;
    [HideInInspector]
    public Sprite BackgroundWinArabic, BackgroundWinEnglish, BackgroundLoseArabic, BackgroundLoseEnglish,
                  ButtonBackArabic, ButtonBackEnglish, ImageGoldArabic, ImageGoldEnglish;
    [HideInInspector]
    public GameObject CanvasWinLose;

    public static WinLoseController Inistance;

    private GameObject[] EnemysList;
    private GameObject[] SpawnPoints;
    bool ShouldStart = false;
    int BotsCount = 0;

    bool CanvasDisplayedBefore = false;

    private void Awake()
    {
        Inistance = this;

        ToastHolder = GameSetup.instance.ToastHolder;
        ObjectsToDisable = GameSetup.instance.ObjectsToDisable;
        BackgroundWinArabic = GameSetup.instance.BackgroundWinArabic;
        BackgroundWinEnglish = GameSetup.instance.BackgroundWinEnglish;
        BackgroundLoseArabic = GameSetup.instance.BackgroundLoseArabic;
        BackgroundLoseEnglish = GameSetup.instance.BackgroundLoseEnglish;
        ButtonBackArabic = GameSetup.instance.ButtonBackArabic;
        ButtonBackEnglish = GameSetup.instance.ButtonBackEnglish;
        ImageGoldArabic = GameSetup.instance.ImageGoldArabic;
        ImageGoldEnglish = GameSetup.instance.ImageGoldEnglish;
        CanvasWinLose = GameSetup.instance.CanvasWinLose;

        ShouldStart = false;
        CanvasDisplayedBefore = false;
        CanvasWinLose.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { LoadScene(); });
    }

    public void StartCheckForWin()
    {
        EnemysList = new GameObject[3];
        EnemysList[0] = GameObject.FindGameObjectWithTag("Enemy");
        EnemysList[1] = GameObject.FindGameObjectWithTag("Enemy2");
        EnemysList[2] = GameObject.FindGameObjectWithTag("Enemy3");
        ShouldStart = true;

        SpawnPoints = new GameObject[7];
        SpawnPoints[0] = GameObject.FindGameObjectWithTag("SP1");
        SpawnPoints[1] = GameObject.FindGameObjectWithTag("SP2");
        SpawnPoints[2] = GameObject.FindGameObjectWithTag("SP3");
        SpawnPoints[3] = GameObject.FindGameObjectWithTag("SP4");
        SpawnPoints[4] = GameObject.FindGameObjectWithTag("SE1");
        SpawnPoints[5] = GameObject.FindGameObjectWithTag("SE2");
        SpawnPoints[6] = GameObject.FindGameObjectWithTag("SE3");
    }

    void Update()
    {
        //all enemys are dead or desabled and there is only one player

        if (ShouldStart)
        {
            if ((EnemysList[0] == null || !EnemysList[0].activeSelf) &&
                        (EnemysList[1] == null || !EnemysList[1].activeSelf) &&
                        (EnemysList[2] == null || !EnemysList[2].activeSelf) &&
                        PhotonNetwork.PlayerList.Length == 1)
            {
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GetComponent<PhotonView>().ViewID == 1001*/)
                    BotsCount = GameObject.FindGameObjectWithTag("SP1").transform.childCount;
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GetComponent<PhotonView>().ViewID == 2001*/)
                    BotsCount = GameObject.FindGameObjectWithTag("SP2").transform.childCount;
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*GetComponent<PhotonView>().ViewID == 3001*/)
                    BotsCount = GameObject.FindGameObjectWithTag("SP3").transform.childCount;
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GetComponent<PhotonView>().ViewID == 4001*/)
                    BotsCount = GameObject.FindGameObjectWithTag("SP4").transform.childCount;

                WhenPlayerWin(BotsCount, false);
            }
        }
    }

    public IEnumerator ToastDisplayer(string Message, float TimeDisplay = 2f)
    {
        ToastHolder.SetActive(true);
        ToastHolder.transform.GetChild(0).GetComponent<Text>().text = Message;
        yield return new WaitForSeconds(TimeDisplay);
        ToastHolder.SetActive(false);
    }

    public void WhenPlayerWin(int BotsCount, bool IsTimeOver)
    {
        CanvasDisplayedBefore = true;
        ShouldStart = false;

        foreach (GameObject item in ObjectsToDisable)
        {
            item.SetActive(false);
        }


        int EarnedGold = BotsCount * BotValue;
        if (!IsTimeOver)
            EarnedGold += KillAllEnemies;
        string Message;

        //Show Win Canvas

        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
        {
            CanvasWinLose.transform.GetChild(1).GetComponent<Image>().sprite = BackgroundWinEnglish;
            CanvasWinLose.transform.GetChild(2).GetComponent<Image>().sprite = ButtonBackEnglish;
            CanvasWinLose.transform.GetChild(3).GetComponent<Image>().sprite = ImageGoldEnglish;
        }
        else
        {
            CanvasWinLose.transform.GetChild(1).GetComponent<Image>().sprite = BackgroundWinArabic;
            CanvasWinLose.transform.GetChild(2).GetComponent<Image>().sprite = ButtonBackArabic;
            CanvasWinLose.transform.GetChild(3).GetComponent<Image>().sprite = ImageGoldArabic;
        }
        // CanvasWinLose.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { LoadScene(); });
        CanvasWinLose.transform.GetChild(3).gameObject.SetActive(true);
        CanvasWinLose.transform.GetChild(4).gameObject.SetActive(false);
        CanvasWinLose.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = EarnedGold.ToString();
        CanvasWinLose.SetActive(true);


        if (PlayerVariables.Inistance.getCurrentLevel() != 100 && PlayerVariables.Inistance.getCurrentLevel() % 10 == 0)
        {
            PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() + EarnedGold);
            if (PlayerVariables.Inistance.getCurrentGold() >= TenLevelsOpenPrice)
            {
                PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() - TenLevelsOpenPrice);
                PlayerVariables.Inistance.setCurrentLevel(PlayerVariables.Inistance.getCurrentLevel() + 1);
                if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                {
                    Message = "You got " +
                        BotsCount +
                        " minions" +
                        "\n" +
                        "and earned " +
                        EarnedGold +
                        " gold." +
                        "\n\n" +
                        "You entered level " +
                        PlayerVariables.Inistance.getCurrentLevel() +
                        "\n" +
                        TenLevelsOpenPrice +
                        " gold has been decreased";
                }
                else
                {
                    Message = " ﻚﻳﺪﻟ" +
                          BotsCount +
                          " ﻊﺑﺎﺗ" +
                          "\n" +
                          " ﻰﻠﻋ ﺖﻠﺼﺣﻭ" +
                          EarnedGold +
                          ".ﺐﻫﺩ " +
                          "\n\n" +
                          " ﺔﻠﺣﺮﻤﻟﺍ ﺖﻠﺧﺩ ﺪﻘﻟ" +
                          PlayerVariables.Inistance.getCurrentLevel() +
                          "\n" +
                          TenLevelsOpenPrice +
                          "ﻢﻬﻤﺼﺧ ﻢﺗ ﺐﻫﺩ";
                }
            }
            else
            {
                if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                {
                    Message = "You got " +
                          BotsCount +
                          " minions" +
                          "\n" +
                          "and earned " +
                          EarnedGold +
                          " gold." +
                          "\n\n" +
                          "You don't have enough gold to level up, tou need " +
                          TenLevelsOpenPrice +
                          " gold.";
                }
                else
                {
                    Message = " ﻚﻳﺪﻟ" +
                          BotsCount +
                          " ﻊﺑﺎﺗ" +
                          "\n" +
                          " ﻰﻠﻋ ﺖﻠﺼﺣﻭ" +
                          EarnedGold +
                          ".ﺐﻫﺩ " +
                          "\n\n" +
                          " ﺝﺎﺘﺤﺗ ,ﺔﻴﻟﺎﺘﻟﺍ ﺔﻠﺣﺮﻤﻠﻟ ﻝﻮﺧﺪﻠﻟ ﻲﻓﺎﻛ ﺐﻫﺩ ﻚﻳﺪﻟ ﺲﻴﻟ" +
                          TenLevelsOpenPrice +
                          ".ﺐﻫﺩ ";
                }
            }
        }
        else
        {
            PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() + EarnedGold);
            PlayerVariables.Inistance.setCurrentLevel(PlayerVariables.Inistance.getCurrentLevel() + 1);
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            {
                Message = "You got " +
                    BotsCount +
                    " minions" +
                    "\n" +
                    "and earned " +
                    EarnedGold +
                    " gold." +
                    "\n\n" +
                    "You entered level " +
                    PlayerVariables.Inistance.getCurrentLevel();
            }
            else
            {
                Message = " ﻚﻳﺪﻟ" +
                      BotsCount +
                      " ﻊﺑﺎﺗ" +
                      "\n" +
                      " ﻰﻠﻋ ﺖﻠﺼﺣﻭ" +
                      EarnedGold +
                      ".ﺐﻫﺩ " +
                      "\n\n" +
                      " ﺔﻠﺣﺮﻤﻟﺍ ﺖﻠﺧﺩ ﺪﻘﻟ" +
                      PlayerVariables.Inistance.getCurrentLevel();
            }
        }
        StartCoroutine(ToastDisplayer(Message, 3f));
    }

    public void LoadScene()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("ManMenu");
    }

    public void WhenPlayerLose()
    {
        CanvasDisplayedBefore = true;
        foreach (GameObject item in ObjectsToDisable)
        {
            item.SetActive(false);
        }


        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
        {
            CanvasWinLose.transform.GetChild(1).GetComponent<Image>().sprite = BackgroundLoseEnglish;
            CanvasWinLose.transform.GetChild(2).GetComponent<Image>().sprite = ButtonBackEnglish;
            CanvasWinLose.transform.GetChild(3).GetComponent<Image>().sprite = ImageGoldEnglish;
        }
        else
        {
            CanvasWinLose.transform.GetChild(1).GetComponent<Image>().sprite = BackgroundLoseArabic;
            CanvasWinLose.transform.GetChild(2).GetComponent<Image>().sprite = ButtonBackArabic;
            CanvasWinLose.transform.GetChild(3).GetComponent<Image>().sprite = ImageGoldArabic;
        }
        // CanvasWinLose.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { LoadScene(); });
        //CanvasWinLose.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = 0.ToString();
        CanvasWinLose.transform.GetChild(3).gameObject.SetActive(false);
        CanvasWinLose.transform.GetChild(4).gameObject.SetActive(true);
        CanvasWinLose.SetActive(true);
    }

    public void WhenTimeOver()
    {
        if (CanvasDisplayedBefore)
            return;
        int[] ScoreList = new int[7];

        if (SpawnPoints[0] != null)
            ScoreList[0] = SpawnPoints[0].transform.childCount;
        else
            ScoreList[0] =0;
        if (SpawnPoints[1] != null)
            ScoreList[1] = SpawnPoints[1].transform.childCount;
        else
            ScoreList[1] = 0;
        if (SpawnPoints[2] != null)
            ScoreList[2] = SpawnPoints[2].transform.childCount;
        else
            ScoreList[2] = 0;
        if (SpawnPoints[3] != null)
            ScoreList[3] = SpawnPoints[3].transform.childCount;
        else
            ScoreList[3] = 0;
        if (SpawnPoints[4] != null)
            ScoreList[4] = SpawnPoints[4].transform.childCount;
        else
            ScoreList[4] = 0;
        if (SpawnPoints[5] != null)
            ScoreList[5] = SpawnPoints[5].transform.childCount;
        else
            ScoreList[5] = 0;
        if (SpawnPoints[6] != null)
            ScoreList[6] = SpawnPoints[6].transform.childCount;
        else
            ScoreList[6] = 0;

        int MaxScore = 0;
        foreach (int Score in ScoreList)
        {
            if (Score > MaxScore)
                MaxScore = Score;
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GetComponent<PhotonView>().ViewID == 1001*/)
        {
            if (ScoreList[0] == MaxScore && MaxScore > 0)
                WhenPlayerWin(ScoreList[0], true);
            else
                WhenPlayerLose();
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GetComponent<PhotonView>().ViewID == 2001*/)
        {
            if (ScoreList[1] == MaxScore && MaxScore > 0)
                WhenPlayerWin(ScoreList[1], true);
            else
                WhenPlayerLose();
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*GetComponent<PhotonView>().ViewID == 3001*/)
        {
            if (ScoreList[2] == MaxScore && MaxScore > 0)
                WhenPlayerWin(ScoreList[2], true);
            else
                WhenPlayerLose();
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GetComponent<PhotonView>().ViewID == 4001*/)
        {
            if (ScoreList[3] == MaxScore && MaxScore > 0)
                WhenPlayerWin(ScoreList[3], true);
            else
                WhenPlayerLose();
        }

    }
}
