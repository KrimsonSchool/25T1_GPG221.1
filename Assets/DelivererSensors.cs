using Anthill.AI;
using Anthill.Utils;
using UnityEngine;

public class DelivererSensors : MonoBehaviour, ISense
{
    private bool _seeBox;
    private bool _hasBox;
    private bool _nearBox;
    private bool _seeTruck;
    private bool _nearTruck;
    
    private GameObject _box;
    private GameObject _truck;

    public bool SeeBox{set => _seeBox = value; }
    public GameObject Box{set => _box = value; get => _box; }
    public bool HasBox{set => _hasBox = value; }
    public GameObject Truck{set => _truck = value; get => _truck; }
    public bool SeeTruck{set => _seeTruck = value; }
    private void Awake()
    {
        
    }

    /// <summary>
    /// This method will be called automaticaly each time when AntAIAgent decide to update the plan.
    /// You should attach this script to the same object where is AntAIAgent.
    /// </summary>
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(Deliverer.SeeBox, _seeBox);
            aWorldState.Set(Deliverer.NearBox, IsNearBox());
            aWorldState.Set(Deliverer.HasBox, _hasBox);
            aWorldState.Set(Deliverer.SeeTruck, _seeTruck);
            aWorldState.Set(Deliverer.NearTruck, IsNearTruck());
        }
        aWorldState.EndUpdate();
    }

    public bool IsNearBox()
    {
        if (_box != null)
        {
            print("Dist to box: " +AntMath.Distance(transform.position, _box.transform.position));
            if (AntMath.Distance(transform.position, _box.transform.position) < 1.6f)
            {
                print("Near box!");
                return true;
            }
        }

        return false;
    }
    
    public bool IsNearTruck()
    {
        if (_truck != null)
        {
            print("Dist to truck: " +AntMath.Distance(transform.position, _truck.transform.position));
            if (AntMath.Distance(transform.position, _truck.transform.position) < 1.6f)
            {
                print("Near truck!");
                return true;
            }
        }

        return false;
    }
}