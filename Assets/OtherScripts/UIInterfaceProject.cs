using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterfaceProject : MonoBehaviour {

    [SerializeField]
    GameObject noBulletsPanel; 

    public void NoBullets()
    {
        StartCoroutine(PanelBriefShow()); 
    }

    IEnumerator PanelBriefShow()
    {
        noBulletsPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        noBulletsPanel.SetActive(false);
    }
}
