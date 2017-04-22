using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Movement {
    Animator anim;
    int moveSpeed = 60;
    void Start() {
        targetPos = transform.position;

        anim = GetComponent<Animator>();

        StartCoroutine(FollowPath(path));
    }

    void LateUpdate() {
        MoveWithAxis();
    }

    public Transform[] path;
    int currPath;

    IEnumerator FollowPath(Transform[] path) {
        Vector2 pathDir = (path[currPath].position - transform.position).normalized;
        axisVector = pathDir;

        while (transform.position != path[currPath].position) {
            yield return null;
        }

        if (currPath + 1 >= path.Length)
            axisVector = Vector2.zero;
        else {
            currPath++;
            StartCoroutine(FollowPath(path));
        }
    }

    Vector2 axisVector;

    void MoveWithAxis() {
        if (transform.position.x % gridCubeSize == 0 && transform.position.y % gridCubeSize == 0
            && (axisVector.x == 0 || axisVector.y == 0)) {
            isMoving = false;

            targetPos = (Vector2)transform.position + axisVector * gridCubeSize;
            //facingDirection = axisVector;

            if (anim != null)
                Animation();
        }

        if ((Vector2)transform.position != targetPos) {
            isMoving = true;

            //for collision
            if (Physics2D.OverlapCircle(targetPos, gridCubeSize / 4) == null) {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            else if (Physics2D.OverlapCircle(targetPos, gridCubeSize / 4).gameObject.layer != 8) {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            else if (Physics2D.OverlapCircle(targetPos, gridCubeSize / 4).gameObject != transform) {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            else {
                Debug.Log("layer is " + LayerMask.LayerToName(Physics2D.OverlapCircle(targetPos, gridCubeSize / 4).gameObject.layer));
            }

        }
    }

    void Animation() {
        if (axisVector != Vector2.zero) {
            anim.SetBool("is_moving", true);

            if (axisVector.x == 1) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (KeyAxisVector.x == -1) {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            anim.SetFloat("y", axisVector.y);
            anim.SetFloat("x", -Mathf.Abs(axisVector.x));
        }
        else {
            anim.SetBool("is_moving", false);
        }
    }
}