using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    private GroundPieceController[] allGroundPieces;
    private PlayerController player;
  


    private void Start()
    {
        SetupNewLevel();
        player = FindObjectOfType<PlayerController>();
    }

    private void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPieceController>();
    }

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColored == false)
            {
                isFinished = false;
                break;
            }
        }

        if (isFinished)
        {
            StartCoroutine(waiter());
            
           

        }

           
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator waiter()
    {
        
        //player.winParticle.Play();

        //Wait for 2 seconds
        yield return new WaitForSeconds(1.2f);
        NextLevel();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
