using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projDown : MonoBehaviour
{
     public float vel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(this.gameObject.tag=="bad"){
        gameObject.transform.Translate(new Vector3(0,vel,0));
    	}
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
    	GameObject ney= other.gameObject;
    	if(ney.tag!="checkpoint"&&this.gameObject.tag=="bad"){
    		Destroy(gameObject);
    		}
    }
    private void OnCollisionEnter2D(Collision2D other) {
    	if(this.gameObject.tag=="bad"){
    		Destroy(gameObject);
    	}
    		
    }
}
