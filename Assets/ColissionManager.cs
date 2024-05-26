using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColissionManager : MonoBehaviour
{
    AudioSource mainAudioSource;
    public AudioClip crashSound;
    public AudioClip winSound;
    Rigidbody Rocket;
    [SerializeField] ParticleSystem ExplosionParticles;
    string colissionTag;
    float reloadDelay = 2.5f;
    bool isChanging = false;
    int spaceshipSpecular = 0;  
    void Start()
    {
        mainAudioSource = GetComponent<AudioSource>();
        Rocket = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isChanging == false)
        {
            colissionTag = collision.gameObject.tag;
            switch (colissionTag)
            {
                case "Start":
                    isChanging = true;
                    startSequence();
                    break;
                case "Obstacle":
                    isChanging = true;
                    crashSequence();
                    break;
                case "Finish":
                    isChanging = true;
                    finishSequence();
                    break;
                default:
                    break;
            }
        }
    }
    void startSequence()
    {
        //Debug.Log("On lunching Pad");
        isChanging = false;
    }

    void crashSequence()
    {
        mainAudioSource.Stop();
        Rocket.freezeRotation = true;
        Rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetChild(spaceshipSpecular).GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Movement>().enabled = false;
        ExplosionParticles.Play();
        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.PlayOneShot(crashSound);
        }
        Invoke("reloadLevel", reloadDelay);
        
    }

    void finishSequence()
    {
        mainAudioSource.Stop();
        GetComponent<Movement>().enabled = false;
        mainAudioSource.PlayOneShot(winSound);
        Invoke("nextLevel", reloadDelay);
    }

    void reloadLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void nextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings) 
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
