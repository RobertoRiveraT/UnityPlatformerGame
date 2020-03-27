using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public PlayerController pc;
    public LayerMask EnemyLayer;
    public Transform hold;
    public  bool _grounded = false;
    public float MaxSpeed = 10f;
    public float JumpForce = 400;
    public bool isGrabbing = false;
    
    public Transform carryLocation;
	public Transform drop;	
    public Transform currentItem = null;
    //public Transform currentItem = null;
    
    public RaycastHit2D hit;
    public bool grabbed=false;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = player.GetComponent<PlayerController>();
    }

    void FixedUpdate (){

        _grounded = onGround();
        animator.SetBool("Ground", _grounded);
        animator.SetFloat("vSpeed", pc.GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");
        Rigidbody2D r2d = player.GetComponent<Rigidbody2D>();
        animator.SetFloat("Speed", Mathf.Abs(move));
        r2d.velocity = new Vector2(move * MaxSpeed, r2d.velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
        if(currentItem!= null){
            pc.flyB = false;
            animator.SetBool("grab", true);
        }
        else
        {
            animator.SetBool("grab", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && onGround()){
            Debug.Log("Jump");
            animator.SetBool("Ground", false);
        }

        if (Input.GetKeyDown(KeyCode.Z)){
            if (currentItem == null){
                Debug.Log("Shoot");
                animator.SetBool("Shoot", true);
            }else if (currentItem != null){
                animator.SetBool("Throw", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentItem!=null && !onGround()){
                Debug.Log("Vas a caer");
                pc.flyB = false;
                pc.DJump();
                currentItem.position = pc.holdPoint.position;
                //Aqui Nacho 
                BoxCollider2D box= currentItem.GetComponent<BoxCollider2D>();
               	box.isTrigger=false;
				Rigidbody2D rb= currentItem.gameObject.GetComponent<Rigidbody2D>();
				rb.bodyType = RigidbodyType2D.Dynamic;
    			rb.gravityScale=4.0f;
    			currentItem.tag="thrown";
                currentItem.parent = null;
                currentItem = null;
                grabbed=false;
                isGrabbing = false;
            }
            else{
                pc.flyB = true;;
            }
        }

        if (isMoving() && onGround())
        {
            Debug.Log("moving");
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
           
        }

        if(currentItem!=null){
            currentItem.position = carryLocation.position;
        }
    }

    public bool isMoving()
    {
        return (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));
    }

    public bool onGround()
    {
        return pc.onPlatform();
    }

    public void desGrab()
    {
        animator.SetBool("Shoot", false);
    }
    
    public void grab()
    {
    	Debug.Log("Grabbing");
        isGrabbing = true;
         if (currentItem!=null)
            {
                //THROW ENEMY
                // remove as child
                currentItem.parent = null;

                //set position near player
                
               	currentItem.position = drop.position;
               	if(pc.fRight){
                	currentItem.tag="thrownRight";
               	}else{
               		currentItem.tag="thrownLeft";
               	}
               	
               	BoxCollider2D box= currentItem.GetComponent<BoxCollider2D>();
               	box.isTrigger=false;
				Rigidbody2D rb= currentItem.gameObject.GetComponent<Rigidbody2D>();
				rb.bodyType = RigidbodyType2D.Dynamic;
    			rb.gravityScale=0;
                // release reference
                currentItem = null;
                grabbed=false;
                isGrabbing = false;
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
    			BoxCollider2D box= currentItem.GetComponent<BoxCollider2D>();
    			rb.gravityScale=0;
    			rb.constraints = RigidbodyConstraints2D.None;
    			if(box.isTrigger==false){
    				box.isTrigger=true;
    			}
    			currentItem.position = carryLocation.position;
    			currentItem.parent = transform;
    		}
         }

     



        }

    public void thowEnemy(){
        animator.SetBool("Throw", false);
        if (currentItem != null){
            
            //THROW ENEMY
            // remove as child
            currentItem.parent = null;

            //set position near player

            currentItem.position = drop.position;
            if (pc.fRight)
            {
                currentItem.tag = "thrownRight";
            }
            else
            {
                currentItem.tag = "thrownLeft";
            }

            BoxCollider2D box = currentItem.GetComponent<BoxCollider2D>();
            box.isTrigger = false;
            Rigidbody2D rb = currentItem.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;
            // release reference
            currentItem = null;
            grabbed = false;
            isGrabbing = false;
        }
    }
      


    }