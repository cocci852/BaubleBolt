using UnityEngine;
using System.Collections;

public class RightGUITopMask : MonoBehaviour {
	public GameObject player;
	float cooldownRemaining;
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
		
		if (loadedLevel == "PlayGame" || StartScreenButtons.normalMode)
			cooldownRemaining = player.GetComponent<PlayerMovement> ().GetCooldown ();
		else
			cooldownRemaining = player.GetComponent<PlayerMovementSimple> ().GetCooldown ();
		if (cooldownRemaining > 1)
			transform.position = new Vector2(0.5f,0.5f);
		else
			transform.position = new Vector2(cooldownRemaining*-0.5f+1f,0.5f);
		

	}

}
