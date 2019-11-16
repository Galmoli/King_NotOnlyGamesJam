using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetJoystickNames().Length);
        //if (Input.GetButton("Submit"))
        //{
        //    Debug.Log("Submitting");
        //}
        //Debug.Log(Input.GetAxis("Joystick1Horizontal"));
        //Debug.Log(Input.GetAxis("[1]"));
        if (Input.GetAxis("Joystick1Horizontal") < -0.1f)
        {
            Debug.Log("Negative Joystick1Horizontal: " + Input.GetAxis("Joystick1Horizontal"));
        }
        if (Input.GetAxis("Joystick2Horizontal") < -0.1f)
        {
            Debug.Log("Negative Joystick2Horizontal: " + Input.GetAxis("Joystick2Horizontal"));
        }

        if (Input.GetAxis("Joystick1Horizontal") > 0.1f)
        {
            Debug.Log("Positive Joystick1Horizontal: " + Input.GetAxis("Joystick1Horizontal"));
        }
        if (Input.GetAxis("Joystick2Horizontal") > 0.1f)
        {
            Debug.Log("Positive Joystick2Horizontal: " + Input.GetAxis("Joystick2Horizontal"));
        }
        //Debug.Log(Input.GetJoystickNames()[0]);
    }
}
