﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Button[] buttons;
	private GameObject panel;
	private GameObject game_object_text;

	void Awake(){
		
		panel = GameObject.FindGameObjectWithTag("panel");
		game_object_text = GameObject.FindGameObjectWithTag("game_over_text");
		// at the beginging get the buttons
	
		buttons = GetComponentsInChildren<Button>();

		// Disable the buttons
		HideButtons();
	}

	public void HideButtons() {
		panel.SetActive(false);
		game_object_text.SetActive(false);
		//hide each button
		foreach (var b in buttons)
		{
			b.gameObject.SetActive(false);
		}
	}

	public void ShowButtons() {
		panel.SetActive(true);
		game_object_text.SetActive(true);
		//show each button
		foreach (var b in buttons)
		{
			b.gameObject.SetActive(true);
		}
	}
	
    public void Restart() {
    	Application.LoadLevel("MainScene");
    }
}