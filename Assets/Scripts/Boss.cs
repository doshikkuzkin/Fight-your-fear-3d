using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;
    private GameObject player;
    public int damage;
    private int health;
    public int maxHealth;
    public GameObject deathEffect;
    public GameObject instantiateEffect;

    public Enemy[] enemies;
    public int enemyInstantiateChance;
    GameObject[] enemySpawnPoints;

    private Slider healthBar;

    void Start()
    {
        maxHealth = DataHolder.bossHealth;
        health = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        healthBar = GameObject.FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    void Update()
    {
        if (health <= maxHealth / 2)
        {
            animator.SetTrigger("nextStage");
        }
    }

    public void Attack()
    {
        animator.SetBool("attack", false);
        player.GetComponent<Player>().TakeDamage(damage);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!DataHolder.bossDead)
        {
            health -= damageAmount;
            healthBar.value = health;
            int randomChance = Random.Range(0, 101);
            if (randomChance <= enemyInstantiateChance)
            {
                Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
                Transform randomSpot = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].transform;
                Instantiate(randomEnemy, randomSpot.position, player.transform.rotation);
                Instantiate(instantiateEffect, transform.position, Quaternion.identity);
                Instantiate(instantiateEffect, randomSpot.position, Quaternion.identity);
            }
            if (health <= 0)
            {
                animator.SetBool("isDead", true);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                DataHolder.bossDead = true;
                healthBar.gameObject.SetActive(false);
            }
        }
    }
}
