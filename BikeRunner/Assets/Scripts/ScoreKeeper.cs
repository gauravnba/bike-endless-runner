using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    [SerializeField]
    Text HUD;
    [SerializeField]
    GameOverAndRestart GameOverScreen;

    float mScore = 0.0f;
    PlayerControl mPlayerReference;
    bool isPlayerMoving = false;
    bool isPlayerDead = false;

	// Use this for initialization
	void Start () {
        mPlayerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayerMoving && !isPlayerDead)
        {
            mScore += Time.deltaTime;
        }

        HUD.text = "Score: " + ((int)mScore).ToString() + "\nFuel: " + ((int)mPlayerReference.Fuel).ToString();
	}

    public void OnPlayerMoving(bool isMoving)
    {
        isPlayerMoving = isMoving;
    }

    public void OnPlayerDeath()
    {
        isPlayerDead = true;
        GameOverScreen.OnGameOver((int)mScore);
        gameObject.SetActive(false);
    }
}
