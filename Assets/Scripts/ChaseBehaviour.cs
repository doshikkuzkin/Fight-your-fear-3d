using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    private GameObject player;
    public float speed;
    private float attackTime;
    public float timeBetweenAttacks;
    public float stopDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, new Vector3(player.transform.position.x, animator.transform.position.y, player.transform.position.z));
        if (distance >= stopDistance)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(player.transform.position.x, animator.transform.position.y, player.transform.position.z), speed * Time.deltaTime);
            Vector3 difference = player.transform.position - animator.transform.position;
            float rotY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(rotY, Vector3.up);
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotation, Time.deltaTime * 100);
        }
        else
        {
            if (Time.time >= attackTime)
            {
                animator.SetBool("attack", true);
                attackTime = Time.time + timeBetweenAttacks;
            }
        }
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
