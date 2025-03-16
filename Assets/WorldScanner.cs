using System.Collections.Generic;
using UnityEngine;

public class WorldScanner : MonoBehaviour
{
    public Node[,,] gridNodeReferences;

    public Vector3Int size;

    public GameObject target;
    public GameObject start;
    
    [HideInInspector]
    public bool showGrid=true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        showGrid = true;
        gridNodeReferences = new Node[size.x, size.y, size.z];
        Scan();
    }

    public void Scan()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    gridNodeReferences[x, y, z] = new Node();

                    gridNodeReferences[x, y, z].position = transform.position + new Vector3(x, y, z);

                    //print("GRID POS: " + gridNodeReferences[x,y,z].position);

                    if (Physics.CheckBox(transform.position + new Vector3(x, y, z), new Vector3(0.5f, 0.5f, 0.5f),
                            Quaternion.identity))
                    {
                        // Something is there
                        gridNodeReferences[x, y, z].isBlocked = true;
                    }
                    else
                    {
                        gridNodeReferences[x, y, z].isBlocked = false;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gridNodeReferences == null)
        {
            return;
        }

        if (showGrid)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        if (FindFirstObjectByType<PathFind>().closed.Contains(gridNodeReferences[x, y, z]))
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                        }
                        else if (gridNodeReferences[x, y, z].isBlocked)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                        }
                        else
                        {
                            Gizmos.color = Color.green;
                            Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                        }
                    }
                }
            }
        }
    }
}

public class Node
{
    public bool isBlocked;
    public Vector3 position;

    public float dist;
    public float startDist;


    public Node parent;
}