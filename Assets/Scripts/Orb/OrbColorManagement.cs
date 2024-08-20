using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// quy ước thứ tự các orb trong kho như sau.
public enum EnergyOrbColor
{
    Yellow,
    Green,
    Orange,
    Red,
    DefaultColor
}
public static class OrbColorManagement
{
    public static readonly Dictionary<EnergyOrbColor, Color> EocRgbColor = new Dictionary<EnergyOrbColor, Color>
    {
        { EnergyOrbColor.Yellow, new Color(243 / 255f, 229 / 255f, 123 / 255f) }, // RGB for Yellow
        { EnergyOrbColor.Green, new Color(141 / 255f, 236 / 255f, 101 / 255f) }, // RGB for Green
        { EnergyOrbColor.Orange, new Color(255 / 255f, 183 / 255f, 90 / 255f) }, // RGB for Orange
        { EnergyOrbColor.Red, new Color(243 / 255f, 123 / 255f, 123 / 255f) }, // RGB for Red
        { EnergyOrbColor.DefaultColor, Color.white } // RGB for Red

    };
}
