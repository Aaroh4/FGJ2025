using UnityEngine;

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

    public void DealDamageToEnemy(int damage, int enemyNumber)
    {
        if (enemyNumber == 1)
        {
            Enemy1HP -= damage;
            Debug.Log($"Enemy 1 took {damage} damage! Current HP: {Enemy1HP}");
            if (Enemy1HP <= 0)
            {
                Debug.Log("Enemy 1 has been defeated!");
                encounterManager.StartEncounter(2); 
            }
        }
        else if (enemyNumber == 2)
        {
            Enemy2HP -= damage;
            Debug.Log($"Enemy 2 took {damage} damage! Current HP: {Enemy2HP}");

            if (Enemy2HP <= 0)
            {
                Debug.Log("Enemy 2 has been defeated!");
                encounterManager.StartEncounter(3);
            }
        }
        else if (enemyNumber == 3)
        {
            Enemy3HP -= damage;
            Debug.Log($"Enemy 3 took {damage} damage! Current HP: {Enemy3HP}");

            if (Enemy3HP <= 0)
            {
                Debug.Log("Enemy 3 has been defeated!");
                
            }
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
