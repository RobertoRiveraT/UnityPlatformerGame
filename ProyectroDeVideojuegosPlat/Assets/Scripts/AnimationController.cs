using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            animator.SetTrigger("jump");
        }

        if (isMoving() && !onAir())
        {
            Debug.Log("moving");
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    public bool isMoving()
    {
        return (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));
    }

    public bool onAir()
    {
        return !(pc.onPlatform());
    }

}