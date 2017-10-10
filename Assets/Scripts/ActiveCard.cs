using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveCard : MonoBehaviour {

	//public static Queue<string> ActiveQ = new Queue<string>();
	
	public static bool flipGui = false;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		if (flipGui)
		{
			Vector3 Scale = new Vector3(-1f,1f,1f);
			transform.localScale = Scale;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
