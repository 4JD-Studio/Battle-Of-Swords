using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    private Transform myTransform;
    public Transform head;
    public LayerMask playerLayer;
    public LayerMask sightLayer;
    private float checkRate;
    private float nextRate;
     float detectedRaduis =10;
    private RaycastHit hit;

    Coroutine TryToPlayer;

    void DisableThis()
    {
        this.enabled = false;

    }

    void SetIntialRefrence()
    {

        enemyMaster = GetComponent<Enemy_Master>();
        myTransform = transform;
        if (head == null)
        {

            head = myTransform;
        }

        checkRate = UnityEngine.Random.Range(0.4f, 1f);
    }

    private void OnEnable()
    {
        SetIntialRefrence();
        enemyMaster.EventEnemyDie += DisableThis;
    }

    private void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;

    }

   

    void Update()
    {
         CarryOutDetection();
    }

    private void CarryOutDetection()
    {
        if (gameObject.tag == "Enemy")
        {
            if (Time.time > nextRate)
            {
                nextRate = Time.time + checkRate;
                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectedRaduis, playerLayer);
                if (colliders.Length > 0)
                {

                    foreach (Collider potentialTargetCollider in colliders)
                    {
                        //Debug.Log("e "+potentialTargetCollider.tag);


                        if (potentialTargetCollider.CompareTag("Player"))
                        {

                            if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                            {
                              //  print("Player insight");
                                break;
                            }
                        }
                        else if (potentialTargetCollider.CompareTag("WanderdBot") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;

                        }
                        else if (potentialTargetCollider.CompareTag("Friendly") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly1") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly3") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Enemy2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("Enemy3") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }



                        else if (potentialTargetCollider.CompareTag("SoliderEnemy2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("SoliderEnemy1") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }




                    }
                }

                else
                {

                    enemyMaster.CallEventEnemyLostTarget();
                }


            }
        }

        if (gameObject.tag == "Enemy2")
        {
            if (Time.time > nextRate)
            {
                nextRate = Time.time + checkRate;
                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectedRaduis, playerLayer);
                if (colliders.Length > 0)
                {

                    foreach (Collider potentialTargetCollider in colliders)
                    {
                       // Debug.Log("e2 " + potentialTargetCollider.tag);


                        if (potentialTargetCollider.CompareTag("Player"))
                        {

                            if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                            {
                             //   print("Player insight");
                                break;
                            }
                        }

                        else if (potentialTargetCollider.CompareTag("WanderdBot") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;

                        }
                        else if (potentialTargetCollider.CompareTag("Friendly") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly1") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("Enemy3") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Enemy") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("SoliderEnemy") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }



                        else if (potentialTargetCollider.CompareTag("SoliderEnemy1") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }
                    }
                }

                else
                {

                    enemyMaster.CallEventEnemyLostTarget();
                }


            }
        }
        if (gameObject.tag == "Enemy3")
        {
            if (Time.time > nextRate)
            {
                nextRate = Time.time + checkRate;
                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectedRaduis, playerLayer);
                if (colliders.Length > 0)
                {

                    foreach (Collider potentialTargetCollider in colliders)
                    {

                      //  Debug.Log("e 3" + potentialTargetCollider.tag);

                        if (potentialTargetCollider.CompareTag("Player"))
                        {

                            if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                            {
                               // print("Player insight");
                                break;
                            }
                        }
                        else if (potentialTargetCollider.CompareTag("WanderdBot") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;

                        }
                        else if (potentialTargetCollider.CompareTag("Friendly") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly1") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Friendly2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                        else if (potentialTargetCollider.CompareTag("Enemy2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("Enemy") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("SoliderEnemy") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }

                        else if (potentialTargetCollider.CompareTag("SoliderEnemy2") && CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {

                            break;
                        }


                    }
                }

                else
                {

                    enemyMaster.CallEventEnemyLostTarget();
                }


            }
        }
       

    }


    bool CanPotentialTargetBeSeen(Transform potentialTarget)
    {
        //Debug.Log("In Method");
        //if (Physics.Linecast(head.position, potentialTarget.position, out hit, sightLayer))
        //{
        //  Debug.Log("In If 1 ");
        //  Debug.Log("Object Name = "  + hit.transform.gameObject.name);
        //if (hit.transform == potentialTarget)
        //{
        //    Debug.Log("In If 2");
        enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
        return true;

        //}
        //else
        //{
        // Debug.Log("In Else 1");
        //        enemyMaster.CallEventEnemyLostTarget();
        //        return false;
        //    }
        //}
        //else
        //{
        //  //  Debug.Log("In Else 2");
        //    enemyMaster.CallEventEnemyLostTarget();
        //    return false;
        //}


    }
}
