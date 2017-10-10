using UnityEngine;
using System.Collections;

public class TerrainManagement : MonoBehaviour {

	public float checkDistance = 50;
	public float groundWidth = 20f;
	public GameObject player;

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		if (player == null)
			player = (GameObject)GameObject.FindGameObjectWithTag ("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerMovement.isNotDead && PlayerMovementSimple.isNotDead)
		{
			//Check if terrain sprite is too far from player. If so, destroy it.
			GameObject player = (GameObject)GameObject.FindGameObjectWithTag ("Player");
			float distanceFromPlayer = player.transform.position.x - transform.position.x;
			
			if ((distanceFromPlayer > checkDistance) && (player.transform.position.x > transform.position.x))
			{
				transform.Translate (Vector3.right * groundWidth * 5);
			}
		}
		
    }
}
