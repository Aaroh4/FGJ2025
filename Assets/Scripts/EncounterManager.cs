using UnityEngine;
using TMPro; // For TextMeshProUGUI
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class EncounterManager : MonoBehaviour
{
    public EncounterParagraphs encounterData; // Assign the ScriptableObject in the Inspector

    public GameManager gameManager;

    public TextComparing textComparing;

    public GameObject writingField;

    public GameObject paragraphs;

    [Header("UI References")]
    public TextMeshProUGUI[] playerOptionTexts; // Array to hold the 4 text fields

    private EncounterData playerData;
    private EnemyEncounterData enemyData;

    private string[] playerOptions;

    private bool selecting;
    private bool playerSpelling;
    private bool enemyResponding;
    public int currentEncounter = 1;


    void Update()
    {
        if (selecting)
        {
            paragraphs.SetActive(true);
            writingField.SetActive(false);
            textComparing.mySecondText.text = "";
            textComparing.greyText.text = "";
            textComparing.myInputField.text = "";
            playerOptions = SelectPlayerParagraphsForRound();
            DisplayPlayerOptions(playerOptions); // Display the player options on the UI
            selecting = false;
            playerSpelling = true;
            Debug.Log($"Round - Player Options: {string.Join(", ", playerOptions)}");
        }

        if (playerSpelling)
        {
            SelectSpell(playerOptions);
        }

        if (textComparing.totalTime <= 0 && enemyResponding)
        {
            SelectEnemyResponse();
            DealDamageToPlayer();
            gameManager.DealDamageToEnemy(15, currentEncounter);
            enemyResponding = false;

            Debug.Log("Player HP: " + gameManager.heroCurrentHP);

            if (gameManager.heroCurrentHP > 0)
            {
                StartCoroutine(WaitAndStartNewRound(8f)); // Wait for 8 seconds before starting a new round
            }
            else
            {
                Debug.Log("Player is defeated!");
                writingField.SetActive(false);
                paragraphs.SetActive(false);
                gameManager.Encounter1Panel.SetActive(false);
                gameManager.Encounter2Panel.SetActive(false);
                gameManager.Encounter3Panel.SetActive(false);
                gameManager.Encounter1VictoryPanel.SetActive(false);
                gameManager.Encounter2VictoryPanel.SetActive(false);
                gameManager.Encounter3VictoryPanel.SetActive(false);
                gameManager.StartCoroutine(gameManager.HandleGameOver());
            }
        }
        Debug.Log("Total time is: " + textComparing.totalTime);
    }

    void SelectEnemyResponse()
    {
        textComparing.myInputField.gameObject.SetActive(false);
        textComparing.greyText.text = "";
        string enemyResponse = SelectRandomParagraph(enemyData.numberedParagraphs);
        Debug.Log($"Enemy Response: {enemyResponse}");
        textComparing.mySecondText.text = "Enemy Response: " + enemyResponse;
    }

    public void StartEncounter(int encounterNumber)
    {
        textComparing.totalTime = 30;
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

        StartNewRound();
    }

    private void StartNewRound()
    {
        selecting = true;
        playerSpelling = false;
        enemyResponding = false;
        textComparing.totalTime = 2147483647;
    }

    private void SelectSpell(string[] options)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            textComparing.compareString = options[0];
            Debug.Log("Selected option 1");
            writingField.SetActive(true);
            paragraphs.SetActive(false);
            textComparing.totalTime = 30;
            playerSpelling = false;
            enemyResponding = true;
            textComparing.myInputField.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            textComparing.compareString = options[1];
            Debug.Log("Selected option 2");
            writingField.SetActive(true);
            paragraphs.SetActive(false);
            textComparing.totalTime = 30;
            playerSpelling = false;
            enemyResponding = true;
            textComparing.myInputField.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            textComparing.compareString = options[2];
            Debug.Log("Selected option 3");
            writingField.SetActive(true);
            paragraphs.SetActive(false);
            textComparing.totalTime = 30;
            playerSpelling = false;
            enemyResponding = true;
            textComparing.myInputField.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            textComparing.compareString = options[3];
            Debug.Log("Selected option 4");
            writingField.SetActive(true);
            paragraphs.SetActive(false);
            textComparing.totalTime = 30;
            playerSpelling = false;
            enemyResponding = true;
            textComparing.myInputField.gameObject.SetActive(true);
        }
    }

    private void DealDamageToPlayer()
    {
        gameManager.TakeDamage(textComparing.compareString.Length - textComparing.CalculatePoints());
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

    private IEnumerator WaitAndStartNewRound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartNewRound();
    }
}
