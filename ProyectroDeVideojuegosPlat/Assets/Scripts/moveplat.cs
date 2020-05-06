using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplat : MonoBehaviour
{
    public Transform one;
    public Transform two;

    public float Speed;

    Vector3 ones;
    Vector3 twos;

    Vector3 _target;
    // Start is called before the first frame update
    void Start()
    {
        ones = one.position;
        twos = two.position;
        _target = ones;
    }

    // Update is called once per frame
    void Update()
    {
    	ones = one.position;
        twos = two.position;
        move();
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
}
