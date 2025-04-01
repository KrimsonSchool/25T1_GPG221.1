using Anthill.AI;
using Anthill.Utils;
using UnityEngine;

public class DelivererSensors : MonoBehaviour, ISense
{
    private UnitControl _control;//unit visuals?
	private Transform _t;
	private Transform _truck;
	private Transform _box;
	private bool _hasBox;

	private void Awake()
	{
		_control = GetComponent<UnitControl>();
		_t = GetComponent<Transform>();
		
		// Save reference for the bot base.
		var go = GameObject.Find("Truck");
		Debug.Assert(go != null, "Base object not exists on the scene!");
		_truck = go.GetComponent<Transform>();

		// Save reference for the cargo.
		go = GameObject.Find("Box");
		Debug.Assert(go != null, "Cargo object not exists on the scene!");
		_box = go.GetComponent<Transform>();
	}

	/// <summary>
	/// This method will be called automaticaly each time when AntAIAgent decide to update the plan.
	/// You should attach this script to the same object where is AntAIAgent.
	/// </summary>
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		// 1. Classic setup of the World Conditions for planner.
		// -------------------------------------------------------
		aWorldState.BeginUpdate(aAgent.planner);
		{
		// 	aWorldState.Set("Is Cargo Delivered", false);
		// 	aWorldState.Set("See Cargo", IsSeeCargo());
		// 	aWorldState.Set("Has Cargo", _control.HasCargo);
		// 	aWorldState.Set("See Base", IsSeeBase());
		// 	aWorldState.Set("Near Base", IsNearBase());
		// 

		// 2. Optimized setup of the World Conditions for the planner.
		// -----------------------------------------------------------
		//aWorldState.Set(DeliveryBot.IsCargoDelivered, false);
		aWorldState.Set("SeeBox", IsSeeBox());
		aWorldState.Set("NearBox", IsNearBox());
		aWorldState.Set("HasBox", _hasBox);//has box
		aWorldState.Set("SeeTruck", IsSeeTruck());
		aWorldState.Set("NearTruck", IsNearTruck());
		}
		aWorldState.EndUpdate();
		// HINT: When you have finished the AI Scenario, just export all conditions
		// as enum and use it to set conditions from the code.
		
		//see box
		//near box
		//has box
		//See truck
		//near truck
	}

	private bool IsSeeBox()
	{
		Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity);
		Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.name == "Box")
			{
				return true;
			}
		}
		return false;
	}
	private bool IsNearBox()
	{
		return (AntMath.Distance(_t.position, _box.position) < 1.0f);
	}

	private bool IsSeeTruck()
	{
		return (AntMath.Distance(_t.position, _truck.position) < 1.5f);
	}

	private bool IsNearTruck()
	{
		return (AntMath.Distance(_t.position, _truck.position) < 0.1f);
	}
}
