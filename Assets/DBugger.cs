using System;
using UnityEngine;
using UnityEngine.UI;

public class DBugger : MonoBehaviour
{
    public Toggle toggleCollisions;
    public Toggle toggleMovement;
    public Toggle toggleAStar;
    public Toggle toggleAntQueen;
    public Button spawnAnt;
    public Toggle toggleGrid;

    public static DBugger Instance;

    public bool debugCollisions;
    public bool debugMovements;
    public bool debugAStar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        debugCollisions = false;
        debugMovements = true;
        debugAStar = true;
    }

    private void OnEnable()
    {
        toggleCollisions.onValueChanged.AddListener(OnToggleCollisions);

        //toggleMovement.onValueChanged.AddListener(OnToggleMovement);

        toggleAStar.onValueChanged.AddListener(OnToggleAStar);
        toggleAntQueen.onValueChanged.AddListener(OnToggleAntQueen);
        
        spawnAnt.onClick.AddListener(OnSpawnAnt);
        
        toggleGrid.onValueChanged.AddListener(OnToggleGrid);
    }

    private void OnToggleAStar(bool state)
    {
        debugAStar = state;
    }

    private void OnSpawnAnt()
    {
        FindFirstObjectByType<AntQueen>().SpawnAnt();
    }

    private void OnToggleMovement(bool state)
    {
        print("Change movement to " + state);
        debugMovements = state;
        print("Now dm = " + debugMovements);
    }

    private void OnToggleCollisions(bool state)
    {
        debugCollisions = state;
    }

    private void OnToggleAntQueen(bool state)
    {
        FindFirstObjectByType<AntQueen>().canSpawn =  state;
    }

    private void OnToggleGrid(bool state)
    {
        FindFirstObjectByType<WorldScanner>().showGrid = state;
    }
}