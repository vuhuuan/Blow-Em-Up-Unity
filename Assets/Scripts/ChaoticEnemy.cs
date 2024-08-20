using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoticEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 4f;

    private void Start()
    {
    }

    private void Update()
    {
        LookAtTarget();
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void LookAtTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
