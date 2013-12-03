using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	public class GamePlayers
	{
	public long Bonus = 0;
	    public float tmpmx = 0;
	    public int star = 1;
		public int playerID;    // id of player
		public long  Money;   // Money owned 
		Vector3 position = new Vector3();    // position in 3d
		public int jailCount=0; // times went to jail
		int Npaper;   // Newspaper token
		int rad; // radio token
		int tv;       // tv token
		public int level;   // level of player
		public bool ingame = true;     // still playing or out ?
	    int posValue=0;      // block number he is on
	    public int [] ProjLevelOwned = {0,0,0,0,0,0,0,0,0};
	    public int skipChance=0;
	    
	    public int TotProj=0;
	    public bool city = true;
	    public int Tprojs;
	    public int[] LevelProjList = new int[20];
	    public int[] IndProjList = new int[20];
	 
	
	    public bool roundcomp;
	
	
	public void GetToken(int t)
	{ // Updating the token count
		if(t==0)
			Npaper++;
	    if(t==1)
			rad++;
		if(t==2)
			tv++;
	}
	
	
	
	void TransitLevel(long mon, int new2own, int rad2own, int tv2own)
	{// Transition between the levels
		if(city == false)
		level +=1;
		else
			city = false;
		Money -= (mon/2);
		Npaper -= new2own;
		rad -= rad2own;
		tv -= tv2own;
		star++;
		Bonus = (level-4) * 15000;
	}
	
	    public bool qualified2Upgrade(int Slevel, int Proj2Own, long Mon2Own, int News2Own, int Rad2Own, int Tv2Own)
	{// If the player is eligible to move up a status
		if(level<0 && level != Slevel-1)
			return false;
		int Pown = LevelProjList[level];
		
		
		Debug.Log(Pown+" "+Money+ " "+Npaper+" "+rad+ " "+tv);
		
		if(Proj2Own<=Pown && Mon2Own<= Money && News2Own <= Npaper && Rad2Own<= rad && Tv2Own <= tv)
		{
			TransitLevel(Mon2Own, News2Own, Rad2Own, Tv2Own);
			return true;
		}
		
		else
			return false;
		
	}
	
		public void init (int id, long  balance, Vector3 pos, int jCount, int news, int radio, int telv, int status, bool gameval)
		{ 
		
			playerID = id;
			Money = balance;
			position = pos;
			jailCount = jCount;
			Npaper = news;
			rad = radio;
			tv = telv;
			level = status;
			ingame = gameval;
		    posValue = 0;
		    Tprojs = 0;
		
	  	    for(int i=0;i<20;i++)
	 	{
			 IndProjList[i]=0;
			 LevelProjList[i]=0;
      	}
		    
		
		}
	
	
	
	public void updatePosition(Vector3 newPos, int index)
	{// U[pdating the position of the player after the move
		  posValue += index;
		if(posValue>=64)
			roundcomp = true;
		  posValue = posValue%64;
		  position = newPos;
	}
	
	public int prevPosInd()
	{// returns the current position of player
		return posValue;
	}
	
	}

