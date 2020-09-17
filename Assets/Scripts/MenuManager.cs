using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    SceneTransitions sceneTransitions;
    public TextMeshProUGUI recordUI;
    //int level;
    int record;

    void Start()
    {
        sceneTransitions = GameObject.Find("TransitionPanel").GetComponent<SceneTransitions>();
        //level = PlayerPrefs.GetInt("Level", 2);
        PlayerPrefs.SetString("GunBought", "false");
        PlayerPrefs.SetString("BombBought", "false");
        PlayerPrefs.SetInt("Pills", 0);
        PlayerPrefs.SetInt("LevelCount", 0);
        PlayerPrefs.SetInt("EnemyCount", 5);
        PlayerPrefs.SetInt("BossHealth", 20);
        record = PlayerPrefs.GetInt("Record", 0);
        recordUI.text = "Record: " + record;
    }

    public void Play()
    {
        //sceneTransitions.LoadScene(level);
        sceneTransitions.LoadScene(2);
    }
}
