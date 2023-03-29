using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnitSpawner _spawnerRef;

    [SerializeField] public List<Unit> enemyUnits = new List<Unit>();

    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _buttons;

    void Start()
    {
        Unit[] enemyUnitArray = FindObjectsOfType<Unit>();
        enemyUnits.AddRange(enemyUnitArray);
    }

    void Update()
    {
        bool enemyFound = false; 
        bool playerFound = false;

        for (int i = enemyUnits.Count - 1; i >= 0; i--)
        {
            Unit unit = enemyUnits[i];

            if (unit == null)
            {
                // Remove destroyed units from the list
                enemyUnits.RemoveAt(i);
                continue;
            }

            if (unit.faction == Faction.Enemy)
            {
                enemyFound = true;
                break;
            }

            if (unit.faction == Faction.Player)
            {
                playerFound = true;
                break;
            }
        }

        for (int i = _spawnerRef.unitFactions.Count - 1; i >= 0; i--)
        {
            Unit unit = _spawnerRef.unitFactions[i];

            if (unit == null)
            {
                // Remove destroyed units from the list
                _spawnerRef.unitFactions.RemoveAt(i);
                continue;
            }

            if (unit.faction == Faction.Player)
            {
                playerFound = true;
                break;
            }
        }

        if (!enemyFound)
        {
            _winScreen.SetActive(true);
            _buttons.SetActive(false);
        }

        if (_spawnerRef.gameHasStarted == true)
        {
            if (!playerFound)
            {
                _loseScreen.SetActive(true);
                _buttons.SetActive(true);
            }
        }
    }
}