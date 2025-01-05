using UnityEngine;
using System.Collections;
public class MissionObject : MonoBehaviour

{
    private MissionManager MM;
    public bool trigger = false;
    public string ObjectName;
    private Inventory Inv;
    private SpriteRenderer ThisObjectSprite;
    public int EmptyIndexInInventory;


    void Start() {
        MM = GameObject.FindGameObjectWithTag("MissionMan").GetComponent<MissionManager>();
        Inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        ThisObjectSprite = gameObject.GetComponent<SpriteRenderer>();
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
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // ���� ����� ����� � �������� � ����� �
        {
            //MP.IsObjectCollected = true;
            //Inv.Icon[Inv.i].sprite = ThisObjectSprite.sprite;
            //Inv.InventoryObjects.Add(ObjectName);

            if (Inv.InventoryObjects.Contains(ObjectName)) 
            {
                Inv.ItemCount[Inv.InventoryObjects.IndexOf(ObjectName)] += 1;
            }

            else
            {
                EmptyIndexInInventory = Inv.InventoryObjects.IndexOf("-");
                Inv.ItemCount[EmptyIndexInInventory] += 1;
                Inv.Icon[EmptyIndexInInventory].sprite = ThisObjectSprite.sprite;
                Inv.InventoryObjects.Insert(EmptyIndexInInventory, ObjectName);
                Inv.InventoryObjects.Remove("-");
            }

            MM.LastAction = "�������� [" + ObjectName + "]";

            Destroy(gameObject); // � ������� ���� ������ �� �����;
        }
    }

    void OnGUI() //������ �������
    {
        if (trigger) //����� �������� �� ������
        {
            GUI.Box(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 90, 25), "[E] �������");
        }
    }
}
