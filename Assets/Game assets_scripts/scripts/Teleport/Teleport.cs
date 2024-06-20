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
    public GameObject Obj;

    void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true) // При нажатии на клавишу Е и если игрок рядом с НПС
        {
            Obj = GameObject.FindGameObjectWithTag("Player");
            Obj.transform.position = point.transform.position;
            trigger = false;
            AudioManager2.instance.PlayMusic(music);
        }
    }
}
