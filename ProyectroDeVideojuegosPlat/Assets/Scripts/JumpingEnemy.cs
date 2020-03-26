using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
	public float salto;
	private Rigidbody2D cha;
	public float jumpTime;
    // Start is called before the first frame update
    void Start()
    {
    	cha= GetComponent<Rigidbody2D>();
    	if(this.gameObject.tag=="bad"){
        	StartCoroutine(jump());
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator jump(){
		while(true){
			yield return new WaitForSeconds(jumpTime);
			cha.velocity = Vector2.up*salto;
		}
	}
}
