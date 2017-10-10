using UnityEngine;
using System.Collections;

public class FodderParticleDeath : MonoBehaviour {
	public AudioClip deathSound;

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		float randomPitch = Random.Range(0.8f,1.2f);

		GetComponent<AudioSource>().volume = 1f;
		GetComponent<AudioSource>().pitch = randomPitch;
		GetComponent<AudioSource>().PlayOneShot(deathSound);
		StartCoroutine(DestroySelf());
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator DestroySelf() {
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}
}
