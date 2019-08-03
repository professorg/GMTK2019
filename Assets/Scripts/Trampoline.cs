using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float jumpHeight;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") {
            boing(collider.GetComponent<PlayerMovement>());
        }
    }

    void boing(PlayerMovement pm)
    {
        pm.Jump(jumpHeight);
    }

}
