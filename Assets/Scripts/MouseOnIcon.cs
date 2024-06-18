using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
<<<<<<< HEAD
=======
using UnityEngine.UIElements;
>>>>>>> 7c167a261aa8cf23c9d8634b9c7b7b0e03278fa0
using System;

public class MouseOnIcon : MonoBehaviour, IPointerDownHandler
{
    public bool IsCloseButtonVisible;
    private Inventory Inv;
    private MissionManager MM;
    public int ButtonIndex;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsCloseButtonVisible)
        {
            Inv.Icon[ButtonIndex].sprite = Inv.Sprites[4];
            MM.LastAction = "Выброшен предмет [" + Inv.InventoryObjects[ButtonIndex] + "]";
            Inv.InventoryObjects[ButtonIndex] = "-";
        }
    }

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
    }

    void Update()
    {

        if (Inv.CloseButtons[ButtonIndex].sprite == Inv.Sprites[0]) //если кнопка невидимая
        {
            IsCloseButtonVisible = false;
        }

        if (Inv.CloseButtons[ButtonIndex].sprite == Inv.Sprites[1]) //если кнопка видимая
        {
            IsCloseButtonVisible = true;
        }
    }
}
