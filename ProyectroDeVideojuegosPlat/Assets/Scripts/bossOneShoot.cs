using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossOneShoot : MonoBehaviour
{
    public Transform shooter;
	public GameObject mil;
	public GameObject en;
	public float shootTime;
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine(shootGood());
	}
	
	private void shoot(){
		Instantiate (mil, shooter.position,Quaternion.identity);
	}
	
	private void spawn(){
		Instantiate (en, shooter.position,Quaternion.identity);
	}
	
	IEnumerator shootGood(){
		while(true){
			
			yield return new WaitForSeconds(shootTime);
			shoot();
			yield return new WaitForSeconds(shootTime);
			spawn();
		}
	}
}
