using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Squish?");
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Squish!");
            collider.gameObject.GetComponent<Transform>().parent = transform;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            collider.gameObject.GetComponent<Transform>().parent = null;
        }
    }
}
