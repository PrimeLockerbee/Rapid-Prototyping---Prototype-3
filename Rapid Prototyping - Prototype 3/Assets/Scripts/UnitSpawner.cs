using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : MonoBehaviour
{
    public GameObject[] unitPrefabs;
    public int activeUnitIndex = 0;

    public GameObject startButton;

    private Camera mainCamera;

    private List<GameObject> units = new List<GameObject>();

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
    }

    public void StartGame()
    {
        foreach (GameObject unit in units)
        {
            Unit unitComponent = unit.GetComponent<Unit>();
            if (unitComponent != null)
            {
                unitComponent.StartMoving();
            }
        }
        startButton.SetActive(false);
    }

    public void EnableStartButton()
    {
        startButton.SetActive(true);
    }
}
