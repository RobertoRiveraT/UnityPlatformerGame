using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingEnemyGrabThrow : MonoBehaviour
{
	public GameObject one;
	public GameObject two;
	public GameObject papa;
	
    // Update is called once per frame
    void Update()
    {
    	if(this.gameObject.tag=="grab"){
    		Destroy(one);
    		Destroy(two);
    	}
    }
    
    private void OnCollisionEnter2D(Collision2D other){
    	Debug.Log("col");
    	if(this.gameObject.tag=="thrownRight"||this.gameObject.tag=="thrownLeft"||this.gameObject.tag=="thrown"){
    		Destroy(papa);
    		
    	}
	}
}
