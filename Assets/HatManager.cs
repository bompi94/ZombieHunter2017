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

    public static HatManager Instance;

    HatType selectedHat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SceneChanged;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
    }

    void SceneChanged(Scene from, Scene to)
    {
        if (to.name == "MainScene")
        {
            print("scene changed with " + selectedHat);
            SpawnHat();
        }
    }

    public void SelectHat(HatType hatType)
    {
        selectedHat = hatType; 
    }

    void SpawnHat()
    {
        if (selectedHat != HatType.None)
        {
            GameObject go = Instantiate(hats[(int)selectedHat - 1], Vector3.zero, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Player").transform);
            go.transform.localPosition = new Vector3(0, 0, -0.1f);
            go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180)); 
        }

        else
        {
            print("i shoul spawn nothing");
        }
    }

    public GameObject GetHat(int pos)
    {
        return hats[pos-1]; 
    }

    public int GetSelectedHat()
    {
        return (int)selectedHat; 
    }
}
