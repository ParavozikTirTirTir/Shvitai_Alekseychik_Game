using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Inventory : MonoBehaviour
{
    //public ТипДанных[] НазваниеМассива;
    public Image[] Icon; // слоты в инвентаре
	public Sprite[] Sprites; //просто пул из спрайтов всех объектов, которые мы крепим к иконке
    private MissionObject MO;
    public List<string> InventoryObjects = new List<string>();

    public int i = 0;
}