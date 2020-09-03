using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class BotsInistantiator : MonoBehaviourPunCallbacks
{
    public GameObject BotFollowerPrefab;
    //public Text AllMinionsCounter;
     float InistantiateTime =1f;
    public string PrefabName = "Bot Minion";
    PhotonView pv;
    private int Counter = 0;
    private void Start()
    {
        //BotFollowerPrefab[0].SetActive(true);
        //BotFollowerPrefab[1].SetActive(false);
        StartCoroutine(InistantiateBot());
         Counter +=5;
        //pv = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            InistantiateBot();
        }



    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            InistantiateBot();
            // StartCoroutine(InistantiateBot()); ;
        }

        //}
        //[PunRPC]
        //public void InitStarter()
        //{
        //    BotFollowerPrefab[0].SetActive(true);
        //    BotFollowerPrefab[1].SetActive(false);
        //    StartCoroutine(InistantiateBot());
        }

        private IEnumerator InistantiateBot()
    {
        while (true)
        {

            yield return new WaitForSeconds(InistantiateTime);
            //BotFollowerPrefab[0].SetActive(false);
            //BotFollowerPrefab[1].SetActive(true);
            for (int i = 0; i < transform.childCount; i++)
            {
                // object[] myCustomInitData = GetInitData();
                GameObject Bot = PhotonNetwork.InstantiateSceneObject("Bots",

                    transform.GetChild(i).position,
                     BotFollowerPrefab.transform.rotation);
                BotFollowerPrefab.transform.GetChild(0).gameObject.SetActive(true);
                Bot.transform.parent = transform.GetChild(i);

                Counter++;

            }

            InistantiateTime += 100;

        }
        //to organize structure
        //inistantiate bots in their places

        //yield return new WaitForSeconds(InistantiateTime);

        //InitStarter();
    }

    //private object[] GetInitData()
    //{
    //    throw new NotImplementedException();
    //}

    //PhotonView PView;
    //private readonly byte CustomManualInstantiationEventCode = 1;
    //private void Start()
    //{
    //    //add player and enemy inital minions
    //    // == 16 >>  4 * 4
    //    Counter += 16;
    //   // InitStarter();
    //    // AllMinionsCounter.text = Counter.ToString();

    //    //PView = GetComponent<PhotonView>();
    //    //   if (PhotonNetwork.IsMasterClient)
    //    //   {
    //    //       PView.RPC("InitStarter", RpcTarget.All);
    //    //       //InitStarter();
    //    //   }
    //    //if (PView.IsMine)
    //    //{

    //    //}
    //    // 
    //}

   
}
