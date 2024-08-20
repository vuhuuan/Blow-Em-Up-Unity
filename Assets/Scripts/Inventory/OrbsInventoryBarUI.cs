using System.Collections;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class OrbsInventoryBarUI : MonoBehaviour
{
    [SerializeField] private OrbSlotUI[] orbSlots;

    private int currentSelectedSlotIndex;

    //public int SelectedSlotIndex {  get { return currentSelectedSlotIndex; } }

    private void Start()
    {

    }

    private void Awake()
    {
        InitializeHighlightIndicator();
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0) 
        {
            HighlightSelected(scroll);
        }
    }

    void HighlightSelected(float scroll)
    {
        orbSlots[currentSelectedSlotIndex].UnHighlight();

        int nextSlotIndex = (currentSelectedSlotIndex - (int)Mathf.Sign(scroll)) % orbSlots.Length;
        if (nextSlotIndex < 0) nextSlotIndex = orbSlots.Length - 1;

        orbSlots[nextSlotIndex].Highlight();

        currentSelectedSlotIndex = nextSlotIndex;
    }

    public void UpdateOrbUI(EnergyOrbColor color, int quantity)
    {
        orbSlots[(int)color].UpdateQuantityUI(quantity);
    }

    private void InitializeHighlightIndicator()
    {
        currentSelectedSlotIndex = 0;

        for (int i = 0; i < orbSlots.Length; i++)
        {
            if (i == currentSelectedSlotIndex)
            {
                orbSlots[i].Highlight();
            } else
            {
                orbSlots[i].UnHighlight();
            }
        }
    }

    public Color GetCurrentSelectedOrbColor()
    {
        return OrbColorManagement.EocRgbColor[(EnergyOrbColor)currentSelectedSlotIndex];
    }

    public int GetCurrentSelectedOrbIndex()
    {
        return currentSelectedSlotIndex;
    }
}
