using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour
{
	public int life;
	public float timeToColor;
	SpriteRenderer render;
    Color dColor;
    
    void Start () {
		render = GetComponent<SpriteRenderer>();
        dColor = render.color;
	}
    
    private void OnTriggerEnter2D(Collider2D other) {
    	
    	GameObject ney= other.gameObject;
    	if(ney.tag=="thrown"||ney.tag=="thrownRight"||ney.tag=="thrownLeft"){
    		StartCoroutine("SwitchColor");
    		Destroy(other.gameObject);
    		life--;
    		Debug.Log(life);
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
	
}
