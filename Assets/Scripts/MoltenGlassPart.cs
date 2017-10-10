using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MoltenGlassPart : MonoBehaviour {
	GameObject player;
	public GameObject scoreKeeper;
	public GameObject HeroDeathObject;
	public int OffsetGravitySwitch = 0;
	public int FrequencyGravitySwitch = 8;
	bool playerInFire = false;
	
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
		if ((Mathf.FloorToInt(Time.time)-OffsetGravitySwitch)%FrequencyGravitySwitch == 0)
			GetComponent<Rigidbody2D>().gravityScale *= -1;
		
		Vector2 distance = player.transform.position - transform.position;
		if (distance.magnitude < 3f)
		{	
			playerInFire = true;
		}
		if (distance.magnitude > 4f && playerInFire)
		{
			SCoreKeeper.singed = true;
			
			playerInFire = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Player" && (PlayerMovement.isNotDead && PlayerMovementSimple.isNotDead))
		{
			StartCoroutine(ResetGame());
			player = col.gameObject;
			Instantiate(HeroDeathObject,player.transform.position,player.transform.rotation);
			
			if (Application.loadedLevelName == "PlayGame")
			{
				PlayerMovement.isNotDead = false;
				SCoreKeeper.enemiesKilled = PlayerMovement.enemiesKilled;
				SCoreKeeper.combos = PlayerMovement.combos;
				SCoreKeeper.levelName = "PlayGame";
				SCoreKeeper.leftOverBoosts = PlayerMovement.turboStock;
			}
			else
			{
				PlayerMovementSimple.isNotDead = false;
				SCoreKeeper.enemiesKilled = PlayerMovementSimple.enemiesKilled;
				SCoreKeeper.levelName = "PlayGameSimple";
				SCoreKeeper.leftOverBoosts = PlayerMovementSimple.turboStock;
			}
			player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			player.GetComponent<Rigidbody2D>().gravityScale = 0f;
			player.gameObject.SetActive(false);
		}
		
		if (col.transform.tag == "Enemy")
			col.gameObject.SendMessage("KilledByLava");
	}
	
	
	IEnumerator ResetGame()
	{
		//Time.timeScale = 0f;
		yield return new WaitForSeconds(3f);
		//Time.timeScale = 1f;
		//SCoreKeeper.DistanceScore = Mathf.FloorToInt(player.transform.position.x * 3);
		scoreKeeper.GetComponent<SCoreKeeper>().SetDistanceScore(player.transform.position.x);
		Application.LoadLevel("GameOver");
		
	}
}
