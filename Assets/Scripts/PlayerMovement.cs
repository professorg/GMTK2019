using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    // Public variables (in editor)
    public float jumpHeight, jumpTime, playerSpeed, deathHeight;
    public LayerMask groundLayer;

    // Local variables
    bool grounded, jumped, spacePressed;
    float gravity, jumpVelocity;
    Rigidbody2D rb;
    Vector2 startingPosition, velocity, size;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumped = false;
        spacePressed = false;
        gravity = -(2f * jumpHeight) / Mathf.Pow(jumpTime, 2);
        jumpVelocity = Mathf.Abs(gravity) * jumpTime;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        startingPosition = transform.position;
        velocity = Vector2.zero;
        size = GetComponent<BoxCollider2D>().size;
    }

    void Update() {
        spacePressed = Input.GetKey(KeyCode.Space);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /* === Grounding === */
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float delta = 0.1f;
        Debug.DrawRay(position, direction * (size.y / 2 + delta), Color.green);
        Debug.DrawRay(position - Vector2.right * size.x / 2, direction * (size.y / 2 + delta), Color.green);
        Debug.DrawRay(position + Vector2.right * size.x / 2, direction * (size.y / 2 + delta), Color.green);
        RaycastHit2D center = Physics2D.Raycast(position, direction, size.y / 2 + delta, groundLayer);
        RaycastHit2D left = Physics2D.Raycast(position - Vector2.right * size.x / 2, direction, size.y / 2 + delta, groundLayer);
        RaycastHit2D right = Physics2D.Raycast(position + Vector2.right * size.x / 2, direction, size.y / 2 + delta, groundLayer);
        grounded = (center.collider != null) || (left.collider != null) || (right.collider != null);

        /* === DEATH === */
        if (transform.position.y < deathHeight) {
            die();
        }


        /* === Horizontal === */
        velocity.x = Input.GetAxis("Horizontal") * playerSpeed;

        /* === Vertical === */
        // Reset velocity on ground
        if (grounded)
            velocity.y = 0;

        // Gravity
        velocity.y += gravity * Time.deltaTime;

        // Jump
        if (!jumped && grounded && spacePressed) {
            velocity.y = jumpVelocity;
            grounded = false;
            //jumped = true;
        } 

        /* === Final Movement === */
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    }

    public void jump(float height) {
        float vel = Mathf.Sqrt(2f * Mathf.Abs(gravity) * height);
        rb.velocity.y = vel;
    }

    void die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //transform.position = startingPosition;
        //jumped = false;        
        //grounded = false;
    }

}
