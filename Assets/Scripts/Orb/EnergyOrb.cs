using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    public EnergyOrbColor color;

    public EnergyOrb(EnergyOrbColor colorInput)
    {
        this.color = colorInput;
    }

    public EnergyOrb()
    {
        //this.color = EnergyOrbColor.DefaultColor;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().color = OrbColorManagement.EocRgbColor[color];
    }

    private void Awake()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = OrbColorManagement.ToRgbColor(color);
    }

}
