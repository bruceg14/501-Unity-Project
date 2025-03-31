using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public enum LocationTrack {NoTrack, Team1, Team2, Team3, Team4, Team1Location, Team2Location, Team3Location, Team4Location}
public class HostInfo : NetworkBehaviour {
	[Header("LocationTracking")]
	//Location tracking
	public float latitudeLocation;
	public float longitudeLocation;

	public Texture2D LocationTexture;
	public Texture2D mapTexture;

	public float minLatitude;
	public float maxLatitude;

	public float minLongitude;
	public float maxLongitude;
	//end location tracking
	[Header("General")]
	public Texture2D BackgroundTexture;
	public Texture2D TextBackground;
	public Texture2D ButtonBackground;
	public Texture2D Terug;
	public Texture2D Team1_Texture;
	public Texture2D Team2_Texture;
	public Texture2D Team3_Texture;
	public Texture2D Team4_Texture;
	public Font dislecticFont;

	[Header("ScoreBoard")]
	public Texture2D ScoreBoardTexture;
	public Texture2D infoButton; 
	public Texture2D Team1Bar;
	public Texture2D Team2Bar;
	public Texture2D Team3Bar;
	public Texture2D Team4Bar;
	public Texture2D ScoreBlock;

	[Header("HelpScreen")]  
	public Texture2D HelpBackground;
	public Texture2D Onderweg;

	[Header("TeamInfo")]     
	public Texture2D infoTopBar;
	public Texture2D mapIcon;  
	public Texture2D BackpackPenseel;
	public Texture2D GroepPenseel;
	public List<Texture2D> PuzzleGrey = new List<Texture2D>(3);
	public List<Texture2D> PuzzleColor = new List<Texture2D>(3);

	[Header("Extra")]    
	public List<GameObject> amountTeam1 = new List<GameObject>();
	public List<GameObject> amountTeam2 = new List<GameObject>();
	public List<GameObject> amountTeam3 = new List<GameObject>();
	public List<GameObject> amountTeam4 = new List<GameObject>();
	public int team1Score;
	public int team2Score;
	public int team3Score;
	public int team4Score;
    GUIStyle titleStyle = new GUIStyle();
	GUIStyle style = new GUIStyle();
	GUIStyle button = new GUIStyle();

	private LocationTrack localTrack = LocationTrack.NoTrack;
	
	private int currentPlayer = 0;
	private bool kairoDone = false;
	private bool milkDone = false;
	private bool nightDone = false;

	private Rect kairoBlock;
	private Rect milkBlock;
	private Rect nightBlock;

	void Start(){
		if(!isServer){
			this.enabled = false;
			return;
		}
		if (!isLocalPlayer)
        {
			this.enabled = false;
            return;
        } 
		if(!localPlayerAuthority){
			this.enabled = false;
			return;
		}
	}

	void Update () {
        titleStyle.alignment = TextAnchor.MiddleCenter;
		titleStyle.fontSize = Screen.height/12;

		style.fontSize = Screen.height/24;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = dislecticFont;
		style.normal.textColor = Color.white;

		// if(amountTeam1.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		// if(amountTeam2.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		// if(amountTeam3.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		// if(amountTeam4.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		kairoBlock 	= new Rect(Screen.width/24,								Screen.height/3 + Screen.height/12,	Screen.width/4,	Screen.height/6);
		milkBlock 		= new Rect((Screen.width/24)*2.5f + Screen.width/4,		Screen.height/3 + Screen.height/12,	Screen.width/4,	Screen.height/6);
		nightBlock 	= new Rect((Screen.width/24)*4 + (Screen.width/4)*2,	Screen.height/3 + Screen.height/12,	Screen.width/4,	Screen.height/6);

		for(int i = 0; i < amountTeam1.Count; ++i){
			if(amountTeam1[i] == null){
				amountTeam1.Remove(amountTeam1[i]);
			}
		}
		for(int i = 0; i < amountTeam2.Count; ++i){
			if(amountTeam2[i] == null){
				amountTeam2.Remove(amountTeam2[i]);
			}
		}
		for(int i = 0; i < amountTeam3.Count; ++i){
			if(amountTeam3[i] == null){
				amountTeam3.Remove(amountTeam3[i]);
			}
		}
		for(int i = 0; i < amountTeam4.Count; ++i){
			if(amountTeam4[i] == null){
				amountTeam4.Remove(amountTeam4[i]);
			}
		}
	}

