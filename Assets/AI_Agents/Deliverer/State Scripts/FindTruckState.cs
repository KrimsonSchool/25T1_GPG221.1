using Anthill.AI;
using UnityEngine;

public class FindTruckState : AntAIState
{
    private bool seeTruck;

    bool hasTarget;
    Vector3 target;
    
    private GameObject deliverer;
    private Rigidbody rb;
    
    DelivererSensors sensors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensors = FindFirstObjectByType<DelivererSensors>();
        
        seeTruck = false;
        deliverer = gameObject.transform.parent.gameObject;
        rb = deliverer.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seeTruck)
        {
            print("Saw Truck");
            sensors.SeeTruck = true;
            Finish();
        }
        
        if (!seeTruck)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity);
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "Truck")
                {
                    seeTruck = true;
                    
                    sensors.Truck = hit.collider.gameObject.transform.parent.gameObject;
                }
            }
            
            rb.AddRelativeTorque(0, Time.deltaTime, 0, ForceMode.Impulse);
        }
    }
}
