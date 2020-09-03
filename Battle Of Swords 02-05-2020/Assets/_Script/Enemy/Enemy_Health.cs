using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour/*, IPunObservable*/
{
    private Enemy_Master enemyMaster;
    public int enemyHealth = 100;

   
    private Animator animator;
  
    private float respawnTime = 2.0f;
    [SerializeField]
    private float sinkTime = 2.5f;
    
    private bool isDead;
    private bool damaged;
    private bool isSinking;
    private int currentHealth;
    public int attackDamage =10;
    PhotonView pv;

    void SetIntialReferance()
    {
        enemyMaster = GetComponent<Enemy_Master>();
        pv = GetComponent<PhotonView>();
          animator = GetComponent<Animator>();
        currentHealth = enemyHealth;
        damaged = false;
        isDead = false;
        isSinking = false;
    }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnEnable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        SetIntialReferance();
        enemyMaster.EventEnemyDedectHealth += DeductionHealth;

    }

 
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnDisable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        enemyMaster.EventEnemyDedectHealth -= DeductionHealth;
    }
    public void DeductionHealth(int attackDamage)
    {

        //Debug.Log("DeductionHealth"+gameObject.tag);
        pv.RPC("TakeDamage", RpcTarget.All, attackDamage);
   


    }

    [PunRPC]
    public void TakeDamage(int amount)
    {
        if (isDead) return;
        //if (photonView.IsMine)
        //{
            damaged = true;
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
            pv.RPC("Death", RpcTarget.All);
            }
           

     
    }
    [PunRPC]
    void Death()
    {
        isDead = true;

      
        //if (photonView.IsMine)
        //{
           // Debug.Log("Die Enemy");
            enemyMaster.enabled = false;
            animator.SetTrigger("die");
            enemyMaster.CallEventEnemyDie();
          //  Destroy(gameObject);
       


       // }
        StartCoroutine("DestoryPlayer", respawnTime);
        
    }
    IEnumerator DestoryPlayer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

       //0 string CurrentTag = null;
        //if (gameObject.tag.Equals("Enemy"))
        //    CurrentTag = "SE1";
        //else if (gameObject.tag.Equals("Enemy2"))
        //    CurrentTag = "SE2";
        //else if (gameObject.tag.Equals("Enemy3"))
        //    CurrentTag = "SE3";
        //if (CurrentTag != null)
        //{
        //    foreach (Transform transform in GameObject.FindWithTag(CurrentTag).transform)
        //    {
        //        PhotonNetwork.Destroy(transform.gameObject);
        //    }
        //}

        Destroy(gameObject);
    }


    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(currentHealth);
    //    }
    //    else
    //    {
    //        currentHealth = (int)stream.ReceiveNext();
    //    }
    //}


}
