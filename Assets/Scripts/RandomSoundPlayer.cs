using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component to play sounds
    public AudioClip[] soundClips;  // Array of sound clips to play
    public float minInterval = 5f;  // Minimum time between sounds
    public float maxInterval = 15f; // Maximum time between sounds

    private void Start()
    {
        // Start the coroutine to play sounds randomly
        StartCoroutine(PlayRandomSounds());
    }

    private IEnumerator PlayRandomSounds()
    {
        while (true) // Infinite loop to keep playing sounds
        {
            // Wait for a random time between minInterval and maxInterval
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Select a random sound clip from the array
            if (soundClips.Length > 0)
            {
                AudioClip clip = soundClips[Random.Range(0, soundClips.Length)];
                audioSource.PlayOneShot(clip); // Play the selected sound clip
            }
        }
    }
}
