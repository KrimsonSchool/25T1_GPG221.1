using Anthill.AI;
using UnityEngine;

public class DropOffBoxState : AntAIState
{
    DelivererSensors _sensors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sensors = FindFirstObjectByType<DelivererSensors>();
        _sensors.Box.transform.parent = null;
        
        _sensors.Box = null;
        _sensors.HasBox = false;
        Finish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
