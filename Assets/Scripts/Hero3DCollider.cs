using UnityEngine;
using System.Collections;

public class Hero3DCollider : MonoBehaviour {
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnParticleCollision(GameObject particle) {
		Vector3 direction = transform.position - particle.transform.position;
		particle.GetComponent<Rigidbody>().AddForce(direction.normalized);
	}
}
