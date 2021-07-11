using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShip_Chase : StateMachineBehaviour
{
    [SerializeField] internal float m_fireRange = 100f, m_backOffDistance = 10f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<DropShip_Movement>().TurnTowardsPlayer();
        animator.SetBool("Laser_Active", false);

        if (animator.GetComponent<DropShip_Movement>().m_distanceToTarget > m_fireRange)
        {
            animator.SetBool("Laser_Active", false);
            animator.GetComponent<DropShip_Movement>().MoveTowardsPlayer();
        }

        if (animator.GetComponent<DropShip_Movement>().m_distanceToTarget < m_backOffDistance)
        {
            animator.SetTrigger("BackOff");
        }

        if (animator.GetComponent<DropShip_Movement>().m_distanceToTarget <= m_fireRange && animator.GetComponent<DropShip_Movement>().m_distanceToTarget >= m_backOffDistance)
        {
            animator.SetBool("Laser_Active", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BackOff");
    }
}
