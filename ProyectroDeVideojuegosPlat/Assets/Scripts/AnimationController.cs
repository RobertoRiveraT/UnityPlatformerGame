using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public PlayerController pc;
    public LayerMask EnemyLayer;

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
        /*
         if (pc.currentItem!=null)
            {
                // remove as child
                pc.currentItem.parent = null;

                //set position near player
                //pc.currentItem.position = transform.GetComponent<pc.TheSpriteRenderer>().bounds.max;

                // release reference
                pc.currentItem = null;
            }
    	RaycastHit2D hit;
    	if(pc.fRight){
    		hit= Physics2D.Raycast(pc.gameObject.transform.position,
    		pc.gameObject.transform.right,
    		1.0f,
    		EnemyLayer);
    	}else{
    		hit= Physics2D.Raycast(pc.gameObject.transform.position,
    		pc.gameObject.transform.left,
    		1.0f,
    		EnemyLayer);
    	}
    	if(hit.collider != null){
    		Debug.Log("grab");
    	}


        */
    }

}