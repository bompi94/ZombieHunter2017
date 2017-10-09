using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HatType
{
    None, Tiger, Halloween, Batman
}

public class HatManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] hats;

    [SerializeField]
    GameObject[] buttons;

    public static HatManager Instance;

    const string hatNumberKey = "HatNumber";

    HatType selectedHat;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneChanged;
        SelectButtonsToShow();
    }


    void SelectButtonsToShow()
    {
        int n = PlayerPrefs.GetInt(hatNumberKey);

        for (int i = 0; i < n; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    public void SelectHat(int type)
    {
        selectedHat = (HatType)type;
        print(selectedHat);
    }

    void SceneChanged(Scene from, Scene to)
    {
        if (to.name == "MainScene")
        {
            print("scene changed with " + selectedHat);
            SpawnHat();
        }
    }

    void SpawnHat()
    {
        if (selectedHat != HatType.None)
        {
            GameObject go = Instantiate(hats[(int)selectedHat - 1], Vector3.zero, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Player").transform);
            go.transform.localPosition = new Vector3(0, 0, go.transform.localPosition.z);
        }

        else
        {
            print("i shoul spawn nothing");
        }
    }
}
