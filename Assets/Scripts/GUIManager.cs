using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour {

    float time, startTime;
    Text timer_gui;
    Text score_gui;
    int score_sum = 0;
    bool isEnd = false;

    public GameObject gameOverUI;
    public Text Score_gameover_gui;

	// Use this for initialization
	void Start () {
        timer_gui = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        score_gui = GameObject.Find("Canvas/Score").GetComponent<Text>();
        //得到游戏开始的时间（单位：秒）
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (isEnd) return;
        setTime();
        setScore();
    }

    void setTime()
    {
        //得到当前时间跟游戏开始的时间的差别（单位：秒）
        time = Time.time - startTime;
        //秒
        int seconds = (int)(time % 60);
        //分
        int minutes = (int)(time / 60);

        string strTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer_gui.text = strTime;
    }
    void setScore()
    {
       int score_thisRound = Grid.caculateScore();
        score_sum += score_thisRound;
        string strScore = string.Format("{0:0000}", score_sum);
        score_gui.text = strScore;
    }

    public void GameOver()
    {
        Score_gameover_gui.text += score_gui.text;
        gameOverUI.SetActive(true);

        isEnd = true;
    }
}
