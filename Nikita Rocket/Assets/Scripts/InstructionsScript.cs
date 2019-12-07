using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))                  //if the player hits the spacebar
        {
            SceneManager.LoadScene(2);                    //proceed to the first level
        }
    }
}