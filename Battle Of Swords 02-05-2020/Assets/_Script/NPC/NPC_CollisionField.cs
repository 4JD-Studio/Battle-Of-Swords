using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NPC_CollisionField : MonoBehaviourPunCallbacks
{
    private NPC_Master npcMster;
    public GameObject[] playerSolider, EnemySolider;
    public GameObject[] PS, ES;
    private bool isDestroyed;
    private void Start()
    {
        npcMster = GetComponent<NPC_Master>();

        PS[0] = GameObject.FindWithTag("SP1");
        PS[1] = GameObject.FindWithTag("SP2");
        PS[2] = GameObject.FindWithTag("SP3");
        PS[3] = GameObject.FindWithTag("SP4");
        ES[0] = GameObject.FindWithTag("SE1");
        ES[1] = GameObject.FindWithTag("SE2");
        ES[2] = GameObject.FindWithTag("SE3");
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (isDestroyed)
        {
            return;
        }
        if (!photonView.IsMine)
        {
            return;
        }




        if (gameObject.tag == "Friendly")


        {


            if (other.CompareTag("swordEnemy") )
            {



                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
           }

            }
         if (other.CompareTag("swordEnemy1") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
               }
            }
            if (other.CompareTag("swordEnemy2") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
             }
            }

            
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {

                    if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[1].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[2].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[3].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
               }

            }

            
            if (other.CompareTag("swordPSolider1"))
            {
                if (photonView.IsMine)
                {


                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
            }
            }
            if (other.CompareTag("swordPSolider2"))
            {
                if (photonView.IsMine)

                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
            }
            }
            if (other.CompareTag("swordPSolider3"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[3].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
             }
            }
        }



        
        if (gameObject.tag == "Friendly1")
        {

            if (other.CompareTag("swordEnemy") )
            {



                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
               }

            }
            if (other.CompareTag("swordEnemy1") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
               }
            }
            if (other.CompareTag("swordEnemy2"))
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
             }
            }

            
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                    {



                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[0].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                   
                    if (PhotonNetwork.LocalPlayer.ActorNumber==3)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[2].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GameSetup.playerId == 4 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "4"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 4001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[3].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
               }

            }

            //
            if (other.CompareTag("swordPSolider"))
            {
                if (photonView.IsMine)
                {
                    

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
              }
            }
            if (other.CompareTag("swordPSolider2"))
            {
                if (photonView.IsMine)
              
                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
             }
            }
            if (other.CompareTag("swordPSolider3"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[3].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

        }


        if (gameObject.tag == "Friendly2")
        {

            if (other.CompareTag("swordEnemy") )
            {



                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }

            }
            if (other.CompareTag("swordEnemy1") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordEnemy2") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
            }
            }

            
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GameSetup.playerId == 1 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "1"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 1001*/)
                    {



                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[0].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GameSetup.playerId == 2 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "2"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 2001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[1].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                   
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GameSetup.playerId == 4 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "4"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 4001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[3].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
              }

            }

            //
            if (other.CompareTag("swordPSolider1"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
              }
            }
            if (other.CompareTag("swordPSolider"))
            {
                if (photonView.IsMine)
                //  BotDie("playerSoliderFolder", playerSolider[2].name);
                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
               }
            }
            if (other.CompareTag("swordPSolider3"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[3].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
               }
            }
        }


        /////
        if (gameObject.tag == "Friendly3")
        {

            if (other.CompareTag("swordEnemy") )
            {



                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }

            }
            if (other.CompareTag("swordEnemy1"))
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordEnemy2"))
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

            //player
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GameSetup.playerId == 1 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "1"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 1001*/)
                    {



                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[0].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GameSetup.playerId == 2|| PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "2"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 2001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[1].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*GameSetup.playerId == 3 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "3"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 3001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[2].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    //if (other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 4001)
                    //{
                    //    isDestroyed = true;
                    //    GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                    //                           gameObject.transform.position,
                    //                           gameObject.transform.rotation,
                    //                           0);
                    //    Ps.transform.parent = PS[3].transform;
                    //    photonView.RPC("DestroyBots", RpcTarget.All);
                    //}
                    //}

                }
            }

            //
            if (other.CompareTag("swordPSolider1"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider2"))
            {
                if (photonView.IsMine)
                //  BotDie("playerSoliderFolder", playerSolider[2].name);
                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

        }


        ///
        if (gameObject.tag == "SoliderEnemy")
        {

           
            if (other.CompareTag("swordEnemy1"))
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordEnemy2") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

            //player
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GameSetup.playerId == 1 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "1"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 1001*/)
                    {



                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[0].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GameSetup.playerId == 2 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "2"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 2001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[1].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*GameSetup.playerId == 3 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "3"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 3001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[2].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GameSetup.playerId == 4 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "4"*//*other.transform.gameObject.GetComponentInParent<PhotonView>().ViewID == 4001*/)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[3].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
              }

            }
            if (other.CompareTag("swordPSolider"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
              }
            }
            //
            if (other.CompareTag("swordPSolider1"))
            {
                if (photonView.IsMine)
                {
                  

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider2"))
            {
                if (photonView.IsMine)
                //  BotDie("playerSoliderFolder", playerSolider[2].name);
                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider3"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[3].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
        }


        /////
        if (gameObject.tag == "SoliderEnemy1")
        {

            if (other.CompareTag("swordEnemy") )
            {



                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }

            }

            if (other.CompareTag("swordEnemy2"))
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

            //player
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                    {



                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[0].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[1].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[2].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[3].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                }

            }
            if (other.CompareTag("swordPSolider"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            //
            if (other.CompareTag("swordPSolider1"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider2"))
            {
                if (photonView.IsMine)
                //  BotDie("playerSoliderFolder", playerSolider[2].name);
                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider3"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[3].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

        }


        /////
        if (gameObject.tag == "SoliderEnemy2")
        {

            if (other.CompareTag("swordEnemy") )
            {



                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }

            }
            if (other.CompareTag("swordEnemy1") )
            {

                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject es = PhotonNetwork.Instantiate(EnemySolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    es.transform.parent = ES[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
           

            //player
            if (other.CompareTag("swordPlayer"))
            {
                if (photonView.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
                    {



                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[0].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[0].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[1].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[1].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[2].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[2].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
                    if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
                    {
                        isDestroyed = true;
                        GameObject Ps = PhotonNetwork.Instantiate(playerSolider[3].name,
                                               gameObject.transform.position,
                                               gameObject.transform.rotation,
                                               0);
                        Ps.transform.parent = PS[3].transform;
                        photonView.RPC("DestroyBots", RpcTarget.All);
                    }
              }

            }
            if (other.CompareTag("swordPSolider"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[0].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[0].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
             }
            }
            //
            if (other.CompareTag("swordPSolider1"))
            {
                if (photonView.IsMine)
                {
                    // BotDie("playerSoliderFolder", playerSolider[1].name);

                    isDestroyed = true;
                    GameObject Ps1 = PhotonNetwork.Instantiate(playerSolider[1].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps1.transform.parent = PS[1].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
             }
            }
            if (other.CompareTag("swordPSolider2"))
            {
                if (photonView.IsMine)
              
                {
                    isDestroyed = true;
                    GameObject Ps2 = PhotonNetwork.Instantiate(playerSolider[2].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps2.transform.parent = PS[2].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }
            if (other.CompareTag("swordPSolider3"))
            {
                if (photonView.IsMine)
                {
                    isDestroyed = true;
                    GameObject Ps3 = PhotonNetwork.Instantiate(playerSolider[3].name,
                                           gameObject.transform.position,
                                           gameObject.transform.rotation,
                                           0);
                    Ps3.transform.parent = PS[3].transform;
                    photonView.RPC("DestroyBots", RpcTarget.All);
                }
            }

        }







    }

 

    //}
    [PunRPC]
    public void DestroyBots()
    {
      Destroy(gameObject);
    }

}
