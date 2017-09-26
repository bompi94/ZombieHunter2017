using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePlacer : MonoBehaviour {

    [SerializeField]
    GameObject node;

    [SerializeField]
    Vector2 bottomLeftCorner;

    [SerializeField]
    Vector2 topRightCorner;

    [SerializeField]
    float precision; 

    float stepX;
    float stepY;

    List<Node> nodes = new List<Node>(); 

    private void Start()
    {
        PlaceNodes();
        ConnectNodes(); 
    }

    void PlaceNodes()
    {
        stepX = (topRightCorner.x - bottomLeftCorner.x)/ precision;
        stepY = (topRightCorner.y - bottomLeftCorner.y)/ precision;
        float x = bottomLeftCorner.x;
        float y = bottomLeftCorner.y;

        GameObject folder = new GameObject();
        folder.name = "Nodes"; 

        for(int i = 0; i<precision; i++)
        {
            for (int j = 0; j < precision; j++)
            {
                x += stepX;
                GameObject n = Instantiate(node, new Vector3(x, y, 0), Quaternion.identity);
                n.transform.SetParent(folder.transform);
                nodes.Add(n.GetComponent<Node>()); 
            }
            y += stepY;
            x = bottomLeftCorner.x; 
        }
    
    }

    void ConnectNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].FindValidNeighbours(stepX,stepY); 
        }
    }

    public Node FindNodeFromPosition(Vector3 v)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if ((v - nodes[i].transform.position).magnitude <1f)
            {
                return nodes[i];
            }
        }
        return null; 
    }
}
