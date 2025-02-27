using System;
using System.Collections.Generic;
using UnityEngine;

public class Neighbors : MonoBehaviour
{
    public List<Transform> neighboursList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Neighbors>() != null)
        {
            neighboursList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        neighboursList.Remove(other.transform);
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphere(transform.position, 10.1f);
    }
}
