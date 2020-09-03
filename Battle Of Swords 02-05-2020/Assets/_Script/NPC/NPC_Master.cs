using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Master : MonoBehaviour
{
    public delegate void GeneralEventHandele();
    public event GeneralEventHandele EventNPCDie;
    public event GeneralEventHandele EventNPCLowHealth;
    public event GeneralEventHandele EventNPCHealthRecoverd;
    public event GeneralEventHandele EventNPCWalkingAnim;
    public event GeneralEventHandele EventNPCStruckAnim;
    public event GeneralEventHandele EventNPCAttackAnim;
    public event GeneralEventHandele EventNPCRecoverdAnim;
    public event GeneralEventHandele EventNPCIdleAnim;

    public delegate void HealthEventHandeler(int health);
    public event HealthEventHandeler EventNPCDedectHealth;
    public event HealthEventHandeler EventNPCIncreaseHealth;

    public string animationBoolPursuing = "isPursuing";
    public string animationTriggerStruck = "Struck";
    public string animationTriggerMelee = "Attack";
    public string animationTriggerRecovered = "Recovered";

    public void CallEventNPCDie()
    {
        if (EventNPCDie != null)
        {
            EventNPCDie();
        }
    }
    public void CallEventNPCLowHealth()
    {
        if (EventNPCLowHealth != null)
        {
            EventNPCLowHealth();
        }
    }
    public void CallEventNPCHealthRecoverd()
    {
        if (EventNPCHealthRecoverd != null)
        {
            EventNPCHealthRecoverd();
        }
    }
    public void CallEventNPCWalkingAnim()
    {
        if (EventNPCWalkingAnim != null)
        {
            EventNPCWalkingAnim();
        }
    }

    public void CallEventNPCStruckAnim()
    {
        if (EventNPCStruckAnim != null)
        {
            EventNPCStruckAnim();
        }
    }

    public void CallEventNPCAttackAnim()
    {
        if (EventNPCAttackAnim != null)
        {
            EventNPCAttackAnim();
        }
    }

    public void CallEventNPCRecoverdAnim()
    {
        if (EventNPCRecoverdAnim != null)
        {
            EventNPCRecoverdAnim();
        }
    }

    public void CallEventNPCIdleAnim()
    {
        if (EventNPCIdleAnim != null)
        {
            EventNPCIdleAnim();
        }
    }



    public void CallEventNPCDedectHealth(int health)
    {
        if (EventNPCDedectHealth != null)
        {
            EventNPCDedectHealth(health);
        }
    }
    public void CallEventNPCIncreaseHealth(int health)
    {
        if (EventNPCIncreaseHealth != null)
        {
            EventNPCIncreaseHealth(health);
        }
    }


}

