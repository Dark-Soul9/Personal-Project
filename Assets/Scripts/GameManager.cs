using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public enum GameState { idle, minigame, postMinigame, playing}
    public GameState currentState;
    
    public GameObject[] minigamePrefabs;
    public GameObject currentMinigame;
    public Transform minigameContainer;
    public Minigame currentMinigameScript;

    public GameObject[] rockPaperScissors;
    public void EnterMinigame()
    {
        currentState = GameState.minigame;
    }    
    public void EnterIdle()
    {
        currentState = GameState.idle;
    }
    public void EnterPostMinigame()
    {
        currentState = GameState.postMinigame;
    }
    private void Update()
    {
        switch(currentState)
        {
            case GameState.idle:
                HandleIdle();
                break;
            case GameState.minigame:
                HandleMinigame();
                break;
            case GameState.postMinigame:
                HandlePostMinigame();
                break;
            case GameState.playing:
                //do nothing
                break;
        }
    }
    void HandleIdle()
    {

    }
    void HandleMinigame()
    {
        for (int i = 0; i < rockPaperScissors.Length; i++)
        {
            rockPaperScissors[i].SetActive(true);
        }
        currentState = GameState.playing;
    }
    void HandlePostMinigame()
    {

    }
    public void StartNewMinigame()
    {
        currentMinigame = Instantiate(minigamePrefabs[Random.Range(0, minigamePrefabs.Length)], minigameContainer.position, Quaternion.identity);
        currentMinigameScript = currentMinigame.GetComponent<Minigame>();
        EnterMinigame();
    }
    public void PlayMinigame(int playerInput)
    {
        currentMinigameScript.AiInput();
        currentMinigameScript.PlayerInput(playerInput);
        Debug.Log($"You {currentMinigameScript.DetermineResult(currentMinigameScript.aiInput, currentMinigameScript.playerInput).ToString()}");
    }
}
