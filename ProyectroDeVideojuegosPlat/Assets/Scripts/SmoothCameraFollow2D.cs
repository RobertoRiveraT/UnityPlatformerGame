using UnityEngine;
using System.Collections;



public class SmoothCameraFollow2D : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	//public Transform target1;
	
	// Update is called once per frame
	void Update ()
	{
		if (target) {
            // Obtiene la posición que tiene el objeto que estamos siguiendo
            // según la vista de la cámara
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x, (target.position.y + 2.2f), target.position.z));
			// la diferencia que existe entre la posición del objeto y la posición que tiene la cámara
			// respecto al objeto
			Vector3 delta = new Vector3(target.position.x, (target.position.y + 2.2f), target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			//Vector3 delta1 = new Vector3(target.position.x, target.position.y, target.position.z);
			// establecemos el destino es decir, la posición que tengo como cámara más la posición
			// delta obtenida anteriormente
			Vector3 destination = transform.position + delta;
			//Vector3 destination1 = transform.position + delta1;
			// Movemos la cámara utilizando el método de Smooth Damp
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			//target1.position = Vector3.SmoothDamp(transform.position, destination1, ref velocity, dampTime);
		}
	}
}
