using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditorPathSettings : MonoBehaviour {
    void OnDrawGizmosSelected() {
        Transform[] paths = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount - 1; i++) {
            //paths[i] = transform.GetChild(i);
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

    }
}