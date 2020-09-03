using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Photon.Pun;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// This script requires a character controller to be attached
[RequireComponent (typeof (CharacterController))]
public class MobileARPGPlayerMove : MonoBehaviour
{

    // const float locomationAnimationSmoothTime = 0,lf;
  
   
    public float distanceError = 0.5f; // The distance where you stop the character between the difference of target.position and character.position.
    public float gravity = 0.4f; // Gravity for the character.
    public float rayCastDisntance = 500.0f; // The ray casting distance of the mouse click.
    public LayerMask layerMask = 1 << 7; // The layers the raycast should ignore.
    public float minMultiInputTouchWaitTime = 0.8f; // Stop player from moving when user zoomed in with pinch for duration.
    int moveSpeed =5; // Character movement speed.
    public int rotationSpeed = 8; // How quick the character rotate to target location.
   
	

    private IEnumerator coroutine,corCol;
   

   


    private Camera myCamera;
    private Transform myTransform;
    private Vector3 currentMoveToPos; // The position of the mouse click, the location where the character should go.
    private bool hasTargetPosition = false; // Tells us if there is a target to move to.
    private CharacterController controller;
    private Animator animator; // The animator for the toon. 
    private CollisionFlags collisionFlags;
    private bool buttonDown = false;    // If player holds the mouse button down.	
    private float verticalSpeed = 0.0f; // The current vertical speed.		
    private bool overUI = false; // If touch event over ui first dont move

    private float lastMultiInputTouch = 0;
    //private float lastMultiInputTouch = 0;


    float rot = 0;
    private Vector3 movDir = Vector3.zero;
    float nexthitTime;
    float hitDelay = 0.25f;
    PhotonView pv;


   // public int BeginningMinionsCount = 4;
    //public GameObject MinionPrefab;

  MobileARPGPlayerMove mobileARPGPlayerMove;
    bool AnimPlaying;
    public GameObject[] swords;
    public GameObject[] Shields;
    public static int armor,level;
    public static int gold;
    [SerializeField]
   // private NameTag nameTag;
    public Text MinionsCount;
 // public  Collider collider;

  //  public GameObject[] PS;

    int SwordIndex;
    float speedPercent;
    void Start()
	{

       // Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);


        pv = GetComponent<PhotonView>();
        myCamera = GetComponentInChildren<Camera>();
      mobileARPGPlayerMove = GetComponent<MobileARPGPlayerMove>();
       
        layerMask =~layerMask; // Get all the layers to raycast on, this will allow the raycast to ignore chosen layers.
	
        myTransform = transform;
    
         controller = myTransform.GetComponent<CharacterController>();
       
      
          animator = myTransform.GetComponent<Animator>();


        SwordIndex = swords.Length - 1;
        if (PlayerVariables.Inistance.getCurrentWeapon() != null)
            SwordIndex = PlayerVariables.Inistance.getCurrentWeapon().PrefabIndex;
        else
        {
            if (PlayerVariables.Inistance.getCurrentLevel() > 10)
                SwordIndex = 25;
            if (PlayerVariables.Inistance.getCurrentLevel() > 20)
                SwordIndex = 26;
            if (PlayerVariables.Inistance.getCurrentLevel() > 30)
                SwordIndex = 27;
            if (PlayerVariables.Inistance.getCurrentLevel() > 40)
                SwordIndex = 28;
            if (PlayerVariables.Inistance.getCurrentLevel() > 50)
                SwordIndex = 29;
            if (PlayerVariables.Inistance.getCurrentLevel() > 60)
                SwordIndex = 30;
            if (PlayerVariables.Inistance.getCurrentLevel() > 70)
                SwordIndex = 31;
            if (PlayerVariables.Inistance.getCurrentLevel() > 80)
                SwordIndex = 32;
            if (PlayerVariables.Inistance.getCurrentLevel() > 90)
                SwordIndex = 33;
            if (PlayerVariables.Inistance.getCurrentLevel() >= 100)
                SwordIndex = 34;
        }

        for (int i = 0; i < swords.Length; i++)
        {
            if (i == SwordIndex)
                swords[i].SetActive(true);
            else
                swords[i].SetActive(false);
        }


        int ShieldIndex = Shields.Length - 1;
        if (PlayerVariables.Inistance.getCurrentShield() != null)
            ShieldIndex = PlayerVariables.Inistance.getCurrentShield().PrefabIndex;

        for (int i = 0; i < Shields.Length; i++)
        {
            if (i == ShieldIndex)
                Shields[i].SetActive(true);
            else
                Shields[i].SetActive(false);
        }


        if (!pv.IsMine)
        {

            myCamera.gameObject.SetActive(false);
          //  mobileARPGPlayerMove.enabled = false;

        }
        //else
        //{
          
        //}

        GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Button>().onClick.AddListener(() => { Attack(); });
















    }



    public void Update()
    {
        if (pv.IsMine)
        {
            Movement();

        }
      
       

    }


    bool IsGrounded()
    {
        return controller.isGrounded;
    }

