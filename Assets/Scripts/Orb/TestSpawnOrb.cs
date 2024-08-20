using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnOrb : MonoBehaviour
{
    public Transform spawnPos;
    void Start()
    {

        InvokeRepeating("SpawnOrb", 2f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnOrb()
    {
        int indexColor = Random.Range(0, 3);   
        EnergyOrb newOrb = OrbPool.Instance.GetOrbEnergy((EnergyOrbColor) indexColor);

        newOrb.transform.position = this.transform.position;
        newOrb.gameObject.SetActive(true);
    }
}
