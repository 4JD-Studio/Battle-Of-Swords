using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_Master : MonoBehaviourPunCallbacks/*, IPunObservable*/
{
    public Transform myTarget;
    private NavMeshAgent myNavMesh;
    public delegate void GeneralEventHandele();
    public event GeneralEventHandele EventEnemyDie;
    public event GeneralEventHandele EventEnemyWalking;
    public event GeneralEventHandele EventEnemyRechedNavTarget;
    public event GeneralEventHandele EventEnemyAttack;
    public event GeneralEventHandele EventEnemyLostTarget;

    public delegate void HealthEventHandeler(int health);
    public event HealthEventHandeler EventEnemyDedectHealth;

    public delegate void NavTargetEventHandeler(Transform targetTransform);
    public event NavTargetEventHandeler EventEnemySetNavTarget;

    private Vector3 destination;
    public bool isOnRoute;
    public bool isNavPuse;
   // public GameObject[] ES;
    PhotonView pv;
    public Text MinionsCount;
    //private AudioSource audio;
    //public AudioClip callDedectedHealth;
    //public AudioClip callEventEnemySetNavTarget;
    //public AudioClip callEventEnemyDie;
    //public AudioClip callEventEnemyAttack;
    //public AudioClip callEventEnemyWalking;
    //public AudioClip callEventEnemyReachedNavTarget;
    //public AudioClip callEventEnemyLostTarget;


    void Start()
    {
        pv = GetComponent<PhotonView>();
        myNavMesh = GetComponent<NavMeshAgent>();
        //audio = GetComponent<AudioSource>();
        // ES[0] = GameObject.FindWithTag("SE1");
        //ES[1] = GameObject.FindWithTag("SE2");
        //ES[2] = GameObject.FindWithTag("SE3");

        // pv.RPC("UpdateMinionsCount", RpcTarget.All);
        //  UpdateMinionsCount();

    }
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        // local player, send data
    //        if (myNavMesh.destination != destination)
    //        {
    //            stream.SendNext(myNavMesh.destination);
    //            destination = myNavMesh.destination;
    //        }
    //    }
    //    else
    //    {
    //        myNavMesh.destination = (Vector3)stream.ReceiveNext();
    //        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
    //        myNavMesh.destination += myNavMesh.velocity * lag;
    //    }
    //}
    //[PunRPC]
    //public void UpdateMinionsCount()
    //{
    //    StartCoroutine(UpdateMinionsCountCorotine());
    //}

    //public IEnumerator UpdateMinionsCountCorotine()
    //{
    //    if (gameObject.tag == "Enemy")
    //    {
    //        pv.RPC("UpdateMinionsCount", RpcTarget.All,0);
    //       // MinionsCount.text = ES[0].transform.childCount.ToString();
    //    }
    //    if (gameObject.tag == "Enemy2")
    //    {
    //        pv.RPC("UpdateMinionsCount", RpcTarget.All, 1);
    //        // MinionsCount.text = ES[1].transform.childCount.ToString();
    //    }

    //    if (gameObject.tag == "Enemy3")
    //    {
    //        pv.RPC("UpdateMinionsCount", RpcTarget.All, 2);
    //       // MinionsCount.text = ES[2].transform.childCount.ToString();

    //    }




    //    yield return new WaitForFixedUpdate();
    //    UpdateMinionsCount();
    //}


    //[PunRPC]
    //public void UpdateMinionsEnemy(int i)
    //{
    //    MinionsCount.text = ES[i].transform.childCount.ToString();
    //}

    //public IEnumerator UpdateMinionsCountCorotine()
    //{
    //    if (gameObject.tag == "Enemy")
    //    {
    //        if (ES[0].transform.childCount!=0)
    //        {
    //            MinionsCount.text = ES[0].transform.childCount.ToString();
    //        }

    //    }
    //    if (gameObject.tag == "Enemy2")
    //    {
    //        if (ES[1].transform.childCount != 0)
    //        {
    //            MinionsCount.text = ES[1].transform.childCount.ToString();
    //        }

    //    }

    //    if (gameObject.tag == "Enemy3")
    //    {
    //        if (ES[2].transform.childCount != 0)
    //        {
    //            MinionsCount.text = ES[2].transform.childCount.ToString();
    //        }


    //    }




    //    yield return new WaitForFixedUpdate();
    //    photonView.RPC("UpdateMinionsCount", RpcTarget.All);
    //}



    public void CallDedectedHealth(int health)
    {

        if (EventEnemyDedectHealth != null)
        {

            EventEnemyDedectHealth(health);

        }

    }

    public void CallEventEnemySetNavTarget(Transform targetTransform)
    {

        if (EventEnemySetNavTarget != null)
        {
            EventEnemySetNavTarget(targetTransform);
            //audio.PlayOneShot(callEventEnemySetNavTarget);
        }
        myTarget = targetTransform;
    }

    public void CallEventEnemyDie()
    {
        if (EventEnemyDie != null)
        {
            EventEnemyDie();
         //   Debug.Log("EventEnemyDie ");
           // audio.PlayOneShot(callEventEnemyDie);
        }


    }

    public void CallEventEnemyAttack()
    {
        if (EventEnemyAttack != null)
        {
            EventEnemyAttack();
          //  audio.PlayOneShot(callEventEnemyAttack);
        }


    }

    public void CallEventEnemyWalking()
    {
        if (EventEnemyWalking != null)
        {
            EventEnemyWalking();
           // audio.PlayOneShot(callEventEnemyWalking);
        }


    }
    public void CallEventEnemyLostTarget()
    {
        if (EventEnemyLostTarget != null)
        {
            EventEnemyLostTarget();

        }

        myTarget = null;
    }

    public void CallEventEnemyReachedNavTarget()
    {
        if (EventEnemyRechedNavTarget != null)
        {
            EventEnemyRechedNavTarget();

        }


    }


}
