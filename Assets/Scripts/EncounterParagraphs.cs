using UnityEngine;

// ScriptableObject to hold all encounter data
[CreateAssetMenu(fileName = "EncounterParagraphs", menuName = "Game/EncounterParagraphs")]
public class EncounterParagraphs : ScriptableObject
{
    // Player encounter data
    public EncounterData playerFirstEncounter;
    public EncounterData playerSecondEncounter;
    public EncounterData playerLastEncounter;

    // Enemy encounter data
    public EnemyEncounterData enemyFirstEncounter;
    public EnemyEncounterData enemySecondEncounter;
    public EnemyEncounterData enemyLastEncounter;
}

// Group of paragraphs for the player's performance (poor, normal, critical hits)
[System.Serializable]
public class EncounterData
{
    public ParagraphGroup poorHit;
    public ParagraphGroup normalHit;
    public ParagraphGroup criticalHit;
}

// Enemy encounter data structured as numbered paragraphs (1-12)
[System.Serializable]
public class EnemyEncounterData
{
    public string[] numberedParagraphs; // 1 = least correct, 12 = most accurate
}

// Group of paragraphs of a specific type (e.g., poor hit)
[System.Serializable]
public class ParagraphGroup
{
    public string[] paragraphs; // Array of paragraphs for this type
}
