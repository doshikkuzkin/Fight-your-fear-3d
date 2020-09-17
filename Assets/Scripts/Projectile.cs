using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    public GameObject sound;
    int damage;

    void Start()
    {
        damage = DataHolder.damage;
        Instantiate(sound, transform.position, transform.rotation);
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            DestroyProjectile();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Boss")
    //    {
    //        collision.gameObject.GetComponent<Boss>().TakeDamage(damage);
    //        DestroyProjectile();
    //    }
    //}
}
