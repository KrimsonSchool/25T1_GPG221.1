using System;
using UnityEngine;
using UnityEngine.UI;

public class DBugger : MonoBehaviour
{
    public Button toggleGrid;
    public Button toggleRays;
    public Button toggleMove;
    public Button toggleAStar;
    public Button spawnAnt;

    public static DBugger Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        toggleGrid.onClick.AddListener(OnToggleGrid);
        toggleRays.onClick.AddListener(OnToggleRays);
        toggleMove.onClick.AddListener(OnToggleMove);
        toggleAStar.onClick.AddListener(OnToggleAStar);
        spawnAnt.onClick.AddListener(OnSpawnAnt);
    }

    private void OnToggleMove()
    {
    }

    private void OnToggleAStar()
    {
    }

    private void OnSpawnAnt()
    {
    }

    private void OnToggleRays()
    {
    }

    private void OnToggleGrid()
    {
    }
}