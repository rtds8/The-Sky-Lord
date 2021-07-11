using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip_Disappear : StateMachineBehaviour
{
    [SerializeField] private float m_disappearTime = 10f;
    private float m_elapsedTime, m_spawnTime;
    internal int m_dropShipsSpawned = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_elapsedTime = 0f;
        m_spawnTime = 6f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(m_elapsedTime > m_spawnTime)
        {
            m_spawnTime += m_spawnTime;
            GameObject dropShip = animator.GetComponent<MotherShip_Manager>().GetDropShip() as GameObject;
            dropShip.SetActive(true);
            m_dropShipsSpawned += 1;
        }

        if(m_elapsedTime >= m_disappearTime)
        {
            animator.SetTrigger("Disappear");
        }

        else
        {
            m_elapsedTime += Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Disappear");
    }
}
