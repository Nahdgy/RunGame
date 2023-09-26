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
    private string menuScene, levelOneScene, levelTwoScene, levelTreeScene;

    [SerializeField]
    private Player player;
    private void Start()
    {
        StartCoroutine(AnimationStart());
    }
    private IEnumerator AnimationStart()
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
        animatorWin.Play(animaWin);
    }
    public void Level1()
    {
        SceneManager.LoadScene(levelOneScene);
    }
    public void Level2() 
    {
        SceneManager.LoadScene(levelTwoScene);
    }
    public void Level3() 
    {
        SceneManager.LoadScene(levelTreeScene);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
