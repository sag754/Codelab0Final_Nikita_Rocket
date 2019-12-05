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
        targetPos = initialPos + travelDistance;        //gets
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        float distanceToTravel;

        switch (currentState)
        {

            case MovementState.Forward:
                direction = travelDistance.normalized;
                distanceToTravel = speed * Time.deltaTime;
                transform.Translate(direction * distanceToTravel);

                distanceTraveled += distanceToTravel;

                if (distanceTraveled >= travelDistance.magnitude)
                {
                    currentState = MovementState.Waiting;
                    previousState = MovementState.Forward;
                    waitTimer = waitDuration;
                }
                break;
            case MovementState.Back:
                direction = -travelDistance.normalized;
                distanceToTravel = speed * Time.deltaTime;
                transform.Translate(direction * distanceToTravel);

                distanceTraveled += distanceToTravel;

                if (distanceTraveled >= travelDistance.magnitude)
                {
                    currentState = MovementState.Waiting;
                    previousState = MovementState.Back;
                    waitTimer = waitDuration;
                }
                break;
            case MovementState.Waiting:

                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    distanceTraveled = 0;


                    if (previousState == MovementState.Forward)
                    {
                        currentState = MovementState.Back;
                    }
                    else
                        currentState = MovementState.Forward;
                }
                break;
            default:
                break;
        }
    }
}