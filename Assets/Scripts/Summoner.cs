using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    Transform randomPoint;
    bool isStopped;
    public GameObject enemy;
    float spawnTime;
    bool isNewPosition;

    public override void Start()
    {
        base.Start();
        isStopped = false;
        isNewPosition = false;
        GameObject[] patrolPoints = GameObject.FindGameObjectsWithTag("SummonerPoint");
        foreach (GameObject point in patrolPoints){
            if (!point.GetComponent<SummonerPoint>().occupied)
            {
                randomPoint = point.transform;
                point.GetComponent<SummonerPoint>().Occupy();
                break;
            }          

        }
        if (randomPoint == null)
        {
            float x = Random.Range(-6, 3);
            float z = Random.Range(-5, 5);
            GameObject point = new GameObject();
            point.transform.position = new Vector3(x, -0.7f, z);
            randomPoint = point.transform;
            isNewPosition = true;
        }
    }

    void Update()
    {
        if (DataHolder.player)
        {
            if (!isStopped)
            {
                if (Vector3.Distance(transform.position, randomPoint.position) > 0.1f)
                {                    
                    transform.position = Vector3.MoveTowards(transform.position, randomPoint.position, speed * Time.deltaTime);
                }
                else
                {
                    isStopped = true;
                    animator.SetTrigger("Stop");
                }
            }
            else
            {
                if (Time.time > spawnTime)
                {
                    animator.SetBool("Summon", true);
                    spawnTime = Time.time + timeBetweenAttacks;
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
            Instantiate(appearEffect, transform.position, Quaternion.identity);
        }
    }

    public void Wait()
    {
        animator.SetTrigger("isStopped");
    }

    public void Summon()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    public void BackToWait()
    {
        animator.SetBool("Summon", false);
    }

    private void OnDestroy()
    {
        if (randomPoint.gameObject.tag == "SummonerPoint")
        randomPoint.GetComponent<SummonerPoint>().Free();
        if (isNewPosition)
        {
            Destroy(randomPoint.gameObject);
        }
    }
}
