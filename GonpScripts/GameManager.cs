using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject 
        BallPrefab,
        PlayerPrefab;
    public TMP_Text
        Player1Score,
        Player2Score;
    [Range(1,2)]
    public int playerCount = 1;
    private GameObject
        Player1,
        Player2;
    private AudioManagerController AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerController>();
        SetupGame();
    }

    private void SetupGame()
    {
        AudioManager.PlaySound(name:"GameTrack", loopable:true, selfDestruct:false, volume:0.15f);
        SpawnPlayers();
        SpawnBall();
    }

    private void SpawnPlayers()
    {
        for(int index = 1; index <= playerCount; index++){
            SpawnPlayers(index);
        }
    }

    private void SpawnPlayers(int id)
    {
        GameObject player = Instantiate(PlayerPrefab);
        if(id == 1){
            player.transform.position = new Vector3(8,0,0);
            Player1 = player;
        }else{
            player.transform.position = new Vector3(-8,0,0);
            Player2 = player;
            if(PlayerPrefs.GetInt("GameMode") == 1){
                player.GetComponent<PlayerController>().isNPC = true;
            }
        }
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<PlayerController>().id = id.ToString();
        player.name = "Player" + id;
    }

    public void ResetField()
    {
        Player1.transform.position = new Vector3(8,0,0);
        Player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player2.transform.position = new Vector3(-8,0,0);
        Player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        SpawnBall();
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(BallPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        Player1Score.text = Player1.GetComponent<PlayerController>().GetScore().ToString();
        Player2Score.text = Player2.GetComponent<PlayerController>().GetScore().ToString();
    }
}
