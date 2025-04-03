using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;

public class GoToTruckState : AntAIState
{
    private DelivererSensors _sensors;
    private GameObject _truck;
    private Rigidbody _rb;

    private NavMeshPath _path;
    Vector3[] _points;

    private int index;

    public GameObject debugSpheres;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _sensors = FindFirstObjectByType<DelivererSensors>();
        _truck = _sensors.Truck;
        _path = new NavMeshPath();

        if (!NavMesh.CalculatePath(_sensors.transform.position, _truck.transform.position, NavMesh.AllAreas, _path))
        {
            Debug.LogWarning("Path not found");
        }
        else
        {
            print("Path found");
        }

        _points = _path.corners;

        for (int i = 0; i < _points.Length; i++)
        {
            GameObject pp = Instantiate(debugSpheres, _points[i], Quaternion.identity);
            pp.name = "s_" + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (index < _points.Length)
        {
            _sensors.gameObject.transform.position =
                Vector3.MoveTowards(_sensors.transform.position, _points[index], Time.deltaTime);

            //print("Dist: " + Vector3.Distance(_sensors.transform.position, _points[index]));
            if (Vector3.Distance(_sensors.transform.position, _points[index]) < 1.5f)
            {
                index++;
            }


        }
        else
        {
            print("At Truck!");
            Finish();
        }
    }
}

