using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Unit> units = new List<Unit>();

    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _buttons;

    void Start()
    {
        Unit[] unitArray = FindObjectsOfType<Unit>();
        units.AddRange(unitArray);
    }

    void Update()
    {
        bool enemyFound = false;

        for (int i = units.Count - 1; i >= 0; i--)
        {
            Unit unit = units[i];

            if (unit == null)
            {
                // Remove destroyed units from the list
                units.RemoveAt(i);
                continue;
            }

            if (unit.faction == Faction.Enemy)
            {
                enemyFound = true;
                break;
            }
        }

        if (!enemyFound)
        {
            _winScreen.SetActive(true);
            _buttons.SetActive(false);
        }
        else if (units.Count == 0)
        {
            _loseScreen.SetActive(true);
            _buttons.SetActive(false);
        }
    }
}