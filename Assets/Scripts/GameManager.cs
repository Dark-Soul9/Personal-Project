using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [Header("Game Settings")]
    public int playerLives = 2;
    public int aiLives = 2;

    [Header("UI References")]
    public GameObject minigamePanel; // Panel for minigame buttons
    public GameObject actionPanel;  // Panel for shoot/dodge buttons
    public TextMeshProUGUI aiChoiceText;       // Displays AI choice
    public TextMeshProUGUI resultText;         // Displays minigame result

    public Button rockButton, paperButton, scissorsButton;  // Rock-Paper-Scissors buttons
    public Button headsButton, tailsButton;                 // Coin Toss buttons
    public Button shootButton, bluffButton;                 // Post-minigame win buttons
    public Button dodgeButton, notDodgeButton;              // Post-minigame lose buttons

    [SerializeField] private List<BaseMinigame> minigamePrefabs; // List of minigame prefabs

    private BaseMinigame currentMinigame;
    private GameResult currentResult;
    private string playerAction = "";
    private string aiAction = "";

    // Called by PlayerInputHandler when a button is clicked
    public void PlayerMadeChoice(int choiceIndex)
    {
        // Convert the choice index into the correct enum for the current minigame
        System.Enum playerChoice = currentMinigame.GetChoiceFromIndex(choiceIndex);

        // Resolve the minigame with the player's choice
        currentMinigame.ResolveMinigame(playerChoice);
    }
    public void StartGame()
    {
        PlayNextMinigame();
    }

    private void PlayNextMinigame()
    {
        if (minigamePrefabs.Count == 0)
        {
            Debug.LogError("No minigames assigned in GameManager!");
            return;
        }

        int randomIndex = Random.Range(0, minigamePrefabs.Count);
        BaseMinigame minigameInstance = Instantiate(minigamePrefabs[randomIndex]);
        currentMinigame = minigameInstance;

        // Subscribe to the minigame end event
        currentMinigame.OnGameEnd += HandleMinigameResult;

        // Display the appropriate minigame buttons
        ShowMinigameButtons();
        currentMinigame.StartMinigame();
    }

    private void ShowMinigameButtons()
    {
        minigamePanel.SetActive(true);

        // Show buttons based on the minigame type
        if (currentMinigame is RockPaperScissors)
        {
            rockButton.gameObject.SetActive(true);
            paperButton.gameObject.SetActive(true);
            scissorsButton.gameObject.SetActive(true);
        }
        else if (currentMinigame is CoinToss)
        {
            headsButton.gameObject.SetActive(true);
            tailsButton.gameObject.SetActive(true);
        }

        actionPanel.SetActive(false);
    }

    private void HideMinigameButtons()
    {
        foreach (Transform child in minigamePanel.transform)
        {
            child.gameObject.SetActive(false);
        }

        minigamePanel.SetActive(false);
    }

    private void HandleMinigameResult(GameResult result)
    {
        currentMinigame.OnGameEnd -= HandleMinigameResult;
        Destroy(currentMinigame.gameObject);

        aiChoiceText.text = currentMinigame.GetAIChoiceDisplayText();
        resultText.text = currentMinigame.GetResultDisplayText(result);
        currentResult = result;

        HideMinigameButtons();

        if (result == GameResult.Win)
        {
            ShowWinActions(); // Present shoot or bluff options
        }
        else if (result == GameResult.Lose)
        {
            ShowLoseActions(); // Present dodge or not dodge options
        }
        else if(result == GameResult.Draw)
        {
            PlayNextMinigame();
        }
    }


    private void ShowWinActions()
    {
        actionPanel.SetActive(true);
        shootButton.gameObject.SetActive(true);
        bluffButton.gameObject.SetActive(true);

        dodgeButton.gameObject.SetActive(false);
        notDodgeButton.gameObject.SetActive(false);
    }

    private void ShowLoseActions()
    {
        actionPanel.SetActive(true);
        dodgeButton.gameObject.SetActive(true);
        notDodgeButton.gameObject.SetActive(true);

        shootButton.gameObject.SetActive(false);
        bluffButton.gameObject.SetActive(false);
    }

    public void OnShootButtonPressed()
    {
        ProcessAction("Shoot");
    }

    public void OnBluffButtonPressed()
    {
        ProcessAction("Bluff");
    }

    public void OnDodgeButtonPressed()
    {
        ProcessAction("Dodge");
    }

    public void OnNotDodgeButtonPressed()
    {
        ProcessAction("Not Dodge");
    }

    private void ProcessAction(string action)
    {
        playerAction = action;
        

        if (currentResult == GameResult.Win)
        {
            aiAction = Random.Range(0, 2) == 0 ? "Dodge" : "Not Dodge"; // AI randomly decides
            if (action == "Shoot" && aiAction == "Not Dodge")
            {
                aiLives--;
            }
            else if (action == "Bluff" && aiAction == "Dodge")
            {
                aiLives--; // Bluff success
            }
        }
        else if (currentResult == GameResult.Lose)
        {
            aiAction = Random.Range(0, 2) == 0 ? "Shoot" : "Bluff"; // AI randomly decides
            if (action == "Not Dodge" && aiAction == "Shoot")
            {
                playerLives--;
            }
            else if (action == "Dodge" && aiAction == "Bluff")
            {
                playerLives--;
            }
        }

        Debug.Log($"Player: {action}, AI: {aiAction}, Player Lives: {playerLives}, AI Lives: {aiLives}");

        if (playerLives > 0 && aiLives > 0)
        {
            PlayNextMinigame();
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log(playerLives > 0 ? "Player Wins!" : "AI Wins!");
    }
}
