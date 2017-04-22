using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform target;

    void FollowTarget() {
        transform.position = target.position + new Vector3(0,0,-10);
    }

    static bool canFollowTarget = true;
    public static bool CanFollowTarget {
        get {
            return canFollowTarget;
        }
        set {
            canFollowTarget = value;
        }
    }

    private void Update() {
        if (canFollowTarget == true)
            FollowTarget();
    }
}
