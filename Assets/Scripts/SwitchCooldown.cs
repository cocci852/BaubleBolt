using UnityEngine;
using System.Collections;

public class SwitchCooldown : MonoBehaviour {

	public GameObject player;
	string loadedLevel;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		loadedLevel = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {
		if (player==null)
			player = GameObject.FindGameObjectWithTag ("Player");
		bool alphaON;
		Color texturecolor;
		if (loadedLevel == "PlayGame" || StartScreenButtons.normalMode)
			alphaON = player.GetComponent<PlayerMovement>().GetGravitySwitchOkay();
		else
			alphaON = player.GetComponent<PlayerMovementSimple>().GetGravitySwitchOkay();

		texturecolor = GetComponent<GUITexture>().color;

		if (alphaON)
			texturecolor.a = .5f;
		else
			texturecolor.a = .01f;

		GetComponent<GUITexture>().color = texturecolor;
	
	}
}
