using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if(CheckEndLevel())
        {
            EndLevel(); 
        }
    }

    bool CheckEndLevel()
    {
        return false; 
    }

    public void EndLevel()
    {

    }

    public void StartLevel()
    {

    }
}
