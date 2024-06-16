using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionPlayer : MonoBehaviour
{
    public int Money; // количество денег;
    public Texture2D Coin;
    private MissionObject MO;
    private Inventory Inv;
    public bool IsObjectCollected;
    public string LastAction = "";
    public List<string> MissionsInProgress = new List<string>(20); //массив из квестов

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D obj) //персонаж взаимодействует с объектом
    {
        MO = obj.GetComponent<MissionObject>();//получаем инфу из объекта, на который наступили
    }

    void OnGUI()
    {
        GUI.Label(new Rect(42, 47, 1000, 30), "" + Money);
        GUI.Label(new Rect(20, 45, 25, 25), Coin);

        if (MissionsInProgress.Count > 0)
        {
            GUI.Label(new Rect(20, 80, 300, 300), MissionsInProgress[0] + "\n"); // значение названия квеста будет браться из скрипта Misson Bot;
        }

        if (MissionsInProgress.Count > 1)
        {
            GUI.Label(new Rect(20, 80, 300, 300), MissionsInProgress[0] + "\n" + MissionsInProgress[1]); // значение названия квеста будет браться из скрипта Misson Bot;
        }

        GUI.Label(new Rect(5, Screen.height - 25, 1000, 25), LastAction);

        //if (IsObjectCollected)
        //{
        //    GUI.Label(new Rect(5, Screen.height - 25, 400, 25), "Получено [" + MO.ObjectName + "]");
        //}
    }
}