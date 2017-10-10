using UnityEngine;
using System.Collections;

public class HeroTrail : MonoBehaviour {
	GameObject player;
	bool isFollowing = true;
	
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
		if (isFollowing)
			transform.position = player.transform.position;
	}
	
	public void StopFollowing () {
		StartCoroutine(DestroyTrail());
		//isFollowing = false;
	}
	
	IEnumerator DestroyTrail() {
		isFollowing = false;
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
}
