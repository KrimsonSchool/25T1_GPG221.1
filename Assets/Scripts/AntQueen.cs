using UnityEngine;
using Unity;
using UnityEditor;

public class AntQueen : MonoBehaviour
{
    public float spawnTime;

    public GameObject ant;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer >= spawnTime)
        {
            timer = 0;
            Instantiate(ant, transform.position, Quaternion.Euler(0, Random.Range(-360, 360), 0));
        }
    }
}
