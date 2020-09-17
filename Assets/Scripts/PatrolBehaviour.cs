using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{

    private GameObject[] patrolPoints;

    int randomPoint;
    public float speed;
    private float rotZ;
    RaycastHit raycastHit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        randomPoint = Random.Range(0, patrolPoints.Length);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Physics.Raycast(new Vector3(animator.transform.position.x, -1, animator.transform.position.z), new Vector3(patrolPoints[randomPoint].transform.position.x, -1, patrolPoints[randomPoint].transform.position.z), out raycastHit, 0.4f))
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
        else
        {
            Debug.DrawLine(new Vector3(animator.transform.position.x, -1, animator.transform.position.z), patrolPoints[randomPoint].transform.position * 0.4f, Color.yellow);
        }

        Vector3 difference = patrolPoints[randomPoint].transform.position - animator.transform.position;
        float rotY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotY, Vector3.up);
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotation, Time.deltaTime * 100);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, new Vector3(patrolPoints[randomPoint].transform.position.x, animator.transform.position.y, patrolPoints[randomPoint].transform.position.z), speed * Time.deltaTime);
        if (Vector3.Distance(animator.transform.position, new Vector3(patrolPoints[randomPoint].transform.position.x, animator.transform.position.y, patrolPoints[randomPoint].transform.position.z)) < 0.1f)
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
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
