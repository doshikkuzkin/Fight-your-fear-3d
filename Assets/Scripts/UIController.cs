using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    Player playerScript;
    public SceneTransitions transitionsScript;
    public TextMeshProUGUI pillsText;
    public TextMeshProUGUI guardText;
    public TextMeshProUGUI levelText;
    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject waitUI;
    public GameObject bombIcon;
    public Animator bombAnimator;
    public float timeBetweenBombs;
    private float bombTime;
    public float waitTime;

    void Start()
    {
        //transitionsScript = GameObject.Find("TransitionPanel").GetComponent<SceneTransitions>();
        bombAnimator = bombIcon.GetComponent<Animator>();
        if (PlayerPrefs.GetString("BombBought", "false") != "true")
            bombIcon.SetActive(true);
        else
            bombIcon.SetActive(false);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        levelText.text = "Level " + (PlayerPrefs.GetInt("LevelCount", 0) + 1).ToString();
    }

    private void Update()
    {
        pillsText.text = DataHolder.currentPills.ToString();
        int guard = playerScript.guard;
        if (guard == playerScript.maxGuard)
            guardText.text = "max";
        else
        guardText.text = playerScript.guard.ToString();
        if (!DataHolder.player)
        {
            pauseUI.SetActive(true);
            DataHolder.MenuShown = true;
        }
        if (Time.time >= bombTime && !bombAnimator.GetBool("ready"))
        {
            DataHolder.bombready = true;
            bombAnimator.SetBool("ready",true);
        }
        if (!DataHolder.bombready)
        {
            if (bombAnimator.GetBool("ready"))
            {
                bombTime = Time.time + timeBetweenBombs;
                bombAnimator.SetBool("ready", false);
            }
        }
        if (DataHolder.bossDead && !DataHolder.waitShown)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                int newCount = PlayerPrefs.GetInt("LevelCount", 0) + 1;
                PlayerPrefs.SetInt("LevelCount", newCount);
                if (newCount > PlayerPrefs.GetInt("Record", 0))
                {
                    PlayerPrefs.SetInt("Record", newCount);
                }
                PlayerPrefs.SetInt("EnemyCount", (PlayerPrefs.GetInt("EnemyCount") + 1));
                DataHolder.waitShown = true;
                bombIcon.SetActive(false);
                StartCoroutine(WaitForMenu());
            }
        }
    }

    public void RestartLevel()
    {
        //transitionsScript.LoadScene(DataHolder.currentScene);        
        PlayerPrefs.SetString("GunBought", "false");
        PlayerPrefs.SetString("BombBought", "false");
        PlayerPrefs.SetInt("Pills", 0);
        PlayerPrefs.SetInt("LevelCount", 0);
        PlayerPrefs.SetInt("EnemyCount", 5);
        PlayerPrefs.SetInt("BossHealth", 20);
        transitionsScript.LoadScene(2);
    }

    public void MainMenu()
    {
        int newCount = PlayerPrefs.GetInt("LevelCount", 0);
        if (newCount > PlayerPrefs.GetInt("Record", 0))
        {
            PlayerPrefs.SetInt("Record", newCount);
        }
        transitionsScript.LoadScene(0);
    }

    public void NextLevel()
    {
        //if (DataHolder.currentScene == 3)
        //{
        //    transitionsScript.LoadScene(0);
        //}
        //else
        //{
        //    transitionsScript.LoadScene(++DataHolder.currentScene);
        //}
        int newPills = PlayerPrefs.GetInt("Pills",0) + DataHolder.currentPills;
        PlayerPrefs.SetInt("Pills", newPills);
        //int newCount = PlayerPrefs.GetInt("LevelCount", 0) + 1;
        //PlayerPrefs.SetInt("LevelCount", newCount);
        //if (newCount > PlayerPrefs.GetInt("Record", 0))
        //{
        //    PlayerPrefs.SetInt("Record", newCount);
        //}
        //if (DataHolder.currentScene == 3) 
        //{
        //    transitionsScript.LoadScene(1);
        //}
        //else
        //{
        if (!DataHolder.ImprovesBought)
            transitionsScript.LoadScene(1);
        else
        {
            int level = PlayerPrefs.GetInt("Level", 2);
            if (level == 4)
            {
                PlayerPrefs.SetInt("EnemyCount", (PlayerPrefs.GetInt("EnemyCount", 5)));
                transitionsScript.LoadScene(2);
            }
            else
                transitionsScript.LoadScene(++level);
        }
        //}
    }

    IEnumerator WaitForMenu()
    {
        waitUI.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        DataHolder.MenuShown = true;
        waitUI.SetActive(false);
        winUI.SetActive(true);
    }
}
