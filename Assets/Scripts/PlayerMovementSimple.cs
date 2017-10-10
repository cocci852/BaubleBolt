using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementSimple : MonoBehaviour {
	
	public static float maxSpeed = 6f;					//Player's max velocity outside of turbo
	float AbsoluteMaxSpeed = 30f;
	public static float accelerationForce = 30f;		//Acceleration force
	float AbsoluteMaxAccelerationForce = 100f;
	public float gravityScale = 4f;				//Gravity Force on player
	public float turboBoost = 1000f;			//Force applied when using turbo move
	float AbsoluteMaxTurboBoost = 2000f;
	public static float drag = 1f;				//Drag player normally experiences
	public float cooldown = 10f;				//Cooldown timer after using turbo move (in seconds)
	public float normalCooldown = 2f;			//cooldown timer after swithcing gravity (in seconds)
	public static int turboStock = 0;			//can stock up on turbo boosts by killing skilled enemy
	public AudioClip impactTerrain;				//Sound played when hitting the Terrain
	public AudioClip soundBoost;				//sound played when using turbo
	public AudioClip swipeLeft;					//Sound played when card is sent to left queue
	public AudioClip swipeRight;				//Sound played when card is sent to right queue
	public AudioClip useActive;					//Sound played when active card is used
	public AudioClip useLeft;					//Sound played when Left q is used
	public AudioClip useRight;					//Sound played when right q is used
	
	public static bool isNotDead = true;		//set to true if death wall hits player. Used to start game over process.
	
	private bool isGrounded = false;			//Check for if player is touch the top or bottom ground
	private bool isGravityDown = true;			//Check for direction of gravity. True = down, false == up
	private bool isInTurboMode = false;			//if in turbo state
	private bool isTouchOkay = false;			//is it okay to touch the screen?
	private bool isTurboNotInCooldown = true;	//is the turbo not in cooldown?
	//	private bool isTouching = false;			//is the player touching the screen in the Active Card area?
	
	private float turboCooldownTimer;				//stores time when cooldown resets for turbo
	
	private float normalCooldownTimer;			//stores time when cooldown resets for normal move
	public GameObject trail;					//for storing the trail renderer of the player. Add as child when turbo, remove after.
	GameObject heroTrail;						//instatiated trail
	
	public static int enemiesKilled = 0;
	
	public static bool inFire = false;
	
	//	float activeCardLeftBound;
	//	float activeCardRightBound;
	
	float widthUnit = Screen.width/1920f;		//normalized width unit
	float heightUnit = Screen.height/1080f;		//normalized heigh unit
	Vector2 touchStart;
	
	public static bool isGridSelected1 = false;
	public static bool isGridSelected2 = false;
	public static bool isGridSelected3 = false;
	public static bool isGridSelected4 = false;
	public static bool isGridSelected5 = false;
	public static bool isGridSelected6 = false;
	public static bool isGridSelected7 = false;
	public static bool isGridSelected8 = false;
	
	Dictionary<int,string> SelectionQ = new Dictionary<int, string>();
	
	float resetTime;
	float resetDelay = 2f;
	
	
	
	// Use this for initialization
	void Awake () {
		SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
		if (mainSpriteRenderer != null)
			mainSpriteRenderer.material.mainTexture = mainSpriteRenderer.sprite.texture;
		
		//start with a velocity to the right
		GetComponent<Rigidbody2D>().velocity = Vector2.right * 2f;
		
		
	}
	
	void Update() {
		
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("StartScreen"); 
		
		if (maxSpeed > AbsoluteMaxSpeed)
			maxSpeed = AbsoluteMaxSpeed;
		if (accelerationForce > AbsoluteMaxAccelerationForce)
			accelerationForce = AbsoluteMaxAccelerationForce;
		if (turboBoost > AbsoluteMaxTurboBoost)
			turboBoost = AbsoluteMaxTurboBoost;
		
		
		if (isNotDead)
		{
			//Check Cooldown
			if (!isTurboNotInCooldown && (turboCooldownTimer <= Time.time))
				isTurboNotInCooldown = true;
			
			//if the Queue is full, activate it. Left Queue name is remnant from PlayerMovementOld
			if (SelectionQ.Count == 3)
				ActivateSelection();
			
			if (Time.time > resetTime)
				ResetSelection();
			
			//Check for input. Tap on left side reverses gravity only. Tap on right side inverts gravity and adds a temporary boost in speed at 45 degrees until hitting something
			if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && isTouchOkay)
			{
				//Vector2 touchLocation = Input.GetTouch(0).position;
				Vector3 touchLocation1 = Input.mousePosition;
				Vector2 touchLocation = new Vector2(touchLocation1.x,touchLocation1.y);
				
				if ((touchLocation.x < Screen.width/2) && isGrounded && Input.GetTouch(0).phase == TouchPhase.Began)		//Touch left side of screen and grounded, switch gravity direction.
				{
					GetComponent<Rigidbody2D>().gravityScale *= -1;
					isGravityDown = !isGravityDown;
					isTouchOkay = false;
					normalCooldownTimer = Time.time;
					isGrounded = false;
				}
				else if ((touchLocation.x > Screen.width/2) && (isTurboNotInCooldown || turboStock >0 && !isInTurboMode) && Input.GetTouch(0).phase == TouchPhase.Began)	//touch right side of screen, propel 45 degrees away from gravity, regardless if grounded or not.
				{
					if (GetComponent<Rigidbody2D>().gravityScale > 0)
						isGravityDown = true;
					else
						isGravityDown = false;
					GetComponent<Rigidbody2D>().gravityScale = 0;
					drag =GetComponent<Rigidbody2D>().drag;
					GetComponent<Rigidbody2D>().drag = 0;
					isInTurboMode = true;
					if (isGravityDown)
						GetComponent<Rigidbody2D>().AddForce(turboBoost * new Vector2 ( 1f, 1f));
					else
						GetComponent<Rigidbody2D>().AddForce(turboBoost * new Vector2 ( 1f, -1f));
					
					//audio.volume = 1f;
					//audio.PlayOneShot(soundBoost);
					isGravityDown = !isGravityDown;  // reverse gravity
					
					if (turboStock>0)				//if there is turbo stocked up, consume one. else, go into cooldown
						turboStock -=1;
					else
					{
						isTurboNotInCooldown = false;
						turboCooldownTimer = Time.time + cooldown;
					}
					
					heroTrail = (GameObject) Instantiate(trail,transform.position,transform.rotation);
					isTouchOkay = false;
					normalCooldownTimer = Time.time;
					isGrounded = false;
					
					
				}
			}
			
			if (Time.time >= normalCooldownTimer + normalCooldown)
			{
				isTouchOkay = true;
				isGrounded = true;
			}
		}		
		
	}
	
	// Update is called once per physics frame
	void FixedUpdate () {
		
		if (isNotDead)
		{
			//Accelerate player to max speed
			if (GetComponent<Rigidbody2D>().velocity.x < maxSpeed && !isInTurboMode)
				GetComponent<Rigidbody2D>().AddForce (Vector2.right * accelerationForce);	
		}
		
		
		
	}
	
	//Player collides with something
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (isNotDead)
		{
			//if was in turbo mode, resets to normal and restores states
			if (isInTurboMode)
			{
				if (isGravityDown)
				{
					GetComponent<Rigidbody2D>().gravityScale = gravityScale;
				}
				else
				{
					GetComponent<Rigidbody2D>().gravityScale = -gravityScale;
				}
				GetComponent<Rigidbody2D>().drag = drag;
				isInTurboMode = false;
				//heroTrail.SendMessage("DestroyTrail");
				heroTrail.GetComponent<HeroTrail>().StopFollowing();
			}
			
			//if player colides with the terrain, set grounded state to true
			if (coll.transform.tag == "Terrain")
			{
				isGrounded = true;
				if (!GetComponent<AudioSource>().isPlaying && !isTouchOkay)
				{
					GetComponent<AudioSource>().volume = 0.2f;
					GetComponent<AudioSource>().PlayOneShot(impactTerrain);
				}
			}
			
			//enable touch events
			isTouchOkay = true;
		}
		
	}
	
	
	public float GetCooldown() {
		float percentRemaining = (cooldown - (turboCooldownTimer - Time.time)) / cooldown;
		return percentRemaining;
	}
	
	public bool GetGravitySwitchOkay() {
		if (isGrounded && isTouchOkay)
			return true;
		else
			return false;
	}
	
	public bool canTurbo() {
		return isTurboNotInCooldown;
	}
	
	
	//Activating the queue when full results in some additional benefit compared to the individual benefits
	//3 skilled: turboboost force increases
	//3 brute: gravity force increases
	//3 tall: slow down Death Drill slightly
	//1 skilled, 1 brute, 1 tall: permanent score multiplier boost
	//3 fodder: extra max speed boost point
	//otherwise, 10% boost for bonuses
	
	void ActivateSelection() {
		List<int> keys = new List<int>();
		GetComponent<AudioSource>().volume = 0.3f;
		GetComponent<AudioSource>().PlayOneShot(useLeft);
		int numFodder = 0;
		int numBrute = 0;
		int numTall = 0;
		int numSkilled = 0;
		
		foreach(KeyValuePair<int,string> item in SelectionQ)
		{
			switch (item.Value)
			{
			case "fodder":
				numFodder += 1;
				break;
			case "brute":
				numBrute += 1;
				break;
			case "tall":
				numTall += 1;
				break;
			case "skilled":
				numSkilled += 1;
				break;
			default:
				break;
			}
			keys.Add(item.Key);
			keys.Sort();
		}
		ResetSelection();
		for (int i = 2;i >= 0;i--)
			ActiveCardQueue.ActiveCardQ.RemoveAt(keys[i]);
		
		if (numFodder == 3)
		{
			maxSpeed += 0.4f;
			SCoreKeeper.bonusScore += 10;
		}
		else if (numBrute == 3)
		{
			maxSpeed += 0.3f;
			GetComponent<Rigidbody2D>().mass += 0.3f;
			gravityScale += 0.3f;
			SCoreKeeper.bonusScore += 30;
		}
		else if (numTall == 3)
		{
			DeathWallMovement.speedAdjust += 2f;
			maxSpeed += 0.3f;
			accelerationForce = 1.01f*1.01f*1.01f*accelerationForce;
			drag = 0.99f*0.99f*0.99f*drag;
			SCoreKeeper.bonusScore += 30;
		}
		else if (numSkilled == 3)
		{
			turboStock += 3;
			maxSpeed += 0.3f;
			turboBoost += 100f;
			SCoreKeeper.bonusScore += 30;
		}
		else if (numSkilled == 1 && numTall == 1 && numBrute == 1)
		{
			SCoreKeeper.multiplier += .1f;
			maxSpeed += 3f;
			turboStock += 1;
			accelerationForce *= 1.01f;
			drag *= 0.99f;
			GetComponent<Rigidbody2D>().mass += 0.1f;
			SCoreKeeper.bonusScore += 30;
		}
		else
		{
			maxSpeed += .33f;
			turboStock += numSkilled;
			accelerationForce *= Mathf.Pow(1.01f,numTall);
			drag *= Mathf.Pow(0.99f,numTall);
			GetComponent<Rigidbody2D>().mass += numBrute*0.1f;
			SCoreKeeper.bonusScore += 25;
		}
		
	}
	
	/*    void UseActiveCard() {
		audio.volume = 0.3f;
		audio.PlayOneShot(useActive);
        string card = ActiveCard.ActiveQ.Dequeue();
        if (card == "fodder")
        {
            maxSpeed += 0.1f;
        }
        else if (card == "brute")
        {
            maxSpeed += 0.1f;
            rigidbody2D.mass += 0.1f;
        }
        else if (card == "tall")
		{
			maxSpeed += 0.1f;
			accelerationForce = 1.01f*accelerationForce;
			drag = 0.99f*drag;
		}
		else
		{
			maxSpeed += 0.1f;
			turboStock +=1;
		}
	}*/
	
	bool IsInGridPosition1(Vector2 position) {
		Vector2 center = new Vector2(100*widthUnit,100*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition2(Vector2 position) {
		Vector2 center = new Vector2(100*widthUnit,300*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition3(Vector2 position) {
		Vector2 center = new Vector2(274*widthUnit,200*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition4(Vector2 position) {
		Vector2 center = new Vector2(448*widthUnit,100*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition5(Vector2 position) {
		Vector2 center = new Vector2(100*widthUnit,500*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition6(Vector2 position) {
		Vector2 center = new Vector2(274*widthUnit,400*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition7(Vector2 position) {
		Vector2 center = new Vector2(448*widthUnit,300*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	bool IsInGridPosition8(Vector2 position) {
		Vector2 center = new Vector2(622*widthUnit,200*heightUnit);
		float xDiff = (position.x - center.x);
		float yDiff = (position.y - center.y);
		float test = xDiff*xDiff/(10000*widthUnit*widthUnit) + yDiff*yDiff/(10000*heightUnit*heightUnit);		
		if (test < 1f)
			return true;
		else
			return false;
	}
	
	
	void GridPosition1 () {
		if (!isGridSelected1)
		{
			isGridSelected1 = true;
			SelectionQ.Add(0,ActiveCardQueue.ActiveCardQ[0]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected1 = false;
			SelectionQ.Remove(0);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition2 () {
		if (!isGridSelected2)
		{
			isGridSelected2 = true;
			SelectionQ.Add(1,ActiveCardQueue.ActiveCardQ[1]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected2 = false;
			SelectionQ.Remove(1);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition3 () {
		if (!isGridSelected3)
		{
			isGridSelected3 = true;
			SelectionQ.Add(2,ActiveCardQueue.ActiveCardQ[2]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected3 = false;
			SelectionQ.Remove(2);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition4 () {
		if (!isGridSelected4)
		{
			isGridSelected4 = true;
			SelectionQ.Add(3,ActiveCardQueue.ActiveCardQ[3]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected4 = false;
			SelectionQ.Remove(3);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition5 () {
		if (!isGridSelected5)
		{
			isGridSelected5 = true;
			SelectionQ.Add(4,ActiveCardQueue.ActiveCardQ[4]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected5 = false;
			SelectionQ.Remove(4);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition6 () {
		if (!isGridSelected6)
		{
			isGridSelected6 = true;
			SelectionQ.Add(5,ActiveCardQueue.ActiveCardQ[5]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected6 = false;
			SelectionQ.Remove(5);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition7 () {
		if (!isGridSelected7)
		{
			isGridSelected7 = true;
			SelectionQ.Add(6,ActiveCardQueue.ActiveCardQ[6]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected7 = false;
			SelectionQ.Remove(6);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void GridPosition8 () {
		if (!isGridSelected8)
		{
			isGridSelected8 = true;
			SelectionQ.Add(7,ActiveCardQueue.ActiveCardQ[7]);
			resetTime = Time.time + resetDelay;
		}
		else
		{	
			isGridSelected8 = false;
			SelectionQ.Remove(7);
			resetTime = Time.time + resetDelay;
		}
	}
	
	void ResetSelection() {
		SelectionQ.Clear();
		isGridSelected1 = false;
		isGridSelected2 = false;
		isGridSelected3 = false;
		isGridSelected4 = false;
		isGridSelected5 = false;
		isGridSelected6 = false;
		isGridSelected7 = false;
		isGridSelected8 = false;
	}
}
