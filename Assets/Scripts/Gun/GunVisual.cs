using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer EnergyPill;
    [SerializeField] private GunConnector gunConnector;


    private void Awake()
    {
        EnergyPill = transform.Find("Energy Pill").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        EnergyPill.color = OrbsInventoryManager.Instance.orbsInventoryBarUI.GetCurrentSelectedOrbColor();
    }

    public Color GetEnergyPillColor()
    {
        return EnergyPill.color;
    }
}
