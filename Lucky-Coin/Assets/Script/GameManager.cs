using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text stageText;
    public Text scoreText;
    public Image[] healthImageList;
    public GameObject restartButton;

    int stageScore = 0;
    int stage = 1;
    int totalScore = 0;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deactiveHealth(int index)
    {
        healthImageList[index].color = new Vector4(1,1,1, 0.4f);
    }

    public void addScore(int score)
    {
        stageScore += score;
        scoreText.text = (totalScore + stageScore).ToString();
    }

    public void openRestart()
    {
        restartButton.SetActive(true);
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
