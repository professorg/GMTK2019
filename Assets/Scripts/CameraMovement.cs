using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float targetOrthoSize = 5f;
    public float speed = 5f;
    
    Camera cam;
    Vector3 velocity,dampPosition = Vector3.zero;
    float t;

    bool playerMoved = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f) { playerMoved = true; }

        if (Input.GetKey(KeyCode.F))
        { t = Mathf.Clamp01(t - Time.deltaTime); } else if (playerMoved) { t = Mathf.Clamp01(t + Time.deltaTime); }
        
        

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);
        dampPosition = Vector3.SmoothDamp(dampPosition, targetPosition, ref velocity, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(new Vector3(0, 0, -10f), dampPosition, t);
        cam.orthographicSize = Mathf.Lerp(25f, targetOrthoSize, t);


    }


}
