using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DvManager : MonoBehaviour
{
    public static DvManager instance;
    VideoPlayer mainPlayer;
    public Button BTNGo;
    public RawImage videoCanvas;
    public Action passToMainThread;

    public Text infoText;
    public Color GoodColor;
    
    void Awake(){
        instance = this;
    }

    void Start()
    {
        mainPlayer = GetComponent<VideoPlayer>();
        DvTCPClient.OnTouchStart += OnGameStart;
        DvTCPClient.OnRecieveGameFinished += OnGameFinished;
    }

    void Update(){
        if(passToMainThread != null){
            passToMainThread.Invoke();
            passToMainThread = null;
        }

        if(DvTCPClient.instance.GetSocketInfo){
            infoText.text = "Online";
            infoText.color = GoodColor;
        } else {
            infoText.text = "Offline";
            infoText.color = Color.red;
        }
    }

    void OnGameStart(){
        videoCanvas.gameObject.SetActive(true);
        mainPlayer.Play();
        StartCoroutine(SendStartMessage());
    }

    IEnumerator SendStartMessage(){
        yield return new WaitForSeconds(0.5f);
        DvTCPClient.instance.WriteSocket("1");
    }

    void OnGameFinished(){
        passToMainThread += () => {
            BTNGo.interactable = true;
            videoCanvas.gameObject.SetActive(false);
            mainPlayer.Stop();
        };
    }
}
