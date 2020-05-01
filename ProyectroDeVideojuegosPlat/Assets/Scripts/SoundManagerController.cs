using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour
{
    public static AudioClip playerWalk, jumpSound, hookSound;
    static AudioSource audioSrc;
    public static bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        playerWalk = Resources.Load<AudioClip>("pickup");
        jumpSound = Resources.Load<AudioClip>("playerJump");
        hookSound = Resources.Load<AudioClip>("hook");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip){
        switch (clip)
        {
            case "pickup":
                audioSrc.PlayOneShot(playerWalk);
                playing = true;
                break;
            case "playerJump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "hook":
                audioSrc.PlayOneShot(hookSound);
                break;
        }
    }
}
