using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    float speed;

    GameObject player;
    Vector3 destination;
    Pathfinder pathfinder;
    NodePlacer nodePlacer;
    List<Node> path = new List<Node>(); 

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        pathfinder = FindObjectOfType<Pathfinder>();
        nodePlacer = FindObjectOfType<NodePlacer>();
    }

    void FixedUpdate()
    {
        if (path.Count != 0)
        {

            transform.position += (path[0].transform.position - transform.position).normalized * speed * Time.fixedDeltaTime;

            if (Vector2.Distance(transform.position, path[0].transform.position) < .5f)
                path.RemoveAt(0);
        }

        else
        {
            transform.position += (destination - transform.position).normalized * speed * Time.fixedDeltaTime;
        }
    }

    public void SetDestination(Vector3 v)
    {
        if (v != destination)
        {
            destination = v;
            Node start = FindNodeFromPosition(transform.position);
            Node end = FindNodeFromPosition(v);
            path = pathfinder.FindPath(start, end);
        }
    }

    Node FindNodeFromPosition(Vector3 v)
    {
        return nodePlacer.FindNodeFromPosition(v);
    }
}
