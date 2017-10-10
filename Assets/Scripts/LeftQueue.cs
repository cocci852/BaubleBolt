using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeftQueue : MonoBehaviour {

	public static Queue<string> leftQ = new Queue<string>();
	
	public Texture2D[] QCard;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
}
