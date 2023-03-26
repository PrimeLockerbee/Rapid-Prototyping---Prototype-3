using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 10f;
    private Unit target;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit != null && collision.gameObject != target.gameObject)
        {
            unit.TakeDamage(damage);
        }

        //Destroy(gameObject);
    }

    public void SetTarget(Unit target)
    {
        this.target = target;
    }
}
