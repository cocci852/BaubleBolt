using UnityEngine;
using System.Collections;

public class CameraMovementStartScreen : MonoBehaviour {
	
	public GameObject target;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10f);
	}
	
	// Update is called once per frame at the end
	void FixedUpdate () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10f);
		
	}
}
