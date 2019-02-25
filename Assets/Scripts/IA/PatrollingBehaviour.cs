using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBehaviour : StateMachineBehaviour
{   
    private PatrolSpots patrol;

    private Agent agent;

    private int randomSpot;

    private Vector3 enemyVelocity;

    private Transform playerPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
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

        //Vector3 dirPrueba = playerPos.position - animator.transform.position;
        //float pos = dirPrueba.magnitude;

        //Debug.Log("Player Pos: " + playerPos.position);
        //Debug.Log("Enemy Pos: " + animator.transform.position);
        //Debug.Log("A mano: " + pos);
        //Debug.Log("Funcion: " + Vector3.Distance(animator.transform.position, playerPos.position));
        if(Vector3.Distance(animator.transform.position, playerPos.position) < 5f) {
            
            int randomMove = Random.Range(0, 2);

            switch(randomMove) {
                case 0:
                    animator.SetBool("IsFollowing", true);
                    animator.SetBool("IsPatrolling", false);
                    break;
                case 1:
                    animator.SetTrigger("FastJump");
                    break;
            }
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
