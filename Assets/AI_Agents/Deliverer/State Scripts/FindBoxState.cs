using Anthill.AI;
using UnityEngine;

public class FindBoxState : AntAIState
{
    private bool seeBox;

    bool hasTarget;
    Vector3 target;

    GlobalSettings globalSettings;

    private GameObject deliverer;
    private Rigidbody rb;

    public GameObject box;
    
    DelivererSensors sensors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensors = FindFirstObjectByType<DelivererSensors>();
        
        seeBox = false;
        globalSettings = FindFirstObjectByType<GlobalSettings>();
        
        deliverer = gameObject.transform.parent.gameObject;
        rb = deliverer.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seeBox)
        {
            //print("Saw box");
            
            //AntAICondition cond = new AntAICondition();
            //cond.Set("SeeBox", seeBox);
            sensors.SeeBox = true;
            Finish();
        }

        /*if (!hasTarget)
        {
            target = new Vector3(Random.Range(globalSettings.worldBoundsMin.x, globalSettings.worldBoundsMax.x), 0,
                Random.Range(globalSettings.worldBoundsMin.z, globalSettings.worldBoundsMax.z));
            
            hasTarget = true;
        }*/

        if (!seeBox)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity);
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "Box")
                {
                    seeBox = true;
                    
                    sensors.Box = hit.collider.gameObject.transform.parent.gameObject;
                }
            }
            
            rb.AddRelativeTorque(0, Time.deltaTime, 0, ForceMode.Impulse);
        }
    }
}