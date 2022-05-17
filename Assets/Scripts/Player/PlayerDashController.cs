using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashController : StateMachineBehaviour
{
    private float normalizedStart;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        normalizedStart = 0.25f - animator.gameObject.GetComponent<PlayerController>().DashRange / 4f;
        //normalizedEnd = 1f - normalizedStart;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("dash_iframes") && stateInfo.normalizedTime > normalizedStart)
            animator.SetBool("dash_iframes", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("can_dash", true);
        animator.SetBool("dash_iframes", false);
        //animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
