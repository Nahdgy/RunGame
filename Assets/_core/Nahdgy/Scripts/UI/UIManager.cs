using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private GameObject goUi;
    [SerializeField] 
    private GameObject countDownUi;

    [SerializeField]
    public float timerStart,timerGo;


    [SerializeField]
    private Animator animatorWin;
    [SerializeField]
    private string animaWin;
    [SerializeField]
    private Animator animatorGameOver;
    [SerializeField]
    private string animaGameOver;
    

    [SerializeField]
    private string menuScene, levelOneScene, creditsScene;

    [SerializeField]
    private Player player;
 
    private void Start()
    {
        if (player != null) {countDownUi = GameObject.FindGameObjectWithTag("CountDown");}
        
        //player = GameObject.FindAnyObjectByType<Player>();
        
        StartCoroutine(AnimationStart());       
    }
    private IEnumerator AnimationStart()
    {
        if (player != null)
        {
            player.canMove = false;
            countDownUi.SetActive(true);
            yield return new WaitForSeconds(timerStart);
            countDownUi.SetActive(false);
            goUi.SetActive(true);
            yield return new WaitForSeconds(timerGo);
            goUi.SetActive(false);
            player.canMove = true;
        }
        else
        {
            yield return null;
        }
    }
    public void StartMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
    }
    public void GameOver()
    {
        player.playerRb.isKinematic = true;
        gameOverUI.SetActive(true);
        animatorGameOver.Play(animaGameOver);    
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Win()
    {
        winUI.SetActive(true);
        player.canMove = false;
        player.canRun = false;
        player.speed = 1;
        animatorWin.Play(animaWin);
    }
    public void Play()
    {
        SceneManager.LoadScene(levelOneScene);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        player.speed += 10;
    }
    public void Credit()
    {
        SceneManager.LoadScene(creditsScene);
    }

}
