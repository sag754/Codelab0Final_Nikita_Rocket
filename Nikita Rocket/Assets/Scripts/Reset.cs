using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))                  //if the player hits the space bar
        {
            SceneManager.LoadScene(0);                    //load the main menu screen
        }
    }
}