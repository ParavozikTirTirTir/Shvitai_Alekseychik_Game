using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionBot : MonoBehaviour
{
    public bool trigger = false;
    public bool CanGiveAnItem;
    public bool vis; // переменная, которая будет отображать диалог между персонажами
    public bool MissionDone = false; //переменная, которая определеяет, что квест уже сделан
    public string MissionName; // Текст который будет отображать наименование квеста
    public string MissionDialoge; // Текст диалога в квесте
    public string MissionDialogeDone; // Текст диалога в квесте
    public string MissionObjectName; //Квестовый предмет

    public string RewardName; //Квестовый предмет
    public Sprite RewardSprite;

    private MissionPlayer MP; // подключаем скрипт MissionPlayer
    private Inventory Inv;
    public int ObjectIndexInInventory;
    public int EmptyIndexInInventory;

    void Start()
    {
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionPlayer>(); // определяем что скрипт MissionPlayer будет находится на персонаже с тэгом player;
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerStay2D(Collider2D obj) //игрок рядом с НПС
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //игрок отошел от НПС
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update()
    {
        GameObject MissionTagScanner = GameObject.FindGameObjectWithTag("Player"); // персонаж у которого берем квест будет взаимодействовать только с тем объектом у которого тэг Player;

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone) // При нажатии на клавишу Е и если игрок рядом с НПС
        {
            vis = true;
        }
    }

    void OnGUI()
    {
        if (vis)
        {
            if (!MP.MissionsInProgress.Contains(MissionName) && !MissionDone) //если квест еще не взят и не выполнен;
            {
                GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), MissionName); //на экране отображается окно с названием Квест;
                GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 20, 290, 250), MissionDialoge); //текстом описывает квест;
                if (GUI.Button(new Rect((Screen.width - 100) / 2 - 25, (Screen.height - 300) / 2 + 250, 150, 40), "Принять квест")) // при нажатии на кнопку Ok;
                {
                    MP.MissionsInProgress.Add(MissionName); // название квеста;
                    vis = false; // все диалоговые окна закрываются;

                    MP.LastAction = "Принят квест [" + MissionName + "]";
                }
            }

            if (MP.MissionsInProgress.Contains(MissionName) && !MissionDone) // если квест уже взят, но не завершен;
            {
                GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), MissionName);
                GUI.Label(new Rect((Screen.width - 300) / 2 + 10, (Screen.height - 300) / 2 + 20, 290, 250), MissionDialogeDone); //то описание квеста меняется на другой текст;
                if (Inv.InventoryObjects.Contains(MissionObjectName))
                {
                    if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 300) / 2 + 250, 100, 40), "Да")) // то появится кнопка да, при нажатии на которую;
                    {
                        ObjectIndexInInventory = Inv.InventoryObjects.IndexOf(MissionObjectName);
                        Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
                        Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
                        Inv.InventoryObjects.Remove(MissionObjectName);

                        MP.LastAction = "Закончен квест [" + MissionName + "]";
                        MP.MissionsInProgress.Remove(MissionName); // убираем квест из списка активных

                        if (CanGiveAnItem)
                        {
                            EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                            Inv.Icon[EmptyIndexInInventory].sprite = RewardSprite;
                            Inv.InventoryObjects.Insert(EmptyIndexInInventory, RewardName);
                            Inv.InventoryObjects.Remove("-");

                            MP.LastAction = "Закончен квест [" + MissionName + "] и получен предмет [" + RewardName + "]";
                        }

                        MP.Money = MP.Money + 100; //добавление денег за выполнение квеста;
                        vis = false; // диалоговое окно закрывается;
                        MissionDone = true;
                    }
                }

                else
                { // если вы еще не подобрали объект;
                    if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 300) / 2 + 250, 100, 40), "Нет")) // то вместо кнопки да, будет кнопка нет;
                    {
                        vis = false; // при нажатии на которую, окно просто закроется;
                    }
                }
            }
        }
    }
}
   