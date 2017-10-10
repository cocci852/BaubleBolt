using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	public GUIStyle title;
	public GUIStyle textLabel;
	public GUIStyle hyperlink;
	public GUIStyle flatButtons;
	
	float widthUnit = Screen.width/1920f;
	float heightUnit = Screen.height/1080f;

	float ButtonWidth = 6*Screen.width/32f;			//360*widthUnit
	float ButtonHeight = 3*Screen.height/32f;		//180*heightUnit
			
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		title.fontSize = Mathf.FloorToInt(60*widthUnit);
		textLabel.fontSize = Mathf.FloorToInt(40*widthUnit);
		hyperlink.fontSize = Mathf.FloorToInt(50*widthUnit);
		flatButtons.fontSize = Mathf.FloorToInt(40*widthUnit);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("StartScreen"); 
	}
	
	void OnGUI() {
		GUI.Label(new Rect(60*widthUnit,60*heightUnit,200*widthUnit,70*heightUnit),"CREDITS",title);
		
		GUI.Label(new Rect(120*widthUnit,160*heightUnit,1800*widthUnit,70*heightUnit),"MUSIC: MAIN MENU",textLabel);
		GUI.Label(new Rect(120*widthUnit,210*heightUnit,1800*widthUnit,70*heightUnit),"\"New Friendly\" Kevin MacLeod (incompetech.com)",textLabel);
		GUI.Label(new Rect(120*widthUnit,260*heightUnit,1800*widthUnit,70*heightUnit),"Licensed under Creative Commons: By Attribution 3.0",textLabel);
		
		GUI.Label(new Rect(120*widthUnit,350*heightUnit,1800*widthUnit,70*heightUnit),"MUSIC: DURING GAME",textLabel);
		GUI.Label(new Rect(120*widthUnit,400*heightUnit,1800*widthUnit,70*heightUnit),"\"Show Your Moves\" Kevin MacLeod (incompetech.com)",textLabel);
		GUI.Label(new Rect(120*widthUnit,450*heightUnit,1800*widthUnit,70*heightUnit),"Licensed under Creative Commons: By Attribution 3.0",textLabel);
		
		GUI.Label(new Rect(120*widthUnit,540*heightUnit,1800*widthUnit,70*heightUnit),"MUSIC: GAMEOVER",textLabel);
		GUI.Label(new Rect(120*widthUnit,590*heightUnit,1800*widthUnit,70*heightUnit),"\"Industrial Music Box\" Kevin MacLeod (incompetech.com)",textLabel);
		GUI.Label(new Rect(120*widthUnit,640*heightUnit,1800*widthUnit,70*heightUnit),"Licensed under Creative Commons: By Attribution 3.0",textLabel);
		
		
		if (GUI.Button(new Rect(120*widthUnit,920*heightUnit,1800*widthUnit,120*heightUnit),"http://creativecommons.org/licenses/by/3.0/",hyperlink))
		{
			Application.OpenURL("http://creativecommons.org/licenses/by/3.0/");
		}
		
		if (GUI.Button(new Rect(1400*widthUnit,820*heightUnit,ButtonWidth,ButtonHeight),"BACK",flatButtons))
		{
			Application.LoadLevel("StartScreen");
		}
		
	}
}
