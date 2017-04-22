using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    bool freezeAxisVector = false;
    public bool FreezeAxisVector {
        set {
            freezeAxisVector = value;
        }
    }

    public Vector2 KeyAxisVector {
        get {
            if (freezeAxisVector == false) {
                return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            else {
                return Vector2.zero;
            }
        }
    }

    protected bool isMoving;
    public bool IsMoving {
        get {
            return isMoving;
        }
    }

    public int gridCubeSize = 16;

    protected Vector2 targetPos;

    public void MoveTo(Vector2 pos) {
        targetPos = pos;
        transform.position = new Vector2(Mathf.Round(pos.x / gridCubeSize) * gridCubeSize, Mathf.Round(pos.y / gridCubeSize) * gridCubeSize);
    }

    public void MoveTo(Transform pos) {
        targetPos = pos.position;
        transform.position = new Vector2(Mathf.Round(pos.position.x / gridCubeSize) * gridCubeSize, Mathf.Round(pos.position.y / gridCubeSize) * gridCubeSize);
    }
}