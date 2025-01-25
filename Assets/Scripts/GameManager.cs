using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EncounterManager encounterManager; // Reference to the EncounterManager
    public int heroMaxHP = 15;                // Maximum HP for the hero
    private int heroCurrentHP;

    void Start()
    {
        ResetHeroHP(); 
        StartFirstEncounter();
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

    private void HandleGameOver()
    {
        Debug.Log("Game Over! The hero has been defeated.");
        // Additional game over logic can be added here, such as restarting the game or showing a Game Over screen
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
