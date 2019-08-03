using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float jumpHeight;
    float jumpVelocity;

    void Start()
    {

        jumpVelocity = Mathf.Abs(gravity) * jumpTime;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") {
            boing(collider.GetComponent<PlayerMovement>());
        }
    }

    void boing(PlayerMovement pm)
    {
        pm.jump(jumpHeight);
    }

}
