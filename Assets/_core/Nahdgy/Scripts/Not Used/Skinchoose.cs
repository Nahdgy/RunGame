using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skinchoose : MonoBehaviour
{
    private float delay = 0.5f;
    
    [SerializeField]
    private GameObject cubi,cyli,capuci;
    [SerializeField]
    private GameObject [] ui;
    

    public GameObject playerChosen;

    private string menuDifficiculty = "MainMenu";


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChooseCubi()
    {
        playerChosen = cubi;
        EnableUI();
        StartCoroutine(GoToMenuDifficulty());
        
    }
    public void ChooseCapuci() 
    {
        playerChosen = capuci;
        EnableUI();
        StartCoroutine(GoToMenuDifficulty());
    }
    public void ChooseCyli() 
    {
        playerChosen = cyli;
        EnableUI();
        StartCoroutine(GoToMenuDifficulty());
    }

    private void EnableUI()
    {
        
        for (int i = 0; i < ui.Length; i++) 
        {
            GameObject.Destroy(ui[i]);
        }
//or just enable the gameobject "characters" and activate it on start scene
    }
    private IEnumerator GoToMenuDifficulty()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(menuDifficiculty);
    }
}
