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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            print("Saw box");
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
            rb.AddTorque(0, Time.deltaTime, 0);
            
            Physics.Raycast(deliverer.transform.position, deliverer.transform.forward, out RaycastHit hit, Mathf.Infinity);
            Debug.DrawRay(deliverer.transform.position, deliverer.transform.forward * 1000, Color.red);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "Box")
                {
                    seeBox = true;
                    box = hit.collider.gameObject;
                }
            }
        }
    }
}