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
    private MissionPlayer MP;
    //private Canvas canvas;
    public List<string> InventoryObjects = new List<string>();
    public Texture2D CloseButton;

    void Start()
    {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>();
    }

    void OnGUI() //кнопка собрать
    {
        for (int i = 0; i < InventoryObjects.Count; i++)
        {

            if (InventoryObjects[i] != "-")
            {
                if (GUI.Button(new Rect(50, 50, 30, 30), CloseButton))
                {
                    Icon[i].sprite = Sprites[4];
                    InventoryObjects[i] = "-";

                    MP.LastAction = "Выброшен предмет [" + InventoryObjects[i] + "]";
                }
            }
        }
    }
}