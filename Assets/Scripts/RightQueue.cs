using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RightQueue : MonoBehaviour {

	public static Queue<string> rightQ = new Queue<string>(3);
	
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
