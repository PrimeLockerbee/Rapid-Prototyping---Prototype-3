using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameManager _managerRef;

    public GameObject[] unitPrefabs;
    public int activeUnitIndex = 0;

    public bool gameHasStarted = false;
    public GameObject startButton;

    private Camera mainCamera;

    public List<GameObject> units = new List<GameObject>();
    public List<Unit> unitFactions = new List<Unit>();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Spawn(mousePosition);
        }
    }

    public void SetSoldierActive()
    {
        activeUnitIndex = 0;
    }

    public void SetTankActive()
    {
        activeUnitIndex = 1;
    }

    public void SetArcherActive()
    {
        activeUnitIndex = 2;
    }

    public void Spawn(Vector2 position)
    {
        GameObject unit = Instantiate(unitPrefabs[activeUnitIndex], position, Quaternion.identity);
        units.Add(unit);
        unitFactions.Add(unit.GetComponent<Unit>());
    }

    public void StartGame()
    {
        gameHasStarted = true;
        Unit[] allUnits = FindObjectsOfType<Unit>();
        foreach (Unit unit in allUnits)
        {
            unit.StartMoving();
        }
        startButton.SetActive(false);
    }

    public void EnableStartButton()
    {
        startButton.SetActive(true);
    }
}
