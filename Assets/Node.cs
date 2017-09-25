using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    List<Node> validNeighbours = new List<Node>(); 

    public void FindValidNeighbours(float x, float y)
    {
        VerifyConnection(Vector2.up);
        VerifyConnection(Vector2.right);
        VerifyConnection(-Vector2.up);
        VerifyConnection(-Vector2.right);

        float angleX = x;
        float angleY = y;  

        VerifyConnection(new Vector2(angleX, angleY));
        VerifyConnection(new Vector2(-angleX, angleY)); 
        VerifyConnection(new Vector2(angleX, -angleY));
        VerifyConnection(new Vector2(-angleX, -angleY));

    }

    void VerifyConnection(Vector2 dir)
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, dir, 3);

        if(ray.Length == 1)
        {
            Verify(ray[0]); 
        }

        else
        {
            Verify(ray[1]); 
        }
    }

    void Verify(RaycastHit2D ray)
    {
        if (ray.collider && ray.collider.gameObject.GetComponent<Node>())
        {
            DrawLineWith(ray.collider.gameObject.GetComponent<Node>(), Color.green);
            validNeighbours.Add(ray.collider.gameObject.GetComponent<Node>());
        }
    }

    void DrawLineWith(Node n, Color c)
    {
        Debug.DrawLine(transform.position, n.transform.position, c, 1000000); 
    }
	
}
