using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeUI : MonoBehaviour
{
    WaveSpawner waveSpawner;
    Player player;
    Weapon weapon;
    public GameObject[] leftButtons;
    public GameObject[] rightButtons;
    Animator animator;

    void Start()
    {
        waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void onShow()
    {
        Time.timeScale = 0.0f;        
        foreach (GameObject button in leftButtons){
            button.SetActive(false);
        }
        foreach (GameObject button in rightButtons)
        {
            button.SetActive(false);
        }
        GameObject leftButton = leftButtons[Random.Range(0, leftButtons.Length)];
        GameObject rightButton = rightButtons[Random.Range(0, rightButtons.Length)];
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        Debug.Log("Appear");
    }

    public void DamageVsSpeed()
    {
        if (DataHolder.damage < DataHolder.maxDamage) DataHolder.damage++;
        float playerSpeed = player.speed;
        if (playerSpeed > DataHolder.minSpeed)
        {
            playerSpeed -= 1.0f;
            player.speed = playerSpeed;
        }
        Close();
    }

    public void SpeedVsRate()
    {
        if (player.speed < DataHolder.maxSpeed) player.speed++;
        if (weapon.timeBetweenShots + DataHolder.upRate <= DataHolder.maxRate)
        {
            weapon.timeBetweenShots += DataHolder.upRate;
        }
        else
        {
            weapon.timeBetweenShots = DataHolder.maxRate;
        }
        Close();
    }

    public void SpeedVsLife()
    {
        if (player.speed < DataHolder.maxSpeed) player.speed++;
        if (player.health > 1)
        {
            player.health = player.health / 2;
            player.UpdateHealthUI(player.health);
        }
        Close();
    }

    public void RateVsLife()
    {
        if (weapon.timeBetweenShots - DataHolder.upRate >= DataHolder.minRate)
        {
            weapon.timeBetweenShots -= DataHolder.upRate;
        }
        else
        {
            weapon.timeBetweenShots = DataHolder.minRate;
        }
        player.health = 1;
        player.UpdateHealthUI(player.health);
        Close();
    }

    public void LifeVsSpeed()
    {
        player.health = player.maxHealth;
        player.UpdateHealthUI(player.health);
        float playerSpeed = player.speed;
        if (playerSpeed > DataHolder.minSpeed)
        {
            playerSpeed -= 1.0f;
            player.speed = playerSpeed;
        }
        Close();
    }

    void Close()
    {
        animator.SetTrigger("Disappear");
        Debug.Log("Close");
    }

    public void finishAnimation()
    {
        DataHolder.MenuShown = false;
        waveSpawner.hideSacrifice();
        Time.timeScale = 1.0f;
    } 
    
}
