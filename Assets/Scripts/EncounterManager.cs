using UnityEngine;
using TMPro; // For TextMeshProUGUI
using System.Collections.Generic;

public class EncounterManager : MonoBehaviour
{
    public EncounterParagraphs encounterData; // Assign the ScriptableObject in the Inspector

	public TextComparing textComparing;

	public GameObject	writingField;

	public GameObject	paragraphs;

    [Header("UI References")]
    public TextMeshProUGUI[] playerOptionTexts; // Array to hold the 4 text fields

    private EncounterData playerData;
    private EnemyEncounterData enemyData;

	private string[] playerOptions;

	private bool selecting;

	void Update()
	{

		if (selecting == true)
			SelectSpell(playerOptions);
		//if (textComparing.totalTime <= 0)
		//	selecting = false;

		if (selecting == false)
		{
			playerOptions = SelectPlayerParagraphsForRound();
			DisplayPlayerOptions(playerOptions); // Display the player options on the UI
			selecting = true;
		}

		if (textComparing.totalTime <= 0)
		{
			Debug.Log($"Round - Player Options: {string.Join(", ", playerOptions)}");

			// Select enemy response (replace scoring logic with your custom implementation)
			string enemyResponse = SelectRandomParagraph(enemyData.numberedParagraphs);
			Debug.Log($"Enemy Response: {enemyResponse}");
			textComparing.mySecondText.text = enemyResponse;
		}
	}

    public void StartEncounter(int encounterNumber)
    {
        // Select the current encounter data for the player
        playerData = encounterNumber switch
        {
            1 => encounterData.playerFirstEncounter,
            2 => encounterData.playerSecondEncounter,
            3 => encounterData.playerLastEncounter,
            _ => null
        };

        // Select the current encounter data for the enemy
        enemyData = encounterNumber switch
        {
            1 => encounterData.enemyFirstEncounter,
            2 => encounterData.enemySecondEncounter,
            3 => encounterData.enemyLastEncounter,
            _ => null
        };

        if (playerData == null || enemyData == null)
        {
            Debug.LogError("Invalid encounter number!");
            return;
        }
    }

	private void SelectSpell(string[] options)
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			textComparing.compareString = options[0];
			Debug.Log("Selected option 1");
			writingField.SetActive(true);
			paragraphs.SetActive(false);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			textComparing.compareString = options[1];
			Debug.Log("Selected option 2");
			writingField.SetActive(true);
			paragraphs.SetActive(false);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			textComparing.compareString = options[2];
			Debug.Log("Selected option 3");
			writingField.SetActive(true);
			paragraphs.SetActive(false);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			textComparing.compareString = options[3];
			Debug.Log("Selected option 4");
			writingField.SetActive(true);
			paragraphs.SetActive(false);
		}
	}

    private string[] SelectPlayerParagraphsForRound()
    {
        // Select 1 poor hit, 2 normal hits, and 1 critical hit paragraph
        string poorHit = SelectRandomParagraph(playerData.poorHit.paragraphs);
        string[] normalHits = SelectMultipleRandom(playerData.normalHit.paragraphs, 2);
        string criticalHit = SelectRandomParagraph(playerData.criticalHit.paragraphs);

        // Combine into an array of 4 options
        string[] paragraphs = new string[4];
        paragraphs[0] = poorHit;
        paragraphs[1] = normalHits[0];
        paragraphs[2] = normalHits[1];
        paragraphs[3] = criticalHit;

        // Shuffle the paragraphs to randomize their order
        return ShuffleArray(paragraphs);
    }

    private void DisplayPlayerOptions(string[] options)
    {
        if (playerOptionTexts.Length != options.Length)
        {
            Debug.LogError("PlayerOptionTexts array size does not match the number of options!");
            return;
        }

        for (int i = 0; i < options.Length; i++)
        {
            playerOptionTexts[i].text = options[i]; // Assign the option text to the corresponding UI field
        }
    }

    // Select a random paragraph from an array for critical and poor hits
    private string SelectRandomParagraph(string[] paragraphs)
    {
        return paragraphs.Length > 0 ? paragraphs[Random.Range(0, paragraphs.Length)] : "No Paragraph";
    }

    // Select multiple random paragraphs from an array made for the player's normal hits
    private string[] SelectMultipleRandom(string[] paragraphs, int count)
    {
        if (paragraphs.Length < count) return paragraphs; // Edge case: fewer options than requested

        List<string> selected = new List<string>(); // List to store selected paragraphs
        List<string> remaining = new List<string>(paragraphs);  // List to store remaining paragraphs

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, remaining.Count); // Random index within remaining paragraphs
            selected.Add(remaining[index]); // Add selected paragraph to the list
            remaining.RemoveAt(index); // Remove selected paragraph from remaining
        }

        return selected.ToArray();
    }

    // Fisher-Yates shuffle algorithm to shuffle an array
    private string[] ShuffleArray(string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(0, array.Length);
            string temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
}
