using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player,
    Enemy
}

public abstract class Unit : MonoBehaviour
{
    public Faction faction;

    public float baseStrength = 10f;
    public float baseHealth = 100f;

    private float strength;
    [SerializeField] private float health;

    public float attackRange = 1f;

    private int numUnits;

    public float speed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    public Vector2 targetPosition;
    public Vector2 startPosition;

    protected float cooldownTimer;
    public bool isMoving = false;
    public float attackCooldown = 1f;
    public float attackDamage = 5f;

    private Vector2 moveTarget;

    protected virtual void Start()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        numUnits = units.Length;

        strength = baseStrength * Mathf.Sqrt(numUnits);
        health = baseHealth * Mathf.Sqrt(numUnits);

        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (!isMoving)
        {
            return;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        Unit target = GetNearestEnemy();
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            Attack(target);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
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
        if (cooldownTimer > 0f)
        {
            return;
        }

        float damage = GetStrength();
        target.TakeDamage(damage);
        cooldownTimer = attackCooldown;
    }

    public Unit GetNearestEnemy()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        Unit nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Unit unit in units)
        {
            if (unit == this || unit.faction == faction)
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