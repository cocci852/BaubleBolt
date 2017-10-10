using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControllerSkilled : MonoBehaviour {
	public float health = 1f;
	public int pointValue = 1;
	public GameObject scoreKeeper;
	Vector2 initialVelocity = Vector2.zero;
	bool didCollideWithPlayer = false;
	bool isFalling = false;
	
	public GameObject player;
	
    public GameObject deathObject;
	public GameObject deathObjectMelt;
	public AudioClip CollisionSound;
    
	public float timeToFall = 3;
	
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
		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");	
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
						ActiveCardQueue.ActiveCardQ.Add("skilled");
					else
					{
						if (levelName == "PlayGame")
						{
							PlayerMovement.maxSpeed += 0.05f;
							PlayerMovement.turboStock +=1;
						}
						else
						{
							PlayerMovementSimple.maxSpeed += 0.05f;
							PlayerMovementSimple.turboStock +=1;
						}
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
	
	void FixedUpdate () {
		if (PlayerMovement.isNotDead && PlayerMovementSimple.isNotDead)
		{
			float test = ((transform.position.x - player.transform.position.x)/player.GetComponent<Rigidbody2D>().velocity.x);
			if ((test <= timeToFall) && (Mathf.Sign(player.GetComponent<Rigidbody2D>().gravityScale) != Mathf.Sign(GetComponent<Rigidbody2D>().gravityScale)))
			{
				GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;
				isFalling = true;
			}
		}
		
	}
	
	
	void OnCollisionEnter2D (Collision2D col) {
		if ((col.gameObject.tag == "Player") && !isFalling)
		{
			didCollideWithPlayer = true;
		}
		if ((col.gameObject.tag == "Terrain") && isFalling)
		{
			isFalling = false;
        }
	}
	
	void KilledByLava() {
		Instantiate(deathObjectMelt,transform.position,transform.rotation);
		Destroy(gameObject);
	}
}