using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStateTrigger : MonoBehaviour
{

    public bool left;
    public TwoState parent;

    void OnTriggerExit2D(Collider2D collider) {
        parent.Notify(left);
    }

}
