using UnityEngine;
using System.Collections;

public class Project
{
	public int owned;        // Owner of this project
	public long cost;
	public long revenue;
	public long sellprice;   
	public int internalevel;  // depts internal level
	public int chnos; // which number child of dept it is
	
	public void init(int own, long CP, long R, long SP, int intlevel, int childn)
	{
		owned = own;
		cost = CP;
		revenue = R;
		sellprice = SP;
		internalevel = intlevel;
		chnos = childn;
	}
	
}

