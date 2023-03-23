using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float baseStrength = 10f;

    private float strength;

    private int numUnits;

    public float speed = 5f; 
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f; 
    public float maxY = 5f; 

    private Vector2 targetPosition; 

    private void Start()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        numUnits = units.Length;

        strength = baseStrength * Mathf.Sqrt(numUnits);

        targetPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

       
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
    }

    public float GetStrength()
    {
        return strength;
    }
}
