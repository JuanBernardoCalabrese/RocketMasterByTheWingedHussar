using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody Rocket;
    AudioSource mainAudioSource;
    AudioSource sideBoosterAudioSource;
    public AudioClip rocketBoosterSound;
    public AudioClip leftSideBoosterSound;
    public AudioClip rightSideBoosterSound;
    [SerializeField] ParticleSystem boosterParticles;
    [SerializeField] ParticleSystem LeftBoosterParticles;
    [SerializeField] ParticleSystem RightBoosterParticles;
    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rocketTurn = 100f;
    float rocketDirectionUp = 1f;
    float rocketDirectionTurn = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Rocket = GetComponent<Rigidbody>();
        mainAudioSource = GetComponent<AudioSource>();
        sideBoosterAudioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit(); 
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            rocketUp();
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) 
        {
            mainAudioSource.Stop();
            boosterParticles.Stop();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rocketLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            RightBoosterParticles.Stop();
            sideBoosterAudioSource.Stop();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rocketRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            LeftBoosterParticles.Stop();
            sideBoosterAudioSource.Stop();
        }
    }

    void rocketUp()
    {
        if (!boosterParticles.isPlaying)
        {
            boosterParticles.Play();
        }
        
        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.PlayOneShot(rocketBoosterSound);
        }
        Rocket.AddRelativeForce(new Vector3(0f,rocketDirectionUp,0f) * rocketThrust * Time.deltaTime);
    }

    void rocketLeft()
    {
        if (!sideBoosterAudioSource.isPlaying)
        {
            sideBoosterAudioSource.PlayOneShot(leftSideBoosterSound);
        }   
        RightBoosterParticles.Play();
        Rocket.freezeRotation = true;
        Rocket.transform.Rotate(new Vector3(0, 0, -rocketDirectionTurn) * rocketTurn * Time.deltaTime);
        Rocket.freezeRotation = false;  
    }

    void rocketRight()
    {
        if (!sideBoosterAudioSource.isPlaying)
        {
            sideBoosterAudioSource.PlayOneShot(rightSideBoosterSound);
        }
        LeftBoosterParticles.Play();
        Rocket.freezeRotation = true;
        Rocket.transform.Rotate(new Vector3(0, 0, rocketDirectionTurn) * rocketTurn * Time.deltaTime);
        Rocket.freezeRotation = false;
    }
}
