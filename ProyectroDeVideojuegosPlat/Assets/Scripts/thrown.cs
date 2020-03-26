using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrown : MonoBehaviour
{
	public float force;
	private Rigidbody2D cha;
    // Start is called before the first frame update
    void Start()
    {
        cha= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(this.gameObject.tag=="thrownRight"){
    		Debug.Log("Right");
    		cha.velocity=new Vector2(force,0);
    	}
    	if(this.gameObject.tag=="thrownLeft"){
    		Debug.Log("Left");
    		cha.velocity=new Vector2(force*(-1),0);
    	}
    }
    
    private void OnCollisionEnter2D(Collision2D other){
    	Debug.Log("col");
    	if(this.gameObject.tag=="thrownRight"||this.gameObject.tag=="thrownLeft"||this.gameObject.tag=="thrown"){
			Destroy(gameObject);
    	}
	}
}
