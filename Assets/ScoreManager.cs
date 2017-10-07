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

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
        UpdateBestScoreUI(); 
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI(); 
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
