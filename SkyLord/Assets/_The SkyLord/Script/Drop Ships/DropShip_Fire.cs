using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShip_Fire : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<DropShip_Movement>().m_distanceToTarget < animator.GetBehaviour<DropShip_Chase>().m_backOffDistance)
        {
            animator.SetBool("Laser_Active", false);
            animator.SetTrigger("BackOff");
        }

        if (animator.GetComponent<DropShip_Movement>().m_distanceToTarget > animator.GetBehaviour<DropShip_Chase>().m_fireRange)
        {
            animator.SetBool("Laser_Active", false);
        }

        if (animator.GetComponentInChildren<Laser_Fire>().InFront() && animator.GetComponentInChildren<Laser_Fire>().InLineOfSight())
            animator.GetComponentInChildren<Laser_Fire>().FireLaser();

        else
        {
            animator.SetBool("Laser_Active", false);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BackOff");
    }
}
