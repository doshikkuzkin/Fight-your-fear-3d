using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float waitTime;
    Animator animator;
    public float blowRadius;
    public GameObject explosion;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (PlayerPrefs.GetString("BombBought", "false") != "true")
        StartCoroutine(WaitForBlow());
    }

    IEnumerator WaitForBlow()
    {
        DataHolder.bombBlowing = true;
        yield return new WaitForSeconds(waitTime);
        animator.SetTrigger("blow");
    }

    public void Blow()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, new Vector3(enemies[i].transform.position.x, transform.position.y, enemies[i].transform.position.z));
            if (distance <= blowRadius)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(100);
            }            
        }
        if (boss != null)
        {
            Debug.Log("BossBlow!");
            float distance = Vector3.Distance(transform.position, new Vector3(boss.transform.position.x, transform.position.y, boss.transform.position.z));
            if (distance <= blowRadius)
            {
                Debug.Log("BossDamage!");
                boss.GetComponent<Boss>().TakeDamage(2);
            }
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        DataHolder.bombBlowing = false;
    }
}
