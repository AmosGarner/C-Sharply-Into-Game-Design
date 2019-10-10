using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class BallController : MonoBehaviour
{
    public GameObject Bounce, Score;
    private float speed = 8f;
    private float maxVelocity = 20f;
    private float minVelocity = 10f;
    private AudioManagerController AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerController>();
        StartCoroutine(LaunchBall());
    }

    IEnumerator LaunchBall()
    {
        yield return new WaitForSeconds(0.25f);
        int x = (Random.Range(-1,1) >= 0) ? 1 : -1;
        Vector3 direction = new Vector3(x, Random.Range(-2,2), 0);
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection( direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            int inverseDirection = ( transform.position.x > 0 ) ? -1 : 1;
            GetComponent<Rigidbody2D>().AddForce(new Vector3(
                inverseDirection * maxVelocity,
                Random.Range(-3,3) * maxVelocity,
                0
            ));
            PlaySound("Bounce");
        }

        if(collision.gameObject.tag == "Bumper"){
            GetComponent<Rigidbody2D>().AddForce(new Vector3(
                (transform.position.x + 1) * maxVelocity,
                transform.position.y * maxVelocity,
                0
            ));
            PlaySound("Bounce");
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) {
        if(trigger.gameObject.tag == "Goal"){
            if(trigger.gameObject.name == "Player2Goal"){
                GameObject.Find("Player1").GetComponent<PlayerController>().IncrementScore();
            }else if(trigger.gameObject.name == "Player1Goal"){
                GameObject.Find("Player2").GetComponent<PlayerController>().IncrementScore();
            }
            PlaySound("Score");
        }
    }

    private void OnTriggerExit2D(Collider2D trigger) {
        if(trigger.gameObject.tag == "Scrubber"){
            Destroy(gameObject);
            GameObject.Find("Main Camera").GetComponent<GameManager>().ResetField();
        }
    }

    private void PlaySound(string sound)
    {
        switch(sound){
            case "Bounce":
                AudioManager.PlaySound("Bounce", modulatePitch:true, volume:0.75f);
                break;
            case "Score":
                AudioManager.PlaySound("Score", modulatePitch:true, volume:0.75f);
                break; 
        }
    }

    private void FixedUpdate()
    {
        ModerateSpeed();
    }

    private void ModerateSpeed()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxVelocity;
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude < minVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * minVelocity;
        }
    }
}
