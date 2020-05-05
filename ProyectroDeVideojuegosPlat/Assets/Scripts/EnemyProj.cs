using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public float vel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(vel,0,0));
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
    	GameObject ney= other.gameObject;
    	if(ney.tag!="checkpoint"){
    		Destroy(gameObject);
    		}
    }
    private void OnCollisionEnter2D(Collision2D other) {
    	Destroy(gameObject);
    		
    }
}