	void OnGUI(){
		button = new GUIStyle("button");
		button.fontSize = Screen.height/16;
		button.alignment = TextAnchor.MiddleCenter;
		// //show numbers
		// GUIStyle style2 = new GUIStyle();
		// style2.fontSize = Screen.height/16;
		// GUI.Label(new Rect(Screen.width/16, Screen.height/100, Screen.width, Screen.height/8), latitudeLocation.ToString(),style2);
		// GUI.Label(new Rect(Screen.width/16, Screen.height/16, Screen.width, Screen.height/8), longitudeLocation.ToString(),style2);
		// //end numbers
		
	//Location tracking---------------------------------------------------
		switch(localTrack){
			//TEAM 1
			case LocationTrack.Team1Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team1;
			}

			if(amountTeam1.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(1, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam1.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(1, true);
				}
			}
			if(amountTeam1.Count != 0){
				latitudeLocation = amountTeam1[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
				longitudeLocation = amountTeam1[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;
				if(amountTeam1[currentPlayer].GetComponent<PlayerController>().helpNeeded){
					ChangeGUI(button,Onderweg);
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10),"",button)){
						amountTeam1[currentPlayer].GetComponent<PlayerController>().help = false;
						RpcHelpIsComing(amountTeam1[currentPlayer]);
					}
				}
			} else {
				latitudeLocation = 0;
				longitudeLocation = 0;
			}

