using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPickup : MonoBehaviour
{
    public Player playerScript;
    public int pillAmount;
    public GameObject effect;
    public float timeBetweenPickups;
    private float pickupTime;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerScript != null)
        {
            if (collision.gameObject.tag == "Player" && Time.time >= pickupTime)
            {
                Instantiate(effect, transform.position, transform.rotation);
                //playerScript.Heal(healAmount);                
                Destroy(gameObject);
                DataHolder.currentPills += pillAmount ;
                pickupTime = Time.time + timeBetweenPickups;
            }
        }
    }
}
