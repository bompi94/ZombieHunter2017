using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    [SerializeField]
    GameObject panel; 

    [SerializeField]
    Text text;

    [SerializeField]
    string[] words;

    [SerializeField]
    float timeBetweenWords;

    int wordIndex = 0; 

    public void ShowDialog()
    {
        StartCoroutine(OneWordAtATime());
    }

    IEnumerator OneWordAtATime()
    {
        yield return new WaitForSeconds(1); 
        panel.SetActive(true); 
        while(wordIndex<words.Length)
        {
            text.text = words[wordIndex];
            wordIndex++;
            yield return new WaitForSeconds(timeBetweenWords);
        }
        panel.SetActive(false);
        wordIndex = 0; 
    }
}
