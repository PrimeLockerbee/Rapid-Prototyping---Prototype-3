using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    new void Start()
    {
        base.Start();

        Debug.Log("Soldier started!");
    }

    public override float GetStrength()
    {
        return 15f;
    }

    public override void Attack(Unit target)
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
        {
            Debug.Log("Attacking at close range!");

            target.TakeDamage(GetStrength());
        }
    }
}
