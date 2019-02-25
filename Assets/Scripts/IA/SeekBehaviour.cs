using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class SeekBehaviour : StateMachineBehaviour
{   
    private Transform playerPos;
    private Agent agent;

    private Vector3 enemyVelocity;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<Agent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent != null) {
            enemyVelocity = playerPos.position - animator.transform.position;
		    enemyVelocity.Normalize();
		    enemyVelocity *= agent.MaxSpeed;

            agent.UpdateAgent(enemyVelocity);
        }

        if(Vector3.Distance(animator.transform.position, playerPos.position) > 3f) {
            animator.SetBool("IsFollowing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
