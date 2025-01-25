using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EncounterManager encounterManager;

    private void Start()
    {
        // Start the first encounter
        encounterManager.StartEncounter(1);
    }
}