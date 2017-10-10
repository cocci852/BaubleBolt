using UnityEngine;
using System.Collections;

public class DeathWallMovement : MonoBehaviour {
	public float timeEqualHeroSpeed = 45f;
	public static float speedAdjust = 0f;
	public GameObject HeroDeathObject;
	GameObject player;
	public GameObject scoreKeeper;

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().transform.position = new Vector2 (-100f, 0f);
		if (scoreKeeper == null)
			scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper");
	}
	
	void Awake() {
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().transform.position = new Vector2 (-100f, 0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (PlayerMovement.isNotDead && PlayerMovementSimple.isNotDead)
			GetComponent<Rigidbody2D>().velocity = Vector2.right * (Mathf.Sqrt(transform.position.x+120) * (5f / (timeEqualHeroSpeed + speedAdjust)) + (transform.position.x+120)/(50 + speedAdjust));  //After X seconds, deathwall velocity will equal player starting maxspeed
		else
			GetComponent<Rigidbody2D>().velocity = Vector2.right*5f;
		
		transform.position = new Vector3(transform.position.x,0f,0f);
	}

}


//rigidbody2D.velocity = Vector2.right * Mathf.Sqrt((transform.position.x+120)) * (13f / (timeEqualHeroSpeed + speedAdjust));   old velocity equation
