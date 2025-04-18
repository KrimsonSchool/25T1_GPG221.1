using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PathFind : MonoBehaviour
{
    private WorldScanner grid;

    public List<Node> open;
    public List<Node> closed;

    public Node current;
    //start at 0,0,0

    //movement stuff
    public int index;

    public Rigidbody rb;
    
    List<Node> path;

    [HideInInspector] public bool aStar;


    private Node targetNode;
    private Node startNode;

    public GameObject counterText;

    private DBugger dBugger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aStar = true;
        
        open = new List<Node>();
        closed = new List<Node>();

        grid = FindAnyObjectByType<WorldScanner>();

        current = grid.gridNodeReferences[10, 0, 10];
        open.Add(current);

        dBugger = FindFirstObjectByType<DBugger>();

        //print("POS: " + current.position);

        Path();
    }

    // Update is called once per frame
    void Update()
    {//
        if (dBugger != null)
        {
            aStar = dBugger.debugAStar;
        }
        //face toward next node in closed list
        //print("dist: " + Vector3.Distance(closed[index].position, transform.position));

        //START FROM TARGET NODE, GO THROUGH CHILDREN
        
        //invert path
        if (aStar && path != null)
        {
            //print(MakePathList().Count);


            if (index < path.Count-1)
            {
                if (Vector3.Distance(path[index].position, transform.position) <= 1)
                {
                    index++;
                }
            }
            else
            {
                print("At Target!");
            }
            
            //print(path[index].position);

            Vector3 toObject = path[index].position - transform.position;
            float dotProduct = Vector3.Dot(transform.right, toObject.normalized);

            //print("dot product " + dotProduct);
            if (dotProduct > 0)
            {
                //right
                rb.AddRelativeTorque(0, 1, 0, ForceMode.Impulse);
            }
            else if (dotProduct < 0)
            {
                //left
                rb.AddRelativeTorque(0, -1, 0, ForceMode.Impulse);
            }
            else
            {
                //front or behind
            }
        }
    }

    public void Path()
    {
        startNode = current;
        open.Add(current);
        while (open.Count > 0)
        {
            //tried sort by cost, made it worse?
            open.Sort((a, b) => a.dist.CompareTo(b.dist));

            current = open[0];
            open.RemoveAt(0);
            closed.Add(current);

            //print(current.position);
            if (current.position == new Vector3Int(Mathf.RoundToInt(grid.target.transform.position.x),
                    Mathf.RoundToInt(grid.target.transform.position.y),
                    Mathf.RoundToInt(grid.target.transform.position.z)))
            {
                print("Path found " + path);
                path = MakePathList();
                path.Reverse();
                break;
            }


            List<Node> neighbours = GetNeighbors(current);

            foreach (Node neighbour in neighbours)
            {
                if (!neighbour.isBlocked && !open.Contains(neighbour) && !closed.Contains(neighbour))
                {
                    neighbour.dist = Vector3.Distance(neighbour.position, grid.target.transform.position);
                    neighbour.startDist = current.startDist + Vector3.Distance(neighbour.position, current.position);
                    
                    neighbour.parent = current;

                    open.Add(neighbour);
                }
            }
        }
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbours = new List<Node>();

        //check x, -x, z, -z is 

        int posX = Mathf.RoundToInt(current.position.x);
        int posZ = Mathf.RoundToInt(current.position.z);

        for (int x = -1; x < 2; x++)
        {
            for (int z = -1; z < 2; z++)
            {
                if (posX + x > grid.size.x - 1 || posX + x < 0 || posZ + z > grid.size.z - 1 || posZ + z < 0)
                {
                    //uh oh
                }
                else
                {
                    if (!grid.gridNodeReferences[posX + x, 0, posZ + z].isBlocked)
                    {
                        neighbours.Add(grid.gridNodeReferences[posX + x, 0, posZ + z]);
                    }
                }
            }
        }

        return neighbours;
    }

    private void RunMyFunction()
    {
        Path();
    }

    public List<Node> MakePathList()
    {
        Node checkNode = new Node();
        List<Node> pathe = new List<Node>();
        //from target node -> start
            
        //find target node...
        foreach (Node node in closed)
        {
            if (Vector3.Distance(node.position, grid.target.transform.position) > 1)
            {
                targetNode = node;
                checkNode = targetNode;
            }
        }
            
        int counter = 0;
        while(checkNode.parent != null)
        {
            pathe.Add(checkNode.parent);
            checkNode = checkNode.parent;
            
            //GameObject ct = Instantiate(counterText, checkNode.position, Quaternion.identity);
            //ct.GetComponentInChildren<TextMeshPro>().text = counter.ToString();
            
            
            if (checkNode == startNode)
            {
                break;
            }
            counter++;
        }
        
        return pathe;
    }
}
