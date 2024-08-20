using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class OrbPool : MonoBehaviour
{
    public int initPoolSize;
    public static OrbPool Instance { get; set; }

    public EnergyOrb energyOrbPrefab;

    private Transform orbPoolContainer;

    private List<EnergyOrb> pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }

        orbPoolContainer = gameObject.transform;
        pool = new List<EnergyOrb>();
        InitializeOrbPool();
    }

    void InitializeOrbPool()
    {
        for (int i = 0; i < initPoolSize; i++) 
        {
            NewEnergyOrbInPool(EnergyOrbColor.DefaultColor);
        }
    }

    public EnergyOrb GetOrbEnergy(EnergyOrbColor color)
    {
        foreach (EnergyOrb energyOrb in pool)
        {
            if (!energyOrb.gameObject.activeSelf) 
            {
                energyOrb.color = color;
                return energyOrb;
            }
        }

        return NewEnergyOrbInPool(color);
    }

    public EnergyOrb NewEnergyOrbInPool(EnergyOrbColor color)
    {
        EnergyOrb newOrb = Instantiate(energyOrbPrefab, orbPoolContainer);
        newOrb.gameObject.SetActive(false);
        newOrb.color = color;
        pool.Add(newOrb);

        return newOrb;
    }

    public void ReturnEnergyOrb(EnergyOrb energyOrb)
    {
        energyOrb.color = EnergyOrbColor.DefaultColor;
        energyOrb.gameObject.SetActive(false);
    }
}
