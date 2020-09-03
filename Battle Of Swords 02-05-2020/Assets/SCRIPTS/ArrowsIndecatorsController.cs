using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ArrowsIndecatorsController : MonoBehaviour
{
    public static ArrowsIndecatorsController Inistance;

    public Image Arrow1, Arrow2, Arrow3;
    public float UpdateTime = 0.5f;
    public float BorderSize = 50;
    public Camera UICamera;


    GameObject[] CurrentPlayers;
    bool[] IsAlive;

    private void Awake()
    {
        Inistance = this;
    }

    public void StartAfterAllPlayersInit()
    {
        CurrentPlayers = new GameObject[3];
        IsAlive = new bool[3] { true, true, true };

        GameObject[] ExistPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < ExistPlayers.Length; i++)
        {
            if (!ExistPlayers[i].GetComponent<PhotonView>().IsMine)
                CurrentPlayers[i] = ExistPlayers[i];
        }

        if (CurrentPlayers[2] == null)
            CurrentPlayers[2] = GameObject.FindGameObjectWithTag("Enemy");
        if (CurrentPlayers[1] == null)
            CurrentPlayers[1] = GameObject.FindGameObjectWithTag("Enemy2");
        if (CurrentPlayers[0] == null)
            CurrentPlayers[0] = GameObject.FindGameObjectWithTag("Enemy3");

        StartCoroutine(ArrowsUpdater());
        
        Arrow1.gameObject.SetActive(true);
        Arrow2.gameObject.SetActive(true);
        Arrow3.gameObject.SetActive(true);
    }

    IEnumerator ArrowsUpdater()
    {
        if(IsAlive[0])
            ShowPlayerDirectionOnScreen(Arrow1, IsAlive[0], CurrentPlayers[0]);
        if (IsAlive[1])
            ShowPlayerDirectionOnScreen(Arrow2, IsAlive[1], CurrentPlayers[1]);
        if (IsAlive[2])
            ShowPlayerDirectionOnScreen(Arrow3, IsAlive[2], CurrentPlayers[2]);
        yield return new WaitForSeconds(UpdateTime);
        if (IsAlive[0] || IsAlive[1] || IsAlive[2])
            StartCoroutine(ArrowsUpdater());
    }

    void ShowPlayerDirectionOnScreen(Image CArrow, bool IsCurrentPlayerAlive, GameObject CPlayer)
    {
        if(CPlayer != null && CPlayer.activeSelf)
        {
            //get direction if far
            //make arrow disappear if near
            Vector3 ScreenPos = Camera.main.WorldToScreenPoint(CPlayer.transform.position);

            Vector3 ScreenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
            ScreenPos -= ScreenCenter;

            float Angle = Mathf.Atan2(ScreenPos.y, ScreenPos.x);
            Angle -= 90 * Mathf.Deg2Rad;
            CArrow.transform.localRotation = Quaternion.Euler(0, 0, Angle * Mathf.Rad2Deg);

            Vector3 EnempyPosScreenPoint = Camera.main.WorldToScreenPoint(CPlayer.transform.position);

            bool IsOffScreen = EnempyPosScreenPoint.x <= BorderSize || EnempyPosScreenPoint.x >= Screen.width - BorderSize ||
            EnempyPosScreenPoint.y <= BorderSize || EnempyPosScreenPoint.y >= Screen.height - BorderSize;

            if (IsOffScreen)
            {
                CArrow.gameObject.SetActive(true);
                Vector3 CappedEnemyScreenPos = EnempyPosScreenPoint;
                if (CappedEnemyScreenPos.x <= BorderSize) CappedEnemyScreenPos.x = BorderSize;
                if (CappedEnemyScreenPos.x >= Screen.width - BorderSize) CappedEnemyScreenPos.x = Screen.width - BorderSize;
                if (CappedEnemyScreenPos.y <= BorderSize) CappedEnemyScreenPos.y = BorderSize;
                if (CappedEnemyScreenPos.y >= Screen.height - BorderSize) CappedEnemyScreenPos.y = Screen.height - BorderSize;

                Vector3 PointerWorldPos = UICamera.ScreenToWorldPoint(CappedEnemyScreenPos);//UI Camera
                CArrow.GetComponent<RectTransform>().position = PointerWorldPos;
                CArrow.GetComponent<RectTransform>().localPosition = 
                    new Vector3(CArrow.GetComponent<RectTransform>().localPosition.x, CArrow.GetComponent<RectTransform>().localPosition.y, 0f);
            }
            else
            {
                //change it to circle or remove it
                CArrow.gameObject.SetActive(false);
            }
        }
        else
        {
            CArrow.gameObject.SetActive(false);
            IsCurrentPlayerAlive = false;
        }
    }
}
