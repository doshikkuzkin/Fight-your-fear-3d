using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPickup : MonoBehaviour
{
    public Player playerScript;
    public int guardAmount;
    public GameObject effect;
    public float timeBetweenPickups;
    private float pickupTime;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerScript != null)
        {
            if (collision.gameObject.tag == "Player" && Time.time >= pickupTime)
            {
                Instantiate(effect, transform.position, transform.rotation);
                playerScript.Guard(guardAmount);
                Destroy(gameObject);
                pickupTime = Time.time + timeBetweenPickups;
            }
        }
    }
}
