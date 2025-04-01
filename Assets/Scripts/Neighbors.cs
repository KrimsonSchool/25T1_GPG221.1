using System;
using System.Collections.Generic;
using UnityEngine;

public class Neighbors : MonoBehaviour
{
    public List<Transform> neighboursList;

    public Vector3 averagePos;

    public Rigidbody rb;

    public GameObject collisionSphere;

    private GameObject cS;

    private DBugger debug;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        debug = FindFirstObjectByType<DBugger>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Neighbors>() != null)
        {
            neighboursList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        neighboursList.Remove(other.transform);
    }*/

    private void FixedUpdate()
    {
        averagePos = rb.position;
        neighboursList.Clear();
        
        Collider[] col = Physics.OverlapSphere(transform.position, 10.1f);
        foreach (Collider coll in col)
        {
            if (coll.GetComponent<Neighbors>() != null)
            {
                neighboursList.Add(coll.transform);
            }
        }
        

        if (cS != null)
        {
            Destroy(cS);
        }
        if (debug.debugCollisions)
        {
            cS = Instantiate(collisionSphere, transform.position, Quaternion.identity);
            cS.transform.localScale = new Vector3(10.1f * 2, 10.1f * 2, 10.1f * 2);
        }

        if (neighboursList.Count > 0)
        {
            foreach (Transform n in neighboursList)
            {
                averagePos += n.position;
            }

            //print(averagePos);
            averagePos /= neighboursList.Count+1;

            Vector3 away = (transform.position - averagePos).normalized;
            Vector3 loacDir = transform.InverseTransformDirection(away);

            rb.AddRelativeForce(loacDir * 10, ForceMode.Impulse);
        }
    }
}