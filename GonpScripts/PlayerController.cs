using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isNPC;
    public string id = "1";
    private float speed = 10f;
    private int score = 0;

    void FixedUpdate()
    {
        if(!isNPC){
            HandlePlayerMovement();
        }else{
            ModerateSpeed();
            MatchBallPosition();
        }
    }

    public void HandlePlayerMovement()
    {
        if(!isNPC){
            float translation = Input.GetAxis("Vertical"+id) * speed;
            translation *= Time.deltaTime;
            transform.Translate(0, translation, 0);
        }else{
            ModerateSpeed();
            MatchBallPosition();
        }
    }
    
    private void MatchBallPosition()
    {
        transform.position = new Vector3(-8, GameObject.FindWithTag("Ball").transform.position.y, 0);
    }

    public void IncrementScore(){
        score++;
    }

    public int GetScore()
    {
        return score;
    }

    private void ModerateSpeed()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > (speed-2f))
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed;
        }
    }
}
