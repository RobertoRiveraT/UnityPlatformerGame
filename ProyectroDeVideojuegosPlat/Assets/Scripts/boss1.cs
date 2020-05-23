using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour
{
	public int life;
	public float timeToColor;
	SpriteRenderer render;
    Color dColor;
    
    public Transform one;
    public Transform two;
    public Transform three;
    Vector3 _target;
    Vector3 ones;
    Vector3 twos;
    Vector3 threes;
    public float Speed;
    
    public Transform shooter1;
	public GameObject mil1;
	public Transform shooter2;
	public GameObject mil2;
    public float shootTime;
    
    void Start () {
		render = GetComponent<SpriteRenderer>();
        dColor = render.color;
		life = 8;
		ones = one.position;
        twos = two.position;
        threes= three.position;
        _target = ones;
	}
    
    void Update()
    {
    	if(life>4){
        	move();
    	}else{
    		moveT();
    	}
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
    	
    	GameObject ney= other.gameObject;
    	if(ney.tag=="thrown"||ney.tag=="thrownRight"||ney.tag=="thrownLeft"){
    		StartCoroutine("SwitchColor");
    		Destroy(other.gameObject);
    		life--;
    		Debug.Log(life);
    		if(life==4){
    			setTarget(threes);
    			Speed=4;
    			StartCoroutine(shootGood());
    		}
    		if(life==0){
    		Destroy(gameObject);
    		}
    	}
    }
    
    IEnumerator SwitchColor()
    {
        render.color = new Color(2,0,0);
        yield return new WaitForSeconds(timeToColor);
        render.color = dColor;
    }
    
    void setTarget(Vector3 target) {
        _target = target;
    }
    
    void move() {
        float distance = Vector3.Distance(gameObject.transform.position, _target);
        if (distance <= 0) {
            if (_target == ones) {
                setTarget(twos);
            } else {
                setTarget(ones);
            }
        } else {
            transform.position = Vector3.Lerp(transform.position, _target, (Time.deltaTime * Speed) / distance);
        }
    }
    
    void moveT() {
        float distance = Vector3.Distance(gameObject.transform.position, _target);
        if (distance <= 0) {
            if (_target == ones) {
                setTarget(twos);
            } else {
        		if(_target == twos){
                setTarget(threes);
        		}else{
        			setTarget(ones);
        		}
            }
        } else {
            transform.position = Vector3.Lerp(transform.position, _target, (Time.deltaTime * Speed) / distance);
        }
    }
    
    private void shoot(){
		Instantiate (mil1, shooter1.position,Quaternion.identity);
		Instantiate (mil2, shooter2.position,Quaternion.identity);
	}
    
    IEnumerator shootGood(){
		while(true){
			yield return new WaitForSeconds(shootTime);
			shoot();
		}
	}
	
}
