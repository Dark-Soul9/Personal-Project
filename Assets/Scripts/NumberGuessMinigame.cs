using UnityEngine;

public class NumberGuessMinigame : BaseMinigame
{
    // Enum for possible number choices
    public enum NumberChoices { One = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten }

    private NumberChoices aiChoice; // AI's choice
    private NumberChoices playerChoice; // Player's choice

    public override System.Enum AIChoice => aiChoice; // AI choice as an enum

    // StartMinigame: Randomly assign AI's choice
    public override void StartMinigame()
    {
        aiChoice = (NumberChoices)Random.Range(1, 11); // Random number between 1 and 10
    }

    // Resolve the minigame based on the player's choice
    public override void ResolveMinigame(System.Enum playerChoice)
    {
        this.playerChoice = (NumberChoices)playerChoice;

        if (this.playerChoice == aiChoice)
        {
            EndGame(GameResult.Win);
        }
        else
        {
            EndGame(GameResult.Lose);
        }
    }

    // Get the result display text (override)
    public override string GetResultDisplayText(GameResult result)
    {
        switch (result)
        {
            case GameResult.Win:
                return $"You Win! You guessed {playerChoice}, and AI picked {aiChoice}.";
            case GameResult.Lose:
                return $"You Lose! You guessed {playerChoice}, but AI picked {aiChoice}.";
            default:
                return "Game ended unexpectedly.";
        }
    }

    // Get AI choice display text (override)
    public override string GetAIChoiceDisplayText()
    {
        return $"AI Picked: {aiChoice}";
    }

    // GetChoiceFromIndex: Map button index to NumberChoices
    public override System.Enum GetChoiceFromIndex(int index)
    {
        return (NumberChoices)(index + 1); // Index 0 corresponds to NumberChoices.One
    }
}
