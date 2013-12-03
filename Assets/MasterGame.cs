	using UnityEngine;
	using System.Collections;
	
	public class MasterGame : MonoBehaviour {
		
		public GamePlayers [] Players = new GamePlayers[4];  // stores player info
		public GameObject[] GobPlayer;    // store gameobject of player
		int totalPlayers;     // total playing players
		int turn;                 // who will roll the dice
	    int rotation=0;	
	    int actCam=0;   
	    
	
	string []TEXT ={"There was an acute shortage of water as you gulped the money alloted for Water Treatment plants","There were 4670 farmer suicides last year","There was a cutoff of electricity everyday for 2 hours since last year","Money alloted for new weapons was diverted to your account"," The standard of public schools and the literacy rate continue to fall","fire caused a damage of 134 crores last year","Animals continue to die as since you continue accepting bribe from poachers","You could have provided free medicine to people in BPL instead of gulping down the money intended for Healthcare"}
;
	
	    float Umx = 0.01f;
	    float Dmx = 0;
	    bool goUp  = true;
	
	bool swatch = true;
		/*  
		 * Justice:0  Promotion:1  Chance:2   Jail:3  Surprise:4    City|Dis:5  State:6   National:7
		   Power:5   Water:6   Fire:7  Police:8   Housing:9   PublicWorks:10   Tourism:11  Health:12
		   Education:13   Agro:14   Roadways:15   Forest:16
		         Defence:17           Coal:18          Rail:19 
		 */
	    string congrats="Congratulations!!!\n";
	int subcongos=0;
	    bool Congomessage = false;
	    bool noMessage = true;
		string []playerProfile={"Player: ","\n Level: ","\n Money: ","\n Jail","\n C|D Projs: ","\n S Projs: ","\n N Projs","\n Radio","\n NPaper","\n Tv"};
		
	    int [] Internal2Level = {0, 1, 2,3,4,5,5,5,5,5,5,5,5,6,6,6,6,7,8,9};
		
		string [] LevelNames={"Justice", "Promotion", "Chance","Jail","Surprise","City|District","State","National","Winner"};
	    
	    // task Performed Success
	    bool paidUp = true;
	    bool paidOk = true;
	    string JText="";
	    // Jail Mode 3   Duel Mode = 0 Normal Mode = 1  ChanceMode = 2 Surprise Mode = 4
	    int Mode = 1;
	    bool modeDecided = false;    
	
		string choiceString="";
	    int choiceOption=0;
	
	    long Money2lose=50000;
	    long AddnCharge = 100000;
	
	    bool DoneJob = false; // time to switch to next player 
			
	    // Sell Mode
	    bool sellmode = false;
	    int sellPlayer = -1;
	    string sellDisplay ="";
	
		// Duel Mode activated
		bool DuelMode = false;
		bool DefChance = true;
		int DefVal=0;
		int AttVal=0;
		bool DRoll = false;
		float rollStime;
		int Dturn;
		int Aturn;
		bool takeAttack = false;
	    bool MoveToRoll = false;
 		
		// Req for going to Dist
		int CityLevel=5;
		int CityProj = 2;
		int CityRadio = 1;
		int CityNews = 0;
		int CityTv = 0;
		long CityMoney = 30000;
		
		// Req for going to StateLev
		int DistLevel=5;
		int DistProj=4;
		int DistRadio=1;
		int DistNews=1;
		int DistTv=0;
		long DistMoney=50000;
		
		// Req for going to NatLevel
		int StaLevel = 6;
		int StaProj=2;
		int StaRad=1;
		int StaNews=1;
		int StaTv=1;
		long StaMoney=75000;
		
		// One of Req for Winning
		int NatLevel=7;
		int NatProj=1;
		int NatRad=2;
		int NatNews=2;
		int NatTv=1;
		long NatMoney=100000;
		
		// Round Bonus ofr each level
		long CityBonus=20000;
		long DistBonus=40000;
		long StateBonus=50000;
		long NatBonus=70000;
		
		int winwidth;
		int winheight;
		
		// display updateBar
		bool displayUpdate = false;
		string displayUpdateValue=""; 
		
		public DiceValue V1Dice;      // stores the dices    
		public DiceValue V2Dice;
		
		private PathList Plist;        // the list of blocks with coords
		bool rollyes;                    // dice is in roll motion
		bool pause;            
		float startTime;                   // measuring turn times, roll times etc.
		float allTime;
		float deltaTime;
		
		float prevX=-2;
		float prevY=-2;
		
		bool strafe = false;               // boolean for camera movement
		
		bool pointOutPlayer=false;
		
		bool Rollpress;                   // Cast the dices ?
		bool Reroll;
		float rerollTime;                    // when to reroll
		
		public AudioClip tapsound;
		public AudioClip upSound;
		public AudioClip wrongTry;
		
		public int[] InfoBox = new int[64];     // if the current blobk is owned and by whom ?? fast access.
		
		public Camera MainCam;                               // Main and each player camera
		public Camera pl1;
		public Camera pl2;
		public Camera pl3;
		public Camera pl4;
		
		public bool thinking=false;                   // player is thinking after the roll
		
	    public GameObject Dicefab;                           // Dice gen prefab
		public GameObject Dice2fab;
		
		public GameObject PlayerFab;                        //  Player prefab
		
		
		public GUITexture Dice1;                            // Dice value display prefab
	 	public GUITexture Dice2;
		
		public GUITexture[] PlayerTextures = new GUITexture[4];
		// clipartist.info , freevector, 4vector
	
	    public Texture InfoButton;
		public Texture[] diceImages;
		
		bool InfoDisplay = false;
		string toDisplay;
		/*  
		 * Justice:0  Promotion:1  Chance:2   Jail:3  Surprise:4    City|Dis:5  State:6   National:7
		   Power:5   Water:6   Fire:7  Police:8   Housing:9   PublicWorks:10   Tourism:11  Health:12
		   Education:13   Agro:14   Roadways:15   Forest:16
		         Defence:17           Coal:18          Rail:19 
		 */
		
		string [] Dept_det = {"Justice", "Promotion", "Chance","Jail","Surprise","Power","Water","Fire","Police","Housing","PublicWorks","Tourism","Health","Education","Agro","Roadways","Forest","Defence","Coal","Rail"};
		Department[] Departments;
	
		void SellStart(int plID)
	{
		sellPlayer = plID;	
	
	}
		// Use this for initialization
		void Start () {
		
		
		    takeAttack = false;
		    DuelMode = false;
		
			winwidth = Screen.width;
			winheight = Screen.height;
			
			Debug.Log(winwidth+" "+winheight);
			
			// initialise 4player game
			Vector3 st = new Vector3(0,5,0);
			 long amount = 250000;
			
		     startTime =0;
			deltaTime = 0.5f;
			
			pause = false;
			rollyes = false;
			totalPlayers = 4;
			turn = 0;
			
			for(int i=0;i<64;i++)
				InfoBox[i]=-1;
			
				float x=-0.08f,y=-0.095f,wx=0.23f,wy=0.2f;
			Rect A = new Rect(x*winwidth,y*winheight,wx*winwidth,wy*winheight);
			
			wx = 0.05f;
			wy=0.1f;
			y = -0.077f;
			Rect B1 = new Rect(x*winwidth,y*winheight,wx*winwidth,wy*winheight);
		
		    float bX = -0.09f;
		    float bY = -0.1f;
		    float bH = 0.04f;
		    float bW = 0.02f;
		    Rect B2 = new Rect(bX*winwidth,bY*winheight,bW*winwidth,bH*winheight);
		
	 	    for(int i=0;i<totalPlayers;i++)
			{
				Players[i] = new GamePlayers();
				Players[i].init(i, amount, st, 0, 0, 0, 0, 5,true);
				PlayerTextures[i].pixelInset = A;
				PlayerTextures[i].transform.Find("logo").guiTexture.pixelInset = B1;
			    PlayerTextures[i].transform.Find("cuff1").guiTexture.pixelInset = B2;
			    PlayerTextures[i].transform.Find("cuff2").guiTexture.pixelInset = B2; 
				PlayerTextures[i].transform.Find("cuff3").guiTexture.pixelInset = B2;
			 
			}
			
			wx = 0.05f;
			wy = 0.05f;
			Rect D1 = new Rect(x*winwidth,y*winheight,wx*winwidth,wy*winwidth);  
			Dice1.pixelInset = D1;
			y=0.0001f;
			Rect D2 = new Rect(x*winwidth,y*winheight,wx*winwidth,wy*winwidth);  
			Dice2.pixelInset = D2;
			
			
		   
			
			
			
			
			Vector3 startpos = new Vector3(0,7.5f,0);
			
			GobPlayer = new GameObject[4];
			
		//	GobPlayer = Resources.LoadAll("Player") as GameObject[];
			
			float tosub=-1;
			
			for(int i=0;i<totalPlayers;i++)
			{
			GameObject instanceg = new GameObject();
				startpos.x = tosub + i;
			instanceg = Instantiate(PlayerFab,startpos,transform.rotation) as GameObject;
			GobPlayer[i] = instanceg;
			for(int j=0;j<4;j++)
			GobPlayer[i].transform.Find("Pcam"+j).camera.enabled = false;
			gameObject.transform.Find("Camera"+i).camera.enabled = false;
	///	gameObject.transform.Find("Camera"+i).camera.transform.Find("AmbientLight").light.enabled = false;
			}
			
			gameObject.transform.Find("Camera0").camera.enabled = true;
	//	gameObject.transform.Find("Camera0").camera.transform.Find("AmbientLight").light.enabled= true;
		    Plist = new PathList();
			Plist.initList();
			
				GameObject instance = new GameObject();
			    instance = Instantiate(Dicefab,instance.transform.position,instance.transform.rotation) as GameObject;
				
		
				instance.name="Dicer1";
					
				GameObject instance2 = new GameObject();
			    instance2 = Instantiate(Dice2fab,instance2.transform.position,transform.rotation) as GameObject;
				instance2.name="Dicer2";
				
					
				V1Dice = instance.GetComponent<DiceValue>();
				V2Dice = instance2.GetComponent<DiceValue>();
			
			Dice1 = Instantiate(Dice1) as GUITexture;
			
			Dice2 = Instantiate (Dice2) as GUITexture;
			
			
			initDepartments();
			
			// 
			Debug.Log("gameinited");
			
			
		}
		
		bool checkDeptBuy(int dnos)
		{
			if(Departments[dnos].Level == 2 || Departments[dnos].Level == 3 || Departments[dnos].Level == 4)
				return false;
			else
				return true;
		}
		
		bool checkProjOwn(int bl_nos)
		{
			 if(InfoBox[bl_nos] == -1)
				return true;
			else
				return false;
		}
		
		bool checkPlDtLev(int dnos)
		{
			Debug.Log(Departments[dnos].Level +" "+ Players[turn].level);
			if(Departments[dnos].Level > Players[turn].level)
				return false;
			else return true;
		}
		
		bool checkPlDtMoney(int dnos, int pnos)
		{
			Debug.Log(Departments[dnos].Projects[pnos].cost +" "+ Players[turn].Money);
			
			if(Departments[dnos].Projects[pnos].cost <= Players[turn].Money)
				return true;
			else
				return false;
		}
		
		void ProjBought(int dno, int pno, int bl_no)
		{
				                    InfoBox[bl_no]=turn;
									Players[turn].IndProjList[dno]++;
		                            Players[turn].LevelProjList[Departments[dno].Level]++;
		                            Players[turn].Money -= Departments[dno].Projects[pno].cost;
		                            Players[turn].TotProj++;
			                         paidOk = false;
		                             noMessage = false;
		                             Congomessage  = true;
		    int rl = TEXT.Length;
			subcongos = Random.Range(1,rl-1);
		}
		
	
		void InitiateBuy()
		{
			long cp;
			int DPlevel;
			
			int plevel;
			long monpl;
			
			int dno = -1,pno=-1;
			
			int bl_no = Players[turn].prevPosInd();
			
			if(bl_no == 0)
			{
			
				displayUpdateValue = "Go Reached";
				displayUpdate = true;
				return;
		
			}
			for(int i=0;i<Departments.Length;i++)
				{
					for(int j=0;j<Departments[i].indices.Length;j++)
					{
						if(Players[turn].prevPosInd() == Departments[i].indices[j])
						{
						Debug.Log(Departments[i].names+ " " + Departments[i].Projects[j].cost+" "+Departments[i].Projects[j].revenue);
					    dno=i;
						pno=j;
					   }
					}
		 		}
			
			bool D_buyType = checkDeptBuy(dno);
			bool freeProj  = checkProjOwn(bl_no);
			bool pl_dt_lev = checkPlDtLev(dno);
			bool pl_dt_money = checkPlDtMoney(dno,pno);
			
		Debug.Log(D_buyType+" "+freeProj+" "+pl_dt_lev+" "+pl_dt_money);
			
			if(D_buyType)
			{
			DPlevel = Departments[dno].Level;
			plevel = Players[turn].level;
			
				
				if(DPlevel == 0 && pl_dt_money)
				{
				    Players[turn].Money -= Departments[dno].Projects[pno].cost;
					displayUpdateValue = "Entered Duel Mode";
				    Mode = 0;
				    displayUpdate = true;
					audio.PlayOneShot(upSound);
					Dturn = turn;
				}
				else
				{
					if(DPlevel == 0)
					{
						displayUpdateValue = "Level Out of Reach";
				        displayUpdate = true;
						Debug.Log("Dont dream big");
						audio.PlayOneShot(wrongTry);
					}
				 	else{
						if(DPlevel == 1 && pl_dt_money)
						{
						if(Departments[dno].Projects[pno].internalevel == 2)
					 		displayUpdateValue = "Television Token Earned";
						if(Departments[dno].Projects[pno].internalevel == 1)
					 		displayUpdateValue = "News Token Earned";
						if(Departments[dno].Projects[pno].internalevel == 0)
					 		displayUpdateValue = "Radio Token Earned";
				            displayUpdate = true;
							Players[turn].GetToken(Departments[dno].Projects[pno].internalevel);
						    Players[turn].Money -= Departments[dno].Projects[pno].cost;
							audio.PlayOneShot(upSound);
						}
						else
						{
							if(DPlevel == 1)
							{
								displayUpdateValue = "Promotional Campaign Failed";
				                displayUpdate = true;
								audio.PlayOneShot(wrongTry);
							}
				            else
							{
								if(DPlevel >= 5 && freeProj && pl_dt_lev && pl_dt_money)
								{
								    ProjBought(dno,pno,bl_no);
									displayUpdateValue = "Project Bought";
				            		displayUpdate = true;
									audio.PlayOneShot(upSound);
								}
								else
								{
									audio.PlayOneShot(wrongTry);
									displayUpdateValue = "Failure !!";
				            		displayUpdate = true;
								}
							}
						}
					}
				}
			}
			else
			{
				displayUpdateValue = "Can't Buy this !!";
				displayUpdate = true;
			}
		}
	
	    public void GetOwnedInfo()
	    {	// The player owns which projects
		toDisplay="";
		Debug.Log (" Giving out Projects owned info ");
		for(int i=0;i<20;i++)
		{
			string ddisplay="";
			if(Players[turn].IndProjList[i]>0)
			{
				ddisplay += LevelNames[Departments[i].Level]+" "+Dept_det[i]+" "+Players[turn].IndProjList[i]+"\n";
			}
			toDisplay+=ddisplay;
		}
		 InfoDisplay = true;
	    }
	
	    public void RoundMoneyUpdate()
	{
		if(Players[turn].TotProj > 0 )
		{
			Players[turn].Money += Players[turn].Bonus;
		 for(int i=0;i<20;i++)
		{
			 if(Players[turn].LevelProjList[i]>0)
			{
				Players[turn].Money += Players[turn].LevelProjList[i]* Departments[Internal2Level[i]].Projects[0].revenue;
			    Debug.Log("Revenue: "+Players[turn].LevelProjList[i]* Departments[Internal2Level[i]].Projects[0].revenue);
			}
		}
		}
		else 
			Players[turn].Money += 10000+Players[turn].Bonus;
	}
	
	    void InformationGatheringFunction()
	{
		     for(int i=0;i<Departments.Length;i++)
				{
					for(int j=0;j<Departments[i].indices.Length;j++)
					{
						if(Players[turn].prevPosInd() == Departments[i].indices[j])
						{
						toDisplay = "";
						Debug.Log(Departments[i].names+ " " + Departments[i].Projects[j].cost+" "+Departments[i].Projects[j].revenue);
						string dp = "Name: "+Departments[i].names+"\n";
					    string lp = "Level: "+LevelNames[Departments[i].Level]+"\n";
						string cp = "Cost Price: "+Departments[i].Projects[j].cost+"\n";
						string sp = "Selling Price: "+Departments[i].Projects[j].sellprice+"\n";
						string re = "Revenue: "+Departments[i].Projects[j].revenue+"\n";
						string ownedby="Owner: "+InfoBox[Players[turn].prevPosInd()];
						toDisplay = dp+lp+cp+sp+re+ownedby;
				//		InfoDisplay = true;
						}
					}
		 		}
	}
	 
	    void ModeDecider()
	{
		int dno = 0,pno=0;
		for(int i=0;i<Departments.Length;i++)
				{
					for(int j=0;j<Departments[i].indices.Length;j++)
					{
						if(Players[turn].prevPosInd() == Departments[i].indices[j])
						{
						Debug.Log(Departments[i].names);
					    dno=i;
						pno=j;
					   }
					}
		 		}
		
		
		
		if(Departments[dno].Level == 2)
			Mode = 2;
		else
		{
		  if(Departments[dno].Level == 3)
			{
			Mode = 3;
				paidOk = false;
			}
			else
			{
		    if(Departments[dno].Level == 4)
			  Mode = 4;
				else
					Mode = 1;
			}
		}
		modeDecided  = true;
		
		Debug.Log(Departments[dno].Level+ " " + Mode);
	}
		void PlayerActionPerform()
		{
		  
		
  		
			
			if(strafe == true)
			{
			float moveDeltaX = (-prevX + Input.mousePosition.x)*1.5f;
			float moveDeltaY = (-prevY + Input.mousePosition.y)*1.5f;
			
			prevX = Input.mousePosition.x;
			prevY = Input.mousePosition.y;
			
			GobPlayer[turn].transform.Find("Pcam0").camera.transform.Translate(moveDeltaX,moveDeltaY,0); 
				
			}
			
			
	
			
			if(Input.GetKeyUp(KeyCode.U))
			{
			
			}
			
			if(Input.GetKeyUp(KeyCode.O))
			{
				GetOwnedInfo();
			}
			
			// Buy Initiative
			if(Input.GetKeyUp(KeyCode.B))
			{
			
				 InitiateBuy();
			}
			
		}
		
		void DuelChallenge()
		{
			if(DRoll == false)
			{
			  if(DefChance == true)
			{
				int ppos = Players[turn].prevPosInd();
				ppos = ppos/16;
				V1Dice.Rolling(ppos+1);
				V2Dice.Rolling(ppos+1);
			}
			else
			{
				int ppos = Players[Aturn].prevPosInd();
				ppos = ppos/16+1;
				V1Dice.Rolling(ppos);
			}
				DRoll = true;
				rollStime = Time.time;
			}
			else
			{
				float ttime = Time.time;
				randomDiceValue(false,1,2,ttime);
				if(ttime - rollStime >= 20.0)
				{
				DRoll = false;	
				V1Dice.sleep();
				V2Dice.sleep ();
			    }
				if(DefChance == true)
			{
				if(V1Dice.getRollValue() != -1 && V1Dice.getRollValue() != 0 && V2Dice.getRollValue() != 0 && V2Dice.getRollValue() != -1)
				{
					
				int Dvalue = Mathf.Max(V1Dice.getRollValue(),V2Dice.getRollValue());
				int gt_rv1 = V1Dice.getRollValue();
				int gt_rv2 = V2Dice.getRollValue();
				randomDiceValue(true,gt_rv1-1,gt_rv2-1,0);			
				V1Dice.sleep();
				V2Dice.sleep ();
				DRoll = false;
				
					
						AttVal = Dvalue;
						DefChance = false;		
				     	
				}
			}
			else
			{
				if(V1Dice.getRollValue() != -1 && V1Dice.getRollValue() != 0)
				{
					
				
				int gt_rv1 = V1Dice.getRollValue();
				randomDiceValue(true,gt_rv1-1,2,0);			
				V1Dice.sleep();
				DRoll = false;
						 DefVal = gt_rv1;
						 DefChance = true;
					     
					     MoveToRoll = false;
					     displayUpdate = true;
					     displayUpdateValue = "Att: "+AttVal+"Def: "+DefVal;
					     paidUp = false;
				}
			}
				
			}
		}
	void Upgrade()
	{
			bool possible = false;
				Debug.Log(Players[turn].level);
		//		int Slevel, int Proj2Own, long Mon2Own, int News2Own, int Rad2Own, int Tv2Own
			Debug.Log(Players[turn].level);
				if(Players[turn].level==CityLevel)
				{
					 if(Players[turn].city == true)
			        	possible = Players[turn].qualified2Upgrade(CityLevel+1,CityProj,CityMoney,CityNews,CityRadio,CityTv);
					else
						possible = Players[turn].qualified2Upgrade(DistLevel+1,DistProj,DistMoney,DistNews,DistRadio,DistTv);
				}
				else
				{
					if(Players[turn].level == StaLevel)
					{
						possible = Players[turn].qualified2Upgrade(StaLevel+1,StaProj,StaMoney,StaNews,StaRad,StaTv);
					}
					else
					{
						if(Players[turn].level == NatLevel)
						{
							possible = Players[turn].qualified2Upgrade(NatLevel+1,NatProj,NatMoney,NatNews,NatRad,NatTv);
						}
					}
				}
				if(possible == false)
				{
					audio.PlayOneShot(wrongTry);
					displayUpdateValue = "Upgrade Failed";
				    displayUpdate = true;
				}
				else
				{
					audio.PlayOneShot(upSound);
					displayUpdateValue = "Update Successful: Level "+ LevelNames[Players[turn].level];
				    displayUpdate = true;
				}
	}
	    void ResetValues()
	{
		  if(Players[turn].jailCount == 3)
			Players[turn].ingame = false;
		      Congomessage = false;
		                sellmode = false;
						PlayerTextures[turn].transform.Translate(new Vector3(0,-0.02f,0));
				//PlayerTextures[turn].transform.Find("logo").Translate(new Vector3(0,0,-1));
		
					PlayerTextures[turn].transform.Find("rank1").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank1").transform.position.x,PlayerTextures[turn].transform.Find("rank1").transform.position.y,-1);
				
				
					PlayerTextures[turn].transform.Find("rank2").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank2").transform.position.x,PlayerTextures[turn].transform.Find("rank2").transform.position.y,-1);
				
					PlayerTextures[turn].transform.Find("rank3").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank3").transform.position.x,PlayerTextures[turn].transform.Find("rank3").transform.position.y,-1);
				
					PlayerTextures[turn].transform.Find("rank4").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank4").transform.position.x,PlayerTextures[turn].transform.Find("rank4").transform.position.y,-1);
			
		for(int i=0;i<4;i++)
		{
			GobPlayer[turn].transform.Find("Pcam"+i).camera.enabled = false;	
			gameObject.transform.Find("Camera"+i).camera.enabled = false;
		}
	    //  gameObject.transform.Find("Camera"+actCam).camera.transform.Find("AmbientLight").light.enabled= false;
		
		GobPlayer[turn].transform.Translate(new Vector3(0,-Players[turn].tmpmx,0));
		Players[turn].tmpmx = 0;
		
						pointOutPlayer = false;
					    turn = (turn+1)%totalPlayers;
		rotation = Players[turn].prevPosInd();
		actCam = rotation/16;
		gameObject.transform.Find("Camera"+actCam).camera.enabled = true;
		//    gameObject.transform.Find("Camera"+actCam).camera.transform.Find("AmbientLight").light.enabled = true;
						thinking = false;	
						Rollpress = false;
						rollyes = false;
						V1Dice.sleep();
						V2Dice.sleep ();
		modeDecided = false;
		sellPlayer = -1;
		Mode = 1;
		paidUp = true;
					DuelMode = false;
		paidOk = true;
	}
	
	    void UpdateDuel()
		{
		if(paidUp == true)
		{
		 	if(MoveToRoll == true)
				{
		           DuelChallenge();
				}
				else
				{
				 takeAttack = true;
				
				}
		}
		if(paidUp == false)
		{
		  if(DefVal<AttVal)
			{
		  if(Players[Aturn].Money < Money2lose+AddnCharge && Players[Aturn].TotProj > 0)	
				{
					sellmode = true;
					sellPlayer = Aturn;
				}
		     else
				{
					if(Players[Aturn].Money < Money2lose+AddnCharge)
					{
						Players[Aturn].ingame = false;
						ResetValues();
					}
					else
					{
					 Players[turn].Money += Money2lose;
					 Players[Aturn].Money -= (Money2lose+AddnCharge);
					 ResetValues();
					}
				}
			}
			else
			{
				if(Players[Aturn].Money < Money2lose && Players[Aturn].TotProj > 0)	
				{
					sellPlayer = Aturn;
					sellmode = true;
				}
		     else
				{
					if(Players[Aturn].Money < Money2lose)
					{
					  Players[Aturn].ingame = false;
					  ResetValues ();
					}
					 else
					{
					 Players[Aturn].Money -= Money2lose;
					 ResetValues();
					}
					
				}
			}
		}
	}
	    void UpdateJail()
	{
	     	 
	}
	  
	    void UpdateNormal()
	{
		
		
			
	}
	
	    void DiceRoll()
	{
		if(rollyes == false && Rollpress == true && thinking == false)
			{
			 
			
				Debug.Log("turn is: "+turn);	
				if(pointOutPlayer == false)
				{
				    PlayerTextures[turn].transform.Translate(new Vector3(0,0.02f,0));
				//	PlayerTextures[turn].transform.Find("logo").Translate(new Vector3(0,0,1));
					pointOutPlayer = true;
				}
				rollyes = true;
				Rollpress = false;
				displayUpdate = false;
				displayUpdateValue="";
			    int ppos = Players[turn].prevPosInd();
			    ppos = ppos/16+1;
				V1Dice.Rolling(ppos);
				V2Dice.Rolling(ppos);	
				rerollTime = Time.time;
			    InfoDisplay = false;
			
		//				
			}
			
			
			if(rollyes == true)
			{
					
			    
			    if(thinking == false)
			{
				float ttime = Time.time;
				randomDiceValue(false,1,2,ttime);
					if(ttime - rerollTime >= 15.0)
				{
				rollyes = false;	
				V1Dice.sleep();
				V2Dice.sleep ();
			     }
			}
				
				
					
					pause = !pause;
														
				if(V1Dice.getRollValue() != -1 && V1Dice.getRollValue() != 0 && V2Dice.getRollValue() != 0 && V2Dice.getRollValue() != -1)
				{
				int zz = Players[turn].prevPosInd();
				 zz = zz/16;
			GobPlayer[turn].transform.Find("Pcam"+zz).camera.enabled = true;	
			gameObject.transform.Find("Camera0").camera.enabled =false;
					if(thinking == false)
					{
				int Dvalue = V1Dice.getRollValue()+V2Dice.getRollValue();
				int gt_rv1 = V1Dice.getRollValue();
				int gt_rv2 = V2Dice.getRollValue();
				int prevPos = Players[turn].prevPosInd();
				Vector3 CurrentPos = new Vector3();
				CurrentPos = Plist.getBoardPosition(prevPos); 	
				Vector3 nextMove = new Vector3();
				nextMove = Plist.getBoardPosition((prevPos+Dvalue)%64);		
				Players[turn].updatePosition(nextMove,Dvalue);						
				GobPlayer[turn].transform.Translate(nextMove[0]-CurrentPos[0],nextMove[1]-CurrentPos[1],nextMove[2]-CurrentPos[2]);
			    Debug.Log(Players[turn].prevPosInd()+" "+Dvalue+" "+Players[turn].roundcomp);
				audio.PlayOneShot(tapsound);
				pause = false;
				Debug.Log(Dvalue);
				 float xt = Time.time;
				 randomDiceValue(true,gt_rv1-1,gt_rv2-1,xt);			
				
					//roundCOmplete
						bool roundV = Players[turn].roundcomp;
			           if(roundV == true)
			           {
			     RoundMoneyUpdate();
				displayUpdateValue = "RoundCompleted";
				displayUpdate = true;
				Players[turn].roundcomp = false;
		        audio.PlayOneShot(upSound);	
						
					
		                }
					sellPlayer = turn;
					
				thinking = true;
					}
					else
					{
						if(thinking == true)
						{
							ModeDecider();
						}
					}
				}
			  }
	}
	void UpdateChance()
	{
		
	}
	
	void UpdateSurprise()
	{
		
	}
	
		// Update is called once per frame
		void Update () {
		
		if(goUp == true)
		{
			if(Players[turn].tmpmx < Umx)
			{
				GobPlayer[turn].transform.Translate(new Vector3(0,Players[turn].tmpmx,0));
				Players[turn].tmpmx += 0.0001f;
			}
			else
			{
				goUp = false;
			}
		}
		else{
			if(Players[turn].tmpmx > Dmx)
			{
				GobPlayer[turn].transform.Translate(new Vector3(0,-1* Players[turn].tmpmx,0));
				Players[turn].tmpmx -= 0.0001f;
			}
			else
			{
				goUp = true;
			}
		}
		
	/*	if(Input.GetKeyDown("1"))
	//		{
//			GobPlayer[turn].transform.Find("Pcam"+actCam).camera.enabled = true;	
		//	gameObject.transform.Find("Camera"+actCam).camera.enabled =false;
	//	    InfoDisplay = false;
	//		strafe = true;
			for(int i=0;i<totalPlayers;i++)
				{
					 if(i!=turn)
					{
				    PlayerTextures[i].guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("cuff1").guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("cuff2").guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("cuff3").guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("rank1").guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("rank2").guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("rank3").guiTexture.enabled = false;
					PlayerTextures[i].transform.Find("rank4").guiTexture.enabled = false;
					
					
					    PlayerTextures[i].transform.Find("logo").guiTexture.enabled = false;
					}
					 
					
				}
			}*/
			
			if(Input.GetKeyUp("1"))
			{
		//	strafe = false;
			swatch = !swatch;
			actCam = Players[turn].prevPosInd()/16;
				for(int i=0;i<4;i++)
				{
					 GobPlayer[turn].transform.Find("Pcam"+i).camera.enabled = false;	
			         gameObject.transform.Find("Camera"+i).camera.enabled = false;	
				}
			if(swatch == true)
			{
			gameObject.transform.Find("Camera"+actCam).camera.enabled =true;
			}
			else
			{
				GobPlayer[turn].transform.Find("Pcam"+actCam).camera.enabled = true;
			}
		}
		//	      gameObject.transform.Find("Camera"+actCam).camera.transform.Find("AmbientLight").light.enabled = true;
		//	GobPlayer[turn].transform.Find("Pcam").camera.transform.position = new Vector3(-1,15,0); 
			//InfoDisplay = true;
		/*	prevX = -2;
			prevY = -2;
			for(int i=0;i<totalPlayers;i++)
				{
					 PlayerTextures[i].guiTexture.enabled = true;
					   PlayerTextures[i].transform.Find("logo").guiTexture.enabled = true;
				  PlayerTextures[i].transform.Find("cuff1").guiTexture.enabled = true;
					PlayerTextures[i].transform.Find("cuff2").guiTexture.enabled = true;
					PlayerTextures[i].transform.Find("cuff3").guiTexture.enabled = true;
					PlayerTextures[i].transform.Find("rank1").guiTexture.enabled = true;
					PlayerTextures[i].transform.Find("rank2").guiTexture.enabled = true;
					PlayerTextures[i].transform.Find("rank3").guiTexture.enabled = true;
					PlayerTextures[i].transform.Find("rank4").guiTexture.enabled = true;
				}*/
			
		
			if(Mode == 0) // DuelMode
			{
			     UpdateDuel();
			}
		   else
		   {
		     if(Mode == 3) // JailCard
			{
				 UpdateJail();
			}
			else
			{
				if(Mode == 1) // Normal
				{
					if(Players[turn].ingame == false)
					{
						turn = (turn+1)%totalPlayers;
					}
					else
					{
					if(modeDecided == false)
					DiceRoll();
					else
					PlayerActionPerform();
					}
				}
				else
				{
					if(Mode == 2) // chance
					{
						UpdateChance();
					}
					else
					{
						if(Mode == 4) // Surprise
						{
							UpdateSurprise();
						}
					}
						
				}
			}
		
		}
	}
		
		void  initDepartments()
		{
			//init(int l, int intlevl, int chnos, int[] indx, int Own, string Nn, long cost, long revenue, long sell)
			/*  
		 * Justice:0  Promotion:1  Chance:2   Jail:3  Surprise:4    City|Dis:5  State:6   National:7
		   City|Dist: Power:0   Water:1   Fire:2  Police:3   Housing:4   PublicWorks:5   Tourism:6  Health:7
		   State: Education:0   Agro:1   Roadways:2   Forest:3
		   National: Defence:0           Coal:1          Rail:2 
		 */
			
			Departments = new Department[Dept_det.Length];
			
			for(int i=0;i<Dept_det.Length;i++)
				Departments[i] = new Department();
		
			// Others
			int [] jusindex={56};
			Departments[0].init(0,-1,jusindex.Length, jusindex,-1,"Justice" , 100000, 0, 0);
			
			int [] prindx={12,25,39,47,55};
			Departments[1].init(1, -1,prindx.Length, prindx,-1,"Promotion" , 30000, 0, 0);
			int [] cindx = {3,16,35,48,59};
			Departments[2].init(2,-1,cindx.Length, cindx, -1, "Chance", 0, 0, 0);
			int [] jaindex={32};
			Departments[3].init(3,-1,jaindex.Length, jaindex,-1,"Jail" , 0, 0, 0);
			int [] surindx={13,44};
			Departments[4].init(4, -1,surindx.Length, surindx,-1,"Surprise" , 0, 0, 0);
			
	
					// City | District
			int [] powindex={6,10,28,43,52};
			Departments[5].init(5,5,powindex.Length, powindex,-1,"Power" ,100000, 20000, 50000);
			int [] waindex={1,9,20,33,49};
			Departments[6].init(5,6,waindex.Length, waindex,-1,"Water" ,100000, 20000, 50000);
			int [] findex={2,14,23,36,53};
			Departments[7].init(5,7,findex.Length, findex,-1,"Fire" ,100000, 20000, 50000);
			int [] polindex={15,34,61};
			Departments[8].init(5,8,polindex.Length, polindex,-1,"Police" ,100000, 20000, 50000);
			 int [] hwindex={5,17,30,41,58};
			Departments[9].init(5,9,hwindex.Length, hwindex,-1,"Housing" ,100000, 20000, 50000);	
			int [] pwindex={7,21,38,54,60};
			Departments[10].init(5,10,pwindex.Length, pwindex,-1,"Public Works" ,100000, 20000, 50000);
		    int [] touindex={19,27,46};
			Departments[11].init(5,11,touindex.Length, touindex,-1,"Tourism" ,100000, 20000, 50000);
			int [] helindex={29,50,63};
			Departments[12].init(5,12,helindex.Length, helindex,-1,"Health" ,100000, 20000, 50000);
			
		   
			// State
			
			
			
			int [] eduindx={4,26,57};
			Departments[13].init(6,13,eduindx.Length, eduindx,-1,"Education" ,135000, 45000, 75000);
			int [] agrindx={22,31,45};
			Departments[14].init(6,14,agrindx.Length, agrindx,-1,"Agro" ,135000, 45000, 75000);
			int [] roindx={11,42,51};
			Departments[15].init(6,15,roindx.Length, roindx,-1,"Roadways" ,135000, 45000, 75000);
			int [] forindx={18,37,62};
			Departments[16].init(6,16,forindx.Length, forindx,-1,"Forest" ,135000, 45000, 75000);
			
			
			// National
			int [] defindx={8};
			Departments[17].init(7,17,defindx.Length, defindx,-1,"Defence" ,200000, 70000, 100000);
			int [] coaindx={24};
			Departments[18].init(7,18,coaindx.Length, coaindx,-1,"Coal" ,200000, 70000, 100000);
			int [] raindx={40};
			Departments[19].init(7,19, raindx.Length, raindx,-1,"Rail" ,200000, 70000, 100000);
			
			 
			
		}
	
		void sellScreen(int dp)
	{
		 Players[sellPlayer].Money += Departments[dp].Projects[0].sellprice;
		 Players[sellPlayer].IndProjList[dp]--;
		 Players[sellPlayer].LevelProjList[Departments[dp].Level]--;
		for(int i=0;i<Departments[dp].children;i++)
			{
			   if(InfoBox[Departments[dp].indices[i]] == sellPlayer)
			{
			   Players[sellPlayer].TotProj--;
		       InfoBox[Departments[dp].indices[i]] = -1;
			   break;
			}
			}
		
	}
		void randomDiceValue(bool yes, int val1, int val2 , float dt)
		{
			int x = ((int) Random.Range(0,1234))%6;
			int y = ((int)Random.Range(0,1234))%6;
			
			float xtime = dt - startTime;
			
			if(yes == false && xtime >= deltaTime)
			{
			//	Valdice1.GetComponent<GUITexture>();
				Dice1.guiTexture.texture = diceImages[x];
				Dice2.guiTexture.texture = diceImages[y];
				startTime = dt;		
			}
			if(yes == true)
			{
				startTime = 0;
				Dice1.guiTexture.texture = diceImages[val1];
				Dice2.guiTexture.texture = diceImages[val2];
			}
		}
		private void OnGUI()
		{
			
		if(Congomessage == true && noMessage == false)
		{
			float jx=0.25f,jy=0.2f,jh=0.43f,jw=0.45f;
			Rect MBox = new Rect(jx*winwidth,jy*winheight,jw*winwidth,jh*winheight);
			
			GUI.Box(MBox,congrats+"\n"+TEXT[subcongos]);
			float jx1 = 0.25f,jy1=0.5f,jh1=0.06f,jw1=0.05f,jx2=0.65f;
			Rect KLButt = new Rect(jx2*winwidth,jy1*winheight,jw1*winwidth,jh1*winheight);
			if(GUI.Button(KLButt,"CLOSE"))
			{
				Congomessage = false;
				noMessage = true;
				paidOk = true;
			}
		}
		
		if(Mode == 3 && paidOk == false)
		{
			float jx=0.25f,jy=0.2f,jh=0.43f,jw=0.45f;
			Rect JailBox = new Rect(jx*winwidth,jy*winheight,jw*winwidth,jh*winheight);
			JText = "Either Pay 50,000 to stay out of Jail or Get a Jail Token. \n If you have 3 Jail Tokens you'll be out of the game";
			GUI.Box(JailBox,JText);
			
			float jx1 = 0.25f,jy1=0.5f,jh1=0.06f,jw1=0.05f,jx2=0.65f;
			Rect PayButt = new Rect(jx1*winwidth,jy1*winheight,jw1*winwidth,jh1*winheight);
			Rect TokButt = new Rect(jx2*winwidth,jy1*winheight,jw1*winwidth,jh1*winheight);
			if(GUI.Button(PayButt,"Bribe")&& Players[turn].Money >= 50000)
			{
				Players[turn].Money -= 50000;
				paidOk = true;
			}
			else
			{
				if(GUI.Button(TokButt,"Jail") || (Players[turn].Money < 50000 && Players[turn].TotProj == 0))
				{
				         Players[turn].jailCount += 1;
					     paidOk = true;
				}
			}
		}
		
		    if(sellmode == true && sellPlayer != -1)
		{
			InfoDisplay = false;
			sellDisplay="";
			Rect sellbox = new Rect();
			float sboxw,sboxh,sboxx,sboxy;
			sboxx = 0.85f;
			sboxy = 0.3f;
			sboxw = 0.13f;
			sboxh = 0.45f;
			sellbox.x = sboxx*winwidth;
			sellbox.y = sboxy*winheight;
			sellbox.width = sboxw*winwidth;
			sellbox.height = sboxh*winheight;
			
		
			int whichDept = -1;
			int whichPro = -1;
			
			float addon=0;
			
		for(int i=0;i<20;i++)
		{
			string ddisplay="";
				
			if(Players[sellPlayer].IndProjList[i]>0)
			{
				Rect sshow = new Rect((sboxx-0.030f)*winwidth,(sboxy+0.03f+addon)*winheight,(sboxw-0.1f)*winwidth,(sboxh-0.43f)*winheight);
				ddisplay += LevelNames[Departments[i].Level]+" "+Dept_det[i]+" "+Departments[i].Projects[0].sellprice + "\n";
				if(GUI.Button(sshow,"Sell"))
					{
						whichDept = i;
						displayUpdate = true;
						displayUpdateValue = "SOLD";
						audio.PlayOneShot(upSound);
					}
				  addon += 0.028f;
			}
					
			sellDisplay+=ddisplay;
				
		}
		 
				GUI.Box(sellbox,"Sell Info\n"+sellDisplay);
			    if(whichDept != -1)
			{
				 sellScreen(whichDept);
				sellmode = !sellmode;
			}
		}
		
			
			if(takeAttack == true)
			{
			    
				 float p1x=0.82f,p1y=0.24f,p1w=0.034f,p1h=0.043f,addn=0;
			
			Rect OutBox = new Rect(p1x*winwidth,(p1y-0.04f)*winheight,(p1w*5)*winwidth,(p1h*2)*winheight);
			GUI.Box(OutBox,"Opponent to Frame");
			
				for(int i=0;i<totalPlayers;i++)
				{
				if(Players[i].ingame == true)
				{
				    Rect Choices = new Rect((p1x+addn)*winwidth,p1y*winheight,p1w*winwidth,p1h*winheight);
					if(i!=turn)
					 if(GUI.Button(Choices,i+" "))
				{
					 Aturn = i;
					 takeAttack = false;
					 MoveToRoll = true;
				}
					addn += 0.04f;
				}
			}
			}
			
			if(displayUpdate == true)
			{
				GUI.color = Color.red;
				float ux = 0.38f,uy=0.01f,uw=.25f,uh=0.06f;
				Rect UpInfo = new Rect(ux*winwidth,uy*winheight,uw*winwidth,uh*winheight);
				GUI.Box(UpInfo,displayUpdateValue);
				GUI.color = Color.white;
			}
			
			
			float butwidth,butheight,x,y;
			
			x= 0.85f;
			y= 0.01f;
			
			butwidth = 0.13f;
			butheight = 0.17f;
			
			Rect rollbutton = new Rect();
			rollbutton.x = x * winwidth;
			rollbutton.y = y * winheight;
			rollbutton.width = butwidth * winwidth;
			rollbutton.height = butheight * winheight;
			
			
			Rect informationbox = new Rect();
			float boxw,boxh,boxx,boxy;
			boxx = 0.85f;
			boxy = 0.3f;
			boxw = 0.13f;
			boxh = 0.45f;
			informationbox.x = boxx*winwidth;
			informationbox.y = boxy*winheight;
			informationbox.width = boxw*winwidth;
			informationbox.height = boxh*winheight;
			
		
			
			
			float lift = 5;
			
			Rect [] P = new Rect[4];
			P[0] = new Rect();
			P[1] = new Rect();
			P[2] = new Rect();
			P[3] = new Rect();
			
			
			x=0.062f;
			y=0.812f;
			butwidth = 0.175f;
			butheight = 0.18f;
			
			
			
		//	P[0] = new Rect(55,230,120,50);
	//		P[1] = new Rect(240,230,120,50);
		//	P[2] = new Rect(425,230,120,50);
		//	P[3] = new Rect(610,230,120,50);
			
	
			
			for(int i=0;i<totalPlayers;i++)
			{
				
				P[i].x = x * winwidth;
			    P[i].y = y * winheight;
			    P[i].width = butwidth * winwidth;
			    P[i].height = butheight * winheight;
			  
				if(pointOutPlayer == true)
				{
						
					P[turn].y -= 0.02f * winheight;
				}
			     if(i == turn && strafe == false)
			{
				Rect Ubutt = new Rect((x+0.055f)*winwidth,(y+0.14f)*winheight,(butwidth-0.130f)*winwidth,(butheight-0.135f)*winheight);
				 Rect sellButt = new Rect((x+0.01f)*winwidth,(y+0.08f)*winheight,(butwidth-0.135f)*winwidth,(butheight-0.135f)*winheight);
					Rect Ibutt = new Rect((x+0.01f)*winwidth,(y+0.14f)*winheight,(butwidth-0.135f)*winwidth,(butheight-0.135f)*winheight);
				  Rect DoneButt = new Rect((x+0.10f)*winwidth,(y+0.08f)*winheight,(butwidth-0.135f)*winwidth,(butheight-0.135f)*winheight);
				 Rect BuyButt = new Rect((x+0.055f)*winwidth,(y+0.08f)*winheight,(butwidth-0.135f)*winwidth,(butheight-0.135f)*winheight);
				if(pointOutPlayer == true)
				{
					Ubutt.y -= 0.02f*winheight;
					sellButt.y -= 0.02f*winheight;
					Ibutt.y -= 0.02f*winheight;
					DoneButt.y -= 0.02f*winheight;
					BuyButt.y -= 0.02f*winheight;
					if(Players[turn].star == 1)
					PlayerTextures[turn].transform.Find("rank1").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank1").transform.position.x,PlayerTextures[turn].transform.Find("rank1").transform.position.y,2);
				
					if(Players[turn].star == 2)
					PlayerTextures[turn].transform.Find("rank2").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank2").transform.position.x,PlayerTextures[turn].transform.Find("rank2").transform.position.y,2);
					if(Players[turn].star == 3)
					PlayerTextures[turn].transform.Find("rank3").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank3").transform.position.x,PlayerTextures[turn].transform.Find("rank3").transform.position.y,2);
					if(Players[turn].star == 4)
					PlayerTextures[turn].transform.Find("rank4").transform.position = new Vector3(PlayerTextures[turn].transform.Find("rank4").transform.position.x,PlayerTextures[turn].transform.Find("rank4").transform.position.y,2);
					
					for(int k=0;k<Players[turn].jailCount;k++)
					{
						PlayerTextures[turn].transform.Find("cuff"+(k+1)).transform.position = new Vector3(PlayerTextures[turn].transform.Find("cuff"+(k+1)).transform.position.x,PlayerTextures[turn].transform.Find("cuff"+(k+1)).transform.position.y,1);
					}
					
					//    PlayerTextures[turn].transform.FindChild("rank1").transform.position = new Vector3(PlayerTextures[turn].transform.FindChild("rank1").transform.position.x,PlayerTextures[turn].transform.FindChild("rank1").transform.position.y,-1);
				}
				if(GUI.Button(Ubutt,"Upgrade"))
				{
					Upgrade();
				}
				if(GUI.Button(sellButt,"Sell"))
				{
					 sellmode = !sellmode;
				}
				
				if(GUI.Button(DoneButt,"Done") && paidOk == true)
				{
					 if(sellPlayer == turn)
					{
						 ResetValues();
					}
				}
				
				if(GUI.Button(BuyButt,"Buy") && pointOutPlayer == true)
				{
					 if(Mode == 0 || Mode == 1)
					{
						InitiateBuy();
					}
				}
				
				 if(GUI.Button(Ibutt,InfoButton))
				{
					InfoDisplay  = !InfoDisplay;
					InformationGatheringFunction();
				}
			}
				x+=0.260f;
					string comb="";
					
					comb+=playerProfile[2]+Players[i].Money;
			if(strafe == false)
				GUI.Box(P[i],comb);
				else
					if(i==turn)
						GUI.Box(P[i],comb);
					
			}
			
				
	
				
			
			
			if(InfoDisplay == true && strafe == false)
			{
				GUI.Box(informationbox,toDisplay );
			}
			
			if(GUI.Button(rollbutton,"ROLL" ))
			{
				Rollpress = true;	
			}
			
			
			
		}
	}
