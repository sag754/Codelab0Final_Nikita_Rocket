using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Vector3 targetPos;
    public Vector3 travelDistance;
    private Vector3 initialPos;
    enum MovementState { Forward, Back, Waiting }
    private MovementState currentState;
    public float speed;
    public float waitDuration;
    private float waitTimer;
    private float distanceTraveled;
    private MovementState previousState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = MovementState.Forward;
        initialPos = transform.position;
        targetPos = initialPos + travelDistance;
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