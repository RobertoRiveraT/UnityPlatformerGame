using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    public string PlayerName;
    public Rigidbody2D rigid;

    [SerializeField] private HealthBar healthBar;

    public SpriteRenderer TheSpriteRenderer;
    //private PlayerBase playerBase;
    public BoxCollider2D boxc;
    public bool useGravity;
    public Sprite[] frames;

    public bool fRight = true;

    public float jumpHeight, health;
    public float playerVelocity;
    public float playerRunningVelocity;
    public int doubleJump;
    public int floatP;
    public float rate = 1.5f;
    float nextSpawn = 0.0f;

    public float lastPressTime;

    public float iniX;
    public float iniY;

    public int step = 0;
    float flyTime = 1.5f;

    
    private Animator animator;
    
    public Transform carryLocation; 
    public Transform currentItem = null;

    // Start is called before the first frame update
    void Start()
    {

        iniX = gameObject.transform.position.x;
        iniY = gameObject.transform.position.y;
        rigid = transform.GetComponent<Rigidbody2D>();
        boxc = transform.GetComponent<BoxCollider2D>();
        health = 1f;
        healthBar.setHealth(health);

        step = 0;

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        boxc.enabled = true;

        float x = Input.GetAxis("Horizontal");

        //run
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        { 
            if(onPlatform()){
                gameObject.transform.Translate(new Vector3(x * playerRunningVelocity, 0, 0));
            //animator.SetBool("running", true);
            }
            else{
                gameObject.transform.Translate(new Vector3(x * playerRunningVelocity/2, 0, 0));
            }
            
        }
        else
        {
            //walk
            if(onPlatform()){
                gameObject.transform.Translate(new Vector3(x * playerVelocity, 0, 0));
      
            }
            else{
                gameObject.transform.Translate(new Vector3(x * playerVelocity/2, 0, 0));
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("moving");
            //animator.SetBool("running", true);
        }


        if (step > 7)
        {
            step = 0;
        }


        if (onPlatform())
        {
            doubleJump = 1;
            floatP = 1;
            rigid.rotation = 0;
        }
        /*if (Input.GetKeyDown(KeyCode.Space) && doubleJump > 0)
        {
            rigid.velocity = Vector2.up * jumpHeight;
            rigid.rotation = 0;
            doubleJump--;
        }*/
        if (Input.GetKeyDown(KeyCode.Space) && floatP > 0 && doubleJump > 0)
        {   

            fly();
            

            rigid.velocity = Vector2.up * jumpHeight;
            rigid.rotation = 0;
            floatP--;
        }
        else if (onPlatform() && Input.GetKeyDown(KeyCode.Space))
        {
            //fly();
            rigid.rotation = 0;
            rigid.velocity = Vector2.up * jumpHeight;
            doubleJump = 1;
            floatP = 1;
        }

        if (fRight == false && x > 0)
        {
            Flip();
        }
        else if (fRight == true && x < 0)
        {
            Flip();
        }

        if (Input.GetKey("down") && onPlatform())
        {
            boxc.enabled = false;
            
            //print("down arrow key is held down");
        }

        if (gameObject.transform.position.y < -5)
        {
            //Debug.Log("caiste");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public bool onPlatform()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxc.bounds.center, boxc.bounds.size, 0f, Vector2.down, .1f, layerMask);
        return raycastHit2d.collider != null;
    }

    public void Flip()
    {
        fRight = !fRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;

        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Col");
        GameObject ney = other.gameObject;
        if (ney.tag == "bad")
        {
            //el jugador recibe daño
            Debug.Log("ouch");
            health = health - .1f;
            if(health <= 0){
                Debug.Log("You died");
                healthBar.setHealth(0);
            }
            else{
                Debug.Log(health);
                healthBar.setHealth(health);
            }
            
        }
    }
    private void fly(){
        //gameObject.transform.Translate(new Vector2(.2f,0.0f));
        /*useGravity = !useGravity;
            
        if(Time.time > nextSpawn){
            useGravity = true;
            nextSpawn = Time.time + rate;
        }

        if (!useGravity){
            rigid.gravityScale = 0.0f;
        }*/
            
    }
}


