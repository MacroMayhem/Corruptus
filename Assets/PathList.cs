using UnityEngine;
using System.Collections;

public class PathList {
	
	//Directions(1,2,3,4), Path(1,2,3,4,5)
	Vector3 [] List;
	//Vector containing cordinates to move to;
	
	// Use this for initialization
	public void Start () {	
	}
	
	public Vector3 getBoardPosition(int pos)
	{
		return List[pos];
	}
	
	public void initList()
	{
		//Direction1
	  	List = new Vector3[64];
		List[0] = new Vector3(0,5,0);
		List[1] = new Vector3(5,5,0);
		List[2] = new Vector3(10,5,0);
		List[3] = new Vector3(15,5,0);
		List[4] = new Vector3(20,5,0);
		
		List[5] = new Vector3(20,5,5);	
		List[6] = new Vector3(20,5,10);
		List[7] = new Vector3(20,5,15);
		
		List[8] = new Vector3(27.5f,5,15);
         
		List[9] = new Vector3(35,5,15);
		List[10] = new Vector3(35,5,10);
		List[11] = new Vector3(35,5,5);
		
		List[12] = new Vector3(35,5,0);
		List[13] = new Vector3(40,5,0);
		List[14] = new Vector3(45,5,0);
		List[15] = new Vector3(50,5,0);
		
		
		// Now Direction 2
		List[16] = new Vector3(55,5,0);
		List[17] = new Vector3(55,5,5);
		List[18] = new Vector3(55,5,10);
	    List[19] = new Vector3(55,5,15);
		List[20] = new Vector3(55,5,20);
		
		List[21] = new Vector3(50,5,20);
		List[22] = new Vector3(45,5,20);
		List[23] = new Vector3(40,5,20);
		
		List[24] = new Vector3(40,5,27.5f);
		
		
		List[25] = new Vector3(40,5,35);
		List[26] = new Vector3(45,5,35);
		List[27] = new Vector3(50,5,35);
		
		List[28] = new Vector3(55,5,35);
		List[29] = new Vector3(55,5,40);
		List[30] = new Vector3(55,5,45);
		List[31] = new Vector3(55,5,50);
		
		//Direction 3
		List[32] = new Vector3(55,5,55);
		List[33] = new Vector3(50,5,55);
		List[34] = new Vector3(45,5,55);
		List[35] = new Vector3(40,5,55);
		List[36] = new Vector3(35,5,55);
	
		List[37] = new Vector3(35,5,50);
		List[38] = new Vector3(35,5,45);
		List[39] = new Vector3(35,5,40);
		
		List[40] = new Vector3(27.5f,5,40);
		
		List[41] = new Vector3(20,5,40);
		List[42] = new Vector3(20,5,45);
		List[43] = new Vector3(20,5,50);
		
		List[44] = new Vector3(20,5,55);
		List[45] = new Vector3(15,5,55);
		List[46] = new Vector3(10,5,55);
		List[47] = new Vector3(5,5,55);
		
		//Direction4
		
		List[48] = new Vector3(0,5,55);
		List[49] = new Vector3(0,5,50);
		List[50] = new Vector3(0,5,45);
		List[51] = new Vector3(0,5,40);
		List[52] = new Vector3(0,5,35);
		
		List[53] = new Vector3(5,5,35);
		List[54] = new Vector3(10,5,35);
		List[55] = new Vector3(15,5,35);
		
		List[56] = new Vector3(15,5,27.5f);
		
		List[57] = new Vector3(15,5,20);
		List[58] = new Vector3(10,5,20);
		List[59] = new Vector3(5,5,20);
		
		List[60] = new Vector3(0,5,20);
		List[61] = new Vector3(0,5,15);
		List[62] = new Vector3(0,5,10);
		List[63] = new Vector3(0,5,5);
	}
}
