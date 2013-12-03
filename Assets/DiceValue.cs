using UnityEngine;
using System.Collections;

public class DiceValue : MonoBehaviour {
	
	
	Vector3 Up = new Vector3(0,1,0);
	public bool rolling;
	public int move = -1;
	
	public AudioClip Rollsound;
	
	public void sleep()
	{
		gameObject.SetActive(false);
	}
	
	public int getRollValue()
	{
	
			return move;
	}
	// Use this for initialization
	void Start () {
	
		gameObject.SetActive(false);
		
	}
	
	public void Rolling(int dir)
	{
	gameObject.SetActive(true);	
	float velrand = Random.Range(1,9);
		float vrand = Random.Range(1,9);
		float swingRandX = Random.Range(35,50);
		float swingRandY = Random.Range(1,5);
		float swingRandZ = Random.Range(25,40);
		
		
	rolling = true;	
	if(dir == 1)
		{
			velrand = -velrand;
			vrand = 0;
		}
	if(dir == 2)
		{
			velrand = 0;
			vrand = vrand;
		}
		if(dir == 3)
			{
			velrand = velrand;
			vrand = 0;
		}
		if(dir == 4)
			{
			velrand = 0;
			vrand = -vrand;
		}
	gameObject.audio.PlayOneShot(Rollsound);
    transform.position = new Vector3(25,15,25);
	this.transform.Translate(0,vrand*2+velrand,vrand+velrand);
	this.rigidbody.velocity = new Vector3(5*vrand,2,5* velrand);
	this.rigidbody.angularVelocity = new Vector3(swingRandX,swingRandY,swingRandZ);
	move = -1;
	}
	
	void TurnOff()
	{
	    	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		
		if(rolling && this.rigidbody.velocity[0] == 0f && this.rigidbody.velocity[1] == 0f && this.rigidbody.velocity[2] == 0f)
		{
		Transform[] allChildren = GetComponentsInChildren< Transform >();
    	foreach (Transform child in allChildren) {
			
			Vector3 up = child.up;

			if(up[1]>0.0 && Mathf.Abs(up[0]) <0.1  && Mathf.Abs(up[1]) <= 1.1 && Mathf.Abs(up[2]) <=0.1 && Mathf.Abs(up[1])>= 0.9)
			{
			
					if(child.name == "side1")
						move = 1;
					if(child.name == "side2")
						move = 2;
					if(child.name == "side3")
						move = 3;
					if(child.name == "side4")
						move = 4;
					if(child.name == "side5")
						move = 5;
					if(child.name == "side6")
						move = 6;

				rolling = false;	
			}
		  }
		}
    }
	
}
