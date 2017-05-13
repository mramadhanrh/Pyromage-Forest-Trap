using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public VirtualJoystick joystick;
    
    public float speedRot;
    public float speedMove;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ////Player Rotation
        //transform.Rotate(new Vector3(0, joystick.Horizontal(), 0) * Time.deltaTime * speedRot);

        ////Player Move
        //if (joystick.Vertical() > 0)
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime * speedMove);
        //}

        //if (joystick.Vertical() < -0.3)
        //{
        //    transform.Translate(Vector3.back * Time.deltaTime * speedMove);
        //}

        if (VirtualJoystick.inputVector != Vector3.zero)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speedMove);
        }
        float x, y, z;
        x = transform.position.x + VirtualJoystick.inputVector.x;
        y = transform.position.y;
        z = transform.position.z + VirtualJoystick.inputVector.z;
        transform.LookAt(new Vector3(x, y, z));
        
        
	}

}
