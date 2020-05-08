using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projDown : MonoBehaviour
{
    public float vel;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(this.gameObject.tag=="bad"){
        gameObject.transform.Translate(new Vector3(0,vel,0));
    	}

        if (this.gameObject.tag == "grab")
        {
            animator.SetBool("grabbed", true);
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
