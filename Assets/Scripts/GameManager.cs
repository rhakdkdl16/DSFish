using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Fish fish;
    [SerializeField] GameObject pipes;
    int score;
    
    enum State
    {
        READY, PLAY, GAMEOVER
    }
    State state;

    private void Start()
    {
        pipes.SetActive(false);

        state = State.READY;
        fish.SetKinematic(true);         
    }


    private void Update()
    {
        switch (state)
        {
            case State.READY:
                if(Input.GetButtonDown("Fire2"))
                {
                    GameStart();
                }
                break;
            case State.PLAY:
                if (fish.IsDead) GameOver();
                
                break;
            case State.GAMEOVER:
                if(Input.GetButtonDown("Fire1"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
    }
    void GameStart()
    {
        state = State.PLAY;        
        fish.SetKinematic(false);
        pipes.SetActive(true);

    }

    void GameOver()
    {
        state = State.GAMEOVER;

        ScrollObject[] scrollObjects =   GameObject.FindObjectsOfType<ScrollObject>();  
        foreach(ScrollObject scrollObject in scrollObjects)
        {
            scrollObject.enabled = false;    // < SCrollObject를 비활성화 
        }
    }

    public void IncreaseScore()//게임스코어 증가
    {
        score++;
       ScrollObject[] games  =  GameObject.FindObjectsOfType<ScrollObject>();
        for(int i = 0; i < games.Length;)
        {
        games[i].speedUp();
        i++;
        }
        
        scoreText.text = "Score " + score.ToString();
        
    }
}

