using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bot_Health : MonoBehaviour
{
    public string[] playerSolider, EnemySolider;
    public GameObject[] PS, ES;
    private bool isDestroyed;
    public GameObject player;
    PhotonView photonView;
    private void Start()
    {
        PS[0] = GameObject.FindWithTag("SP1");
        PS[1] = GameObject.FindWithTag("SP2");
        PS[2] = GameObject.FindWithTag("SP3");
        PS[3] = GameObject.FindWithTag("SP4");
        ES[0] = GameObject.FindWithTag("SE1");
        ES[1] = GameObject.FindWithTag("SE2");
        ES[2] = GameObject.FindWithTag("SE3");
        photonView = GetComponent<PhotonView>();
       // player = GameObject.FindWithTag("Player");
    }
    void OnTriggerExit(Collider other)
    {
        if (isDestroyed)
        {
            return;
        }
        //if (!photonView.IsMine)
        //{
        //    return;
        //}





        if (other.CompareTag("swordPlayer"))
        {
           
            
            //if (photonView.IsMine)
            //{
                
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 1001*/)
                {



                    isDestroyed = true;
                    GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0],
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
               }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 2001*/)
                {
                    isDestroyed = true;
                    GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1],
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 3001*/)
                {
                    isDestroyed = true;
                    GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2],
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
                if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 4001*/)
                {
                    isDestroyed = true;
                    GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3],
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
               

           // }
        }



        if (other.CompareTag("swordPSolider"))
        {
            //if (photonView.IsMine)
            //{
                //BotDie("playerSoliderFolder", playerSolider[0].name);
                isDestroyed = true;
                GameObject Ps=PhotonNetwork.Instantiate( playerSolider[0],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                Ps.transform.parent = PS[0].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
           // }
            // photonView.RPC("BotDie", RpcTarget.All, "playerSoliderFolder", playerSolider[0].name);

        }
        if (other.CompareTag("swordPSolider1"))
        {
            //if (photonView.IsMine)
            //{
               // BotDie("playerSoliderFolder", playerSolider[1].name);

                isDestroyed = true;
                GameObject Ps1 = PhotonNetwork.Instantiate( playerSolider[1],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                Ps1.transform.parent = PS[1].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
           // }
            // photonView.RPC("BotDie", RpcTarget.All, "playerSoliderFolder", playerSolider[1].name);

        }

        if (other.CompareTag("swordPSolider2"))
        {
            //if (photonView.IsMine)
            ////  BotDie("playerSoliderFolder", playerSolider[2].name);
            //{
                isDestroyed = true;
                GameObject Ps2 = PhotonNetwork.Instantiate( playerSolider[2],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                Ps2.transform.parent = PS[2].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
           // }


        } 
            if (other.CompareTag("swordPSolider3"))
            {
            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject Ps3 = PhotonNetwork.Instantiate( playerSolider[3],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                Ps3.transform.parent = PS[3].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
            }
               // BotDie("playerSoliderFolder", playerSolider[3].name);
           // }


        ///Enemy

        if (other.CompareTag("swordEnemy"))
        {

            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject es = PhotonNetwork.Instantiate(EnemySolider[0],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                es.transform.parent = ES[0].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
           // }
            // BotDie("EnemySoliderFolder", EnemySolider[0].name);


        }

        if (other.CompareTag("swordEnemy1"))
        {


            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject es1 = PhotonNetwork.Instantiate(EnemySolider[1],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                es1.transform.parent = ES[1].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
           // }
            // BotDie("EnemySoliderFolder", EnemySolider[1].name);

        }

        if (other.CompareTag("swordEnemy2"))
        {
            // photonView.RPC("BotDie", RpcTarget.All, "EnemySoliderFolder", EnemySolider[2].name);
            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject es2 = PhotonNetwork.Instantiate(EnemySolider[2],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                es2.transform.parent = ES[2].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
            //}
            // BotDie("EnemySoliderFolder", EnemySolider[2].name);


        }
        if (other.CompareTag("swordEsolider"))
            {
            // photonView.RPC("BotDie", RpcTarget.All, "EnemySoliderFolder", EnemySolider[0].name);
            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject es = PhotonNetwork.Instantiate( EnemySolider[0],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                es.transform.parent = ES[0].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
            //}
        }
            if (other.CompareTag("swordEsolider1"))
        {
            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject es1 = PhotonNetwork.Instantiate( EnemySolider[1],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                es1.transform.parent = ES[1].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
           // }
        }
            if (other.CompareTag("swordEsolider2"))
            {
            //if (photonView.IsMine)
            //{
                isDestroyed = true;
                GameObject es2 = PhotonNetwork.Instantiate( EnemySolider[2],
                                       gameObject.transform.position,
                                       gameObject.transform.rotation,
                                       0);
                es2.transform.parent = ES[2].transform;
                photonView.RPC("DestroyBots", RpcTarget.All);
          //  }
        }

      //}
    }
   

       
        //public  void BotDie(string folderName,string PlayerName)
        //{
        //isDestroyed = true;
        //PhotonNetwork.Instantiate(Path.Combine(folderName, PlayerName),
        //                       gameObject.transform.position,
        //                       gameObject.transform.rotation,
        //                       0);
        //photonView.RPC("DestroyBots", RpcTarget.All);


        //}

    [PunRPC]
    public void DestroyBots()
    {
        isDestroyed = true;
       Destroy(gameObject);
    }

}
