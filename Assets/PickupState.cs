using Anthill.AI;
using UnityEngine;

public class PickupState : AntAIState
{
    private GameObject _box;
    TruckDriverSensors _sensors;

    private bool hasBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sensors = GetComponentInParent<TruckDriverSensors>();
        _box = GameObject.Find("_b");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBox)
        {
            print("Dist to box: " + Vector3.Distance(transform.position, _box.transform.position));
            if (Vector3.Distance(transform.position, _box.transform.position) < 2.5f && _box.transform.parent==null)
            {
                hasBox = true;
                _box.transform.parent = transform.parent.transform;
                _sensors.HasCargo = true;
                Finish();
            }
        }
    }
}
