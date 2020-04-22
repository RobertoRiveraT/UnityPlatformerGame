using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 public class PingPong : MonoBehaviour
 {
    public Transform[] points;
    public float Speed;
    int destPoint;
    public float allowence = 5f;
 
     // Use this for initialization
     void Start()
     {
         // Set first target
         UpdateTarget();
     }
 
     // Update is called once per frame
     void Update()
     {
         // Update this position
        Vector3 thisPos = new Vector3(transform.position.x, transform.position.y, 0);
 
 
         // Distance between current position and next position < alloence
        if (Vector3.Distance(thisPos, points[destPoint].position) <= allowence)
        {
            UpdateTarget();
        }
 
        transform.position = Vector3.Lerp(transform.position, points[destPoint].position, Time.deltaTime*Speed);
        //Vector3.Lerp(transform.position,currentTarget,(Time.deltaTime * Speed)/distance);
     }
 
     void UpdateTarget()
     {
         if (points.Length == 0)
         {
             return;
         }
         transform.position = points[destPoint].position;
         destPoint = (destPoint + 1) % points.Length;
 
     }
 }