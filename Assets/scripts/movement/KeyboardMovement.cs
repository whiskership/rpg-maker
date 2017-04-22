using UnityEngine;

public class KeyboardMovement : Movement {
    void Start() {
        transform.position = new Vector2(Mathf.Round(transform.position.x / gridCubeSize) * gridCubeSize, Mathf.Round(transform.position.y / gridCubeSize) * gridCubeSize);
        targetPos = transform.position;

        anim = GetComponent<Animator>();
    }

	void LateUpdate () {
       MoveWithKeyboard();
	}

    public Vector2 FacingDirection {
        get {
            return facingDirection;
        }
    }

    Vector2 facingDirection;
    void MoveWithKeyboard() {
        if (transform.position.x % gridCubeSize == 0 && transform.position.y % gridCubeSize == 0
            && (KeyAxisVector.x == 0 || KeyAxisVector.y == 0) ) {
            isMoving = false;

            targetPos = (Vector2)transform.position + KeyAxisVector * gridCubeSize;
            facingDirection = KeyAxisVector;

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
        }
    }

    Animator anim;
    void Animation() {
        if (KeyAxisVector != Vector2.zero) {
            anim.SetBool("is_moving", true);

            if (KeyAxisVector.x == 1) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (KeyAxisVector.x == -1) {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            anim.SetFloat("y", KeyAxisVector.y);
            anim.SetFloat("x", -Mathf.Abs(KeyAxisVector.x));
        }
        else {
            anim.SetBool("is_moving", false);
        }
    }

    [SerializeField]
    int moveSpeed = 80;
    public int MoveSpeed {
        get {
            return moveSpeed;
        }
        set {
            moveSpeed = value;
        }
    }
}