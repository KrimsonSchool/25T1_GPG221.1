using Anthill.AI;
using Anthill.Utils;
using UnityEngine;

public class TruckDriverSensors : MonoBehaviour, ISense
{
    private bool _hasCargo;
    private bool _haveDroppedOff;
    private bool _nearDropOff;
    private bool _nearPickUp;

    private GameObject _box;

    public GameObject Box
    {
        set => _box = value;
        get => _box;
    }

    public bool HasCargo
    {
        set => _hasCargo = value;
    }

    public bool NearDropOff
    {
        set => _nearDropOff = value;
    }

    public bool NearPickUp
    {
        set => _nearPickUp = value;
    }

    public bool HaveDroppedOff
    {
        set => _haveDroppedOff = value;
    }

    /// <summary>
    /// This method will be called automaticaly each time when AntAIAgent decide to update the plan.
    /// You should attach this script to the same object where is AntAIAgent.
    /// </summary>
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(TruckDriver.HasCargo, _hasCargo);
            aWorldState.Set(TruckDriver.HaveDroppedOff, _haveDroppedOff);

            aWorldState.Set(TruckDriver.NearDropOff, IsNearDropOff());
            aWorldState.Set(TruckDriver.NearPickUp, IsNearPickup());
        }
        aWorldState.EndUpdate();
    }

    public bool IsNearDropOff()
    {
        GameObject dropOff = GameObject.Find("DropOff");
        print("Dist to Drop Off: " + AntMath.Distance(transform.position, dropOff.transform.position));
        if (AntMath.Distance(transform.position, dropOff.transform.position) < 1.6f)
        {
            print("Near Drop Off!");
            return true;
        }
        
        return false;
    }

    public bool IsNearPickup()
    {
        GameObject pickUp = GameObject.Find("PickUp");
        print("Dist to Pick Up: " + AntMath.Distance(transform.position, pickUp.transform.position));
        if (AntMath.Distance(transform.position, pickUp.transform.position) < 1.6f)
        {
            print("Near Pick Up!");
            return true;
        }
        
        return false;
    }
}