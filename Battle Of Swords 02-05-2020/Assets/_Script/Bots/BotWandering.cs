using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotWandering : MonoBehaviour/*, IPunObservable*/
{
   

    private NavMeshHit navHit;
    private NavMeshAgent myNavMesh;
    private float checkRate=.1f;
    private float nextCheck;
    private float wanderRange = 20;
    private Transform myTransform;
    private Vector3 wanderTarget;
    private Vector3 destination;



    private void Start()
    {
       // Timer = WanderTimer;
       

        if (GetComponent<NavMeshAgent>() != null)
        {
            myNavMesh = GetComponent<NavMeshAgent>();
        }
        myTransform = transform;
        //  checkRate = UnityEngine.Random.Range(0.1f, 0.2f);
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(onCoroutine());
        }
       
    }

   


    private void TrytoCheckIfShouldWanderd()
    {
        

            if (RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
            {
                myNavMesh.SetDestination(wanderTarget);
               
            }
      
    }
    bool RandomWanderTarget(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * wanderRange;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {

            result = navHit.position;
            return true;
        }
        else
        {
            result = center;
            return false;
        }
    }
    IEnumerator onCoroutine()
    {
        while (true)
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
               
            }


            TrytoCheckIfShouldWanderd();



            //Timer += Time.deltaTime;

            //if (Timer >= WanderTimer)
            //{
            //  //  Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * WanderRadius;
            //    Vector3 newPos = RandomNavSphere(transform.position, WanderRadius, -1);
            //    Agent.SetDestination(newPos);
            //    Timer = 0;
            //}

            yield return new WaitForSeconds(.1f);
        }
    }

    //private void Update()
    //{
      
    //}

    //private static Vector3 RandomNavSphere(Vector3 Origin, float Distenation, int Layermask)
    //{
    //    Vector3 RandomDirection = Random.insideUnitSphere * Distenation;
    //    RandomDirection += Origin;
    //    NavMeshHit NavHit;
    //    NavMesh.SamplePosition(RandomDirection, out NavHit, Distenation, Layermask);
    //    return NavHit.position;
    //}
}