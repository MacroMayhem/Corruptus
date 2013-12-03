using UnityEngine;
using System.Collections;

public class PieceMovement : MonoBehaviour {
	
	// jump to make, position in PathList, pressbutton
	int playerID;

	bool press;
	PathList Plist;
	Vector3 CurrentPos;
	public DiceValue V1Dice;
	public DiceValue V2Dice;
	int turn;
	int blockNos = 0;
	
	// Use this for initialization
	void Start () {
		//at GO
		press = false;
		Plist = new PathList();
		Plist.initList();
		turn = 0;
		CurrentPos = new Vector3(0,5,0);
	}
	
	// Update is called once per frame
	void Update () {
	 if(Input.GetKey("right")&& press==false)
		{
			if(turn%4==0)
			{
		    blockNos += V1Dice.move;
			Vector3 nextMove = Plist.getBoardPosition(blockNos%64);
			transform.Translate(nextMove[0]-CurrentPos[0],nextMove[1]-CurrentPos[1],nextMove[2]-CurrentPos[2]);
			press=true;
			CurrentPos = nextMove;
			}
				turn++;
			
		}
		
     if(Input.GetKey("left")&& press==true)
		{
			press=false;
		}
	}
}
