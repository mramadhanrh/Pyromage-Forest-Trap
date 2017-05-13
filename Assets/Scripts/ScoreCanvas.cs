using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCanvas : MonoBehaviour {

    int score, highScore;
    public string[] words;
    
	// Use this for initialization
	void Start () {
        score = GameManager.score;
        highScore = GameManager.highScore;
        ChangeWords();
        TextMeshProUGUI scoreUI = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
        TextMeshProUGUI highScoreUI = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2];
        scoreUI.SetText(score.ToString());
        highScoreUI.SetText("Highscore : " + highScore.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeWords()
    {
        TextMeshProUGUI wordsUI = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1];
        if (score < 5)
            wordsUI.SetText(words[0]);
        else if (score < 15)
            wordsUI.SetText(words[1]);
        else if (score < 25)
            wordsUI.SetText(words[2]);
        else if (score < 50)
            wordsUI.SetText(words[3]);
        else
            wordsUI.SetText(words[4]);

    }
}
