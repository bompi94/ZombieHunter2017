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
            buttons[i].SetActive(true);

    }
    public void SelectHat(int hatType)
    {
        hatManager.SelectHat((HatType)hatType);
    }
}




