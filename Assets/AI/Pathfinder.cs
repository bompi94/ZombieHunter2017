using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //A* implementation
    public List<Node> FindPath(Node start, Node goal) 
    {
        List<Node> finalPath = new List<Node>(); 

        List<Node> frontier = new List<Node>();
        frontier.Add(start);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        cameFrom[start] = null;

        Dictionary<Node, int> costSoFar = new Dictionary<Node, int>();
        costSoFar[start] = 0;

        Node n = null; 

        while (frontier.Count > 0)
        {
            Node current = frontier[0];
            frontier.RemoveAt(0);

            if (current == goal)
            {
                n = current; 
                break;
            }

            for (int i = 0; i < current.validNeighbours.Count; i++)
            {
                Node next = current.validNeighbours[i];
                int newCost = costSoFar[current] + Cost(current, next);
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    frontier.Add(next);
                    cameFrom[next] = current;
                }
            }
        }

        while(n!=null)
        {
            finalPath.Insert(0, n);
            n = cameFrom[n]; 
        }

        return finalPath; 
    }

    public int Cost(Node a, Node b)
    {
        return 1; 
    }
}
