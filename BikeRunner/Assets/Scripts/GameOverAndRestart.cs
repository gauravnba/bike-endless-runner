using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverAndRestart : MonoBehaviour {
    [SerializeField]
    Text FinalScore;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameOver(int score)
    {
        gameObject.SetActive(true);
        FinalScore.text = "Score: " + score.ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
