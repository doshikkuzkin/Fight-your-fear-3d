using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Player playerScript;
    public int healAmount;
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
                playerScript.Heal(healAmount);
                Destroy(gameObject);
                pickupTime = Time.time + timeBetweenPickups;
            }
        }
    }
}
