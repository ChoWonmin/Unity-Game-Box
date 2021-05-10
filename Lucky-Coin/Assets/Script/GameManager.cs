using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text stageText;
    
    public Text btcScoreText;
    public Text ethScoreText;
    public Text xrpScoreText;

    public Image[] healthImageList;
    public GameObject restartButton;

    public GameObject startInfo;
    public GameObject finishInfo;

    int btcScore = 0;
    int ethScore = 0;
    int xrpScore = 0;

    int btcTotalScore = 0;
    int ethTotalScore = 0;
    int xrpTotalScore = 0;

    int stage = 1;


    public void deactiveHealth(int index)
    {
        healthImageList[index].color = new Vector4(1,1,1, 0.4f);
    }

    public void addScore(Coin coin)
    {
        if (coin.name == "Btc")
        {
            btcScore += coin.score;
            btcScoreText.text = (btcTotalScore + btcScore).ToString();
        }

        if (coin.name == "Eth")
        {
            ethScore += coin.score;
            ethScoreText.text = (ethTotalScore + ethScore).ToString();
        }

        if (coin.name == "Xrp")
        {
            xrpScore += coin.score;
            xrpScoreText.text = (xrpTotalScore + xrpScore).ToString();
        }
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

    public void finish()
    {
        finishInfo.SetActive(true);
        // Time.timeScale = 0;
        // restartButton.SetActive(true);
        // restartButton.GetComponentInChildren<Text>().text = "Finish!! :)";
    }

}
