using UnityEngine;

public class MoveForward : MonoBehaviour
{    public float speed;
    private Rigidbody rb;

    DBugger  dB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dB = FindFirstObjectByType<DBugger>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(dB.debugMovements);
        if (dB.debugMovements)
        {
            rb.AddRelativeForce(0, 0, speed);
        }
    }
}



