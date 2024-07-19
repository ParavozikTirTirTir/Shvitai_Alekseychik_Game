using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class MissionBot : MonoBehaviour
{
    public bool trigger = false;
    public bool vis;
    public bool CanGiveAnItem;
    public bool MissionDone = false; //����������, ������� �����������, ��� ����� ��� ������
    public string NpcName;
    public string MissionName; // ����� ������� ����� ���������� ������������ ������
    public string MissionDialoge; // ����� ������� � ������
    public string MissionDialogeDone; // ����� ������� � ������
    public string MissionObjectName; //��������� �������
    public string MissionPriority = "1";
    public string MissionInformation; //���������� � ������, ������� ��������� ��� ������� �� ������ � ����� ������� ����

    public string RewardName; //��������� �������
    public Sprite RewardSprite;
    public Texture2D[] Emotions;

    private MissionManager MM;
    private Inventory Inv;
    private PlayerController PC;
    private PlayerCombatController PCC;
    private IsPlayerInDialoge PinD;
    private int ObjectIndexInInventory;
    private int EmptyIndexInInventory;
    public int MoneyForMission;

    void Start()
    {
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
    }

    void OnTriggerStay2D(Collider2D obj) //����� ����� � ���
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //����� ������ �� ���
    {
        if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update()
    {
        GameObject MissionTagScanner = GameObject.FindGameObjectWithTag("Player"); // �������� � �������� ����� ����� ����� ����������������� ������ � ��� �������� � �������� ��� Player;

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !MissionDone) // ��� ������� �� ������� � � ���� ����� ����� � ���
        {
            vis = true;
            PinD.InDialoge = true;
        }
    }

    void DialogeState()
    {
        PC.movementSpeed = 0;
        PC.jumpForce = 0;
        PC.dashSpeed = 0;
        PCC.combatEnabled = false;
        MM.LastAction = "";
    }

    void DialogeExit()
    {
        PC.movementSpeed = 7;
        PC.jumpForce = 16;
        PC.dashSpeed = 20;
        PCC.combatEnabled = true;
    }

    void OnGUI()
    {
        if (vis)
        {
            DialogeState();

            GUI.Box(new Rect( //���� �����
                    15,
                    Screen.height * 2 / 4 - 15,
                    Screen.height * 2 / 4,
                    Screen.height * 2 / 4),
                    Emotions[0]);

            if (!MM.MissionsInProgress.Contains(MissionName) && !MissionDone) //���� ����� ��� �� ���� � �� ��������;
            {
                GUI.Box(new Rect( //������ � ������
                    30 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 - 15,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4),
                    Screen.height / 4),
                    NpcName); //�� ������ ������������ ���� � ��������� ������;
                GUI.Label(new Rect( // ����� �����
                    40 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 + 20,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4) - 10,
                    250),
                    MissionDialoge); //������� ��������� �����;
                if (GUI.Button(new Rect( // ����� 1
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15,
                    200,
                    Screen.height / 4 / 4),
                    "������� �������")) // ��� ������� �� ������ Ok;
                {
                    MM.MissionsInProgress.Add(MissionName);
                    MM.MissionsPriority.Add(MissionPriority);
                    MM.MissionsInformation.Add(MissionInformation);
                    MM.MissionsObjectName.Add(MissionObjectName);
                    vis = false; // ��� ���������� ���� �����������;
                    PinD.InDialoge = false;

                    MM.LastAction = "������ ����� [" + MissionName + "]";
                    DialogeExit();
                }

                if (GUI.Button(new Rect( // ����� 2
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15 + Screen.width / 4 / 20 + Screen.height / 4 / 4,
                    200,
                    Screen.height / 4 / 4),
                    "�� ������"))
                {
                    vis = false;
                    PinD.InDialoge = false;
                    DialogeExit();
                }
            }

            if (MM.MissionsInProgress.Contains(MissionName) && !MissionDone) // ���� ����� ��� ����, �� �� ��������;
            {
                GUI.Box(new Rect( //������ � ������
                    30 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 - 15,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4),
                    Screen.height / 4),
                    NpcName);
                GUI.Label(new Rect( // ����� �����
                    40 + Screen.height * 2 / 4,
                    Screen.height * 3 / 4 + 20,
                    Screen.width - (30 + Screen.height * 2 / 4) - 15 - (Screen.width / 4) - 10,
                    250),
                    MissionDialogeDone); //�� �������� ������ �������� �� ������ �����;
                if (Inv.InventoryObjects.Contains(MissionObjectName))
                {
                    if (GUI.Button(new Rect( // ����� 1
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15,
                    200,
                    Screen.height / 4 / 4),
                    "������ �������")) // �� �������� ������ ��, ��� ������� �� �������;
                    {
                        ObjectIndexInInventory = Inv.InventoryObjects.IndexOf(MissionObjectName);
                        Inv.Icon[ObjectIndexInInventory].sprite = Inv.Sprites[4];
                        Inv.InventoryObjects.Insert(ObjectIndexInInventory, "-");
                        Inv.InventoryObjects.Remove(MissionObjectName);

                        MM.LastAction = "�������� ����� [" + MissionName + "]";
                        MM.MissionsInProgress.Remove(MissionName); // ������� ����� �� ������ ��������
                        MM.MissionsPriority.Remove(MissionPriority);
                        MM.MissionsInformation.Remove(MissionInformation);
                        MM.MissionsObjectName.Remove(MissionObjectName);

                        if (CanGiveAnItem)
                        {
                            EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                            Inv.Icon[EmptyIndexInInventory].sprite = RewardSprite;
                            Inv.InventoryObjects.Insert(EmptyIndexInInventory, RewardName);
                            Inv.InventoryObjects.Remove("-");

                            MM.LastAction = "�������� ����� [" + MissionName + "] � ������� ������� [" + RewardName + "]";
                        }

                        MM.Money = MM.Money + MoneyForMission; //���������� ����� �� ���������� ������;
                        vis = false; // ���������� ���� �����������;
                        PinD.InDialoge = false;
                        MissionDone = true;

                        DialogeExit();
                    }
                }

                else
                { // ���� �� ��� �� ��������� ������;
                    if (GUI.Button(new Rect( // ����� 1
                    Screen.width * 3 / 4,
                    Screen.height * 3 / 4 - 15,
                    200,
                    Screen.height / 4 / 4),
                    "� ����� �����")) // �� ������ ������ ��, ����� ������ ���;
                    {
                        vis = false; // ��� ������� �� �������, ���� ������ ���������;
                        PinD.InDialoge = false;

                        DialogeExit();
                    }
                }
            }
        }
    }
}
   