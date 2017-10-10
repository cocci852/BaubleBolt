using UnityEngine;
using System.Collections;

public class MoltenGlass : MonoBehaviour {
	public GameObject HeroDeathObject;
	public GameObject scoreKeeper;
	
	
	GameObject player;
	public float DistanceBehindPlayer = 25f;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}

	// Use this for initialization
	void Start () {
		if (scoreKeeper == null)
			scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper");
		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x - transform.position.x > DistanceBehindPlayer)
			transform.position = new Vector3(player.transform.position.x - DistanceBehindPlayer,0f,0f);
	}
}
