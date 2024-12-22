using UnityEngine;

public class Player : MonoBehaviour
{

    // Variables
    // 1. public or private references (public means every thing knows about it while the opposite for private)
    // 2. Data Types: Int (whole numbers), float (decimal numbers), bool (T/F), string (Characters)
    // 3. Every Variable has a name so its always unique (its all just declaration)
    // Optional 4. A value that is assigned to the variable (initialization)
    // For a private variable use _variable_name with underscore at the beginning to let you know that it is a private variable
    // Using attributes above a variable declaration or initialization like [SerializeField] which allows the ability for the inspector to read the variable and overwrite it

    // Most of the variables used throughout the course are going to be private, variables are public if it is required.

    // Example
    [SerializeField]                        // This allows a private variable to be viewed in the inspector
    private float _speed = 3.5f;            // Variable to store the speed for player movement
    [SerializeField]
    private GameObject _laserPrefab;        // Variable to be used to instantiate the laser prefab object
    [SerializeField]
    private float _fireRate = 0.15f;        // Variable to control the firerate (to built a cooldown system in this case)
    private float _canFire = -1f;           // Variable to determine if the player can fire
    [SerializeField]
    private int _lives = 3;                 // Variable to store the player's lives value
    private SpawnManager _spawnManager;     // Variable to access the spawnManager and communicate


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();   // Finds the SpawnManager and gets its component (successfully get access to the SpawnManager Script file)
        
        // Nullcheck (if spawnmanager is null then good to see log error message to realize the game is not ready to be deployed)
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();    // Calls the player movement function

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    // Function for the player object's movement like up, down, left, right 
    void CalculateMovement(){
        // Float variable accessing axis Horizontal
        float horizontalInput = Input.GetAxis("Horizontal"); // right = 1 and left = -1

        // Float variable accessing axis Vertical
        float verticalInput = Input.GetAxis("Vertical"); // up = 1 and down = -1

        // 1 unit in unity is equal to 1 meter or in this case, 1 meter per frame or (1 meter x 60 = 60 meters)
        // Time.deltaTime or 1 second or using real-time or time it took to achieve in seconds from previous frame to the next frame

        //transform.Translate(new Vector3(1,1,0) * Time.deltaTime);       // 1 meter per second to diagonally top right.

        //new Vector(1,0,0) is the equavalent to Vector3.right

        // Implementation #1 (Least Optimized cuz creating 2 Vector3 objects instead of 1 in #2):
        // Horizontal Input Code (Going to right or 1, 0, 0) * 0 * 3.5f * real-time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

        // Challenge: Vertical movement UP and DOWN
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); 

        // Implementation #2 (Optimized cuz of 1 line code and vector object for both input's compared to 2 found in #1): 
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        // Implementation #3 (More cleaner, merging of #1 and #2)
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // if-case (User Input Bounds)
        
        // Vertical Bounds
        // Implementation #1:
        if (transform.position.y >= 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f ) {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        // Implementation #2:
        // Mathf.clamp(transform.position.y, -3.8f, 0) = y's min is -3.8f and max is 0
        //transform.position = new Vector3(transform.position.x, Mathf.clamp(transform.position.y, -3.8f, 0), 0);

        // Challenge: Do the horizontal bounds
        if (transform.position.x > 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    // Function to handle the reassignation of the canFire variable and instantiation so no need of the if statement
    void FireLaser()        // if no private or public is mentioned, its private by default
    {
        // To debug (see if space key works properly)
        //Debug.Log("Space Key Pressed");

        _canFire = Time.time + _fireRate;       // Updates the canFire variable to the current time (Time.time) plus the fire rate which sets the next time, the player can fire

        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);   // Spawns or instantiates a laser object using laserprefab object that spawns 0.8 units above the player with no rotation
    }

    public void Damage()        // Its going to be a public function because the damage/lives part needs to be viewed/modified by the enemy class
    {
        _lives--;       // Same thing as lives -= 1 or lives = lives - 1

        if (_lives < 1)         // If the lives is less than 1 (meaning 0)
        {
            _spawnManager.OnPlayerDeath();      // Communicates with the SpawnManager and uses its OnPlayerDeath function where no lives, spawning stops
            Destroy(this.gameObject);   // Then the player dies meaning the player object is destroyed
        }
    }
}