			float createLatitude = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude-10,createLatitude-10,20,20), LocationTexture);

			if(amountTeam1.Count != 0){
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/10), amountTeam1[currentPlayer].name, titleStyle);
			} else {
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/10), "!no players!", titleStyle);
			}
			break;

			//TEAM 2
			case LocationTrack.Team2Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team2;
			}

			if(amountTeam2.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(2, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam2.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(2, true);
				}
			}
			if(amountTeam2.Count != 0){
				latitudeLocation = amountTeam2[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
				longitudeLocation = amountTeam2[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;
				if(amountTeam2[currentPlayer].GetComponent<PlayerController>().helpNeeded){
					ChangeGUI(button,Onderweg);
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10),"",button)){
						RpcHelpIsComing(amountTeam2[currentPlayer]);
					}
				}
			} else {
				latitudeLocation = 0;
				longitudeLocation = 0;
			}

			float createLatitude2 = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude2 = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude2-10,createLatitude2-10,20,20), LocationTexture);
			if(amountTeam2.Count >= 1){
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/12), amountTeam2[currentPlayer].name, titleStyle);
			} else {
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/12), "!no players!", titleStyle);
			}
			break;

			//TEAM 3
			case LocationTrack.Team3Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team3;
			}

			if(amountTeam3.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(3, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam3.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(3, true);
				}
			}
			if(amountTeam3.Count != 0){
				latitudeLocation = amountTeam3[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
				longitudeLocation = amountTeam3[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;
				if(amountTeam3[currentPlayer].GetComponent<PlayerController>().helpNeeded){
					ChangeGUI(button,Onderweg);
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10),"",button)){
						RpcHelpIsComing(amountTeam3[currentPlayer]);
					}
				}
			} else {
				latitudeLocation = 0;
				longitudeLocation = 0;
			}

			float createLatitude3 = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude3 = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude3-10,createLatitude3-10,20,20), LocationTexture);
			if(amountTeam3.Count >= 1){
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/12), amountTeam3[currentPlayer].name, titleStyle);
			} else {
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/12), "!no players!", titleStyle);
			}
			break;

			//TEAM 4
			case LocationTrack.Team4Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team4;
			}

			if(amountTeam4.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(4, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam4.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(4, true);
				}
			}
			if(amountTeam4.Count != 0){
				latitudeLocation = amountTeam4[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
				longitudeLocation = amountTeam4[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;
				if(amountTeam4[currentPlayer].GetComponent<PlayerController>().helpNeeded){
					ChangeGUI(button,Onderweg);
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10),"",button)){
						RpcHelpIsComing(amountTeam4[currentPlayer]);
					}
				}
			} else {
				latitudeLocation = 0;
				longitudeLocation = 0;
			}

			float createLatitude4 = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude4 = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude4-10,createLatitude4-10,20,20), LocationTexture);
			if(amountTeam4.Count >= 1){
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/12), amountTeam4[currentPlayer].name, titleStyle);
			} else {
				GUI.Box(new Rect(0, Screen.height - Screen.height/10, Screen.width, Screen.height/12), "!no players!", titleStyle);
			}
			break;
	//End=LocationInfo--------------------------------------------------------------------

	//TeamInformationScreens-------------------------------------------------------------
			//TEAM 1
			case LocationTrack.Team1:
			ChangeGUI(style, null);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/12), infoTopBar, ScaleMode.StretchToFill, true, 10.0F); //TopScreen
			GUI.DrawTexture(new Rect(Screen.width/2 - Screen.width/6, Screen.height/12 + Screen.height/48, Screen.width/3, Screen.height/5), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F); //Icon

			ChangeGUI(button,mapIcon);
			if(GUI.Button(new Rect(Screen.width - Screen.width/4,Screen.height/6, Screen.width/5, Screen.height/8), "", button)){
				localTrack = LocationTrack.Team1Location;
			}
			
			//BACKPACK=========================================================
			kairoDone = false;
			milkDone = false;
			nightDone = false;
			GUI.DrawTexture(new Rect(0, Screen.height/3, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam1.Count; i++){
				if(amountTeam1[i].GetComponent<PlayerController>().puzzle1){
					kairoDone = true;
				}
				if(amountTeam1[i].GetComponent<PlayerController>().puzzle2){
					milkDone = true;
				}
				if(amountTeam1[i].GetComponent<PlayerController>().puzzle3){
					nightDone = true;
				}
			}
			//kairo
			if(kairoDone){
				GUI.DrawTexture(kairoBlock,PuzzleColor[0]);
			} else {
				GUI.DrawTexture(kairoBlock,PuzzleGrey[0]);
			}
			//milk
			if(milkDone){
				GUI.DrawTexture(milkBlock,PuzzleColor[1]);
			} else {
				GUI.DrawTexture(milkBlock,PuzzleGrey[1]);
			}
			//night
			if(nightDone){
				GUI.DrawTexture(nightBlock,PuzzleColor[2]);
			} else {
				GUI.DrawTexture(nightBlock,PuzzleGrey[2]);
			}

			//GROEP=========================================================================================
			GUI.DrawTexture(new Rect(0, (Screen.height - Screen.height/3)-Screen.height/20, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam1.Count; ++i){
			
				int screenParts = (Screen.height/3)/amountTeam1.Count;
				string playerName = "";
				if(amountTeam1[i].GetComponent<PlayerController>().helpNeeded){
					playerName = "	" + amountTeam1[i].name + " - !Help!";
				} else {
					playerName = "	" + amountTeam1[i].name;
				}
				GUI.Box(new Rect(0, Screen.height - Screen.height/3, Screen.width/5, screenParts), playerName, style);
			}

			ChangeGUI(button,Terug);
			if(GUI.Button(new Rect(Screen.width - Screen.width/3,Screen.height - Screen.height/10, Screen.width/4, Screen.height/12), "", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;

			//TEAM 2
			case LocationTrack.Team2:
			ChangeGUI(style, null);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/12), infoTopBar, ScaleMode.StretchToFill, true, 10.0F); //TopScreen
			GUI.DrawTexture(new Rect(Screen.width/2 - Screen.width/6, Screen.height/12 + Screen.height/48, Screen.width/3, Screen.height/5), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F); //Icon

			ChangeGUI(button,mapIcon);
			if(GUI.Button(new Rect(Screen.width - Screen.width/4,Screen.height/6, Screen.width/5, Screen.height/8), "", button)){
				localTrack = LocationTrack.Team2Location;
			}
			
			//BACKPACK=========================================================
			kairoDone = false;
			milkDone = false;
			nightDone = false;
			GUI.DrawTexture(new Rect(0, Screen.height/3, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam2.Count; i++){
				if(amountTeam2[i].GetComponent<PlayerController>().puzzle1){
					kairoDone = true;
				}
				if(amountTeam2[i].GetComponent<PlayerController>().puzzle2){
					milkDone = true;
				}
				if(amountTeam2[i].GetComponent<PlayerController>().puzzle3){
					nightDone = true;
				}
			}
			//kairo
			if(kairoDone){
				GUI.DrawTexture(kairoBlock,PuzzleColor[0]);
			} else {
				GUI.DrawTexture(kairoBlock,PuzzleGrey[0]);
			}
			//milk
			if(milkDone){
				GUI.DrawTexture(milkBlock,PuzzleColor[1]);
			} else {
				GUI.DrawTexture(milkBlock,PuzzleGrey[1]);
			}
			//night
			if(nightDone){
				GUI.DrawTexture(nightBlock,PuzzleColor[2]);
			} else {
				GUI.DrawTexture(nightBlock,PuzzleGrey[2]);
			}

			//GROEP=========================================================================================
			GUI.DrawTexture(new Rect(0, (Screen.height - Screen.height/3)-Screen.height/20, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam2.Count; ++i){
			
				int screenParts = (Screen.height/3)/amountTeam2.Count;
				string playerName = "";
				if(amountTeam2[i].GetComponent<PlayerController>().helpNeeded){
					playerName = "	" + amountTeam2[i].name + " - !Help!";
				} else {
					playerName = "	" + amountTeam2[i].name;
				}
				GUI.Box(new Rect(0, Screen.height - Screen.height/3, Screen.width/5, screenParts), playerName, style);
			}

			ChangeGUI(button,Terug);
			if(GUI.Button(new Rect(Screen.width - Screen.width/3,Screen.height - Screen.height/10, Screen.width/4, Screen.height/12), "", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;

			//TEAM 3
			case LocationTrack.Team3:
			ChangeGUI(style, null);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/12), infoTopBar, ScaleMode.StretchToFill, true, 10.0F); //TopScreen
			GUI.DrawTexture(new Rect(Screen.width/2 - Screen.width/6, Screen.height/12 + Screen.height/48, Screen.width/3, Screen.height/5), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F); //Icon

			ChangeGUI(button,mapIcon);
			if(GUI.Button(new Rect(Screen.width - Screen.width/4,Screen.height/6, Screen.width/5, Screen.height/8), "", button)){
				localTrack = LocationTrack.Team3Location;
			}
			
			//BACKPACK=========================================================
			kairoDone = false;
			milkDone = false;
			nightDone = false;
			GUI.DrawTexture(new Rect(0, Screen.height/3, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam3.Count; i++){
				if(amountTeam3[i].GetComponent<PlayerController>().puzzle1){
					kairoDone = true;
				}
				if(amountTeam3[i].GetComponent<PlayerController>().puzzle2){
					milkDone = true;
				}
				if(amountTeam3[i].GetComponent<PlayerController>().puzzle3){
					nightDone = true;
				}
			}
			//kairo
			if(kairoDone){
				GUI.DrawTexture(kairoBlock,PuzzleColor[0]);
			} else {
				GUI.DrawTexture(kairoBlock,PuzzleGrey[0]);
			}
			//milk
			if(milkDone){
				GUI.DrawTexture(milkBlock,PuzzleColor[1]);
			} else {
				GUI.DrawTexture(milkBlock,PuzzleGrey[1]);
			}
			//night
			if(nightDone){
				GUI.DrawTexture(nightBlock,PuzzleColor[2]);
			} else {
				GUI.DrawTexture(nightBlock,PuzzleGrey[2]);
			}

			//GROEP=========================================================================================
			GUI.DrawTexture(new Rect(0, (Screen.height - Screen.height/3)-Screen.height/20, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam3.Count; ++i){
			
				int screenParts = (Screen.height/3)/amountTeam3.Count;
				string playerName = "";
				if(amountTeam3[i].GetComponent<PlayerController>().helpNeeded){
					playerName = "	" + amountTeam3[i].name + " - !Help!";
				} else {
					playerName = "	" + amountTeam3[i].name;
				}
				GUI.Box(new Rect(0, Screen.height - Screen.height/3, Screen.width/5, screenParts), playerName, style);
			}

			ChangeGUI(button,Terug);
			if(GUI.Button(new Rect(Screen.width - Screen.width/3,Screen.height - Screen.height/10, Screen.width/4, Screen.height/12), "", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;

			//TEAM 4
			case LocationTrack.Team4:
			ChangeGUI(style, null);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/12), infoTopBar, ScaleMode.StretchToFill, true, 10.0F); //TopScreen
			GUI.DrawTexture(new Rect(Screen.width/2 - Screen.width/6, Screen.height/12 + Screen.height/48, Screen.width/3, Screen.height/5), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F); //Icon

			ChangeGUI(button,mapIcon);
			if(GUI.Button(new Rect(Screen.width - Screen.width/4,Screen.height/6, Screen.width/5, Screen.height/8), "", button)){
				localTrack = LocationTrack.Team4Location;
			}
			
			//BACKPACK=========================================================
			kairoDone = false;
			milkDone = false;
			nightDone = false;
			GUI.DrawTexture(new Rect(0, Screen.height/3, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam4.Count; i++){
				if(amountTeam4[i].GetComponent<PlayerController>().puzzle1){
					kairoDone = true;
				}
				if(amountTeam4[i].GetComponent<PlayerController>().puzzle2){
					milkDone = true;
				}
				if(amountTeam4[i].GetComponent<PlayerController>().puzzle3){
					nightDone = true;
				}
			}
			//kairo
			if(kairoDone){
				GUI.DrawTexture(kairoBlock,PuzzleColor[0]);
			} else {
				GUI.DrawTexture(kairoBlock,PuzzleGrey[0]);
			}
			//milk
			if(milkDone){
				GUI.DrawTexture(milkBlock,PuzzleColor[1]);
			} else {
				GUI.DrawTexture(milkBlock,PuzzleGrey[1]);
			}
			//night
			if(nightDone){
				GUI.DrawTexture(nightBlock,PuzzleColor[2]);
			} else {
				GUI.DrawTexture(nightBlock,PuzzleGrey[2]);
			}

			//GROEP=========================================================================================
			GUI.DrawTexture(new Rect(0, (Screen.height - Screen.height/3)-Screen.height/20, Screen.width, Screen.height/20), GroepPenseel);
			for(int i = 0; i < amountTeam4.Count; ++i){
			
				int screenParts = (Screen.height/3)/amountTeam4.Count;
				string playerName = "";
				if(amountTeam4[i].GetComponent<PlayerController>().helpNeeded){
					playerName = "	" + amountTeam4[i].name + " - !Help!";
				} else {
					playerName = "	" + amountTeam4[i].name;
				}
				GUI.Box(new Rect(0, Screen.height - Screen.height/3, Screen.width/5, screenParts), playerName, style);
			}

			ChangeGUI(button,Terug);
			if(GUI.Button(new Rect(Screen.width - Screen.width/3,Screen.height - Screen.height/10, Screen.width/4, Screen.height/12), "", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;
	//End=TeamInfo------------------------------------------------------------------------

	//NORMAL------------------------------------------------------------------------
			case LocationTrack.NoTrack:
			currentPlayer = 0;
			//Background and title
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/12), ScoreBoardTexture, ScaleMode.StretchToFill, true, 10.0F);

			//Teams
			int FrameHeight = Screen.height/5;
			int FrameWidth = Screen.height/7;
			int HeighMargin = Screen.height/40;
			int WidthMargin = Screen.width/36;
			int OriginalWidthMargin = Screen.width/36;
				//Team 1
				string team1Info = "" + team1Score;
				GUI.DrawTexture(new Rect(WidthMargin, Screen.height/12 + HeighMargin, FrameWidth, FrameHeight), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(((Screen.width/3)*2)-OriginalWidthMargin, Screen.height/12 + HeighMargin + OriginalWidthMargin, Screen.width/3.5f, Screen.height/6), team1Info, ChangeGUI(style, ScoreBlock));
				GUI.DrawTexture(new Rect((WidthMargin*2) + FrameWidth, (Screen.height/12) + HeighMargin + OriginalWidthMargin*2, FrameWidth, FrameHeight/3), Team1Bar);
				if(GUI.Button(new Rect(Screen.width/3, Screen.height/12 + HeighMargin + FrameHeight/1.8f, Screen.width/4, Screen.height/20), "", ChangeGUI(style, infoButton))){
					localTrack = LocationTrack.Team1;
				}

				//Team 2
				string team2Info = "" + team2Score;
				HeighMargin += FrameHeight + OriginalWidthMargin;
				GUI.DrawTexture(new Rect(WidthMargin, Screen.height/12 + HeighMargin, FrameWidth, FrameHeight), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(((Screen.width/3)*2)-OriginalWidthMargin, Screen.height/12 + HeighMargin + OriginalWidthMargin, Screen.width/3.5f, Screen.height/6), team2Info, ChangeGUI(style, ScoreBlock));
				GUI.DrawTexture(new Rect((WidthMargin*2) + FrameWidth, (Screen.height/12) + HeighMargin + OriginalWidthMargin*2, FrameWidth, FrameHeight/3), Team2Bar);
				if(GUI.Button(new Rect(Screen.width/3, Screen.height/12 + HeighMargin + FrameHeight/1.8f, Screen.width/4, Screen.height/20), "", ChangeGUI(style, infoButton))){
					localTrack = LocationTrack.Team2;
				}

				//Team 3
				string team3Info = "" + team3Score;
				HeighMargin += FrameHeight + OriginalWidthMargin;
				GUI.DrawTexture(new Rect(WidthMargin, Screen.height/12 + HeighMargin, FrameWidth, FrameHeight), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(((Screen.width/3)*2)-OriginalWidthMargin, Screen.height/12 + HeighMargin + OriginalWidthMargin, Screen.width/3.5f, Screen.height/6), team3Info, ChangeGUI(style, ScoreBlock));
				GUI.DrawTexture(new Rect((WidthMargin*2) + FrameWidth, (Screen.height/12) + HeighMargin + OriginalWidthMargin*2, FrameWidth, FrameHeight/3), Team3Bar);
				if(GUI.Button(new Rect(Screen.width/3, Screen.height/12 + HeighMargin + FrameHeight/1.8f, Screen.width/4, Screen.height/20), "", ChangeGUI(style, infoButton))){
					localTrack = LocationTrack.Team3;
				}

				//Team 4
				string team4Info = "" + team4Score;
				HeighMargin += FrameHeight + OriginalWidthMargin;
				GUI.DrawTexture(new Rect(WidthMargin, Screen.height/12 + HeighMargin, FrameWidth, FrameHeight), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(((Screen.width/3)*2)-OriginalWidthMargin, Screen.height/12 + HeighMargin + OriginalWidthMargin, Screen.width/3.5f, Screen.height/6), team4Info, ChangeGUI(style, ScoreBlock));
				GUI.DrawTexture(new Rect((WidthMargin*2) + FrameWidth, (Screen.height/12) + HeighMargin + OriginalWidthMargin*2, FrameWidth, FrameHeight/3), Team4Bar);
				if(GUI.Button(new Rect(Screen.width/3, Screen.height/12 + HeighMargin + FrameHeight/1.8f, Screen.width/4, Screen.height/20), "", ChangeGUI(style, infoButton))){
					localTrack = LocationTrack.Team4;
				}

			for(int i = 0; i < amountTeam1.Count; i++){
				if(amountTeam1[i].GetComponent<PlayerController>().helpNeeded){
					string info = amountTeam1[i].name + " needs your help!";
					GUI.Box(new Rect(0,Screen.height/4,Screen.width,Screen.height/4), info, ChangeGUI(style, TextBackground));
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10), "Onderweg", ChangeGUI(style, ButtonBackground))){
						RpcHelpIsComing(amountTeam1[currentPlayer]);
					}
				}
			}
			for(int i = 0; i < amountTeam2.Count; i++){
				if(amountTeam2[i].GetComponent<PlayerController>().helpNeeded){
					string info = amountTeam2[i].name + " needs your help!";
					GUI.Box(new Rect(0,Screen.height/4,Screen.width,Screen.height/4), info, ChangeGUI(style, TextBackground));
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10), "Onderweg", ChangeGUI(style, ButtonBackground))){
						RpcHelpIsComing(amountTeam2[currentPlayer]);
					}
				}
			}
			for(int i = 0; i < amountTeam3.Count; i++){
				if(amountTeam3[i].GetComponent<PlayerController>().helpNeeded){
					string info = amountTeam3[i].name + " needs your help!";
					GUI.Box(new Rect(0,Screen.height/4,Screen.width,Screen.height/4), info, ChangeGUI(style, TextBackground));
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10), "Onderweg", ChangeGUI(style, ButtonBackground))){
						RpcHelpIsComing(amountTeam3[currentPlayer]);
					}
				}
			}
			for(int i = 0; i < amountTeam4.Count; i++){
				if(amountTeam4[i].GetComponent<PlayerController>().helpNeeded){
					string info = amountTeam4[i].name + " needs your help!";
					GUI.Box(new Rect(0,Screen.height/4,Screen.width,Screen.height/4), info, ChangeGUI(style, TextBackground));
					if(GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/10), "Onderweg", ChangeGUI(style, ButtonBackground))){
						RpcHelpIsComing(amountTeam4[currentPlayer]);
					}
				}
			}
			break;
	//End=NormalScreen--------------------------------------------------------------------------------------------------
		}
	}

	[ClientRpc]
	public void RpcUpdatePlayer(int teamNumber, bool start){
		if(!start){
			switch(teamNumber){
				case 1:
				StopCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
				case 2:
				StopCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
				case 3:
				StopCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
				case 4:
				StopCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
			}
		} else {
			switch(teamNumber){
				case 1:
				StopCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
				case 2:
				StopCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
				case 3:
				StopCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
				case 4:
				StopCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
			}
		}
	}

	private GUIStyle ChangeGUI(GUIStyle currentStyle, Texture2D buttonTexture){
		currentStyle.normal.background = buttonTexture;
		currentStyle.hover.background = buttonTexture;
		currentStyle.focused.background = buttonTexture;
		currentStyle.active.background = buttonTexture;
		return currentStyle;
	}

	[ClientRpc]
	public void RpcHelpIsComing(GameObject Playa){
		Playa.GetComponent<PlayerController>().help = false;
	}
}

