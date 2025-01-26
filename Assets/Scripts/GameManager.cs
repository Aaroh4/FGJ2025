using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EncounterManager encounterManager; // Reference to the EncounterManager
    public int heroMaxHP = 15;                // Maximum HP for the hero
    public int heroCurrentHP;

    public int Enemy1HP = 20;
    public int Enemy2HP = 40;
    public int Enemy3HP = 60;

    public GameObject deadPanel;
    public GameObject victoryPanel;

    public GameObject Encounter1Panel;
    public GameObject Encounter2Panel;
    public GameObject Encounter3Panel;

    public GameObject Encounter1VictoryPanel;
    public GameObject Encounter2VictoryPanel;
    public GameObject Encounter3VictoryPanel;

    void Start()
    {
        ResetHeroHP(); 
        StartFirstEncounter();
        heroCurrentHP = heroMaxHP;
    }

    public void ResetHeroHP()
    {
        heroCurrentHP = heroMaxHP;
        Debug.Log($"Hero HP Reset: {heroCurrentHP}/{heroMaxHP}");
    }

    public void TakeDamage(int damage)
    {
        heroCurrentHP -= damage;
        Debug.Log($"Hero took {damage} damage! Current HP: {heroCurrentHP}/{heroMaxHP}");

        if (heroCurrentHP <= 0)
        {
            heroCurrentHP = 0;
            HandleGameOver();
        }
    }

    public void DealDamageToEnemy(int damage, int enemyNumber)
    {
        if (enemyNumber == 1)
        {
            Enemy1HP -= damage;
            Debug.Log($"Enemy 1 took {damage} damage! Current HP: {Enemy1HP}");
            if (Enemy1HP <= 0)
            {
                Debug.Log("Enemy 1 has been defeated!");
                
                StartCoroutine(GoToNextEncounter(2));
            }
        }
        else if (enemyNumber == 2)
        {
            Enemy2HP -= damage;
            Debug.Log($"Enemy 2 took {damage} damage! Current HP: {Enemy2HP}");

            if (Enemy2HP <= 0)
            {
                Debug.Log("Enemy 2 has been defeated!");
                StartCoroutine(GoToNextEncounter(3));
            }
        }
        else if (enemyNumber == 3)
        {
            Enemy3HP -= damage;
            Debug.Log($"Enemy 3 took {damage} damage! Current HP: {Enemy3HP}");

            if (Enemy3HP <= 0)
            {
                Debug.Log("Enemy 3 has been defeated!");
                Encounter3Panel.SetActive(false);
                victoryPanel.SetActive(true);

            }
        }
    }

    private IEnumerator GoToNextEncounter(int encounter)
    {
        switch (encounter)
        {
            case 2:
                encounterManager.writingField.SetActive(false);
                encounterManager.paragraphs.SetActive(false);
                Encounter1Panel.SetActive(false);
                Encounter1VictoryPanel.SetActive(true);
                yield return new WaitForSeconds(8);
                encounterManager.writingField.SetActive(true);
                encounterManager.paragraphs.SetActive(true);
                encounterManager.currentEncounter++;
                Encounter1VictoryPanel.SetActive(false);
                Encounter2Panel.SetActive(true);

                break;
            case 3:
                encounterManager.writingField.SetActive(false);
                encounterManager.paragraphs.SetActive(false);
                Encounter2Panel.SetActive(false);
                Encounter2VictoryPanel.SetActive(true);
                yield return new WaitForSeconds(8);
                encounterManager.writingField.SetActive(true);
                encounterManager.paragraphs.SetActive(true);
                encounterManager.currentEncounter++;
                Encounter2VictoryPanel.SetActive(false);
                Encounter3Panel.SetActive(true);
                encounterManager.StartEncounter(3);
                break;
            default:
                break;
        }
    }

    public IEnumerator HandleGameOver()
    {
        Debug.Log("Game Over! The hero has been defeated.");
        deadPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        deadPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    public void StartFirstEncounter()
    {
        if (encounterManager == null)
        {
            Debug.LogError("EncounterManager is not assigned!");
            return;
        }

        Debug.Log("Starting the first encounter...");
        encounterManager.StartEncounter(1); // Start the first encounter (encounter number 1)
    }

    public void StartNextEncounter(int encounterNumber)
    {
        encounterManager.StartEncounter(encounterNumber);
    }
}
