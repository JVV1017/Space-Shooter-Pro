using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;        // Allows to instantiate the enemy object using its prefab
    [SerializeField]
    private GameObject _enemyContainer;     // Allows to containerize all the new instantiated enemy objects into this container

    private bool _stopSpawning = false;     // Used to stop spawning if the player is dead

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine("SpawnRoutine");     // Option 1
        StartCoroutine(SpawnRoutine());           // Option 2
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn game objects every 5 seconds
    // Create a coroutine of type IEnumerator -- Yield Events
    IEnumerator SpawnRoutine()
    {
        // while loop (infinite loop) 
        while (_stopSpawning == false)          // Infinitely loops until the stopSpawning is true
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);                              // Vector3 object variable that stores the position of each new vector 3 enemy object's position (random value for x ranging from -8f to 8f, 7 for y and 0 for z)
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);         // Instantiates enemy objects using the enemyPrefab with the position to spawn and with no rotation required (Quaternion.identity) and its stored inside a GameObject variable 
            newEnemy.transform.parent = _enemyContainer.transform;                                      // Gets the parent of the newEnemy then assigns to new parent which is enemyContainer.transform because the parent is a transform.parent so it has to match with another transform.
            yield return new WaitForSeconds(5.0f);                                                      // Stops the infinite while loop for 5 seconds then creates new enemy object
        }
    }

    // Function to stop spawning
    public void OnPlayerDeath()
    {
        _stopSpawning = true;       // stops spawning true = stops spawning
    }
}
