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
    [SerializeField] ParticleSystem snowParticleSystem;
    [SerializeField] float delay;

    AudioSource audioSource;
    [SerializeField] AudioClip Deathclip;
    [SerializeField] AudioClip finishClip;

    SurfaceEffector2D surfaceEffector;
    playercontroller playercontroller;
    private void Start()
    {
        surfaceEffector = FindObjectOfType<SurfaceEffector2D>(); 
        audioSource = GetComponent<AudioSource>();
        playercontroller = FindObjectOfType<playercontroller>();
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        playercontroller.move();
        bool triggered = playercontroller.istriggered;
        if (collision.tag == "floor")
        {
            isDead = true;
            Invoke("SceneLoaderDeath", delay);
            particlePlayer();
            triggered = false;
            if (triggered == false)
            {
                audioSource.PlayOneShot(Deathclip);
                triggered = true;
            }
        }
        else if (collision.tag == "Finish")
        {
            isDead = false;
            Debug.Log("Level completed");
            particlePlayer();
            triggered = false;
            if (triggered == false)
            {
                audioSource.PlayOneShot(finishClip);
                triggered = true;
            }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (surfaceEffector.speed > 1)
        {
            snowParticleSystem.Play();
        }
        else
        {
            snowParticleSystem.Stop();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        snowParticleSystem.Stop();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (surfaceEffector.speed < 1)
        {
            snowParticleSystem.Stop();
        }
        else if (surfaceEffector.speed > 1)
        {
            snowParticleSystem.Play();
        }
    }
}