using UnityEngine;

[ExecuteInEditMode]
public class DEBUGGER : MonoBehaviour
{
    public Vector3Int size;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                }
            }
        }
    }
}
