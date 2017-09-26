using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public bool shouldMove = true;

    [SerializeField]
    float speed;

    GameObject player;
    Vector3 destination;
    Pathfinder pathfinder;
    NodePlacer nodePlacer;
    List<Node> path = new List<Node>();
    Node currentNode;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        pathfinder = FindObjectOfType<Pathfinder>();
        nodePlacer = FindObjectOfType<NodePlacer>();
    }

    void FixedUpdate()
    {
        if (path.Count > 0)
        {
            Vector2 pathPos = path[0].transform.position;
            print("a");
            MoveTo(pathPos);
            if (Vector2.Distance(transform.position, pathPos) < 1)
            {
                currentNode = path[0];
                path.RemoveAt(0);
            }
        }
        else
        {
            print("b");
            MoveTo(destination);
        }
    }

    void MoveTo(Vector3 pos)
    {
        if (shouldMove)
            transform.position += (pos - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    public void SetDestination(Vector3 v)
    {
        if (v != destination)
        {
            if (!currentNode)
                currentNode = nodePlacer.FindNodeFromPosition(transform.position);
            destination = v;
            Node start = currentNode;
            Node end = FindNodeFromPosition(v);
            List<Node> newPath = pathfinder.FindPath(start, end);
            MergePath(newPath);
            DrawPath();
        }
    }

    void MergePath(List<Node> newPath)
    {
        path = newPath;
    }

    Node FindNodeFromPosition(Vector3 v)
    {
        return nodePlacer.FindNodeFromPosition(v);
    }

    void DrawPath()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            path[i].DrawLineWith(path[i + 1], Color.red);
        }
    }
}
