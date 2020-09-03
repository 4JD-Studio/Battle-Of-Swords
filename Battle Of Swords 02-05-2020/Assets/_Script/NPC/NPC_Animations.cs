using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Animations : MonoBehaviour
{
    private NPC_Master npcMaster;
    private Animator myAnimator;

    private void OnEnable()
    {
        SetInitialRefrence();
        npcMaster.EventNPCAttackAnim += ActivateAttackAnimation;
        npcMaster.EventNPCWalkingAnim += ActivateWalkingAnimation;
        npcMaster.EventNPCIdleAnim += ActivateIdleAnimation;
        npcMaster.EventNPCRecoverdAnim += ActivateRecoverdAnimation;
        npcMaster.EventNPCStruckAnim += ActivateStruckAnimation;
    }
    private void OnDisable()
    {
        npcMaster.EventNPCAttackAnim -= ActivateAttackAnimation;
        npcMaster.EventNPCWalkingAnim -= ActivateWalkingAnimation;
        npcMaster.EventNPCIdleAnim -= ActivateIdleAnimation;
        npcMaster.EventNPCRecoverdAnim -= ActivateRecoverdAnimation;
        npcMaster.EventNPCStruckAnim -= ActivateStruckAnimation;
    }
    void SetInitialRefrence()
    {
        npcMaster = GetComponent<NPC_Master>();
        if (GetComponent<Animator>()!=null)
        {
            myAnimator = GetComponent<Animator>();
        }
    }
    void ActivateWalkingAnimation()
    {
        if (myAnimator!=null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool(npcMaster.animationBoolPursuing, true);
            }
        }
    }
    void ActivateIdleAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool(npcMaster.animationBoolPursuing, false);
            }
        }
    }

    void ActivateAttackAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger(npcMaster.animationTriggerMelee);
            }
        }
    }
    void ActivateRecoverdAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger(npcMaster.animationTriggerRecovered);
            }
        }
    }

    void ActivateStruckAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger(npcMaster.animationTriggerStruck);
            }
        }
    }

}
