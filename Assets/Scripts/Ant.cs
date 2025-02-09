using UnityEngine;

public class Ant : MonoBehaviour
{
    public float distance;

    private Rigidbody rb;
    
    public GameObject target;

    private float timer;
    public float decisionTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeTorque(0, Random.Range(-500, 500), 0);//gives start variance
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //decide every xths of a second to rotate (weight towards food)
        
        timer += Time.fixedDeltaTime;
        if (timer >= decisionTime)
        {
            if (Random.Range(0, 5) < 2)
            {
                //rot towards food
                Vector3 directionToTarget = target.transform.position - transform.position;
                Vector3 crossProduct = Vector3.Cross(transform.forward, directionToTarget);

                    if (crossProduct.y > 0)
                    {
                        //transform.Rotate(0, 30, 0);
                        rb.AddRelativeTorque(0, 50, 0, ForceMode.Impulse);
                    }
                else if (crossProduct.y < 0)
                {
                    //transform.Rotate(0, -30, 0);
                    rb.AddRelativeTorque(0, -50, 0, ForceMode.Impulse);

                }

            }
            else
            {
                //rot towards rng
                rb.AddRelativeTorque(0, Random.Range(-50, 50), 0, ForceMode.Impulse);
            }

            timer = 0;
        }
        //plus -45 and 45 degrees
        bool hitSomething = Physics.Raycast(transform.position + transform.up, transform.forward, out RaycastHit hit, distance);
        bool hitSomethingL = Physics.Raycast(transform.position + transform.up, (transform.forward + transform.right).normalized, out RaycastHit hitL, distance);
        bool hitSomethingR = Physics.Raycast(transform.position + transform.up, (transform.forward + -transform.right).normalized, out RaycastHit hitR, distance);
        
        Debug.DrawRay(transform.position + transform.up, transform.forward * distance, Color.red);
        Debug.DrawRay(transform.position + transform.up, (transform.forward + transform.right).normalized * distance, Color.red);
        Debug.DrawRay(transform.position + transform.up, (transform.forward + -transform.right).normalized * distance, Color.red);
        
        if (hitSomething)
        {
            print("Hit");
                rb.AddRelativeTorque(0, 1000, 0);
        }
        if (hitSomethingL)
        {
            print("Hit L");
            rb.AddRelativeTorque(0, -1000, 0);
        }
        if (hitSomethingR)
        {            print("Hit R");
            rb.AddRelativeTorque(0, 1000, 0);
        }
        else
        {
            /*
            Vector3 direction = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        */
        }

    }
}
