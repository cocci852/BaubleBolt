using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class GameOverButtons : MonoBehaviour {
	int EnemyScore;
	int TimeScore;
	int BonusScore;
	int Total;
	int enemiesKilled;
	int combos;
	int leftOverBoosts;
	
	float ButtonWidth = 6*Screen.width/32f;			//360*widthUnit
	float ButtonHeight = 3*Screen.height/32f;		//180*heightUnit
	
	float widthUnit = Screen.width/1920f;
	float heightUnit = Screen.height/1080f;
	
	bool temp;
	private BannerView bannerView;
	
	public GUIStyle flatButton;
	public GUIStyle textLabel;
	public GUIStyle textLabel2;
	public GUIStyle separator;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {

		string adUnitId = "ca-app-pub-1813664213939743/1068356915";
		
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner,AdPosition.Top);
		
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
        
        EnemyScore = SCoreKeeper.enemyScore;
		TimeScore = SCoreKeeper.DistanceScore;
		BonusScore = SCoreKeeper.bonusScore;
		Total = EnemyScore+TimeScore+BonusScore;
		enemiesKilled = SCoreKeeper.enemiesKilled;
		combos = SCoreKeeper.combos;
		leftOverBoosts = SCoreKeeper.leftOverBoosts;
		
		
		if (Social.localUser.authenticated)
		{
			if (SCoreKeeper.levelName == "PlayGame")
				Social.ReportScore(Total,"CgkI57Ko4PENEAIQAQ",SendScores);
			else
				Social.ReportScore(Total,"CgkI57Ko4PENEAIQBw",SendScores);
			
			
			((PlayGamesPlatform) Social.Active).IncrementAchievement("CgkI57Ko4PENEAIQAw",enemiesKilled,SendScores);
			((PlayGamesPlatform) Social.Active).IncrementAchievement("CgkI57Ko4PENEAIQBg",combos,SendScores);
			if (leftOverBoosts >= 20)
				Social.ReportProgress("CgkI57Ko4PENEAIQBQ",100f,SendScores);
			if (Total <= 500f)
				Social.ReportProgress("CgkI57Ko4PENEAIQBA",100f,SendScores);
			if (SCoreKeeper.singed)
				Social.ReportProgress("CgkI57Ko4PENEAIQAg",100f,SendScores);
		}
		
		SCoreKeeper.DistanceScore = 0;
		SCoreKeeper.enemyScore = 0;
		SCoreKeeper.bonusScore = 0;
		SCoreKeeper.singed = false;
		
		if (SCoreKeeper.levelName == "PlayGame")
		{
			PlayerMovement.maxSpeed = 6f;
			PlayerMovement.accelerationForce = 30f;
			PlayerMovement.drag = 1f;
			PlayerMovement.turboStock = 0;
			PlayerMovement.isNotDead = true;
		}
		else
		{
			PlayerMovementSimple.maxSpeed = 6f;
			PlayerMovementSimple.accelerationForce = 30f;
			PlayerMovementSimple.drag = 1f;
			PlayerMovementSimple.turboStock = 0;
			PlayerMovementSimple.isNotDead = true;
		}
		LeftQueue.leftQ.Clear();
		RightQueue.rightQ.Clear();
		ActiveCardQueue.ActiveCardQ.Clear();	
		
		textLabel.fontSize = Mathf.FloorToInt(45*widthUnit);
		flatButton.fontSize = Mathf.FloorToInt(40*widthUnit);
		textLabel2.fontSize = Mathf.FloorToInt(55*widthUnit);
		
		RequestBanner();
		ShowBanner();
		
		
		
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{	
			HideBanner();
			Application.LoadLevel("StartScreen"); 
		}
			
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label(new Rect(135*widthUnit,370*heightUnit,ButtonWidth,40*heightUnit),"SCORE BREAKDOWN", textLabel);
		
		GUI.Label(new Rect(160*widthUnit,420*heightUnit,ButtonWidth,40*heightUnit),"Distance:",textLabel);
		GUI.Label(new Rect(380*widthUnit,420*heightUnit,ButtonWidth,40*heightUnit),TimeScore.ToString(),textLabel);
		
		GUI.Label(new Rect(160*widthUnit,470*heightUnit,ButtonWidth,40*heightUnit),"Enemy:",textLabel);
		GUI.Label(new Rect(380*widthUnit,470*heightUnit,ButtonWidth,40*heightUnit),EnemyScore.ToString(),textLabel);
		
		GUI.Label(new Rect(160*widthUnit,520*heightUnit,ButtonWidth,40*heightUnit),"Bonus:",textLabel);
		GUI.Label(new Rect(380*widthUnit,520*heightUnit,ButtonWidth,40*heightUnit),BonusScore.ToString(),textLabel);
		
		GUI.Box(new Rect(120*widthUnit,580*heightUnit, 400*widthUnit,4*heightUnit),"",separator);
		
		GUI.Label(new Rect(160*widthUnit,580*heightUnit,ButtonWidth,40*heightUnit),"Total:",textLabel);		
		GUI.Label(new Rect(380*widthUnit,580*heightUnit,ButtonWidth,40*heightUnit),Total.ToString(), textLabel);
		
		GUI.Label(new Rect(0F,150*heightUnit,Screen.width,ButtonHeight),"YOU'VE BEEN MELTED DOWN AND RECYCLED. CONGRATULATIONS. HAVE AN AD.", textLabel2);
	
	
	
		if (GUI.Button(new Rect(800*widthUnit,380*heightUnit,ButtonWidth,ButtonHeight),"REPLAY",flatButton))
		{
			HideBanner();
			if (SCoreKeeper.levelName == "PlayGame")
				Application.LoadLevel("PlayGame");
			else
				Application.LoadLevel("PlayGameSimple");
		}
		
		if (GUI.Button(new Rect(1300*widthUnit,380*heightUnit,ButtonWidth,ButtonHeight),"MAIN MENU",flatButton))
		{
			HideBanner();
			Application.LoadLevel("StartScreen");
		}
		
		if (GUI.Button(new Rect(800*widthUnit,570*heightUnit,ButtonWidth,ButtonHeight),"LEADERBOARD",flatButton))
		{
			Social.ShowLeaderboardUI();
		}
		
		if (GUI.Button(new Rect(1300*widthUnit,570*heightUnit,ButtonWidth,ButtonHeight),"ACHIEVEMENTS",flatButton))
		{
			Social.ShowAchievementsUI();
		}
		
		
		
		
		
	}
	
	void SendScores(bool success) {
		if (success)
			temp = true;
		else
			temp = false;
	}
	
	
	//from demo
	void RequestBanner() {
		// Request a banner ad, with optional custom ad targeting.
		AdRequest request = new AdRequest.Builder()
			.Build();
		bannerView.LoadAd(request);
	}
	
	void ShowBanner() {
		bannerView.Show();
	}
	
	void HideBanner() {
		bannerView.Hide();
	}
	
	#region Banner callback handlers
	
	public void HandleAdLoaded()
	{
		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(string message)
	{
		print("HandleFailedToReceiveAd event received with message: " + message);
	}
	
	public void HandleAdOpened()
	{
		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing ()
	{
		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed()
	{
		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication()
	{
		print("HandleAdLeftApplication event received");
    }
    
    #endregion
}
