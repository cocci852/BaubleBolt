using UnityEngine;
using System.Collections;

public class ShowSpeed : MonoBehaviour {

	public GameObject hero;

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
		GetComponent<GUIText>().text = "Speed: " + hero.GetComponent<Rigidbody2D>().velocity.magnitude;
	}
}
