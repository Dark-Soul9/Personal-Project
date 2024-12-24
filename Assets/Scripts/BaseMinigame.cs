using UnityEngine;
using static CoinToss;

public abstract class BaseMinigame : MonoBehaviour
{
    public abstract string GetResultDisplayText(GameResult result);
    public abstract string GetAIChoiceDisplayText();
    // Abstract StartMinigame method
    public abstract void StartMinigame();
    public abstract System.Enum GetChoiceFromIndex(int index);
    // Abstract property for AI choice
    public abstract System.Enum AIChoice { get; }

    // Abstract method to resolve the minigame
    public abstract void ResolveMinigame(System.Enum playerChoice);

    public delegate void GameEndDelegate(GameResult result);
    public event GameEndDelegate OnGameEnd;

    public void EndGame(GameResult result)
    {
        OnGameEnd?.Invoke(result);
    }
}
public enum GameResult
{
    Win,
    Lose,
    Draw
}
