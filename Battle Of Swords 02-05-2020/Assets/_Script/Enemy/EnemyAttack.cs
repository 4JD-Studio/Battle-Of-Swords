using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    private Transform attackTarget;
    private Transform myTransform;
   float attackRate =1.5f;
    private float nextAttack;
    public float attackRange = 3.5f;
    public int attackDamage ;
    public float range = 0.5f;

    //public GameObject KillObjectDamage;
    //public Transform KillObjectDamagepostion;
    Coroutine TryToAttackPlayer;
   

    void SetAttackTarget(Transform targetTransform)
    {
        attackTarget = targetTransform;
    }
    void DiableTHis()
    {
        this.enabled = false;
    }

    void SetIntialRefrance()
    {
        enemyMaster = GetComponent<Enemy_Master>();
        myTransform = transform;
    
    }
   
    private void OnEnable()
    {
        SetIntialRefrance();
        enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
        enemyMaster.EventEnemyDie += DiableTHis;
    }
    private void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DiableTHis;
        enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
    }


   
    // Update is called once per frame
    void OnEnemyAttack()
    {
        if (attackTarget != null)
        {
            if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
            {
                Vector3 toOther = attackTarget.position - myTransform.position;
                if (Vector3.Dot(toOther, myTransform.forward) > range)
                {
                    if (gameObject.tag == "Enemy")
                    {
                      

                        if (attackTarget.CompareTag("Player"))
                        {
                            attackDamage = 20;
                            attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                            
                        }
                        if (attackTarget.CompareTag("Enemy2"))
                        {
                            attackDamage = 5;
                            // enemyMaster.CallDedectedHealth(attackDamage);
                            attackTarget.GetComponent<Enemy_Health>().DeductionHealth(20);
                            //attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }
                        if (attackTarget.CompareTag("Enemy3"))
                        {
                            attackDamage =5;
                            attackTarget.GetComponent<Enemy_Health>().DeductionHealth(20);
                            //  attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }

                       // attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                    }
                    if (gameObject.tag == "Enemy2")
                    {
                        

                        if (attackTarget.CompareTag("Player"))
                        {
                            attackDamage = 20;
                            attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }
                     
                        if (attackTarget.CompareTag("Enemy3"))
                        {
                            attackDamage = 5;
                            attackTarget.GetComponent<Enemy_Health>().DeductionHealth(20);
                            //attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }
                        if (attackTarget.CompareTag("Enemy"))
                        {
                            attackDamage = 5;
                            attackTarget.GetComponent<Enemy_Health>().DeductionHealth(20);
                           // attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }

                       
                    }
                    if (gameObject.tag == "Enemy3")
                    {
                       

                        if (attackTarget.CompareTag("Player"))
                        {
                            attackDamage = 20;
                            attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }
                        if (attackTarget.CompareTag("Enemy2"))
                        {
                            attackDamage = 5;
                            attackTarget.GetComponent<Enemy_Health>().DeductionHealth(20);
                            //attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                        }
                        if (attackTarget.CompareTag("Enemy"))
                        {
                            attackDamage = 5;
                            attackTarget.GetComponent<Enemy_Health>().DeductionHealth(20);
                           // attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);

                        }
                       



                       // attackTarget.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
                    }


                  
                  
                }

            }
        }
    }

    private void Update()
    {
        if (attackTarget != null)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;

                if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
                {
                    Vector3 lookAtVector3 = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                    myTransform.LookAt(lookAtVector3);
                    enemyMaster.CallEventEnemyAttack();
                    enemyMaster.isOnRoute = false;
                }

            }

        }
    }
}
