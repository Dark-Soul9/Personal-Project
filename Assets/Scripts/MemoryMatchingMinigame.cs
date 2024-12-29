using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryMatchingMinigame : BaseMinigame
{
    public enum MemoryChoices { Red, Blue, Green, Yellow, Purple, Orange }

    private List<MemoryChoices> aiSequence; // AI's generated sequence
    private List<MemoryChoices> playerSequence; // Player's entered sequence
    private int sequenceLength = 3; // Default sequence length (modifiable in Inspector)

    public override System.Enum AIChoice => null; // Not applicable for this minigame

    [SerializeField] private TMPro.TextMeshProUGUI aiSequenceText; // Text element to display AI sequence
    [SerializeField] private float displayDuration = 3f; // How long the sequence is displayed

    private bool isDisplayingSequence = false; // To check if sequence display is active

    // Initialize the game
    public override void StartMinigame()
    {
        aiSequence = GenerateRandomSequence(sequenceLength);
        playerSequence = new List<MemoryChoices>();

        StartCoroutine(DisplaySequence());
    }

    // Resolve the game once the player completes their sequence
    public override void ResolveMinigame(System.Enum playerChoice)
    {
        if (isDisplayingSequence) return; // Ignore input while the sequence is being displayed

        playerSequence.Add((MemoryChoices)playerChoice);

        if (playerSequence.Count == aiSequence.Count)
        {
            if (IsSequenceMatching(aiSequence, playerSequence))
            {
                EndGame(GameResult.Win);
            }
            else
            {
                EndGame(GameResult.Lose);
            }
        }
    }

    // Generate a random sequence for AI
    private List<MemoryChoices> GenerateRandomSequence(int length)
    {
        List<MemoryChoices> sequence = new List<MemoryChoices>();
        for (int i = 0; i < length; i++)
        {
            sequence.Add((MemoryChoices)Random.Range(0, System.Enum.GetValues(typeof(MemoryChoices)).Length));
        }
        return sequence;
    }

    // Coroutine to display the sequence briefly and then hide it
    private IEnumerator DisplaySequence()
    {
        isDisplayingSequence = true;
        aiSequenceText.gameObject.SetActive(true);

        // Show each choice in the sequence briefly
        foreach (var choice in aiSequence)
        {
            aiSequenceText.text = choice.ToString();
            yield return new WaitForSeconds(displayDuration / sequenceLength);
        }

        // Hide the text after the sequence is shown
        aiSequenceText.text = string.Empty;
        aiSequenceText.gameObject.SetActive(false);

        isDisplayingSequence = false;
    }

    // Check if the player's sequence matches the AI's sequence
    private bool IsSequenceMatching(List<MemoryChoices> aiSequence, List<MemoryChoices> playerSequence)
    {
        for (int i = 0; i < aiSequence.Count; i++)
        {
            if (aiSequence[i] != playerSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    public override string GetResultDisplayText(GameResult result)
    {
        switch (result)
        {
            case GameResult.Win:
                return "You matched the sequence correctly!";
            case GameResult.Lose:
                return "You did not match the sequence.";
            default:
                return "Game ended unexpectedly.";
        }
    }

    public override string GetAIChoiceDisplayText()
    {
        return "";
    }

    public override System.Enum GetChoiceFromIndex(int index)
    {
        return (MemoryChoices)index;
    }
}
