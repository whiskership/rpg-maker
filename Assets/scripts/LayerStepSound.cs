using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerStepSound : MonoBehaviour {
    [SerializeField]
    AudioClip[] sounds;

    public AudioClip[] Sounds {
        get {
            return sounds;
        }
    }

}
