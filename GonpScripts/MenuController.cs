using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    AudioManagerController AudioManager;
    private void Start() {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerController>();
        AudioManager.PlaySound(name:"Menu", loopable:true, selfDestruct:false, volume:0.15f);
    }

    public void LoadPlayerSelect(){
        AudioManager.PlaySound(name:"Button", modulatePitch:true, volume:0.75f);
        StartCoroutine(PlayerSelect());
    }

    IEnumerator PlayerSelect()
    {
        yield return new WaitForSeconds(0.25f);
        AudioManager.StopSound();
        SceneManager.LoadScene("PlayerSelect");
    }

    public void LoadSinglePlayerGame(){
        AudioManager.PlaySound(name:"Button",modulatePitch:true, volume:0.75f);
        PlayerPrefs.SetInt("GameMode", 1);
        StartCoroutine(Game());
    }

    public void LoadMultiPlayerGame(){
        AudioManager.PlaySound(name:"Button",modulatePitch:true, volume:0.75f);
        PlayerPrefs.SetInt("GameMode", 0);
        StartCoroutine(Game());
    }

    IEnumerator Game(){
        yield return new WaitForSeconds(0.25f);
        AudioManager.StopSound();
        SceneManager.LoadScene("Game");
    }
}
