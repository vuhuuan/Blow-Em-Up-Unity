using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunShooter : MonoBehaviour
{
    [SerializeField] private GunConnector gunConnector;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ExecuteSkill((EnergyOrbColor) OrbsInventoryManager.Instance.orbsInventoryBarUI.GetCurrentSelectedOrbIndex());
        }
    }
    public void ExecuteSkill (EnergyOrbColor color)
    {
        if (OrbsInventoryManager.Instance.ConsumeEnergy(color))
        {
            Bullet bullet = BulletPool.Instance.GetBullet((BulletType) ((int) color));
            bullet.Shoot(gameObject);
        }
        else
        {
            Debug.Log("Empty energy orbs");
        }
    }
    private Vector3 GetShootingDirection()
    {
        // Calculate shooting direction based on mouse position or other factors
        return transform.right;
    }
}


