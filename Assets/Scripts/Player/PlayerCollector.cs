using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Orb")
        {
            CollectOrb(collision.gameObject.GetComponent<EnergyOrb>());
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Orb")
    //    {
    //        CollectOrb(collision.gameObject.GetComponent<EnergyOrb>());
    //    }
    //}

    private void CollectOrb(EnergyOrb orb)
    {
        OrbsInventoryManager.Instance.AddEnergyOrbs(orb.color);
        OrbPool.Instance.ReturnEnergyOrb(orb);
    }
}
