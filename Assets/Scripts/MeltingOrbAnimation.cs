using UnityEngine;
using System.Collections;

public class MeltingOrbAnimation : MonoBehaviour {

	void awake() {
/*		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;  */
	}
	
	// Use this for initialization
	void Start () {
		Destroy(gameObject,3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
