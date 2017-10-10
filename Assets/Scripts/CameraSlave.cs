using UnityEngine;
using System.Collections;

public class CameraSlave : MonoBehaviour {

	public float camMaxSpeed = 1f;
	public float smoothFollow = 1f;
	public float offset = 1f;
	GameObject target;
	Vector3 velocity = Vector3.zero;

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		transform.position = Vector3.zero;

	}
	
	// Update is called once per physics frame
	void FixedUpdate () {
/*		float cameraLead = (float)target.rigidbody2D.velocity.magnitude;

		float separation = transform.position.x - target.transform.position.x - cameraLead;
		if (Mathf.Abs(separation) > 0.01f)
			rigidbody2D.velocity = cameraLead * -separation * Vector2.right;
			*/
		transform.position = Vector3.SmoothDamp(transform.position, new Vector3 (target.transform.position.x + target.GetComponent<Rigidbody2D>().velocity.x * camMaxSpeed + offset,0,0), ref velocity, smoothFollow);
			

	}
}
