using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoState : MonoBehaviour
{

    public bool facingLeft;
    public float resetTime;
    public Sprite spriteLeft, spriteMiddle, spriteRight;

    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 0;
        GetComponent<SpriteRenderer>().sprite = (facingLeft ? spriteLeft : spriteRight);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        } else {
            GetComponent<SpriteRenderer>().sprite = (facingLeft ? spriteLeft : spriteRight);
        }
    }

    public void Notify(bool left)
    {
        if (left == facingLeft) {
            timeLeft = resetTime;
            GetComponent<SpriteRenderer>().sprite = spriteMiddle;
        } else if (timeLeft > 0) {
            facingLeft = !facingLeft;
            timeLeft = resetTime;
            GetComponent<SpriteRenderer>().sprite = (facingLeft ? spriteLeft : spriteRight);
            Array.ForEach(FindObjectsOfType<TwoStateBlock>(), o => o.Toggle());
        }
    }
}
