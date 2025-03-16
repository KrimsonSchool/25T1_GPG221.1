using UnityEngine;
using Unity;
using UnityEditor;

public class AntQueen : MonoBehaviour
{
    public float spawnTime;

    public GameObject ant;

    private float timer;
    
    [HideInInspector]
    public bool canSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                SpawnAnt();
            }
        }
    }

    public void SpawnAnt()
    {
        Instantiate(ant, transform.position, Quaternion.Euler(0, Random.Range(-360, 360), 0));
    }
}
