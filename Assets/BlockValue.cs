using UnityEngine;
using System.Collections;

public class BlockValue : MonoBehaviour {
	
	int Department_Level;
	/* -3 : Surprise 
	 * -1 : Jail 
	 *  0 : Chance block
	 *  1 : City / District
	 *  2 : State
	 *  3 : National
	 */
	
	int Dept_Cost;
	
	
	/* 0 : Not Owned
	 * i : i player owns it
	*/
	int Owned;
	
	
	string Department;
	/* fire, water, power, housing, pubwork
	 * police, health, tourism
	 * edu, forest, agro, road
	 * rail, defence, coal
	 */
	
	// Use this for initialization
	void Start () {
	
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
