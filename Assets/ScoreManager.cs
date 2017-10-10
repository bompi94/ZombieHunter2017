using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text bestScoreText;

    [SerializeField]
    GameObject unlockedHatPanel;

    float unlockedHatTime = 1;
    float unlockedHatTimer = 0;
    bool unlocked; 

    int score = 0;

    int bestScore;

    [SerializeField]
    bool deleter; 

    const string bestScoreKey = "BestScore";

    int n;

    private void Awake()
    {
        if (deleter)
            PlayerPrefs.DeleteAll(); 
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
        UpdateBestScoreUI();
        n = PlayerPrefs.GetInt("HatNumber");
    }

    private void Update()
    {
        if (unlocked)
        {
            unlockedHatTimer += Time.deltaTime;
            if (unlockedHatTimer >= unlockedHatTime)
            {
                unlockedHatPanel.SetActive(false);
                unlocked = false; 
            }
        }
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI();
        CheckHatUnlock(); 
    }

    void CheckHatUnlock()
    {
        if(score >= 10 && n == 0)
        {
            PlayerPrefs.SetInt("HatNumber", 1);
            n = 1;
            UnlockedHat();
        }

        else if(score >= 20 && n <= 1)
        {
            PlayerPrefs.SetInt("HatNumber", 2);
            n = 2;
            UnlockedHat();
        }
        else if (score >= 30 && n <= 2)
        {
            PlayerPrefs.SetInt("HatNumber", 3);
            n = 3;
            UnlockedHat();
        }
    }

    void UnlockedHat()
    {
        unlocked = true;
        unlockedHatPanel.SetActive(true);
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
