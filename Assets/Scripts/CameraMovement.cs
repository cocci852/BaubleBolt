using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject target;
	public GameObject deathWall;
	Color close = new Color(243f/255f,91f/255f,62f/355f,1f);
	Color far = new Color(9f/255f,128f/255f,1f,1f);

	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10f);
	}
	
	// Update is called once per frame at the end
	void FixedUpdate () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10f);
		Vector2 distance = transform.position - deathWall.transform.position;
		GetComponent<Camera>().backgroundColor = Color.Lerp(close,far,(distance.x-25f)/100f);


	}
}
