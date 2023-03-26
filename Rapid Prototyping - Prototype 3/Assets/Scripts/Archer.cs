using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 10f;

    private float attackTimer = 0f;
    private bool canAttack = true;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Archer started!");
    }

    protected override void Update()
    {
        base.Update();

        if (isMoving)
        {
            cooldownTimer -= Time.deltaTime;

            Unit target = GetNearestEnemy();

            if (target != null && InRange(target))
            {
                // Move away from the target
                Vector2 direction = transform.position - target.transform.position;
                transform.position = (Vector2)transform.position + direction.normalized * speed * Time.deltaTime;

                if (cooldownTimer <= 0)
                {
                    Attack(target);
                    cooldownTimer = attackCooldown;
                }
            }
            else if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            }
        }
    }

    public override float GetStrength()
    {
        return 10f;
    }

    public override void Attack(Unit target)
    {
        if (attackTimer > 0)
        {
            return;
        }

        isMoving = false;
        attackTimer = attackCooldown;

        Debug.Log("Attacking with ranged attack!");
        GameObject arrowObject = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Debug.Log($"Arrow object instantiated: {arrowObject != null}");
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.damage = GetStrength();

        if (target != null)
        {
            arrow.SetTarget(target);
        }

        StartCoroutine(WaitForAttackCooldown());
    }

    IEnumerator WaitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isMoving = true;
    }
}