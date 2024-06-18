using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Inventory : MonoBehaviour
{
    public Image[] Icon; // слоты в инвентаре
    public Sprite[] Sprites; //просто пул из спрайтов всех объектов, которые мы крепим к иконке
    public Image[] CloseButtons;
    private MissionObject MO;
    private MissionManager MM;
    public List<string> InventoryObjects = new List<string>();
    public Texture2D CloseButton;

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
    }

    void OnGUI()
    {
        for (int i = 0; i < InventoryObjects.Count; i++)
        {
            CloseButtons[i].sprite = Sprites[0];

            if (InventoryObjects[i] != "-")
            {
                CloseButtons[i].sprite = Sprites[1];
            }
        }
    }
}