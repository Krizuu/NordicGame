using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightBehaviour : StateMachineBehaviour
{
    private Transform playerPos;
    private Rigidbody2D body;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isWalking", false);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        body = animator.GetComponent<Rigidbody2D>();
        Vector2 attackDirection = getDirection(animator.transform.position, playerPos.position);
        body.velocity = new Vector2(0, 0);
        body.AddForce(-attackDirection * 2, ForceMode2D.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isFighting", false);
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

    Vector2 getDirection(Vector2 a, Vector2 b)
    {
        return (b - a).normalized;
    }
}
