using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    new void Start()
    {
        base.Start();

        Debug.Log("Tank started!");
    }

    public override float GetStrength()
    {
        return 20f;
    }

    public override void Attack(Unit target)
    {
        if (target.GetType() != GetType()) // Check if the target is of a different type
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
            {
                Debug.Log("Attacking at close range!");

                target.TakeDamage(GetStrength());
            }
        }
    }
}