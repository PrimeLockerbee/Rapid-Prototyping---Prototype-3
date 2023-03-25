using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected float baseStrength = 10f;
    protected float baseHealth = 100f;

    private float strength;
    private float health;

    public float attackRange = 1f;

    private int numUnits;

    public float speed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector2 targetPosition;

    protected float cooldownTimer;
    private bool isMoving = false;
    public float attackCooldown = 1f;
    public float attackDamage = 5f;

    private Vector2 moveTarget;

    protected virtual void Start()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        numUnits = units.Length;

        strength = baseStrength * Mathf.Sqrt(numUnits);
        health = baseHealth * Mathf.Sqrt(numUnits);

        targetPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (!isMoving)
        {
            return;
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            Unit target = GetNearestEnemy();

            if (target != null && Vector2.Distance(transform.position, target.transform.position) <= attackRange)
            {
                target.TakeDamage(attackDamage);
                cooldownTimer = attackCooldown;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
    }


    public void StartMoving()
    {
        isMoving = true;
    }

    protected virtual void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public virtual float GetStrength()
    {
        return strength;
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public bool InRange(Unit otherUnit)
    {
        return Vector2.Distance(transform.position, otherUnit.transform.position) <= attackRange;
    }

    public virtual void Attack(Unit target)
    {
        float damage = GetStrength();
        target.TakeDamage(damage);
    }

    private Unit GetNearestEnemy()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        Unit nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Unit unit in units)
        {
            if (unit == this || unit.GetType() == GetType())
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, unit.transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = unit;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }
}