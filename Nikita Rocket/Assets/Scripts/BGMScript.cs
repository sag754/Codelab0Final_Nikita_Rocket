using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMScript : MonoBehaviour
{
    public static BGMScript instance = null;                       //establishes a reference to a BGMScript and sets the reference to null when this objected is created
    public int currentLevel;                                       //variable that holds an integer to manage which scene/level to load

    void Awake()                                                   //do/enable this when we begin to play
    {
        if (instance != null && instance != this)                  //checks to see if there are two instances of BGMScipt
        {
            Destroy(this.gameObject);                              //if ther are more than 1, it destroys the gameobject
            return;                                                //exits the function (don't do the DontDestroyOnLoad)
        }
        else
        {
            instance = this;                                        //if instance is not already assigned, assign it to this script
            currentLevel = 2;                                       //if the above is not executed, reset to level 1
        }

        DontDestroyOnLoad(this.gameObject);                         //upon loading a new scene or level, this object should persist
    }

    public void StopSong()                                          //function that disables background music
    {
        GetComponent<AudioSource>().Stop();                         //stops background music
    }

    public void LoadNextLevel()                                     //exeucutes code when called
    {
        currentLevel++;                                             //increments 'currentLevel' int variable +1 so that it loads the next scene
        SceneManager.LoadScene(currentLevel);                       //loads the next level (i.e., level 2)
    }

    public void Update()
    {
        if (currentLevel > 4)                                       //if the current level is greater than 4, do this
        {
            StopSong();                                             //stop the background music
        }
    }
}
