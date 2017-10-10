using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControllerFodder : MonoBehaviour {
	public float health = 1f;
	public int pointValue = 1;
	public GameObject scoreKeeper;
	Vector2 initialVelocity = Vector2.zero;
	bool didCollideWithPlayer = false;
	public GameObject deathObject;
	public GameObject deathObjectMelt;
	public AudioClip CollisionSound;
	
	string levelName;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		if (scoreKeeper == null)
			scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper");
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2,0);
		levelName = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Rigidbody2D>().velocity != initialVelocity)
		{
			if (didCollideWithPlayer)
			{
				Vector2 deltaVelocity = gameObject.GetComponent<Rigidbody2D>().velocity - initialVelocity;
				health -= deltaVelocity.magnitude;
				if (health <= 0)
				{
					Instantiate(deathObject,transform.position,transform.rotation);
					Destroy(gameObject);
					scoreKeeper.GetComponent<SCoreKeeper>().EnemyScoreAdd(pointValue);
					
					if (levelName == "PlayGame")
						PlayerMovement.enemiesKilled += 1;
					else
						PlayerMovementSimple.enemiesKilled += 1;
					
					if (ActiveCardQueue.ActiveCardQ.Count < 8 && levelName == "PlayGame")
						ActiveCardQueue.ActiveCardQ.Add("fodder");
					else
					{
						if (levelName == "PlayGame")
							PlayerMovement.maxSpeed += 0.05f;
						else
							PlayerMovementSimple.maxSpeed += 0.05f;
					}
					
				}
				else
				{
					GetComponent<AudioSource>().volume = 0.2f;
					GetComponent<AudioSource>().PlayOneShot(CollisionSound);
                }
			}
			initialVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
			didCollideWithPlayer = false;
		}
	
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player")
		{
			didCollideWithPlayer = true;

		}
	}
	
	void KilledByLava() {
		Instantiate(deathObjectMelt,transform.position,transform.rotation);
		Destroy(gameObject);
	}
}
