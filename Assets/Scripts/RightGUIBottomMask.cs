using UnityEngine;
using System.Collections;

public class RightGUIBottomMask : MonoBehaviour {
	public GameObject player;
	float cooldownRemaining;
	Color texturecolor;
	string loadedLevel;
	float defaultAlpha;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		defaultAlpha = GetComponent<GUITexture>().color.a;
		loadedLevel = Application.loadedLevelName;
		if (player==null)
			player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float alphaValue;
		
		if (loadedLevel == "PlayGame" || StartScreenButtons.normalMode)
			cooldownRemaining = player.GetComponent<PlayerMovement> ().GetCooldown ();
		else
			cooldownRemaining = player.GetComponent<PlayerMovementSimple> ().GetCooldown ();
		
		
		if (cooldownRemaining >= 1)
			alphaValue = defaultAlpha;
		else 
			alphaValue = 0f;
		texturecolor = GetComponent<GUITexture>().color;
		texturecolor.a = alphaValue;
		
		GetComponent<GUITexture>().color = texturecolor;
	}
	
}
