using UnityEngine;

[CreateAssetMenu(fileName = "EncounterParagraphs", menuName = "Game/Encounter Paragraphs")]
public class EncounterParagraphs : ScriptableObject
{
    // Player's paragraphs for each encounter
    public EncounterData playerFirstEncounter;
    public EncounterData playerSecondEncounter;
    public EncounterData playerLastEncounter;

    // Enemy's paragraphs for each encounter
    public EncounterData enemyFirstEncounter;
    public EncounterData enemySecondEncounter;
    public EncounterData enemyLastEncounter;
}

[System.Serializable]
public class EncounterData
{
    public ParagraphGroup poorHit;      // Weak attack paragraphs
    public ParagraphGroup normalHit;    // Moderate attack paragraphs
    public ParagraphGroup criticalHit;  // Strong attack paragraphs
}

[System.Serializable]
public class ParagraphGroup
{
    public string[] paragraphs; // Array of paragraphs for each hit type (Poor, Normal, Critical)
}
