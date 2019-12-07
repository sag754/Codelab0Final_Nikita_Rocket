using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Vector3 targetPos;                          //the value in which the object will travel to on the X,Y, or Z
    public Vector3 travelDistance;                      //enables in the inspector to specify a travel distance
    private Vector3 initialPos;                         //the value in which the object start at on the X,Y, or Z
    enum MovementState { Forward, Back, Waiting }       //Enum that holds data for Forward, Back, and Waiting variables
    private MovementState currentState;                 //gets the current state (Forward, Back, or Waiting)
    public float speed;                                 //enables in the inspector how fast the object will move between the two points
    public float waitDuration;                          //enables in the inspector how long the object should wait before moving again
    private float waitTimer;                            //the value of the wait duration that was input within the inspector
    private float distanceTraveled;                     //stores the value of the distance travelled between points
    private MovementState previousState;                //stores the previous state of the enum

    // Start is called before the first frame update
    void Start()
    {
        currentState = MovementState.Forward;           //sets the state to Forward
        initialPos = transform.position;                //gets the objects transform position and sets it as the inital position
        targetPos = initialPos + travelDistance;        //gets initial position and adds the travel distance to it, then sets it as the target position
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;                                                //Vector 3 that will set the direction that object will move on the X, Y, or Z
        float distanceToTravel;                                           //varible that will set the object to the specified distance set in travelDistance

        switch (currentState)                                             //switch that sets the different enum states
        {
            case MovementState.Forward:                                   //sets the enum state to forward
                direction = travelDistance.normalized;                    //travel/move the specified distance on the X, Y, or Z 
                distanceToTravel = speed * Time.deltaTime;                //specified speed of the object
                transform.Translate(direction * distanceToTravel);        //move from initial position to specified position at the specified speed

                distanceTraveled += distanceToTravel;                     //go the distance to travel until the distance is travelled

                if (distanceTraveled >= travelDistance.magnitude)         //if the travel distance is greater than or equal to the vector's origin to it's endpoint, do this
                {
                    currentState = MovementState.Waiting;                 //switch the enum state from forward to waiting
                    previousState = MovementState.Forward;                //holds what the previous state was (Forward)
                    waitTimer = waitDuration;                             //wait in place for the specified wait duration 
                }
                break;

            case MovementState.Back:                                      //sets the enum state to back
                direction = -travelDistance.normalized;                   //go the specified direction, but in reverse / negative direction
                distanceToTravel = speed * Time.deltaTime;                //specified speed of the object (same as before)
                transform.Translate(direction * distanceToTravel);        //move from initial position to specified position at the specified speed

                distanceTraveled += distanceToTravel;                     //go the distance to travel until the distance is travelled

                if (distanceTraveled >= travelDistance.magnitude)         //if the travel distance is greater than or equal to the vector's origin to it's endpoint, do this
                {
                    currentState = MovementState.Waiting;                 //switch the enum state from back to waiting
                    previousState = MovementState.Back;                   //holds what the previous state was (Back)
                    waitTimer = waitDuration;                             //wait in place for the specified wait duration
                }
                break;

            case MovementState.Waiting:                                   //sets the enum state to waiting

                waitTimer -= Time.deltaTime;                              //counts down from specified wait duration
                if (waitTimer <= 0)                                       //when the wait duration timer is equal to or less than 0, do this
                {
                    distanceTraveled = 0;                                 //sets distance traveled to 0 so it doesn't add distance


                    if (previousState == MovementState.Forward)           //if the previous enum state was set to forward
                    {
                        currentState = MovementState.Back;                //switch to the current enum state to back
                    }
                    else
                        currentState = MovementState.Forward;             //otherwise, make the current enum state forward
                }
                break;

            default:

                break;
        }
    }
}