using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PathFind : MonoBehaviour
{
    private WorldScanner grid;

    public List<Node> open;
    public List<Node> closed;

    public Node current;
    //start at 0,0,0

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        open = new List<Node>();
        closed = new List<Node>();

        grid = FindAnyObjectByType<WorldScanner>();

        current = grid.gridNodeReferences[10, 0, 10];
        open.Add(current);

        print("POS: " + current.position);

        Path();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Path()
    {
        open.Add(current);
        while (open.Count > 0)
        {
            // Pick the node with the lowest cost (A* approach)
            

            current = open[0];
            open.RemoveAt(0);
            closed.Add(current);

            // Goal reached
            print(current.position);
            if (current.position == grid.gridNodeReferences[0,0,0].position) // Assuming 'target' is the goal node
            {
                print("Path found");
                break;
            }

            // Get neighbors
            List<Node> neighbors = GetNeighbors(current);

            foreach (var neighbor in neighbors)
            {
                if (!neighbor.isBlocked && !closed.Contains(neighbor))
                {
                    if (!open.Contains(neighbor))
                    {
                        open.Add(neighbor);
                    }
                }
            }
        }
        
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Get adjacent nodes
        Vector3[] directions = new Vector3[] {
            new Vector3(1, 0, 0), new Vector3(-1, 0, 0), 
            new Vector3(0, 0, 1), new Vector3(0, 0, -1)
        };

        foreach (var direction in directions)
        {
            Vector3 neighborPosition = node.position + direction;

            // Ensure the neighborPosition is within bounds
            int x = Mathf.RoundToInt(neighborPosition.x);
            int y = Mathf.RoundToInt(neighborPosition.y);
            int z = Mathf.RoundToInt(neighborPosition.z);

            // Check if the neighbor is within the grid bounds (ensure gridNodeReferences is accessible)
            if (x >= 0 && x < grid.gridNodeReferences.GetLength(0) &&
                y >= 0 && y < grid.gridNodeReferences.GetLength(1) &&
                z >= 0 && z < grid.gridNodeReferences.GetLength(2))
            {
                Node neighborNode = grid.gridNodeReferences[x, y, z];
                if (neighborNode != null) // If the neighbor node is valid
                {
                    neighbors.Add(neighborNode);
                }
            }
        }

        return neighbors;
    }
}