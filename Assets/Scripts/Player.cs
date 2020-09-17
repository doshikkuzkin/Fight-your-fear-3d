using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float speed;
    public int guard;
    public int maxGuard;
    bool guardActive;
    public GameObject guardEffect;
    private Vector3 moveAmount;
    public Transform playergraphic;
    private Animator anim;
    public GameObject hurtEffect;
    public GameObject guardHurtEffect;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject bomb;

    float timeBeforeBlow;
    float blowTime;

    void Start()
    {
        timeBeforeBlow = 1f;

        health = maxHealth;
        guardActive = false;
        guardEffect.SetActive(false);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DataHolder.player && !DataHolder.MenuShown)
        {
            float horMovement = Input.GetAxisRaw("Horizontal");
            float vertMovement = Input.GetAxisRaw("Vertical");
            transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
            transform.Translate(transform.forward * vertMovement * Time.deltaTime * speed);
            Vector3 moveDirection = new Vector3(vertMovement, 0, -horMovement);
            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("isRunning", true);
                Quaternion newRotation = Quaternion.LookRotation(moveDirection);
                playergraphic.transform.rotation = Quaternion.Slerp(playergraphic.transform.rotation, newRotation, Time.deltaTime * 20);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            if (PlayerPrefs.GetString("BombBought", "false") != "true")
            {
                if (Input.GetMouseButton(1) && DataHolder.bombready)
                {
                    Instantiate(bomb, new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.Euler(0, 180, 0));
                    DataHolder.bombready = false;
                }
            }
            else
            {
                if (Input.GetMouseButton(1) && Time.time > blowTime && !DataHolder.bombWaiting)
                {
                    Instantiate(bomb, new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.Euler(0, 180, 0));
                    DataHolder.bombready = false;
                    DataHolder.bombWaiting = true;
                    blowTime = Time.time + timeBeforeBlow;
                }
                if (DataHolder.bombWaiting && Input.GetMouseButton(1) && Time.time > blowTime)
                {
                    Animator blow = GameObject.FindGameObjectWithTag("Bomb").GetComponent<Animator>();
                    DataHolder.bombBlowing = true;
                    blow.SetTrigger("blow");
                    DataHolder.bombWaiting = false;
                    blowTime = Time.time + timeBeforeBlow;
                }
            }
            
        }
    }

    public void TakeDamage(int damageAmount)
    {
        int damage = 0;
        if (guardActive)
        {
            damage = guard - damageAmount;
            if (damage <= 0)
            {
                guard = 0;
                guardEffect.SetActive(false);
                guardActive = false;
                damage = Mathf.Abs(damage);
                Instantiate(guardHurtEffect, new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.identity);
            }
            else
            {
                guard = damage;
                damage = 0;
            }            
        }
        else {
            damage = damageAmount;            
        }
        if (damage > 0) Instantiate(hurtEffect, new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.identity);
        health -= damage;        
        UpdateHealthUI(health);
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            DataHolder.player = false;
        }
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else hearts[i].sprite = emptyHeart;
        }
    }

    public void Heal(int healAmount)
    {
        if (health + healAmount >maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }

    public void Guard(int guardAmount)
    {
        if (guard + guardAmount > maxGuard)
            guard = maxGuard;
        else
        guard += guardAmount;
        if (!guardActive)
        {
            guardEffect.SetActive(true);
            guardActive = true;
        }
    }
}
