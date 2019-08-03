using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            collider.gameObject.GetComponent<Transform>().parent = transform;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            collider.gameObject.GetComponent<Transform>().parent = null;
        }
    }
}
