﻿using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame () {
		Application.LoadLevel("BasicPlatformStaging");
	}

	public void ExitGame () {
		Application.Quit();
	}
}
