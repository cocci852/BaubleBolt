using UnityEngine;
using System.Collections;

public class DeathParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Destroy());
	
	}
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Destroy() {
		float decay = Random.Range(1f,3f);
		yield return new WaitForSeconds(decay);
		Destroy(gameObject);
	}
}
