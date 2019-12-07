using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                         //allows this script to load other scenes/levels

public class NikitaController : MonoBehaviour
{

    public float rotThrustPow = 100f;                      //sets the default rotation power of thrust and allows it to be modified in editor
    public float thrustPow = 15f;                          //sets the default thrust power and allows it to be modified in editor
    public float LevelLoadDelay = 2f;                      //sets the delay between loading levels

    public AudioClip thrustSound;                          //makes visible in editor the input field for the thrust sound 
    public AudioClip destroyed;                            //makes visible in editor the input field for the complete sound
    public AudioClip explode;                              //makes visible in editor the input field for the explode sound

    public ParticleSystem thrustParticles;                 //makes visible in editor the input field the thrustParticle 
    public ParticleSystem destroyedParticles;              //makes visible in editor the input field the destroyedParticle 
    public ParticleSystem explodeParticles;                //makes visible in editor the input field the explodeParticle 

    Rigidbody rb;                                                            //assigns Rigidbody component to rb
    AudioSource NikitaSounds;                                                //assigns AudioSource component to NikitaSounds

    enum State { Alive, Dead, LoadLevel }                                    //decalares enumerator variables Alive, Dead, and LoadLevel [0, 1, 2]
    State state = State.Alive;                                               //sets enume state to Alive

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                       //rb calls RigidBody component to be accessed 
        NikitaSounds = GetComponent<AudioSource>();                           //NikitSOunds calls AudioSource to be accessed
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)                                             //checks to see if we're in the alive state
        {
            NikitaInputs();                                                   //if we're alive, we can control the missle through its input controls
        }

    }
    private void OnCollisionEnter(Collision collision)                        //if there is a collission
    {
        if (state != State.Alive)                                             //checks if we're dead
        {
            return;
        }

        switch (collision.gameObject.tag)                                     //sets up how certain tags on game objects interacts with our missle
        {
            case "Friendly":                                                  //if the object has collided with our missle and is tagged friendly, do nothing

                break;

            case "Finish":                                                    //if the object has collided with our missle and is tagged finish, perform the following
                state = State.LoadLevel;                                      //switches the enum state to LoadLevel
                NikitaSounds.Stop();                                          //stops all sounds the missle is producing
                destroyedParticles.Play();                                    //activate the 'destroyed' particle effect
                NikitaSounds.PlayOneShot(explode);                            //play explosion sound
                NikitaSounds.PlayOneShot(destroyed);                          //play the 'destroyed' sound
                gameObject.GetComponent<MeshRenderer>().enabled = false;      //disables 3D render of the Nikita Missle
                gameObject.GetComponent<Rigidbody>().isKinematic = true;      //disables Rigidbody Component on Nikita Missle
                thrustParticles.Stop();                                       //deactivate thrust particle effect
                Invoke("LoadNextLevel", LevelLoadDelay);                      //call LoadNextLevel after x amount of time
                break;

            default:                                                          //if the object has a default / no tag, do this
                state = State.Dead;                                           //switches the enum state to dead
                NikitaSounds.Stop();                                          //stops all missle sounds
                explodeParticles.Play();                                      //activate death particle
                thrustParticles.Stop();                                       //deactivate thrust particle
                NikitaSounds.PlayOneShot(explode);                            //play death sound
                gameObject.GetComponent<MeshRenderer>().enabled = false;      //disables 3D render of the Nikita Missle
                gameObject.GetComponent<Rigidbody>().isKinematic = true;      //disables Rigidbody Component on Nikita Missle
                Invoke("ReloadLevel", LevelLoadDelay);                        //call ReloadLevel after x amount of time so that death particles can play
                break;
        }
    }

    private void ReloadLevel()                                                //function that lets the player retry the same level
    {
        SceneManager.LoadScene(GameManagerScript.instance.currentLevel);      //restart current level
    }

    private void LoadNextLevel()                                              //function that lets the player play the next level when the objective is met
    {
        GameManagerScript.instance.LoadNextLevel();                           //load the next level
    }

    private void LoadFirstLevel()                                             //function that loads the first level
    {
        SceneManager.LoadScene(2);                                            //loads the first level (level 1)
    }

    private void NikitaInputs()                                               //function that houses the control inputs for the missle
    {
        rb.freezeRotation = true;                                             //take manual control of rotation physics

        if (Input.GetKey(KeyCode.Space))                                      //sets space key to enable thrust
        {
            rb.AddRelativeForce(Vector3.up * thrustPow);                      //locks thrust to the Y axis going up and play thrust sound
            if (!NikitaSounds.isPlaying)                                      //prevents sound from layering on top of itself
            {
                thrustParticles.Play();                                       //activates thrust particles
                NikitaSounds.PlayOneShot(thrustSound);                        //plays the thrust sound
            }
        }

        else
        {
            NikitaSounds.Stop();                                              //stop audio if space isn't being pressed
            thrustParticles.Stop();                                           //deactivate thrust particle
        }

        if (Input.GetKey(KeyCode.A))                                          //enables user to rotate missle to the left by pressing A key
        {
            float rotationSpeed = rotThrustPow * Time.deltaTime;              //increases the rotation speed of the missle
            transform.Rotate(Vector3.forward * rotationSpeed);                //locks the rotate transform to the X-axis, allows for positive rotation (rotates left)
        }
        else if (Input.GetKey(KeyCode.D))                                     //enables user to rotate missle to the right by pressing D key
        {
            float rotationSpeed = rotThrustPow * Time.deltaTime;              //increases the rotation speed of the missle
            transform.Rotate(-Vector3.forward * rotationSpeed);               //locks the rotate transform to the X-axis, allows for negative rotation (rotates right)
        }

        rb.freezeRotation = false;                                            //resume physics control of rotation
    }
}