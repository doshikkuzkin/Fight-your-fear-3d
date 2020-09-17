using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Animator animator;
    public GameObject appearEffect;
    public GameObject deathEffect;
    public int healthPickupChance;
    public GameObject healthPickup;
    public int pillsPickupChance;
    public GameObject pillsPickup;
    public float pickupOffset;
    public int guardPickupChance;
    public GameObject guardPickup;
    public GameObject plant;
    public int plantInstantiateChance;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        Instantiate(appearEffect, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("damage" + damageAmount);
        if (health <= 0)
        {
            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                Instantiate(healthPickup, new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.identity);
            }
            int randPills = Random.Range(0, 101);
            if (randPills < pillsPickupChance)
            {
                Instantiate(pillsPickup, new Vector3(transform.position.x - pickupOffset, -0.5f, transform.position.z - pickupOffset), transform.rotation);
            }
            int randGuard = Random.Range(0, 101);
            if (randGuard < guardPickupChance)
            {
                Instantiate(guardPickup, new Vector3(transform.position.x + pickupOffset, -0.5f, transform.position.z + pickupOffset), transform.rotation);
            }
            Instantiate(deathEffect, transform.position, transform.rotation);
            int randPlant = Random.Range(0, 101);
            if (randPlant < plantInstantiateChance)
            {
                Instantiate(plant, new Vector3(transform.position.x, -1.0f, transform.position.z), transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }
}
