using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Movement))]
public class CharacterFootsteps : MonoBehaviour {
    Movement movement;
    AudioSource audioSource;

    private void Start() {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    int interval = 0;
    void Update () {
		if (movement.IsMoving == true) {
            if (Physics2D.OverlapCircle(transform.position, movement.gridCubeSize / 4, ~(1 << LayerMask.NameToLayer("Default"))) != null) {
                Transform layerTransform = Physics2D.OverlapCircle(transform.position, movement.gridCubeSize / 4, ~(1 << LayerMask.NameToLayer("Default"))).gameObject.transform;

                string layerName = LayerMask.LayerToName(layerTransform.gameObject.layer);


                /*switch (layerName) {
                    case "rock":
                    if (audioSource.isPlaying == false) {

                        interval = interval == 0 ? 1 : 0;

                        audioSource.pitch = Random.Range(.8f, 1.2f);

                        audioSource.clip = layerTransform.GetComponent<LayerStepSound>().Sounds[interval];
                        audioSource.Play();
                    }
                    break;
                }*/
                
                if (layerTransform.GetComponent<LayerStepSound>() != null) {
                    if (audioSource.isPlaying == false) {

                        interval = interval == 0 ? 1 : 0;

                        audioSource.pitch = Random.Range(.8f, 1.2f);

                        audioSource.clip = layerTransform.GetComponent<LayerStepSound>().Sounds[interval];
                        audioSource.Play();
                    }
                }


            }
        }

    }
}
