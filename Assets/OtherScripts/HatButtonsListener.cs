using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatButtonsListener : MonoBehaviour
{

    HatManager hatManager;

    [SerializeField]
    public GameObject[] buttons;

    const string hatNumberKey = "HatNumber";


    private void Awake()
    {
        hatManager = FindObjectOfType<HatManager>();
        SelectButtonsToShow();
    }

    void SelectButtonsToShow()
    {
        int n = PlayerPrefs.GetInt(hatNumberKey);

        for (int i = 0; i <= n; i++)
        {
            buttons[i].SetActive(true);
            if(hatManager.GetSelectedHat() == i)
            {
                buttons[i].GetComponent<Image>().color = Color.yellow; 
            }
        }

    }
    public void SelectHat(int hatType)
    {
        foreach (var g in buttons)
        {
            g.GetComponent<Image>().color = Color.white; 
        }

        hatManager.SelectHat((HatType)hatType);
        buttons[hatManager.GetSelectedHat()].GetComponent<Image>().color = Color.yellow;
    }
}




