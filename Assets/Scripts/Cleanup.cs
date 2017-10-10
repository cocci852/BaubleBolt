using UnityEngine;
using System.Collections;

public class Cleanup : MonoBehaviour {
	public GameObject player;

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( player.transform.position.x, 0f, 0f);
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.transform.tag != "DeathWall" && col.transform.tag != "MoltenGlass")
			Destroy(col.gameObject);
	}
}
