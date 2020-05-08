using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public Transform shooter;
	public GameObject mil;
	public float shootTime;
	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		StartCoroutine(shootGood());	
	}

	void Update()
	{
		animator.SetBool("shooting", false);
	}


	private void shoot(){
		animator.SetBool("shooting", true);
		Instantiate (mil, shooter.position,Quaternion.identity);
	}
	
	IEnumerator shootGood(){
		while(true){
			if(this.gameObject.tag=="bad"){
			yield return new WaitForSeconds(shootTime);
			shoot();
			}
			else{
				yield break;
			}
		}
	}
}