//Original team info
/*//Teams
		int FrameHeight = ((Screen.height - Screen.height/10) / 4);
		int HeighMargin = ((Screen.height - Screen.height/10) / 8);
			//Team 1
			string team1Info = "  T_Size: " + amountTeam1.Count + "   Score: " + team1Score;
			GUI.DrawTexture(new Rect(0, Screen.height/10, Screen.width-Screen.width/10, HeighMargin), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, Screen.height/10 + HeighMargin, Screen.width, HeighMargin), team1Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, Screen.height/10, Screen.width/10, HeighMargin), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}

			//Team 2
			string team2Info = "  T_Size: " + amountTeam2.Count + "   Score: " + team2Score;
			GUI.DrawTexture(new Rect(0, ((Screen.height/10) + FrameHeight), Screen.width-Screen.width/10, HeighMargin), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, ((Screen.height/10) + FrameHeight) + HeighMargin, Screen.width, HeighMargin), team2Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + FrameHeight), Screen.width/10, HeighMargin), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}

			//Team 3
			string team3Info = "  T_Size: " + amountTeam3.Count + "   Score: " + team3Score;
			GUI.DrawTexture(new Rect(0, ((Screen.height/10) + (FrameHeight * 2)), Screen.width-Screen.width/10, HeighMargin), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, ((Screen.height/10) + (FrameHeight * 2)) + HeighMargin, Screen.width, HeighMargin), team3Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + (FrameHeight * 2)), Screen.width/10, HeighMargin), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}

			//Team 4
			string team4Info = "  T_Size: " + amountTeam4.Count + "   Score: " + team4Score;
			GUI.DrawTexture(new Rect(0, (Screen.height - FrameHeight), Screen.width-Screen.width/10, HeighMargin), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, (Screen.height - FrameHeight) + HeighMargin, Screen.width, HeighMargin), team4Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, (Screen.height - FrameHeight), Screen.width/10, HeighMargin), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}
			 */