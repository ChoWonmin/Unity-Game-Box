using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 점수, 스테이지 관리
    public int stagePoint;
    public int totalPoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    // UI
    public Image[] healthUI;
    public Text stageUI;
    public Text pointUI;
    public GameObject restartButton;


    public void NextStage()
    {
        if (stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);

            player.Reposition();

            stageUI.text = "STAGE " + (stageIndex + 1);
        } else
        {
            // Game Clear && Controll Lock
            Time.timeScale = 0;
            restartButton.SetActive(true);
            Text btnText = restartButton.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            restartButton.SetActive(true);
        }

        totalPoint += stagePoint;
        stagePoint = 0;

    }

    private void Update()
    {
        pointUI.text = (totalPoint + stagePoint).ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthDown();
            if (health > 0)
            {
                player.Reposition();
            } else if (health <= 0)
            {
                restartButton.SetActive(true);
            }

            
        }
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            healthUI[health].color = new Color(1, 0, 0, 0.4f);
        } else
        {
            player.onDie();
            restartButton.SetActive(true);
            healthUI[0].color = new Color(1, 0, 0, 0.4f);
        }
        
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
