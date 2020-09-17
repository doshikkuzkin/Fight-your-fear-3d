using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int playerDamage;
    public float maxSpeed;
    public float minSpeed;
    public float maxRate;
    public float minRate;
    public float upRate;
    public int maxDamage;
    public int bossHealth;

    void Start()
    {
        DataHolder.player = true;
        DataHolder.bossDead = false;
        DataHolder.bombready = true;
        DataHolder.bombWaiting = false;
        DataHolder.waitShown = false;
        DataHolder.MenuShown = false;
        DataHolder.currentPills = 0;
        DataHolder.currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level", DataHolder.currentScene);
        DataHolder.maxSpeed = maxSpeed;
        DataHolder.minSpeed = minSpeed;
        DataHolder.maxRate = maxRate;
        DataHolder.minRate = minRate;
        DataHolder.maxDamage = maxDamage;
        DataHolder.upRate = upRate;
        DataHolder.bossHealth = PlayerPrefs.GetInt("BossHealth", 20);
        PlayerPrefs.SetInt("BossHealth", (DataHolder.bossHealth + 10));
        DataHolder.damage = playerDamage;
        //if (SceneManager.GetActiveScene().buildIndex == 2 && PlayerPrefs.GetInt("EnemyCount", 5) == 5)
        //{
        //    PlayerPrefs.SetInt("LevelCount", 0);
        //}
        if (PlayerPrefs.GetString("BombBought", "false") == "true" && PlayerPrefs.GetString("GunBought", "false") == "true")
        {
            DataHolder.ImprovesBought = true;
        }
        else DataHolder.ImprovesBought = false;
        Debug.Log(PlayerPrefs.GetInt("LevelCount", 0));
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            FindObjectOfType<SceneTransitions>().LoadScene(0);
        }
    }
}