    void ApplyGravity()
    {
        if (IsGrounded())
        {
            verticalSpeed = 0f;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
    }
   
    void Movement()
    {

        int tc = Input.touchCount;
        // Check if screen is touched else reset overUI to false.
        if (tc > 0)
        {
            // Check all touches
            foreach (Touch touch in Input.touches)
            {
                // Only check on first touch
                int pointerID = touch.fingerId;
                if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(pointerID))
                {
                    overUI = true;
                }

            }
        }
        else
        {
            overUI = false;
        }

        // Check for single touch and if in UI.
        // Ignores UI if touch is hold down.
        if (tc == 1 && (buttonDown || !overUI) && (Time.time - lastMultiInputTouch > minMultiInputTouchWaitTime))
        {
            Ray ray = myCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayCastDisntance, layerMask))
            {
                currentMoveToPos = hit.point;
                // Keep target height same as player height for accuracy.
                currentMoveToPos.y = myTransform.position.y;
                if (Vector3.Distance(myTransform.position, currentMoveToPos) > distanceError)
                {
                    hasTargetPosition = true;
                }
            }
            buttonDown = true;
        }
        else if (tc >= 2 && !overUI)
        {
            lastMultiInputTouch = Time.time;
            buttonDown = false;
        }
        else
        {
            buttonDown = false;
        }

        // Make sure the character stays on the ground.
        ApplyGravity();

        // Keep target height same as player height for accuracy.
        currentMoveToPos.y = myTransform.position.y;

        // Was a successful move enabled.
        if (hasTargetPosition)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(currentMoveToPos - myTransform.position), rotationSpeed * Time.deltaTime);
            // Move to target location.
            collisionFlags = controller.Move((myTransform.forward * moveSpeed * Time.deltaTime) + (new Vector3(0, verticalSpeed, 0)));
            if (animator != null)
            {
               
                animator.SetFloat("speedPercent", moveSpeed);
                // animator.SetBool("run", true);
            }
            // Look at target.
            //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(currentMoveToPos - myTransform.position), rotationSpeed * Time.deltaTime);
            //// Move to target location.
            //collisionFlags = controller.Move((myTransform.forward * moveSpeed * Time.deltaTime) + (new Vector3(0, verticalSpeed, 0)));
            ////if (animator != null)
            //{
            //    animator.SetFloat("speedPercent", moveSpeed);
            //   // animator.SetBool("run", true);
            //}
            // Check if side was hit and stop character.
            if (collisionFlags.ToString().Equals("5"))
            {
                hasTargetPosition = false;
                // Set character to previous position so animation of running/walking happens next.
             collisionFlags = controller.Move((myTransform.forward * -1 * moveSpeed * Time.deltaTime) + (new Vector3(0, verticalSpeed, 0)));
              
                if (animator != null)
                {
                   
                    animator.SetFloat("speedPercent", 0);
                    //  animator.SetBool("run", false);
                }
            }
            // Calculate distance to target location and stop if in range.
            if (Vector3.Distance(myTransform.position, currentMoveToPos) <= distanceError && !buttonDown)
            {
                hasTargetPosition = false;
                if (animator != null)
                {
                    animator.SetFloat("speedPercent", 0);
                    //animator.SetBool("run", false);
                }
              
            }
        }
        else if (verticalSpeed != 0.0f)
        {
            controller.Move(new Vector3(0, verticalSpeed, 0));
        }




#if UNITY_EDITOR || UNITY_STANDALONE_WIN


        if (!animator)
        {
            return;
        }

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {

                animator.SetFloat("speedPercent", moveSpeed);
                movDir = new Vector3(0, 0, 1);
                movDir *= moveSpeed;
                movDir = transform.TransformDirection(movDir);

            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetFloat("speedPercent", 0);
                movDir = new Vector3(0, 0, 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * 80 * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        movDir.y -= gravity * Time.deltaTime;
        controller.Move(movDir * Time.deltaTime);

#endif



    }








    public void Attack()
    {

        if (pv.IsMine&& AnimPlaying == false)
        {
            AnimPlaying = true;
            animator.SetTrigger("attack");
          
            //collider.GetComponent<Collider>().enabled = true;
            coroutine = WaitAndPrint(1f);
            StartCoroutine(coroutine);

            AudioPlayer.Inistance.OnAttack();

        }
        swords[SwordIndex].GetComponent<Collider>().enabled = true;

    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // print("Coroutine ended: " + Time.time + " seconds");
        AnimPlaying = false;
        swords[SwordIndex].GetComponent<Collider>().enabled = false;
    }


    public void EnableCol()
    {
       // print("Coroutine ended: " + Time.time + " seconds");
        swords[SwordIndex].GetComponent<Collider>().enabled = true;
        corCol = WaitAnd(1f);
       
        StartCoroutine(corCol);

    }
    public void DisableCol()
    {
        swords[SwordIndex].GetComponent<Collider>().enabled = false;

    }
    private IEnumerator WaitAnd(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       
        swords[SwordIndex].GetComponent<Collider>().enabled = false;
    }


    










}