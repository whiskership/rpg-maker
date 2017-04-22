using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEditorFunctions : MonoBehaviour {
    List<GameObject> redFlowers;
    List<GameObject> yellowFlowers;
    List<GameObject> blueFlowers;

    [SerializeField]
    List<Transform> activeParticles;

    [SerializeField] Transform redParticle;

    [SerializeField] Transform blueParticle;

    [SerializeField] Transform yellowParticle;

    void Awake() {

        for (int i = 0; i < activeParticles.Count; i++) {
            if (activeParticles[i] != null)
                DestroyImmediate(activeParticles[i].gameObject);
        }

        activeParticles = new List<Transform>();

        GameObject[] all = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        for (int i = 0; i < all.Length; i++) {

            if (LayerMask.LayerToName(all[i].layer) == "red_flower") {
                GameObject instance = all[i];

                for (int b = 0; b < instance.GetComponent<PolygonCollider2D>().pathCount; b++) {
                    Vector2 spawn = instance.GetComponent<PolygonCollider2D>().GetPath(b)[0];
                    activeParticles.Add(Instantiate(redParticle.gameObject, spawn, Quaternion.Euler(-90, 0, 0)).transform);
                }

            }

            if (LayerMask.LayerToName(all[i].layer) == "blue_flower") {
                GameObject instance = all[i];

                for (int b = 0; b < instance.GetComponent<PolygonCollider2D>().pathCount; b++) {
                    Vector2 spawn = instance.GetComponent<PolygonCollider2D>().GetPath(b)[0];
                    activeParticles.Add(Instantiate(blueParticle.gameObject, spawn, Quaternion.Euler(-90, 0, 0)).transform);
                }
            }

            if (LayerMask.LayerToName(all[i].layer) == "yellow_flower") {
                GameObject instance = all[i];

                for (int b = 0; b < instance.GetComponent<PolygonCollider2D>().pathCount; b++) {
                    Vector2 spawn = instance.GetComponent<PolygonCollider2D>().GetPath(b)[0];
                    activeParticles.Add(Instantiate(yellowParticle.gameObject, spawn, Quaternion.Euler(-90, 0, 0)).transform);
                }
            }

        }
    }
}
