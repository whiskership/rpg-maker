using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellphoneDialogue : CharacterDialogue {
    AudioSource sfx;
    Animator anim;
	void Start () {
        sfx = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
	}

    public bool isRinging;

    [SerializeField]
    AudioClip phonePickup;

    void Update () {
		if (isRinging == true && sfx.isPlaying == false
            /*&&currentCharacterTalking != transform*/) {
            //should have ring .wav attached already
            anim.SetBool("isRinging", true);
            sfx.Play();

            //Debug.Log("current character: " + currentCharacterTalking);
        }

        if (currentCharacterTalking == transform
            && sfx.clip != phonePickup) {
            sfx.clip = phonePickup;
            sfx.Play();

            anim.SetBool("isRinging", false);

            isRinging = false;
        }
	}
}
