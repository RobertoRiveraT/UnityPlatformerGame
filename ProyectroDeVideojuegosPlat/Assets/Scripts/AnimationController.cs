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
    public bool _grounded = false;
    public float MaxSpeed = 10f;
    public float JumpForce = 400;
    public bool isGrabbing = false;

    public Transform carryLocation;
    public Transform drop;
    public Transform currentItem = null;
    //public Transform currentItem = null;

    public RaycastHit2D hit;
    public bool grabbed = false;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = player.GetComponent<PlayerController>();
    }

    void FixedUpdate() {

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
        if (currentItem != null) {
            pc.flyB = false;
            animator.SetBool("grab", true);
        }
        else
        {
            animator.SetBool("grab", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && onGround()) {
            Debug.Log("Jump");
            animator.SetBool("Ground", false);

            FindObjectOfType<AudioController>().Play("PlayerJump");
        }

        ////revisar si hay double jump !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (pc.doubleJumpCheck)
        {
            Debug.Log("pc: true");
            animator.SetBool("Shoot", false);
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            if (currentItem == null) {
                Debug.Log("Shoot");
                animator.SetBool("Throw", false);
                animator.SetBool("Shoot", true);

                FindObjectOfType<AudioController>().Play("PlayerGrab");
            }
            else if (currentItem != null) {
                animator.SetBool("Shoot", false);
                animator.SetBool("Throw", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentItem != null && !onGround()) {
                Debug.Log("Vas a caer");
                pc.flyB = false;
                pc.DJump();
                currentItem.position = pc.holdPoint.position;
                //Aqui Nacho 
                BoxCollider2D box = currentItem.GetComponent<BoxCollider2D>();
                box.isTrigger = false;
                Rigidbody2D rb = currentItem.gameObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 4.0f;
                currentItem.tag = "thrown";
                currentItem.parent = null;
                currentItem = null;
                grabbed = false;
                isGrabbing = false;
            }
            else {
                pc.flyB = true; ;
            }
        }

        if (isMoving())
        {
            Debug.Log("is moveing");
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

        if (Input.GetKeyDown(KeyCode.Space)) {

        }

        if (currentItem != null) {
            currentItem.position = carryLocation.position;
        }

        if (isFloating())
        {
            animator.SetBool("Float", true);
        }
    }

    public bool isMoving()
    {
        ///agregar el map con las jeys A y D sino no va a animar
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool onGround()
    {
        return pc.onPlatform();
    }

    public bool isFloating()
    {
        return pc.isFlying();
    }

    public void desGrab()
    {
        animator.SetBool("Shoot", false);
    }

    public void desFloat()
    {
        animator.SetBool("Float", false);
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

    public void desThrow()
    {
        animator.SetBool("Throw", false);
    }
    public void groundThrow()
    {
        if (onGround())
        {
            thowEnemy();
        }
    }
    public void airThrow()
    {
        if (!onGround())
        {
            thowEnemy();
        }
    }
    public void thowEnemy(){
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