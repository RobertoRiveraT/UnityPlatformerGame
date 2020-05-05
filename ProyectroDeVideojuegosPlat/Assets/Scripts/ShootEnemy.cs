using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public Transform shooter;
	public GameObject mil;
	public float shootTime;
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine(shootGood());
	}
	
	private void shoot(){
		Instantiate (mil, shooter.position,Quaternion.identity);
	}
	
	
	
	IEnumerator shootGood(){
		while(true){
			if(this.gameObject.tag=="bad"){
			yield return new WaitForSeconds(shootTime);
			shoot();
			}else{
				yield break;
			}
		}
	}
}
