using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class SignInStartup : MonoBehaviour {

	void Awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate();
		
		//Sign in
		Social.localUser.Authenticate(LogInPlayServices);
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine("wait",3);
		
	}
	void LogInPlayServices(bool success) {
		bool isLoggedIn;
		if (success)
			isLoggedIn = true;
		else
			isLoggedIn = false;
	}
	
	IEnumerator wait(float time) {
		yield return new WaitForSeconds(time);
		
		Application.LoadLevel("StartScreen");
	}
}
