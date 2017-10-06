using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

    Color originalColor;


    SpriteRenderer sr; 

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color; 
    }

    public void Highlight(Color c)
    {
        sr.color = c; 
    }

    public void DeHighLight()
    {
        sr.color = originalColor; 
    }
}
