using UnityEngine;
using System.Collections;

public class MoltenGlassPool : MonoBehaviour {
	public GameObject MeltingAnimation;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
		Vector3 point = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height*179f/1080f,10f));
		transform.position = point;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		GameObject orbAnimation = (GameObject)Instantiate(MeltingAnimation,col.transform.position,Quaternion.identity);
		float scale = col.transform.localScale.x;
		orbAnimation.transform.localScale = new Vector3 (scale,scale,1f);
		Destroy(col.gameObject);
	}
	
}
