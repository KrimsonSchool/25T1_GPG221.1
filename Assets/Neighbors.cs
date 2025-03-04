using System;
using System.Collections.Generic;
using UnityEngine;

public class Neighbors : MonoBehaviour
{
    public List<Transform> neighboursList;

    public Vector3 averagePos;

    public Rigidbody rb;

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

        if (neighboursList.Count > 0)
        {
            foreach (Transform n in neighboursList)
            {
                averagePos += n.position;
            }

            print(averagePos);
            averagePos /= neighboursList.Count;

            Vector3 away = (transform.position - averagePos).normalized;
            Vector3 loacDir = transform.InverseTransformDirection(away);

            rb.AddRelativeForce(loacDir * 1, ForceMode.Impulse);
        }
    }
}