using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class OpenInventory : MonoBehaviour
{
	private Canvas canvas;
    private MissionPlayer MP;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			canvas.enabled = !canvas.enabled;
			//Close.SetActive(true);
		}
	}
}
