using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public PlayerController pc;
    public LayerMask EnemyLayer;
    
    public Transform carryLocation;
	public Transform drop;	
    public Transform currentItem = null;
    //public Transform currentItem = null;
    
    public RaycastHit2D hit;
    public bool grabbed=false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            animator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Grab");
            animator.SetBool("grab", true);
        }else{
            //animator.SetBool("grab", false);
        }

        if (isMoving() && !onAir())
        {
            Debug.Log("moving");
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
        
         if(Input.GetKeyDown(KeyCode.Space))
         {
           
         }

         if(currentItem!=null){
         	currentItem.position = carryLocation.position;
         }
    }

    public bool isMoving()
    {
        return (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));
    }

    public bool onAir()
    {
        return !(pc.onPlatform());
    }

    public void desGrab()
    {
        animator.SetBool("grab", false);
    }
    
    public void grab()
    {
    	Debug.Log("Grabbing");
        
         if (currentItem!=null)
            {
                // remove as child
                currentItem.parent = null;

                //set position near player
                
               	currentItem.position = drop.position;
                currentItem.tag="bad";
				Rigidbody2D rb= currentItem.gameObject.GetComponent<Rigidbody2D>();
    			rb.gravityScale=1;
                // release reference
                currentItem = null;
                grabbed=false;
         }else{
         	Debug.Log("NoItem");
    		if(pc.fRight){
    			hit= Physics2D.Raycast(pc.gameObject.transform.position,
    			pc.gameObject.transform.right,
    			5.0f,
    			EnemyLayer);
    		}else{
    			hit= Physics2D.Raycast(pc.gameObject.transform.position,
    			pc.gameObject.transform.right*(-1),
    			5.0f,
    			EnemyLayer);
    		}
    		if(hit.collider != null){
    			Debug.Log("detected");
    			grabbed=true;
    			currentItem= hit.collider.transform;
    			currentItem.tag="grab";
    			Rigidbody2D rb= currentItem.gameObject.GetComponent<Rigidbody2D>();
    			rb.gravityScale=0;
    			currentItem.position = carryLocation.position;
    			currentItem.parent = transform;
    		}
         }


        
    }

}