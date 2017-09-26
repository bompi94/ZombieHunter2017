using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour {

    [SerializeField]
    GameObject piece;

    [SerializeField]
    int numberOfPieces;
    [SerializeField]
    float explosionForce; 

    public void Explode()
    {
        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject p = Instantiate(piece);
            p.transform.position = transform.position;
            Vector2 scale = new Vector2(p.transform.localScale.x * Random.Range(0.8f, 1.2f), p.transform.localScale.y * Random.Range(0.8f, 1.2f));
            p.transform.localScale = scale; 
            Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            p.GetComponent<ExplosionPiece>().AddForce(force * explosionForce); 
        }
    }
}
