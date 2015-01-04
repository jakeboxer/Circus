using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame () {
		Application.LoadLevel("Level01");
	}

	public void ExitGame () {
		Application.Quit();
	}

	public void ShowControls () {
		Application.LoadLevel("Controls");
	}
}
