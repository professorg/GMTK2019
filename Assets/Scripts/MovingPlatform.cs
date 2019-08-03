using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector2[] movements;
    public float speed;
    bool reverse;
    int index;
    float remaining;
    Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        reverse = false;
        index = 0;
        remaining = movements[0].magnitude;
        nextPos = transform.position + (Vector3)movements[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed*Time.deltaTime);
        if ((transform.position - nextPos).sqrMagnitude < 0.001) {
            if (reverse) {
                if (index == 0) { // Back to start: reverse
                    reverse = false;
                } else {
                    index--; // Go to next movement in reverse
                }
            } else {
                if (index == movements.Length - 1) { // At end: reverse
                    reverse = true;
                } else {
                    index++; // Go to next movement
                }
            }
            nextPos = nextPos + (reverse ? -1 : 1) * (Vector3)movements[index];
        }
    }

}
