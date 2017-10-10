using UnityEngine;
using System.Collections;

public class TurboCooldownGUI : MonoBehaviour {
	public GameObject player;
	public GUIStyle turboTextStyle;
	//public GUIText TurboIndicator;
	float cooldownRemaining;
	float width = Screen.width/1920f;
	float height = Screen.height/1080f;
	string levelName;

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		turboTextStyle.fontSize = Mathf.RoundToInt(40*height);
		turboTextStyle.fontStyle = FontStyle.Bold;
		//turboTextStyle.font = 
		levelName = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		if (levelName == "PlayGame")
			GUI.Label(new Rect (Screen.width - width*80, 40f*height, 100f*width, 40f*height), PlayerMovement.turboStock.ToString(),turboTextStyle);
		else if (levelName == "PlayGameSimple")
			GUI.Label(new Rect (Screen.width - width*80, 40f*height, 100f*width, 40f*height), PlayerMovementSimple.turboStock.ToString(),turboTextStyle);
	}
}
