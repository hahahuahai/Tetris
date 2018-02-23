using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour {

    float time, startTime;
    Text timer;
    bool isEnd = false;

    public GameObject gameOverUI;

	// Use this for initialization
	void Start () {
        timer = GameObject.Find("Canvas/Timer").GetComponent<Text>();

        //得到游戏开始的时间（单位：秒）
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (isEnd) return;
        //得到当前时间跟游戏开始的时间的差别（单位：秒）
        time = Time.time - startTime;
        //秒
        int seconds = (int)(time % 60);
        //分
        int minutes = (int)(time / 60);

        string strTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer.text = strTime;
	}

    public void GameOver()
    {
        gameOverUI.SetActive(true);

        isEnd = true;
    }
}
