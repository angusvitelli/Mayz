using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding is tagged as "Enemy"
        if (collision.gameObject.tag == "Enemy")
        {
            // Reload the current scene to reset the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

