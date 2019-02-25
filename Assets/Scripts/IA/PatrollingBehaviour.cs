using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBehaviour : StateMachineBehaviour
{   
    private PatrolSpots patrol;

    private Agent agent;

    private int randomSpot;

    private Vector3 enemyVelocity;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.FindGameObjectWithTag("PatrolSpots").GetComponent<PatrolSpots>();
        agent = animator.GetComponent<Agent>();
        randomSpot = Random.Range(0, patrol.moveSpots.Length);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector3.Distance(animator.transform.position, patrol.moveSpots[randomSpot].position) > 0.2f) {
            if(agent != null) {
                enemyVelocity = patrol.moveSpots[randomSpot].position - animator.transform.position;
                enemyVelocity.Normalize();
                enemyVelocity *= agent.MaxSpeed;

                agent.UpdateAgent(enemyVelocity);
            }
        } else {
            randomSpot = Random.Range(0, patrol.moveSpots.Length);
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("IsPatrolling", false);
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
