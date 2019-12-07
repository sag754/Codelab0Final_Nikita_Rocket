using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))                  //if the player hits the spacebar
        {
            SceneManager.LoadScene(1);                    //go to the instructions screen
        }
    }
}