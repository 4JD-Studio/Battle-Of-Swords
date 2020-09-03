using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Player_Health :  MonoBehaviourPunCallbacks, IPunObservable
{
  
    [SerializeField]
    private int startingHealth = 100;
  
  
    [SerializeField]
  //  private NameTag nameTag;

    private Animator animator;

    [SerializeField]
    private float respawnTime = 8.0f;
    [SerializeField]
    private float sinkTime = 2.5f;

    private Slider healthSlider;
  //  private Image imageDie;
    private bool isDead;
    private bool damaged;
    private bool isSinking;
    private int currentHealth;
    int attackDamage =20;
    MobileARPGPlayerMove mobileARPGPlayerMove;
   // PhotonView pv;
 
    int EnemyCount;
    public GameObject gameSetup;
    void SetOnIntialRefrance()
    {
        mobileARPGPlayerMove = GetComponent<MobileARPGPlayerMove>();
        animator = GetComponent<Animator>();
  
         healthSlider = GameObject.FindGameObjectWithTag("Screen").GetComponentInChildren<Slider>();
     
        gameSetup = GameObject.FindWithTag("NetworkManager");
       // PlayerScore playerScore;
       
        currentHealth = startingHealth;
        if (photonView.IsMine)
        {
           
            healthSlider.value = currentHealth;
        }
        damaged = false;
        isDead = false;
        isSinking = false;
    }

    void Start()
    {
     //   pv = GetComponent<PhotonView>();
        SetOnIntialRefrance();
    }


    [PunRPC]
    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            gameObject.SetActive(false);
            return;
        }
      
        if (photonView.IsMine)
        {
            damaged = true;
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                AudioPlayer.Inistance.OnDeath();
                photonView.RPC("Death", RpcTarget.All);
            }
            else
                AudioPlayer.Inistance.OnGetHit();
            healthSlider.value = currentHealth;

            //SetUI();
        }
    }

    [PunRPC]
   public void Death()
    {
        isDead = true;
       
       // nameTag.gameObject.SetActive(false);
        if (photonView.IsMine)
        {
            mobileARPGPlayerMove.enabled = false;
            
            //
            gameSetup.GetComponent<GameSetup>().sceneCamera.SetActive(true);
            gameSetup.GetComponent<GameSetup>().ShowCanvasLosser();
           

            // 
            this.enabled = false;
        }
      
    }

  
   

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (int)stream.ReceiveNext();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("This Line Enter");
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            if (other.CompareTag("swordPlayer")/*|| other.CompareTag("swordEnemy")|| other.CompareTag("swordEnemy1")|| other.CompareTag("swordEnemy2")*/)
            {

                //  if (other.gameObject.GetComponent<PhotonView>().ViewID == hitPlayer.photonView.viewID) return;
                // Debug.Log("hit " + PhotonNetwork.LocalPlayer.NickName);

               // Debug.Log("This Line Called 1 Player");
                photonView.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, attackDamage);
               // Debug.Log("This Line Called 2 Player");



            }
        }
       

     
    }
}
