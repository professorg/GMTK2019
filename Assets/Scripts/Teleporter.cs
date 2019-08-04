using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public Teleporter other;
    public float teleportCooldown;
    float cooldown;
    
    void Start()
    {
        cooldown = 0;
    }

    void Update()
    {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (cooldown <= 0 && collider.gameObject.tag == "Player") {
            Vector3 offset = transform.position - collider.transform.position;
            other.Cooldown();
            collider.transform.position = other.transform.position - offset;
            //other.Cooldown();
            //collider.transform.position = other.transform.position;
        }
    }
    
    public void Cooldown()
    {
        cooldown = teleportCooldown;
    }

}
