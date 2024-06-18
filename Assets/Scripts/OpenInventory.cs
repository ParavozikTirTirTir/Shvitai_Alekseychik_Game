using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class OpenInventory : MonoBehaviour
{
	private Canvas canvas;
    public bool OpenInventoryCheck = false;

    public PlayerController PC;
    public PlayerCombatController PCC;
    public IsPlayerInDialoge PinD;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        PinD = GameObject.FindGameObjectWithTag("Player").GetComponent<IsPlayerInDialoge>();
    }

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.I) && !PinD.InDialoge)
		{
			canvas.enabled = !canvas.enabled;
            OpenInventoryCheck = !OpenInventoryCheck;
        }

        if (OpenInventoryCheck) DialogeState();
        else DialogeExit();
    }

    void DialogeState()
    {
        PC.movementSpeed = 0;
        PC.jumpForce = 0;
        PC.dashSpeed = 0;
        PCC.combatEnabled = false;
    }

    void DialogeExit()
    {
        PC.movementSpeed = 7;
        PC.jumpForce = 16;
        PC.dashSpeed = 20;
        PCC.combatEnabled = true;
    }
}
