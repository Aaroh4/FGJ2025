using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public EncounterParagraphs encounterData; // Assign the ScriptableObject in the Inspector

    private EncounterData playerData;
    private EncounterData enemyData;

    void StartEncounter(int encounterNumber)
    {
        // Select the current encounter data for the player and the enemy
        playerData = encounterNumber switch
        {
            1 => encounterData.playerFirstEncounter,
            2 => encounterData.playerSecondEncounter,
            3 => encounterData.playerLastEncounter,
            _ => null
        };

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

        // Run 6 rounds of encounter
        for (int round = 1; round <= 6; round++)
        {
            string[] playerOptions = SelectPlayerParagraphsForRound();
            Debug.Log($"Round {round} - Player Options: {string.Join(", ", playerOptions)}");

            // Simulate player's performance (replace with actual game logic)
            float playerDamage = Random.Range(1f, 10f);  // Example: random damage
            string enemyResponse = SelectEnemyResponse(playerDamage);
            Debug.Log($"Enemy Response: {enemyResponse}");
        }
    }

    string[] SelectPlayerParagraphsForRound()
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

    string SelectEnemyResponse(float playerDamage)
    {
        // Determine hit type based on player's damage
        ParagraphGroup hitType = playerDamage switch
        {
            <= 3f => enemyData.poorHit,       // Poor Hit
            > 3f and <= 7f => enemyData.normalHit, // Normal Hit
            > 7f => enemyData.criticalHit,   // Critical Hit
            _ => null
        };

        if (hitType == null || hitType.paragraphs.Length == 0)
        {
            return "Enemy says nothing."; // Fallback
        }

        return SelectRandomParagraph(hitType.paragraphs);
    }

    string SelectRandomParagraph(string[] paragraphs)
    {
        return paragraphs.Length > 0 ? paragraphs[Random.Range(0, paragraphs.Length)] : "No Paragraph";
    }

    string[] SelectMultipleRandom(string[] paragraphs, int count)
    {
        if (paragraphs.Length < count) return paragraphs; // Edge case: fewer options than requested

        System.Collections.Generic.List<string> selected = new System.Collections.Generic.List<string>();
        System.Collections.Generic.List<string> remaining = new System.Collections.Generic.List<string>(paragraphs);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, remaining.Count);
            selected.Add(remaining[index]);
            remaining.RemoveAt(index);
        }

        return selected.ToArray();
    }

    string[] ShuffleArray(string[] array)
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
