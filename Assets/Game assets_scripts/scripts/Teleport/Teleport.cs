using NUnit;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class Teleport : MonoBehaviour
{
    public Transform point;
    public string music;
    public  bool trigger;
    private GameObject Obj;
    void Start()
    {
        Obj = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            trigger = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = false;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // При нажатии на клавишу Е и если игрок рядом с НПС
        {
            Obj.transform.position = point.transform.position;
            trigger = false;
            AudioManager2.instance.PlayMusic(music);
        }
    }
}
