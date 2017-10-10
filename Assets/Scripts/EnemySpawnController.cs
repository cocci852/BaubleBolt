using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	public GameObject Fodder;
	public GameObject Brute;
	public GameObject Tall;
	public GameObject Skilled;
	
	public int spawnDelayFodder = 3;
	public int spawnDelayBrute = 19;
	public int spawnDelayTall = 15;
	public int spawnDelaySkilled = 30;
	
	
	public float spawnRateFodder = 5f;
	public float spawnRateBrute = 8f;
	public float spawnRateTall = 10f;
	public float spawnRateSkilled = 12f;
/*	
	public float spawnDelayFodder = 3f;
	public float spawnDelayBrute = 19f;
	public float spawnDelayTall = 15f;
	public float spawnDelaySkilled = 30f;
	
	
	public float spawnRateFodder = 5f;
	public float spawnRateBrute = 8f;
	public float spawnRateTall = 10f;
	public float spawnRateSkilled = 12f;
*/
	
	public float aheadOfPlayer = 40f;
	
	public GameObject player;
	int positionX;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		//Repeatedly spawn enemies after a delay
		//InvokeRepeating ("SpawnFodder", spawnDelayFodder, spawnRateFodder);
		//InvokeRepeating ("SpawnBrute", spawnDelayBrute, spawnRateBrute);
		//InvokeRepeating ("SpawnTall", spawnDelayTall, spawnRateTall);
		//InvokeRepeating ("SpawnSkilled", spawnDelaySkilled, spawnRateSkilled);
		transform.position = new Vector3( aheadOfPlayer, 0f, 0f);

	}

	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( player.transform.position.x + aheadOfPlayer, 0f, 0f);
		int tempPosition = Mathf.FloorToInt(transform.position.x);
		if (tempPosition > positionX)
		{
			positionX = tempPosition;
			SpawnFodder(positionX);
			SpawnBrute(positionX);
			SpawnTall(positionX);
			SpawnSkilled(positionX);
			if (tempPosition%80 == 0)
			{
				if (spawnRateFodder > 2f && positionX > spawnDelayFodder*6)
					spawnRateFodder -= 0.5f;
				if (spawnRateBrute > 3.5f && positionX > spawnDelayBrute*6)
					spawnRateBrute -= 0.5f;
				if (spawnRateTall > 3.5f && positionX > spawnDelayTall*6)
					spawnRateTall -= 0.5f;
				if (spawnRateSkilled > 5.5f && positionX > spawnDelaySkilled*6)
					spawnRateSkilled -= 0.5f;
			}
		}
		
	

	}	

	void SpawnFodder (int position) {
		int delta = position - spawnDelayFodder*6 - (int)aheadOfPlayer;
		if (delta < 0 || delta%(int)(spawnRateFodder*6) != 0)
			return;
		int enemyGravity = Random.Range( 0, 2); 	//Random excludes the max number for int
		GameObject newEnemy;
		newEnemy = (GameObject)Instantiate(Fodder, transform.position, transform.rotation);
		if (enemyGravity == 0)
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = 4;
		else
		{
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = -4;
			newEnemy.transform.Rotate(new Vector3(0,0,180f));
		}
	}
	
	void SpawnBrute (int position) {
		int delta = position - spawnDelayBrute*6 - (int)aheadOfPlayer;
		if (delta < 0 || delta%(int)(spawnRateBrute*6) != 0)
			return;
		int enemyGravity = Random.Range( 0, 2); 	//Random excludes the max number for int
		GameObject newEnemy;
		newEnemy = (GameObject)Instantiate(Brute, new Vector3(transform.position.x,transform.position.y-2,0), transform.rotation);
		if (enemyGravity == 0)
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = 4;
		else
		{
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = -4;
			newEnemy.transform.Rotate(new Vector3(0,0,180f));
		}
	}
	
	void SpawnTall (int position) {
		int delta = position - spawnDelayTall*6 - (int)aheadOfPlayer;
		if (delta < 0 || delta%(int)(spawnRateTall*6) != 0)
			return;
		int enemyGravity = Random.Range( 0, 2); 	//Random excludes the max number for int
		GameObject newEnemy;
		newEnemy = (GameObject)Instantiate(Tall, new Vector3(transform.position.x,transform.position.y+2,0), transform.rotation);
		if (enemyGravity == 0)
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = 4;
		else
		{
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = -4;
			newEnemy.transform.Rotate(new Vector3(0,0,180f));
		}
	}
	
	void SpawnSkilled (int position) {
		int delta = position - spawnDelaySkilled*6 - (int)aheadOfPlayer;
		if (delta < 0 || delta%(int)(spawnRateSkilled*6) != 0)
			return;
		int enemyGravity = Random.Range( 0, 2); 	//Random excludes the max number for int
		GameObject newEnemy;
		newEnemy = (GameObject)Instantiate(Skilled, new Vector3(transform.position.x+2,transform.position.y,0), transform.rotation);
		if (enemyGravity == 0)
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = 5;
		else
		{
			newEnemy.GetComponent<Rigidbody2D>().gravityScale = -5;
			newEnemy.transform.Rotate(new Vector3(0,0,180f));
		}
	}
	
}
