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
    [SerializeField] Text tochText;
    [SerializeField] Image logoImage;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Text finishScoreTExt;
    [SerializeField] Text bestScoreText;
    [SerializeField] Text BestScoret;
    int score;
    int bestScore;
    Vector2 fishLocalpos;
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
        fishLocalpos = fish.transform.position;
        bestScore = PlayerPrefs.GetInt("BEST_SCORE");

        
    }


    private void Update()
    {

        switch (state)
        {
            case State.READY:
                scoreText.gameObject.SetActive(false);
                float a = Mathf.PingPong(Time.time,1);
                tochText.color = new Color(1,1,1,a);
                logoImage.color = new Color(1,1,1,a);
                logoImage.transform.localScale = new Vector2(a , a );
                if(Input.GetButtonDown("Fire1"))
                {
                    GameStart();
                    
                }
                break;
            case State.PLAY:
                if (fish.IsDead) GameOver();
                
                break;
            case State.GAMEOVER:
               break;
        }


        if (score > bestScore)
        {
            float a = Mathf.PingPong(Time.time , 1);
            BestScoret.gameObject.SetActive(true);
            BestScoret.color = new Color(a*4, a*4,a*4, 1);
        }
        else
        {
            BestScoret.gameObject.SetActive(false);
        }
    }
    public void GameStart()
    {
        state = State.PLAY;        
        fish.SetKinematic(false);
        pipes.SetActive(true);
        tochText.gameObject.SetActive(false);
        logoImage.gameObject.SetActive(false);
         scoreText.gameObject.SetActive(true);

    }

    void GameOver()
    {
        state = State.GAMEOVER;

        ScrollObject[] scrollObjects =   GameObject.FindObjectsOfType<ScrollObject>();  
        foreach(ScrollObject scrollObject in scrollObjects)
        {
            scrollObject.enabled = false;    // < SCrollObject를 비활성화 
        }
        gameOverPanel.SetActive(true);
        finishScoreTExt.text = scoreText.text;
        
        if(score >  bestScore)
        {
            
            PlayerPrefs.SetInt("BEST_SCORE", score);
            //bestScoreText.text = "최고점수" + bestScore.ToString();
        }
        bestScore = PlayerPrefs.GetInt("BEST_SCORE");     //베스트스코어 키 
        bestScoreText.text = "최고점수" + bestScore.ToString(); 
        
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
    public void OnClickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickContinue()
    {
        fish.transform.position = fishLocalpos;
        fish.SendMessage("Idle");
        state = State.PLAY;
        gameOverPanel.SetActive(false);
        
         ScrollObject[] scrollObjects =   GameObject.FindObjectsOfType<ScrollObject>();  
        foreach(ScrollObject scrollObject in scrollObjects)
        {
            scrollObject.enabled = true;    // < SCrollObject를 활성화 
        }
      
       
    }
    

   
}

