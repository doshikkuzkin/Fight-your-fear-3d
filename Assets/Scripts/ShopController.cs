using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    public int gunCost;
    public int bombCost;

    public TextMeshProUGUI pillsText;
    public TextMeshProUGUI gunCostUI;
    public TextMeshProUGUI bombCostUI;

    public GameObject gunBuyButton;
    public GameObject bombBuyButton;

    SceneTransitions sceneTransitions;

    void Start()
    {
        sceneTransitions = GameObject.Find("TransitionPanel").GetComponent<SceneTransitions>();
        gunCostUI.text = gunCost.ToString() + " pills";
        bombCostUI.text = bombCost.ToString() + " pills";
        pillsText.text = PlayerPrefs.GetInt("Pills", 0).ToString();
        if (PlayerPrefs.GetString("GunBought", "false") == "true")
        {
            gunBuyButton.SetActive(false);
            gunCostUI.text = "Bought";
        }
        if (PlayerPrefs.GetString("BombBought", "false") == "true")
        {
            bombBuyButton.SetActive(false);
            bombCostUI.text = "Bought";
        }
    }

    void Update()
    {
        
    }

    public void gunBuy()
    {
        int newPills = (PlayerPrefs.GetInt("Pills", 0) - gunCost);
        if (newPills >= 0)
        {
            gunBuyButton.SetActive(false);
            gunCostUI.text = "Bought";
            PlayerPrefs.SetString("GunBought", "true");
            PlayerPrefs.SetInt("Pills", newPills);
            pillsText.text = newPills.ToString();
        }
    }

    public void bombBuy()
    {
        int newPills = (PlayerPrefs.GetInt("Pills", 0) - bombCost);
        if (newPills >= 0)
        {
            bombBuyButton.SetActive(false);
            bombCostUI.text = "Bought";
            PlayerPrefs.SetString("BombBought", "true");
            PlayerPrefs.SetInt("Pills", newPills);
            pillsText.text = newPills.ToString();
        }
    }

    public void Play()
    {
        int level = PlayerPrefs.GetInt("Level", 2);
        if (level == 4)
        {
            //sceneTransitions.LoadScene(1);
            PlayerPrefs.SetInt("EnemyCount", (PlayerPrefs.GetInt("EnemyCount", 5)));
            sceneTransitions.LoadScene(2);
        }
        else
        sceneTransitions.LoadScene(++level);
        
    }
}
