using Anthill.AI;
using UnityEngine;

public class DropOffState : AntAIState
{
    TruckDriverSensors _sensors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sensors = GetComponentInParent<TruckDriverSensors>();
        
        _sensors.HasCargo = false;
        _sensors.HaveDroppedOff = true;
        GameObject.Find("_b").transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
