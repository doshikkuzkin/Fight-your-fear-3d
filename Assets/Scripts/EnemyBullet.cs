using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject explosion;
    private Player playerScript;
    private Vector3 targetPos;
    public float speed;
    public int damage;

    float damageTime;
    float timeBetweenDamage = 1;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPos = new Vector3(playerScript.transform.position.x, transform.position.y, playerScript.transform.position.z);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= damageTime)
        {
            playerScript.TakeDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            damageTime = Time.time + timeBetweenDamage;
        }
    }
}
