using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggers : MonoBehaviour
{
    int currentScene;

    bool isDead = false;

    [SerializeField] ParticleSystem Deathparicles;
    [SerializeField] ParticleSystem EndparticleSystem;
    [SerializeField] float delay;
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "floor")
        {
            isDead = true;
            Invoke("SceneLoaderDeath", delay);
            particlePlayer();
        }
        else if (collision.tag == "Finish")
        {
            isDead = false;
            Debug.Log("Level completed");
            particlePlayer();
        }
        else if (collision.tag == "wall")
        {
            isDead = true;
            Debug.Log("went to high");
            SceneLoaderDeath();
            particlePlayer();
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
    private void particlePlayer()
    {
        if (isDead)
        {
            Deathparicles.Play();
        }
        else if (!isDead)
        {
            EndparticleSystem.Play();
        }
    }
}