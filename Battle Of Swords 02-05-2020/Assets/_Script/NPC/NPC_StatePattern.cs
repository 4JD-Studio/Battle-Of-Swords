using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPC_StatePattern : MonoBehaviour/*, IPunObservable*/
{
    private float checkRate = 0.1f;
    private float nextCheck;
    public float sightRange = 40;

    public float meleeAttackRange = 4;
    public float meleeAttackDamge = 10;

    public float attackRate = .4f;
    public float nextAttack;

    public float offest = .4f;






    public bool hasMeleeAttack;
    public bool isMeleeAttacking;

    public GameObject myFollowTarget;
    [HideInInspector]
    public Transform pursueTarget;






    public LayerMask sightLayer;
    public LayerMask myEnemyLayers;
    public LayerMask myFriendlyLayers;
    public string[] myEnemyTags;
    public string[] myFriendlyTags;



    public Transform head;


    public NPC_Master npcMaster;
    [HideInInspector]
    public NavMeshAgent mynavMshAgent;



    private Transform possibleTarget;

    private Collider[] colliders;

    public GameObject player;
    NPC_StatePattern nPC_StatePattern;
    PhotonView pv;
    void Start()
    {
        mynavMshAgent = GetComponent<NavMeshAgent>();
        nPC_StatePattern = GetComponent<NPC_StatePattern>();
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            nPC_StatePattern.enabled = false;
        }
        player = GameObject.FindWithTag("Player");
        if (gameObject.tag == "Friendly")
        {
            // player.ActorNumber
            // player = GameObject.FindWithTag("Player");
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1/*GameSetup.playerId == 1 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "1"*/)
            {
                myFollowTarget = player;
            }

        }
        if (gameObject.tag == "Friendly1")
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == 2/*GameSetup.playerId == 2 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "2"*/)
            {
                myFollowTarget = player;
            }




        }
        if (gameObject.tag == "Friendly2")
        {

            if (PhotonNetwork.LocalPlayer.ActorNumber == 3/*GameSetup.playerId == 3 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "3"*/)
            {
                myFollowTarget = player;
            }




        }
        if (gameObject.tag == "Friendly3")
        {

            if (PhotonNetwork.LocalPlayer.ActorNumber == 4/*GameSetup.playerId == 4 || PhotonNetwork.LocalPlayer.CustomProperties["spawn"].ToString() == "4"*/)
            {
                myFollowTarget = player;
            }




        }



        if (gameObject.tag == "SoliderEnemy")
        {
            myFollowTarget = GameObject.FindWithTag("Enemy");
        }
        if (gameObject.tag == "SoliderEnemy1")
        {
            myFollowTarget = GameObject.FindWithTag("Enemy2");
        }
        if (gameObject.tag == "SoliderEnemy2")
        {
            myFollowTarget = GameObject.FindWithTag("Enemy3");
        }


    }





    void Update()
    {
        CarryOutUpdateStatae();

    }

    void CarryOutUpdateStatae()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            Follow();
            Look();
        }
    }

    void Follow()
    {
        if (!mynavMshAgent.enabled)
        {
            return;
        }

        if (myFollowTarget != null)
        {
            mynavMshAgent.SetDestination(myFollowTarget.transform.position);
            KeepWalking();


        }

        if (HavReachedDestination())
        {
            StopWalking();


        }

    }

    void KeepWalking()
    {

        mynavMshAgent.isStopped = false;
        npcMaster.CallEventNPCWalkingAnim();

    }

    void StopWalking()
    {

        mynavMshAgent.isStopped = true;
        npcMaster.CallEventNPCIdleAnim();
    }

    bool HavReachedDestination()
    {
        if (mynavMshAgent.remainingDistance <= mynavMshAgent.stoppingDistance + 1.5f &&
            !mynavMshAgent.pathPending)
        {
            //  Debug.Log("HavReachedDestination StopWalking");

            StopWalking();
            return true;
        }
        else
        {
            //Debug.Log("HavReachedDestination KeepWalking");

            KeepWalking();
            return false;
        }
    }

    void Look()
    {

        colliders = Physics.OverlapSphere(transform.position, sightRange - 15, myEnemyLayers);

        foreach (Collider col in colliders)
        {

            foreach (string tags in myEnemyTags)
            {
                if (col.transform.CompareTag(tags))
                {
                    // Debug.Log("Look");
                    possibleTarget = col.transform;

                    pursueTarget = possibleTarget;

                    ToPursueState();


                }
            }
        }
    }

    void ToPursueState()
    {

        if (pursueTarget != null)
        {
            mynavMshAgent.SetDestination(pursueTarget.position);


            float distanceToTarget = Vector3.Distance(transform.position, pursueTarget.position);

            if (distanceToTarget < meleeAttackRange)
            {

                ToMeleeAttackingState();


            }
        }
        else
        {
            Follow();
        }

    }

    void ToMeleeAttackingState()
    {
        if (pursueTarget != null)
        {

            if (Time.deltaTime > nextAttack)
            {
                nextAttack = Time.deltaTime + attackRate;

                if (Vector3.Distance(transform.position, pursueTarget.position) <= meleeAttackRange)
                {
                    Vector3 newPos = new Vector3(pursueTarget.position.x, transform.position.y, pursueTarget.position.z);
                    transform.LookAt(newPos);
                    npcMaster.CallEventNPCAttackAnim();
                    // npc.isMeleeAttacking = true;

                }
                else
                {
                    //ToPatrolState();
                    Look();
                }

            }
        }
        else
        {
            //   ToPatrolState();
            Follow();
        }

    }





}
