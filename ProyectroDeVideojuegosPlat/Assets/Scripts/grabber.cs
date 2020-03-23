using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabber : MonoBehaviour
{
    public bool grabbed;
    RaycastHit2D hit;
    public float distance;
    public Transform holdPoint;
    public float throwForce;
    public LayerMask notbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            if(!grabbed){
                //Physics2D.raycastsStartInColliders = false;
                hit =Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, distance);
                if(hit.collider!= null && hit.collider.tag=="bad"){
                    grabbed = true;
                }
            }else{
                grabbed = false;
                if(hit.collider.gameObject.GetComponent<Rigidbody2D>()!= null){
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x,1)*throwForce;
                }
            }
        }

        if(grabbed){
            Debug.Log(hit.collider.gameObject);
            hit.collider.gameObject.transform.position=holdPoint.position;
        }
    }

}
