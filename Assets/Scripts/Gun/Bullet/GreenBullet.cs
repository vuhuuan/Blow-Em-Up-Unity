using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class GreenBullet : Bullet
{
    [SerializeField] private Shield shieldPrefab;

    public float distanceThreshold = 0.1f; // Ngưỡng khoảng cách để kích hoạt sự kiện
    private Vector3 targetPosition;
    private bool targetLocked = false;
    private bool converted = false;



    private void Update()
    { 
        if (targetLocked)
        {

            if (Vector3.Distance(transform.position, targetPosition) <= distanceThreshold)
            {
                ConvertToShield();
            }
        }
    }

    public override void Shoot(GameObject source)
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        targetLocked = true;
        base.Shoot(source);
    }

    private void ConvertToShield()
    {
        if (converted) return;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(shieldPrefab, transform.position, transform.rotation);
        converted = true;
    }

    void InitGreenBullet()
    {
        targetLocked = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ConvertToShield();
        base.OnCollisionEnter2D(collision);
    }
}
