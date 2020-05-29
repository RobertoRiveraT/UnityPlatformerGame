using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour
{
	public int life;
	public float timeToColor;
	SpriteRenderer render;
    Color dColor;
    public GameObject canvasObject;
    public GameObject canvas1Object;
    public GameObject starObject;
    public GameObject star1Object;
    public GameObject star2Object;
    public GameObject btu;

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
        canvasObject.SetActive(false);
                canvas1Object.SetActive(false);

                starObject.SetActive(false);
                star1Object.SetActive(false);
                star2Object.SetActive(false);
                btu.SetActive(false);
	}
    
    void Update()
    {
        Debug.Log(SaveSystem.numMuertes);
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
                canvasObject.SetActive(true);
                canvas1Object.SetActive(true);
                starObject.SetActive(true);
                star1Object.SetActive(true);
                star2Object.SetActive(true);
                if(SaveSystem.numMuertes < 6){
                    starObject.SetActive(false);
                }
                if(SaveSystem.numMuertes < 4){
                    star1Object.SetActive(false);
                }
                if(SaveSystem.numMuertes < 3){
                    star2Object.SetActive(false);
                }
                

                btu.SetActive(true);
                            
              // drag your canvas object to this variable in the editor
 // make your canvas active from a disables state by calling this method
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
