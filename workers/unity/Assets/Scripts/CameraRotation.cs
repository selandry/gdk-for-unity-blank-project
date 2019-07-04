using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speed = 50.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -speed * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, speed * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(-speed * Time.deltaTime, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);
        }

        //Reset the camera
        if (Input.GetKey(KeyCode.R))
        {
            //transform.rotation = (0,0,0);
        }
    }
}


