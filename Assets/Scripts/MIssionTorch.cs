using UnityEngine;
using System.Collections;
public class MissionTorch : MonoBehaviour

{
    private MissionManager MM;
    private SpellsList SL;
    public bool trigger = false;
    private SpriteRenderer TorchOff;
    public GameObject Fire;
    public bool IsOn;
    public GameObject Chest;


    void Start() {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        SL = GameObject.Find("MagicBookCanvas").GetComponent<SpellsList>();
        TorchOff = gameObject.GetComponent<SpriteRenderer>();
        IsOn = false;
        Fire.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D obj) //������ �� ������
    {
        if (obj.tag == "Player")
        {
            trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj) //���� ����� �� ������
    {
            if (obj.tag == "Player")
        {
            trigger = false;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !IsOn && SL.Spells.Contains("��� �������� �����")) // ���� ����� ����� � �������� � ����� �
        {
            MM.LastAction = "����� ������";
            Fire.SetActive(true);
            IsOn = true;
            Chest.GetComponent<ChestTorch>().NumberTorchesOn += 1;
        }

        if (Input.GetKeyDown(KeyCode.E) && trigger == true && !IsOn && !SL.Spells.Contains("��� �������� �����")) // ���� ����� ����� � �������� � ����� �
        {
            MM.LastAction = "�� �� �������� ����������� �����������, ����� ������ �����";
        }
    }

    void OnGUI() //������ �������
    {
        if (trigger && !IsOn) //����� �������� �� ������
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] ������");
        }
    }
}
