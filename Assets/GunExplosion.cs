using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunExplosion : MonoBehaviour {

    [SerializeField]
    Sprite[] frames;

    int frameIndex = 0; 

    public void Play(float duration)
    {
        StartCoroutine(AnimationPlaying(duration)); 
    }

    IEnumerator AnimationPlaying(float duration)
    {
        while (frameIndex < frames.Length)
        {
            GetComponent<SpriteRenderer>().sprite = frames[frameIndex]; 
            yield return new WaitForSeconds(duration / frames.Length / TimeManager.Instance.GetScale());
            frameIndex++; 
        }

        frameIndex = 0;
        GetComponent<SpriteRenderer>().sprite = null;
    }
}
