using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CollisionField : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    private Rigidbody rigidbodyStriking;
    private int damageToApplay;
   
    int damage=10;

    private float damageFator = 0.1f;
  //  AudioSource audio;
    void DisableThis()
    {
        this.enabled = false;
        // gameObject.SetActive(false);
       enemyMaster.EventEnemyDie += DisableThis;
    }

    private void OnEnable()
    {
        SetIntialReferanc();
      enemyMaster.EventEnemyDie += DisableThis;
    }
    private void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    private void SetIntialReferanc()
    {
        enemyMaster = transform.GetComponent<Enemy_Master>();
        //audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        int damageToApplay = damage;

        if (other.CompareTag("swordPlayer") )
        {




          //  Debug.Log("from player 1:" + damageToApplay);
           
            enemyMaster.GetComponent<Enemy_Master>().CallDedectedHealth(damageToApplay);





        }

      
    }
}
