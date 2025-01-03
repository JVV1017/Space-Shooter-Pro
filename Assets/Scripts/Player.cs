using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.SceneManagement;

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
    private float _speedMultiplier = 2;     // Variable used to increase player speed when speed boost is collected
    [SerializeField]
    private GameObject _laserPrefab;        // Variable to be used to instantiate the laser prefab object
    [SerializeField]
    private GameObject _tripleShot_Prefab;      // Variable used to instantiate the triple shot prefab object
    [SerializeField]
    private GameObject _explosionPrefab;    // Variable used to instantiate the explosion through its prefab
    private SpawnManager _spawnManager;     // Variable to access the spawnManager and communicate with
    private UIManager _uiManager;           // Variable to access the uiManager and communicate with
    private GameManager _gameManager;       // Variable to access the gameManager and communicate with
    [SerializeField]
    private float _fireRate = 0.15f;        // Variable to control the firerate (to built a cooldown system in this case)
    private float _canFire = -1f;           // Variable to determine if the player can fire
    [SerializeField]
    private int _lives = 3;                     // Variable to store the player's lives value
    private bool _isTripleShotActive = false;       // Variable to store the triple shot status
    //private bool _isSpeedBoostActive = false;       // Variable to store the speed boost status
    private bool _isShieldActive = false;           // Variable to store the shield status
    [SerializeField]
    private GameObject _shieldVisualizer;           // Variable to access the shield around the player (visualizer)
    [SerializeField]
    private int _score;                         // Variable used to store the player's score amount
    [SerializeField]
    private int _bestScore;                     // Variable used to store the player's best score amount
    [SerializeField]
    private GameObject _leftEngine, _right_Engine;           // Variables to enable Left and Right Engine damage object 
    // Variable to store the audio clip
    [SerializeField]
    private AudioClip _laserSoundClip;                  // AudioClip = To store the laser audio clip
    private AudioSource _audioSource;                   // AudioSource = To point out the audio source to play
    public bool _isPlayerOne = false;               // Used to differnntiate player 1
    public bool _isPlayerTwo = false;               // Used to differentiate player 2

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();      // Finds the SpawnManager and gets its component (successfully get access to the SpawnManager Script file)
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();                   // Finds the uiManager and gets its component 
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();         // Finds the GameManager and gets its component 
        _audioSource = GetComponent<AudioSource>();                                         // Gets the components of the audio source

        // If the game is not in co-op mode, make the player start from -3 in y and center otherwise, in co-op mode, where they were placed in the inspector
        if (_gameManager._isCoOpMode == false)
            // Take the current position = new position (0, -3, 0)
            transform.position = new Vector3(0, -3, 0);

        // Nullcheck (if spawnmanager is null then good to see log error message to realize the game is not ready to be deployed)
        if (_spawnManager == null)
            Debug.LogError("The Spawn Manager is NULL.");

        // Nullcheck the UiManager
        if (_uiManager == null)
            Debug.LogError("The UI Manager is NULL.");

        // Nullcheck the Game Manager
        if (_gameManager == null)
            Debug.LogError("The Game Manager is NULL.");

        // Nullcheck if audioSource is null or not and if its not null then its going to set the audioSource clip as the laserSoundClip when we play the game
        if (_audioSource == null)
            Debug.LogError("The Audio Source on the Player is NULL.");
        else
            _audioSource.clip = _laserSoundClip;

        GetBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        // If player 1 
        if (_isPlayerOne == true)
        {
            PlayerOneMovement();    // Calls the player movement function

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
                FireLaser();
        }

        // If player 2
        if (_isPlayerTwo == true)
        {
            PlayerTwoMovement();

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
                FireLaser();
        }
    }

    // Function for the player one object's movement like up, down, left and right
    void PlayerOneMovement()
    {
        // Float variable accessing axis Horizontal (removed left and right from input manager so uses a & d)
        float horizontalInput = Input.GetAxis("Horizontal"); // right = 1 and left = -1

        // Float variable accessing axis Vertical (removed up and down from input manager so uses w & s)
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

        //if (_isSpeedBoostActive == false)
        //    transform.Translate(direction * _speed * Time.deltaTime);
        //else
        //    transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);

        // if-case (User Input Bounds)

        // Vertical Bounds
        // Implementation #1:
        if (transform.position.y >= 0) 
            transform.position = new Vector3(transform.position.x, 0, 0);
        
        else if (transform.position.y <= -3.8f ) 
            transform.position = new Vector3(transform.position.x, -3.8f, 0);


        // Implementation #2:
        // Mathf.clamp(transform.position.y, -3.8f, 0) = y's min is -3.8f and max is 0
        //transform.position = new Vector3(transform.position.x, Mathf.clamp(transform.position.y, -3.8f, 0), 0);

        // Horizontal bounds
        if (transform.position.x > 11.3f) 
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        
        else if (transform.position.x < -11.3f) 
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        
    }

    // Function for the player two object's movement like up, down, left and right 
    void PlayerTwoMovement()
    {
        // Up
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Down
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Left
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * _speed * Time.deltaTime);

        // Right
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

        // Vertical Bounds
        if (transform.position.y >= 0)
            transform.position = new Vector3(transform.position.x, 0, 0);

        else if (transform.position.y <= -3.8f)
            transform.position = new Vector3(transform.position.x, -3.8f, 0);

        // Horizontal bounds
        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        else if (transform.position.x < -11.3f)
            transform.position = new Vector3(11.3f, transform.position.y, 0);

    }

    // Firing laser function
    void FireLaser()        // if no private or public is mentioned, its private by default
    {
        // To debug (see if space key works properly)
        //Debug.Log("Space Key Pressed");

        _canFire = Time.time + _fireRate;       // Updates the canFire variable to the current time (Time.time) plus the fire rate which sets the next time, the player can fire

        // If the tripleshot is active then it creates a tripleshot object
        if (_isTripleShotActive == true)
            Instantiate(_tripleShot_Prefab, transform.position, Quaternion.identity);       // Creates triple shot object

        else    // If it isn't active then it creates a laser object
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);   // Spawns or instantiates a laser object using laserprefab object that spawns 1.05 units above the player with no rotation

        // Play the laser audio clip
        _audioSource.Play();
    }

    // Player damage function containing player live functionality
    public void Damage()        // Its going to be a public function because the damage/lives part needs to be viewed/modified by the enemy class
    {
        // if shield is on, then
        if (_isShieldActive == true)
        {
            _isShieldActive = false;                    // if hit, turns it off and 
            _shieldVisualizer.SetActive(false);         // disables the visualizer
            return;                                     // Done/stop
        }
        
        _lives--;       // Same thing as lives -= 1 or lives = lives - 1

        // If lives is 2, enable right engine
        // else if lives is 1, enable left engine
        if (_lives == 2)
            _leftEngine.SetActive(true);
        else if (_lives == 1)
           _right_Engine.SetActive(true);

        _uiManager.UpdateLives(_lives);     // If lives is removed by 1 or more, it gets updated by calling the UpdateLives funciton in uiManager

        if (_lives < 1)         // If the lives is less than 1 (meaning 0)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);     // Creates the explosion when the player dies
            _spawnManager.OnPlayerDeath();      // Communicates with the SpawnManager and uses its OnPlayerDeath function where no lives, spawning stops
            CheckForBestScore();
            Destroy(this.gameObject);   // Then the player dies meaning the player object is destroyed
        }
    }

    // Function to turn on Triple Shot and to start a powerdown of 5 seconds when the function its called
    public void TripleShotActive()
    {
        // Enable triple shot
        _isTripleShotActive = true;
        
        // Start the power down coroutine for triple shot
        StartCoroutine(TripleShotPowerDownRoutine());           // StartCoroutine is to start a IEnumerator function/coroutine
    }

    // Power Down Coroutine Triple Shot
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);      // Yield/wait first then return new after waiting for 5 seconds
        _isTripleShotActive = false;                // Then disable the triple shot powerup 
    }

    // Function to turn on Speed Boost and to start a powerdown of 5 seconds when the function its called 
    public void SpeedBoostActive()
    {
        // Enable speed boost
        //_isSpeedBoostActive = true;
        
        // Multiply speed with speed multiplier variable (5 * 2 = 10 units)
        _speed *= _speedMultiplier;

        // Start the power down coroutine for speed boost
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    // Power Down Coroutine Speed Boost
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);      // Yield/wait first then return new after waiting for 5 seconds
        //_isSpeedBoostActive = false;                // Then disable the speed boost powerup 

        // Divide speed with speed multiplier variable (10 / 2 = 5 units)
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        // Enable shield
        _isShieldActive = true;

        // enable the visualizer
        _shieldVisualizer.SetActive(true);
    }

    // Function to add 10 to the score!
    // Communicate with the UI to update the score!
    public void AddScore(int points)
    {
        // Hard coded method to add 10 points per enemy kill
        //_score += 10;

        // Less hardcoded method where the enemy class decided the points for the kill by the player (good if randomized points or enemy type point system)
        _score += points;

        // Communicates the score amount of the player with the uiManager
        _uiManager.UpdateScore(_score);
    }

    // Function to check if the best score is lower than the score and if it is, updates the best score to the score when game is done and also saves the best score
    public void CheckForBestScore()
    {
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("HighScore", _bestScore);    // PlayerPrefs has set functions to save stuff like in this case, using SetInt, you can save the best score by using a specific key
            _uiManager.UpdateBestScore(_bestScore);
        }    
    }

    // Function to get the high score if you left the game and opened it again or restarted the game by pressing R
    public void GetBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("HighScore", 0);    // PlayerPrefs also has get functions to get the saved stuff like in this case, using GetInt, you can load the best score by using the specific key from your SetInt and then a default value if it can't find the key
        _uiManager.UpdateBestScore(_bestScore);
    }
}