using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggers : MonoBehaviour
{
    int currentScene;
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            SceneLoaderDeath();
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Level completed");
        }
        else if (collision.gameObject.tag == "wall")
        {
            Debug.Log("went to high");
            SceneLoaderDeath();
        }
    }
    private void SceneLoaderNextLevel()
    {
        SceneManager.LoadScene(currentScene + 1);
    }
    private void SceneLoaderDeath()
    {
        SceneManager.LoadScene(currentScene);
    }
}