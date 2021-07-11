using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip_Relocate : StateMachineBehaviour
{
    private Rigidbody m_motherShipRB;
    private Vector3 m_targetPosition;
    private Quaternion m_targetRotation;
    private float m_speed;
    private float m_animationWait = 2f, m_passedTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_passedTime = 0f;
        m_speed = animator.GetComponent<MotherShip_Manager>().m_motherShipSpeed;
        m_motherShipRB = animator.GetComponent<Rigidbody>();
        m_targetPosition = animator.GetComponent<MotherShip_Manager>().GetNextLocation();
        m_targetRotation = animator.GetComponent<MotherShip_Manager>().GetNextOrientation();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(m_passedTime >= m_animationWait)
        {
            Vector3 newPos = Vector3.MoveTowards(m_motherShipRB.position, m_targetPosition, m_speed * Time.fixedDeltaTime);
            m_motherShipRB.MovePosition(newPos);
        }

        else
        {
            m_passedTime += Time.fixedDeltaTime;
        }

        if(Vector3.Distance(m_targetPosition, m_motherShipRB.position) <= 1)
        {
            m_motherShipRB.gameObject.transform.rotation = m_targetRotation;
            animator.SetTrigger("Reappear");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Reappear");
    }
}
