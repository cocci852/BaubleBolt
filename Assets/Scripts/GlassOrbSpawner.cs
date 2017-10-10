using UnityEngine;
using System.Collections;

public class GlassOrbSpawner : MonoBehaviour {
	
	public GameObject GlassOrb;
	public float spawnDelay = 1f;
	public float spawnRate = 0.3f;
	float width = Screen.width/1920f;
	float height = Screen.height/1080f;
	float transformXunit;
	float transformYunit;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}

	// Use this for initialization
	void Start () {
		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
		Vector3 screenSize = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0f));
		transformXunit = screenSize.x/1920f;
		transformYunit = screenSize.y/1080f;
		InvokeRepeating("spawnOrb",spawnDelay,spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void spawnOrb() {
		float Scale = Random.Range (0.5f,1f);
		float xPosition = Random.Range(-1920,1920)*transformXunit;
		GameObject orb = (GameObject)Instantiate(GlassOrb,new Vector3(xPosition,1080*transformYunit,0f),Quaternion.identity);
		orb.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-1f/Scale);
		orb.transform.localScale = new Vector3(Scale,Scale,1f);
	}
}
