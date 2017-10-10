using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveCardQueue : MonoBehaviour {
	
	public static List<string> ActiveCardQ = new List<string>();
	
	public Texture2D[] QCard;
	public Texture2D SelectedCards;
	float width = Screen.width/1920f;
	float height = Screen.height/1080f;
	
	Texture2D GridImage1;
	Texture2D GridImage2;
	Texture2D GridImage3;
	Texture2D GridImage4;
	Texture2D GridImage5;
	Texture2D GridImage6;
	Texture2D GridImage7;
	Texture2D GridImage8;
	
	bool GridOccupied1 = false;
	bool GridOccupied2 = false;
	bool GridOccupied3 = false;
	bool GridOccupied4 = false;
	bool GridOccupied5 = false;
	bool GridOccupied6 = false;
	bool GridOccupied7 = false;
	bool GridOccupied8 = false;

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
		GridOccupied1 = false;
		GridOccupied2 = false;
		GridOccupied3 = false;
		GridOccupied4 = false;
		GridOccupied5 = false;
		GridOccupied6 = false;
		GridOccupied7 = false;
        GridOccupied8 = false;
        
		if (ActiveCardQ.Count >= 1)
		{
			GridImage1 = getGridImage(1);
			GridOccupied1 = true;
		}
		else 
			GridOccupied1 = false;
			
		if (ActiveCardQ.Count >= 2)
		{
			GridImage2 = getGridImage(2);
			GridOccupied2 = true;
		}
        else 
            GridOccupied2 = false;
            
		if (ActiveCardQ.Count >= 3)
		{
			GridImage3 = getGridImage(3);
			GridOccupied3 = true;
		}
		else 
            GridOccupied3 = false;
            
		if (ActiveCardQ.Count >= 4)
		{
			GridImage4 = getGridImage(4);
			GridOccupied4 = true;
		}
		else 
            GridOccupied4 = false;
            
		if (ActiveCardQ.Count >= 5)
		{
			GridImage5 = getGridImage(5);
			GridOccupied5 = true;
		}
		else 
            GridOccupied5 = false;
            
		if (ActiveCardQ.Count >= 6)
		{
			GridImage6 = getGridImage(6);
			GridOccupied6 = true;
		}
		else 
            GridOccupied6 = false;
            
		if (ActiveCardQ.Count >= 7)
		{
			GridImage7 = getGridImage(7);
			GridOccupied7 = true;
		}
		else 
            GridOccupied7 = false;
            
		if (ActiveCardQ.Count >= 8)
		{
			GridImage8 = getGridImage(8);
			GridOccupied8 = true;
		}
		else 
            GridOccupied8 = false;
    }
    
    void OnGUI() {
    	
    	Color guiColor = GUI.color;
    	guiColor.a = .5f;
    	GUI.color = guiColor;
    	
	//Grid GUI images
		if (GridOccupied1)
			GUI.DrawTexture(new Rect(30*width,910*height,140*width,140*height),GridImage1);
		if (GridOccupied2)
			GUI.DrawTexture(new Rect(30*width,710*height,140*width,140*height),GridImage2);
		if (GridOccupied3)
			GUI.DrawTexture(new Rect(204*width,810*height,140*width,140*height),GridImage3);
		if (GridOccupied4)
			GUI.DrawTexture(new Rect(378*width,910*height,140*width,140*height),GridImage4);
		if (GridOccupied5)
			GUI.DrawTexture(new Rect(30*width,510*height,140*width,140*height),GridImage5);
		if (GridOccupied6)
			GUI.DrawTexture(new Rect(204*width,610*height,140*width,140*height),GridImage6);
		if (GridOccupied7)
			GUI.DrawTexture(new Rect(378*width,710*height,140*width,140*height),GridImage7);
		if (GridOccupied8)
			GUI.DrawTexture(new Rect(552*width,810*height,140*width,140*height),GridImage8);
			
		if (PlayerMovement.isGridSelected1)
			GUI.DrawTexture(new Rect(0*width,880*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected2)
			GUI.DrawTexture(new Rect(0*width,680*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected3)
			GUI.DrawTexture(new Rect(174*width,780*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected4)
			GUI.DrawTexture(new Rect(348*width,880*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected5)
			GUI.DrawTexture(new Rect(0*width,480*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected6)
			GUI.DrawTexture(new Rect(174*width,580*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected7)
			GUI.DrawTexture(new Rect(348*width,680*height,200*width,200*height),SelectedCards);
		if (PlayerMovement.isGridSelected8)
            GUI.DrawTexture(new Rect(522*width,780*height,200*width,200*height),SelectedCards);
	}
	
	
	Texture2D getGridImage(int position) {
		string entry = ActiveCardQ[position-1];
		if (entry == "fodder")
			return QCard[0];
		else if (entry == "brute")
			return QCard[1];
		else if (entry == "tall")
			return QCard[2];
		else if (entry == "skilled")
			return QCard[3];
		else
			return QCard[4];
	}

}
