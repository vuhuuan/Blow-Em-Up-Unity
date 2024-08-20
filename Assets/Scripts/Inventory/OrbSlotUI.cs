using System.Collections;
using TMPro;
using UnityEngine;


public class OrbSlotUI : MonoBehaviour
{
    public TextMeshProUGUI quantityUI;

    public Transform highlightIndicator;

    private void Awake()
    {
        if (quantityUI == null )
        {
            quantityUI = transform.Find("Quantity Text").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateQuantityUI(int quantity)
    {
        quantityUI.text = quantity.ToString();
    }

    public void Highlight()
    {
        highlightIndicator.gameObject.SetActive(true);
    }

    public void UnHighlight()
    {
        highlightIndicator.gameObject.SetActive(false);
    }

}
