using UnityEngine;
using System.Collections;

public class SCoreKeeper : MonoBehaviour {

	public static int DistanceScore = 0;
	public static int enemyScore = 0;
	public static int bonusScore = 0;
	
	public static float multiplier = 0f;
	
	public static int enemiesKilled = 0;
	public static int combos = 0;
	public static int leftOverBoosts = 0;
	
	public static string levelName;
	
	public static bool singed = false;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);

		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void EnemyScoreAdd(int increment) {
		enemyScore += increment;
		bonusScore += Mathf.FloorToInt(increment*multiplier);
	}
	
	public int GetEnemyScore() {
		return enemyScore;
	}
	
	public void SetDistanceScore(float distance) {
		DistanceScore = Mathf.FloorToInt(distance*3f);
		bonusScore += Mathf.FloorToInt(distance*3f*multiplier);
	}
	
	public int GetDistanceScore() {
		return DistanceScore;
	}
	
	public void BonusScoreAdd(int bonus) {
		bonusScore += Mathf.FloorToInt(bonus*multiplier);
	}
	
	public int GetBonusScore() {
		return bonusScore;
	}
}
