using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : Enemy
{
    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    RaycastHit raycastHit;

    bool avoidBomb = false;

    public virtual void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (DataHolder.player)
        {
            Bomb bomb = FindObjectOfType<Bomb>();
            
            if (bomb != null && DataHolder.bombBlowing)
            {
                if (Vector3.Distance(transform.position, bomb.transform.position) <= bomb.blowRadius && !avoidBomb)
                {
                    avoidBomb = true;                    
                }
                if (avoidBomb)
                {                    
                    Vector3 difference = transform.position - bomb.transform.position;
                    float rotY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(rotY + 90, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
                    transform.position = Vector3.MoveTowards(transform.position, difference * 3, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, bomb.transform.position) >= bomb.blowRadius + 2) avoidBomb = false;
                    return;
                }
            }
            if (Vector3.Distance(transform.position, player.transform.position) > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), speed * Time.deltaTime);
                Vector3 difference = player.transform.position - transform.position;
                float rotY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(rotY + 90, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);

            }
            else
            {
                if (Time.time >= attackTime)
                {
                    animator.SetBool("attack", true);
                    player.GetComponent<Player>().TakeDamage(damage);
                    Debug.Log("Damage");
                    attackTime = Time.time + timeBetweenAttacks;
                }
                else
                {
                    animator.SetBool("attack", false);
                }
            }

            
            

            //Vector3 avoidAngle = (player.transform.position - transform.position).normalized;
            //if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 0.5f))
            //{
            //    Debug.DrawLine(transform.position, raycastHit.point, Color.red);
            //    avoidAngle += raycastHit.normal * 2;
            //}
            //else
            //{
            //    Debug.DrawLine(transform.position, transform.forward * 0.5f, Color.yellow);
            //}
        }
        else
        {
            Destroy(this.gameObject);
            Instantiate(appearEffect, transform.position, Quaternion.identity);
        }
    }

}
