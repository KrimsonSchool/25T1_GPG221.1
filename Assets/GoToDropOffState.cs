using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;

public class GoToDropOffState : AntAIState
{
    private int index;

    private NavMeshPath _path;
    TruckDriverSensors _sensors;
    private Vector3[] _points;

    private GameObject dropOff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropOff = GameObject.Find("DropOff");
        _sensors = GetComponentInParent<TruckDriverSensors>();
        
        _path = new NavMeshPath();

        if (!NavMesh.CalculatePath(_sensors.transform.position, dropOff.transform.position, NavMesh.AllAreas, _path))
        {
            Debug.LogWarning("Path not found");
        }
        else
        {
            print("Path found");
        }
        _points = _path.corners;
    }

    // Update is called once per frame
    void Update()
    {
        if (index < _points.Length)
        {           _sensors.gameObject.transform.position = Vector3.MoveTowards(_sensors.transform.position, _points[index], Time.deltaTime * 5);

            //print("Dist: " + Vector3.Distance(_sensors.transform.position, _points[index]));
            if (Vector3.Distance(_sensors.transform.position, _points[index]) < 1.5f)
            {
                index++;
            }
            
            
        }
        else
        {
            print("At Box!");
            Finish();
        }
    }
}
