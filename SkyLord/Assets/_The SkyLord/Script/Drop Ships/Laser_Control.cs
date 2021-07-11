using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Control : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Laser_Active") == true)
        {
            animator.SetTrigger("Fire");
            animator.transform.GetComponentInChildren<Laser_Fire>().m_laserBeam.enabled = true;
            animator.transform.GetComponentInChildren<Laser_Fire>().m_canFire = true;
        }

        if (animator.GetBool("Laser_Active") == false)
        {
            animator.SetTrigger("Chase");
            animator.transform.GetComponentInChildren<Laser_Fire>().m_laserBeam.enabled = false;
            animator.transform.GetComponentInChildren<Laser_Fire>().m_canFire = true;
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("Laser_Active") == true)
        {
            animator.ResetTrigger("Fire");
        }

        if (animator.GetBool("Laser_Active") == false)
        {
            animator.ResetTrigger("Chase");
        }
    }
}
