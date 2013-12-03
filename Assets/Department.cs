using UnityEngine;
using System.Collections;

public class Department
{
	
     public int Level;    // Level of department
	 public int internalLevel;   // position in the department list
	 public int children;          // number of child of this department
	 public int [] indices;        // indices of the children
	 public int owned;           // is the department owned ?
	 public Project[] Projects;     // What projects are owned by this player 
	 public string names;        // Name of the department
	
	 int tvalue;
	 int [] fire = {};
	 
public	void init(int l, int intlevel,int chnos, int[] indx, int Own, string Nn, long cost, long revenue, long sell)
	{
		
		Level = l;
		children = chnos;
		indices = new int[indx.Length];
		indices = indx;
		owned = Own;
		names = Nn;
		internalLevel = intlevel;
		
		Projects = new Project[chnos];
		
		
		for(int i=0;i<chnos;i++)
		{
			
		if(l == 1)
		{
			   if(i==0)
				{
		       intlevel = 2;
			   cost = 75000;
				}
			   if(i==1 || i==3)
				{
			   intlevel = 1;
					cost = 35000;
				}
			   if(i==2 || i==4)
				{
			   intlevel = 0;
					cost = 50000;
				}
		}
		
			Projects[i] = new Project();
			Projects[i].init(Own, cost, revenue, sell, intlevel,i);
		}
		
	}
	
};

