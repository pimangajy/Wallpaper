using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Anime : MonoBehaviour
{
    public Animator animator;

    public void HeadIdle_On()
    {
        animator.SetBool("Head", true);
    }
    public void HeadIdle_Off()
    {
        animator.SetBool("Head", false);
    }

    public void HairIdle()
    {
        animator.SetTrigger("Hair");
    }

    public void ArmIdle()
    {
        animator.SetTrigger("Arm");
    }

    public void LegIdle()
    {
        animator.SetTrigger("Leg");
    }

}
