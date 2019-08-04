using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    // Public variables (in editor)
    public float jumpHeight, jumpTime, playerSpeed, sprintMultiplier, deathHeight;
    public LayerMask groundLayer;

    // Local variables
    bool grounded, jumped;
    float gravity, jumpVelocity;
    Vector2 startingPosition, size;
    Vector3 velocity;

    Animator animator;
    int _currentState = STATE_IDLE;

    const int STATE_IDLE = 0;
    const int STATE_RUN = 1;
    const int STATE_JUMP = 2;
    const int STATE_FALL = 3;
    const int STATE_LAND = 4;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumped = false;
        gravity = -(2f * jumpHeight) / Mathf.Pow(jumpTime, 2);
        jumpVelocity = Mathf.Abs(gravity) * jumpTime;
        startingPosition = transform.position;
        velocity = Vector2.zero;
        size = GetComponent<BoxCollider2D>().size;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /* === Final Movement === */
        transform.position += velocity * Time.deltaTime;

        /* === Grounding === */
        Vector2 position = transform.position;
        float delta = 0.1f;

        Vector2 offset = Vector2.left * delta*3f;

        RaycastHit2D up = Physics2D.Raycast(position, Vector2.up, size.y / 2 + delta, groundLayer);

        RaycastHit2D downLeft = Physics2D.Raycast(position - offset, Vector2.down, size.y / 2 + delta, groundLayer);
        RaycastHit2D downRight = Physics2D.Raycast(position + offset, Vector2.down, size.y / 2 + delta, groundLayer);

        RaycastHit2D left = Physics2D.Raycast(position + delta*Vector2.down, Vector2.left, size.x / 2 + delta, groundLayer);
        RaycastHit2D right = Physics2D.Raycast(position + delta*Vector2.down, Vector2.right, size.x / 2 + delta, groundLayer);


        

        grounded = downLeft || downRight;
        
        /* === DEATH === */
        if (transform.position.y < deathHeight) {
            Die();
        }



        /* === Horizontal === */
        velocity.x = Input.GetAxis("Horizontal") * playerSpeed * (Input.GetButton("Fire2") ? sprintMultiplier : 1f);
        if (grounded) {
            if (Mathf.Abs(velocity.x) > delta) {
                SetAnimation(STATE_RUN);
                ChangeDirection(velocity.x > 0);
            } else {
                SetAnimation(STATE_IDLE);
            }
        }

        if(left) {
            velocity.x = -Mathf.Clamp01(-velocity.x);
            transform.position = new Vector3(left.point.x + size.x / 2f + 1.1f * delta, transform.position.y);
        } else if (right) {
            velocity.x = Mathf.Clamp01(velocity.x);
            transform.position = new Vector3(right.point.x - (size.x / 2f + 1.1f * delta), transform.position.y);
        }

        if (!jumped) {
            RaycastHit2D hiLeft = Physics2D.Raycast(position + size.y / 4 * Vector2.up, Vector2.left, size.x / 2 + delta, groundLayer);
            RaycastHit2D hiRight = Physics2D.Raycast(position + size.y / 4 * Vector2.up, Vector2.right, size.x / 2 + delta, groundLayer);
            if(hiLeft) {
                velocity.x = -Mathf.Clamp01(-velocity.x);
                transform.position = new Vector3(hiLeft.point.x + size.x / 2f + 1.1f * delta, transform.position.y);
            } else if (hiRight) {
                velocity.x = Mathf.Clamp01(velocity.x);
                transform.position = new Vector3(hiRight.point.x - (size.x / 2f + 1.1f * delta), transform.position.y);
            }
        }

        /* === Vertical === */
        
        // Gravity
        velocity.y += gravity * Time.deltaTime;

        // Reset velocity/position on ground

        if(up)
        {
            velocity.y = 0f;
            transform.position = new Vector3(transform.position.x, up.point.y - (size.y / 2f + delta + .05f));
        }

        if (grounded)
        {
            velocity.y = 0;

            float y = Mathf.Max((downLeft ? downLeft.point.y : downRight.point.y), (downRight ? downRight.point.y : Mathf.NegativeInfinity));
            //if (downLeft) {
            //    if (downRight) {
            //        y = Mathf.Max(downLeft.point.y, downRight.point.y);
            //    } else {
            //        y = downLeft.point.y;
            //    }
            //} else {
            //    y = downRight.point.y;
            //}

            transform.position = new Vector3(transform.position.x, y + size.y / 2f);
        }

        // Jump
        if (/*!jumped && */grounded && Input.GetButtonDown("Fire1")) {
            velocity.y = jumpVelocity;
            grounded = false;
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (!jumped) {
                collider.size = new Vector2(collider.size.x, collider.size.y / 2f);
                collider.offset = new Vector2(collider.offset.x, collider.offset.y - collider.size.y / 2f);
            }
            SetAnimation(STATE_JUMP);
            // TODO: Spawn legs
            jumped = true;
        }

        if (jumped && velocity.y < -delta) {
            SetAnimation(STATE_FALL);
        }

    }

    void SetAnimation(int state)
    {
        if (state == _currentState)
            return;
        Debug.Log("Changing to " + state);
        animator.SetInteger("state", state); 
        _currentState = state;
    }

    void ChangeDirection(bool right)
    {
        GetComponent<SpriteRenderer>().flipX = right;
    }

    public void Jump(float height) {
        float yvel = Mathf.Sqrt(2f * Mathf.Abs(gravity) * height);
        velocity.y = yvel;
        if (jumped)
            SetAnimation(STATE_JUMP);
    }

    void Die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //transform.position = startingPosition;
        //jumped = false;        
        //grounded = false;
    }

}
