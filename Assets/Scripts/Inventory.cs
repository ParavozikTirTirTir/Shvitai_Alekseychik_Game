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
    private MissionObject MO;
    public List<string> InventoryObjects = new List<string>();
    public Texture2D CloseButton;
    public bool MouseOnLabel = false;

    private MissionPlayer MP;
    void Start()
    {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>();
    }

    void OnGUI() //кнопка собрать
    {
        if (InventoryObjects[0] != "-")
        {
            if (GUI.Button(new Rect(100, 100, 20, 20), CloseButton))
            {
                Icon[0].sprite = Sprites[4];
                InventoryObjects[0] = "-";

                MP.LastAction = "Выброшен предмет [" + InventoryObjects[0] + "]";
            }
        }
    }
}