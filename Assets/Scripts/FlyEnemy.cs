using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    public float stopDistance;
    public float shotDistance;
    private float attackTime;
    public Transform shotPoint;
    public GameObject bullet;

    public virtual void Start()
    {
        base.Start();
        transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
    }

    void Update()
    {
        if (DataHolder.player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), speed * Time.deltaTime);
                Vector3 difference = player.transform.position - transform.position;
                float rotY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(-90, rotY, 180);
            }
            if (Time.time >= attackTime && distance <= shotDistance)
            {
                attackTime = Time.time + timeBetweenAttacks;
                animator.SetTrigger("attack");
            }
        }
        else
        {
            Destroy(this.gameObject);
            Instantiate(appearEffect, transform.position, Quaternion.identity);
        }
    }

    public void Attack()
    {
        if (player != null)
        {
            Vector3 difference = player.transform.position - shotPoint.transform.position;
            float rotY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            shotPoint.transform.rotation = Quaternion.Euler(0, rotY+45, 0);
            Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
        }
    }

}
