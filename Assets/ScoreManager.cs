using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text bestScoreText; 

    int score = 0;

    int bestScore;

    const string bestScoreKey = "BestScore";

    int n;

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
        UpdateBestScoreUI();
        n = PlayerPrefs.GetInt("HatNumber");
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI();
        CheckHatUnlock(); 
    }

    void CheckHatUnlock()
    {
        if(score >= 5 && n == 0)
        {
            PlayerPrefs.SetInt("HatNumber", 1);
            n = 1;
            UnlockedHat();
        }

        else if(score >= 10 && n <= 1)
        {
            PlayerPrefs.SetInt("HatNumber", 2);
            n = 2;
            UnlockedHat();
        }
        else if (score >= 15 && n <= 2)
        {
            PlayerPrefs.SetInt("HatNumber", 3);
            n = 3;
            UnlockedHat();
        }
    }

    void UnlockedHat()
    {
        print("unlocked hat!"); 
    }

    public void GameEnded()
    {
        if(score>bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(bestScoreKey, bestScore); 
        }
    }
    
    void UpdateScoreUI()
    {
        scoreText.text = score.ToString(); 
    }

    void UpdateBestScoreUI()
    {
        bestScoreText.text = bestScore.ToString(); 
    }
}
