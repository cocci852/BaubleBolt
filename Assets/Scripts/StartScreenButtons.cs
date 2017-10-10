using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class StartScreenButtons : MonoBehaviour {

	float ButtonWidth = 6*Screen.width/32f;
	float ButtonHeight = 3*Screen.height/32f;
	
	float widthUnit = Screen.width/1920f;
	float heightUnit = Screen.height/1080f;
	
	public static bool isLoggedIn;
	
	public GUIStyle flatButtons;
	public GUIStyle textLabel;
	public GUIStyle toggle;
	public GameObject player;
	
	bool startNormal = false;
	bool startSimple = false;
	public GameObject bubbleGUI;
	GUITexture componentBubbleGUI;
	
	public static bool normalMode = false;
	
	
	bool dontShowInstructionsNormal = false;
	bool dontShowInstructionsSimple = false;
	
	void awake() {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
	}
	
	// Use this for initialization
	void Start () {
		//debuging
//		PlayGamesPlatform.DebugLogEnabled = true;
		//Activat Google Play Games platform
//		PlayGamesPlatform.Activate();
		
		//Sign in
//		Social.localUser.Authenticate(LogInPlayServices);
		flatButtons.fontSize = Mathf.FloorToInt(40*widthUnit);
		textLabel.fontSize = Mathf.FloorToInt(40*widthUnit);
		toggle.fontSize = Mathf.FloorToInt(40*widthUnit);
		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
		if (bubbleGUI == null)
			bubbleGUI = GameObject.FindGameObjectWithTag("ActiveCard");
		componentBubbleGUI = bubbleGUI.GetComponent<GUITexture>();
		if (PlayerPrefs.HasKey("dontShowInstructionsNormal"))
		{
			if (PlayerPrefs.GetInt("dontShowInstructionsNormal") == 0)
				dontShowInstructionsNormal = false;
			else
				dontShowInstructionsNormal = true;
		}
		if (PlayerPrefs.HasKey("dontShowInstructionsSimple"))
		{
			if (PlayerPrefs.GetInt("dontShowInstructionsSimple") == 0)
				dontShowInstructionsSimple = false;
			else
				dontShowInstructionsSimple = true;
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		
		if (!startNormal && !startSimple)
		{
			//play game (normal)
			if (GUI.Button(new Rect(Screen.width/4f - ButtonWidth/2f,Screen.height/2f - 3*ButtonHeight/2f,ButtonWidth,ButtonHeight),"PLAY NORMAL",flatButtons))
			{
				if (dontShowInstructionsNormal)
					Application.LoadLevel("PlayGame");
				else
				{
					startNormal = true;
					ActiveCardQueue.ActiveCardQ.Add("fodder");
					ActiveCardQueue.ActiveCardQ.Add("fodder");
					ActiveCardQueue.ActiveCardQ.Add("fodder");
				}
				
			}
			
			//play game (stripped)
			if (GUI.Button(new Rect(Screen.width/4f - ButtonWidth/2f,Screen.height/2f + ButtonHeight/4f,ButtonWidth,ButtonHeight),"PLAY SIMPLIFIED",flatButtons))
			{
					if (dontShowInstructionsSimple)
						Application.LoadLevel("PlayGameSimple");
					else
						startSimple = true;
			}
			
			//sign out or sign in
			if (Social.localUser.authenticated)
			{
				if (GUI.Button(new Rect(Screen.width/4f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"SIGN OUT",flatButtons))
				{
					((PlayGamesPlatform) Social.Active).SignOut();
	            }
			}
			else
			{
				if (GUI.Button(new Rect(Screen.width/4f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"SIGN IN",flatButtons))
				{
					Social.localUser.Authenticate(LogInPlayServices);
	            }
	            
				GUI.Label(new Rect(Screen.width/2f - ButtonWidth/2f,Screen.height/2f + 7*ButtonHeight/2f,ButtonWidth,ButtonHeight),"*IF YOU DO NOT SIGN IN YOU WILL NOT BE ABLE TO PARTICIPATE IN THE LEADERBOARD OR UNLOCK ACHIEVEMENTS",textLabel);
			}
			
			if (GUI.Button(new Rect(Screen.width*3/4f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"CREDITS",flatButtons))
			{
				Application.LoadLevel("Credits");
			}
			
				
			if (GUI.Button(new Rect(Screen.width*3/4f - ButtonWidth/2f,Screen.height/2f + ButtonHeight/4f,ButtonWidth,ButtonHeight),"LEADERBOARD",flatButtons))
			{
				Social.ShowLeaderboardUI();
	        }
	        
			if (GUI.Button(new Rect(Screen.width*3/4f - ButtonWidth/2f,Screen.height/2f - 3*ButtonHeight/2f,ButtonWidth,ButtonHeight),"ACHIEVEMENTS",flatButtons))
			{
				Social.ShowAchievementsUI();
			}
			
			if (dontShowInstructionsNormal || dontShowInstructionsSimple)
				if (GUI.Button(new Rect(Screen.width/2f - ButtonWidth,4*ButtonHeight/2f,2*ButtonWidth,ButtonHeight),"SHOW INSTRUCTIONS AT START",flatButtons))
				{
					dontShowInstructionsNormal = false;
					dontShowInstructionsSimple = false;
					PlayerPrefs.SetInt("dontShowInstructionsNormal",0);
					PlayerPrefs.SetInt("dontShowInstructionsSimple",0);
					
				}
		}
		else if (startNormal)
		{
			componentBubbleGUI.enabled = true;
			player.GetComponent<PlayerMovement>().enabled = true;
			player.GetComponent<PlayerMovementSimple>().enabled = false;
			normalMode = true;
			GUI.Label(new Rect(0,Screen.height/2-ButtonHeight/2,1920*widthUnit,ButtonHeight),"TAP ON THE OCCUPIED CIRCLES IN SETS OF 3 TO ACTIVATE A BONUS EFFECT.",textLabel);
			dontShowInstructionsNormal = GUI.Toggle(new Rect(Screen.width - 2*ButtonWidth,2*ButtonHeight,60*widthUnit,60*heightUnit),dontShowInstructionsNormal,"",toggle);
			GUI.Label(new Rect(Screen.width - 2*ButtonWidth + 60*widthUnit,2*ButtonHeight,ButtonWidth,60*heightUnit),"DONT SHOW AGAIN",textLabel);
			if (GUI.Button(new Rect(Screen.width*3/4f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"START",flatButtons))
			{
				ActiveCardQueue.ActiveCardQ.Clear();
				if (dontShowInstructionsNormal)
					PlayerPrefs.SetInt("dontShowInstructionsNormal",1);
				else
					PlayerPrefs.SetInt("dontShowInstructionsNormal",0);
				componentBubbleGUI.enabled = false;
				Application.LoadLevel("PlayGame");
			}
			if (GUI.Button(new Rect(Screen.width/2f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"BACK",flatButtons))
			{
				ActiveCardQueue.ActiveCardQ.Clear();
				player.GetComponent<PlayerMovement>().enabled = false;
				player.GetComponent<PlayerMovementSimple>().enabled = true;
				startNormal = false;
				if (dontShowInstructionsNormal)
					PlayerPrefs.SetInt("dontShowInstructionsNormal",1);
				else
					PlayerPrefs.SetInt("dontShowInstructionsNormal",0);
				componentBubbleGUI.enabled = false;
				normalMode = false;
			}
			
		}
		else
		{
			GUI.Label(new Rect(0,Screen.height/2-ButtonHeight/2,1920*widthUnit,ButtonHeight),"THIS MODE DOES NOT ENABLE USING COMBOS. THE ASSOCIATED ACHIEVEMENT CANNOT BE UNLOCKED.",textLabel);
			dontShowInstructionsSimple = GUI.Toggle(new Rect(Screen.width - 2*ButtonWidth,2*ButtonHeight,60*widthUnit,60*heightUnit),dontShowInstructionsSimple,"",toggle);
			GUI.Label(new Rect(Screen.width - 2*ButtonWidth + 60*widthUnit,2*ButtonHeight,ButtonWidth,60*heightUnit),"DONT SHOW AGAIN",textLabel);
			if (GUI.Button(new Rect(Screen.width*3/4f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"START",flatButtons))
			{
				if (dontShowInstructionsSimple)
					PlayerPrefs.SetInt("dontShowInstructionsSimple",1);
				else
					PlayerPrefs.SetInt("dontShowInstructionsSimple",0);
				Application.LoadLevel("PlayGameSimple");
			}
			if (GUI.Button(new Rect(Screen.width/2f - ButtonWidth/2f,Screen.height/2f + 4*ButtonHeight/2f,ButtonWidth,ButtonHeight),"BACK",flatButtons))
			{
				startSimple = false;
				if (dontShowInstructionsSimple)
					PlayerPrefs.SetInt("dontShowInstructionsSimple",1);
				else
					PlayerPrefs.SetInt("dontShowInstructionsSimple",0);
			}
		}
    }
	
	//Authentication of login
	void LogInPlayServices(bool success) {
		if (success)
			isLoggedIn = true;
		else
			isLoggedIn = false;
	}
}
