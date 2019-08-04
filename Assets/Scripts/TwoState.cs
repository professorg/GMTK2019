using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoState : MonoBehaviour
{

    public bool facingLeft;
    public Sprite spriteLeft, spriteMiddle, spriteRight;

    bool switching;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = (facingLeft ? spriteLeft : spriteRight);
    }


    public void Notify(bool left)
    {
        if (left == facingLeft) {
            switching = true;
            GetComponent<SpriteRenderer>().sprite = spriteMiddle;
        } else if (switching) {
            facingLeft = !facingLeft;
            switching = true;
            GetComponent<SpriteRenderer>().sprite = (facingLeft ? spriteLeft : spriteRight);
            Array.ForEach(FindObjectsOfType<TwoStateBlock>(), o => o.Toggle());
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") {
            switching = false;
            GetComponent<SpriteRenderer>().sprite = (facingLeft ? spriteLeft : spriteRight);
        }
    }
}
