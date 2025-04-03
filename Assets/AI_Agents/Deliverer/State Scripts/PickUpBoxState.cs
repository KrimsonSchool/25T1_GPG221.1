using Anthill.AI;
using UnityEngine;

public class PickUpBoxState : AntAIState
{
    DelivererSensors _sensors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sensors = FindFirstObjectByType<DelivererSensors>();
        
        _sensors.Box.transform.parent = transform.parent;
        _sensors.Box.GetComponentInChildren<Collider>().enabled = false;
        _sensors.HasBox = true;
        Finish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
