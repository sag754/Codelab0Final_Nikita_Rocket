using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikitaController : MonoBehaviour
{

    public float rotThrustPow = 250f;                      //sets the rotation power of thrust and allows it to be modified in editor
    public float thrustPow = 30f;                          //sets the thrust power and allows it to be modified in editor

    Rigidbody rb;                                                        //assigns Rigidbod to rb
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                  //rb calls RigidBody component to be accessed 

    }

    // Update is called once per frame
    void Update()
    {
        
        rb.freezeRotation = true;                                     //take manual control of rotation physics

        if (Input.GetKey(KeyCode.Space))                              //sets space key to enable thrust
        {
            rb.AddRelativeForce(Vector3.up * thrustPow);              //locks thrust to the Y axis going up and play thrust sound

        }

        if (Input.GetKey(KeyCode.A))                                  //enables user to rotate missle to the left by pressing A key
        {
            float rotationSpeed = rotThrustPow * Time.deltaTime;      //increases the rotation speed of the missle
            transform.Rotate(Vector3.forward * rotationSpeed);        //locks the rotate transform to the X-axis, allows for positive rotation (rotates left)
        }
        else if (Input.GetKey(KeyCode.D))                              //enables user to rotate missle to the right by pressing D key
        {
            float rotationSpeed = rotThrustPow * Time.deltaTime;      //increases the rotation speed of the missle
            transform.Rotate(-Vector3.forward * rotationSpeed);       //locks the rotate transform to the X-axis, allows for negative rotation (rotates right)
        }

        rb.freezeRotation = false;                                    //resume physics control of rotation
        
    }
}
