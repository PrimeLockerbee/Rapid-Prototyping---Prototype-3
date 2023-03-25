using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    new void Start()
    {
        base.Start();

        Debug.Log("Archer started!");
    }

    public override float GetStrength()
    {
        return 5f;
    }

    public override void Attack(Unit target)
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
        {
            Debug.Log("Attacking from a distance!");

            target.TakeDamage(GetStrength());
        }
    }
}