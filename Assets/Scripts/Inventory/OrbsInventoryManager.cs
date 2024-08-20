using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class OrbsInventoryManager : MonoBehaviour
{
    private Dictionary<EnergyOrbColor, int> inventory;
    public Dictionary<EnergyOrbColor, int> Inventory { get { return inventory; } }

    public OrbsInventoryBarUI orbsInventoryBarUI;

    public static OrbsInventoryManager Instance;

    public static int super;


    private void Awake()
    {
        // reference to UI
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (orbsInventoryBarUI == null)
        {
            orbsInventoryBarUI = GameObject.Find("Inventory UI").GetComponent<OrbsInventoryBarUI>();
        }

        InitializeInventory();
    }

    private void Update()
    {

    }

    public void ClearOrbsInventory()
    {
        inventory.Clear();
    }

    public void AddEnergyOrbs(EnergyOrbColor color)
    {

        if (inventory.ContainsKey(color))
        {
            inventory[color]++;
        }
        else
        {
            Debug.Log("This orb has not been supported yet!");
            inventory[color] = 1;
        }

        // update UI;
        orbsInventoryBarUI.UpdateOrbUI(color, inventory[color]);
    }

    public bool ConsumeEnergy(EnergyOrbColor color)
    {

        if (inventory.ContainsKey(color) && inventory[color] >= 1)
        {
            inventory[color]--;

            // update UI;
            orbsInventoryBarUI.UpdateOrbUI(color, inventory[color]);
            return true;
        }


        return false;
    }
    public void ShowInventory()
    {
        foreach (var orb in inventory)
        {
            Debug.Log($"Energy Orb {orb.Key}: {orb.Value}");
        }
    }

    private void InitializeInventory()
    {
        if (inventory == null)
        {
            inventory = new Dictionary<EnergyOrbColor, int>();
            inventory[EnergyOrbColor.Yellow] = 100;
            inventory[EnergyOrbColor.Green] = 100;
            inventory[EnergyOrbColor.Orange] = 100;
            inventory[EnergyOrbColor.Red] = 100;
        }

    }
}

