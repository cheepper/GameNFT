using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdle : StateMachineBehaviour
{
    readonly float minTime = 5;
    readonly float maxTime = 15;

    float timer = 0;

    string[] idleTriggers = { "Proud", "Scratching", "Thinking" };

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            randomIdle(animator);
            timer = Random.Range(minTime, maxTime);
        }
        else {
            timer -= Time.deltaTime;
        }
    }

    void randomIdle(Animator animator) {
        System.Random rnd = new System.Random();
        int index = rnd.Next(idleTriggers.Length);
        string idleTrigger = idleTriggers[index];
        animator.SetTrigger(idleTrigger);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
