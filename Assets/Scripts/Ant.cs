using UnityEngine;

public class Ant : MonoBehaviour
{
    public float distance;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeTorque(0, Random.Range(-500, 500), 0);//gives start variance
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        
        if (hitSomething)
        {
            rb.AddRelativeTorque(0, 1000, 0);
        }

    }
}
