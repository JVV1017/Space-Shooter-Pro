using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;        // Variable to store the speed for enemy movement
    [SerializeField]
    private GameObject _enemyPrefab;    // Variable to be used to instantiate the enemy prefab object
    private Player _player;             // Variable to access the components of player
    private Animator _anim;             // Variable to access animator component
    private AudioSource _audioSource;   // Variable to play the explosion sound from the audio source

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets the player's components
        _player = GameObject.Find("Player").GetComponent<Player>();
        
        // Gets the animator's components
        _anim = GetComponent<Animator>();
        
        // Gets the audio source's components
        _audioSource = GetComponent<AudioSource>();

        // Null Checks Player
        if (_player == null)
            Debug.Log("The Player is NULL.");

        // Null Checks animator
        if (_anim == null)
            Debug.LogError("The animator is NULL.");

        // Null Checks the audio source
        if (_audioSource == null)
            Debug.LogError("The Audio Source on the Enemy is NULL.");
    }

    // Update is called once per frame
    void Update()
    {
        // Move down at 4 meters per second 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // If bottom of screen
        // respawn at top with a new random x position (research to figure how to define a random position along the x values)
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    // Note:
    // OnTriggerEnter = 3D collider trigger function with a Collider parameter which is for 3D
    // However OnTriggerEnter2D = 2D collider trigger function with a Collider2D parameter which is for 2D

    // Collider trigger when the enemy collides with the player (damages it and dies) or laser (dies)
    private void OnTriggerEnter2D(Collider2D other)     // When the enemy has collied with another object, its stores as a collider object called other by default (using other, we can identify what collides with our enemy object) 
    {
        //Debug.Log("Hit: " + other.transform.name);

        // Enemy collides with Player
        // Damage the player (3 lives in total and the player loses a life if damaged)

        if (other.tag == "Player")      // If the enemy object collides with an object that is tagged player then
        {
            // Important: Only component in unity that you have direct access to is the transform of an object so we can't do other.player

            // other.transform.GetComponent<MeshRenderer>().material.color();   // Example to get information of the meshrender's material color

            // Damage the player (Method to avoid errors)
            Player player = other.transform.GetComponent<Player>();     // This is used to access the information from the player object and in this case, I want to access the Damage function found in the Player object and add it in a player variable object
            
            if (player != null)         // Checks if the player object really do exist then
                player.Damage();        // Uses the damage function found in the Player script 
            
            _anim.SetTrigger("OnEnemyDeath");     // Trigger the anim
            _speed = 0;                           // When hit by player, it stops so speed is nil
            _audioSource.Play();                  // Plays the explosion sound when enemy collides with the player
            Destroy(this.gameObject, 2.8f);       // the enemy object gets destroyed
        }

        // Enemy collides with Laser
        if (other.tag == "Laser")           // If the enemy object collides with an object that is tagged laser then
        {
            Destroy(other.gameObject);      // The laser object gets destroyed and 
            
            if (_player != null)             // Checks if the player object really do exist then
                _player.AddScore(10);         // Uses the score text function found in the Player script (adds 10 pts to score)

            _anim.SetTrigger("OnEnemyDeath");     // Trigger the anim
            _speed = 0;                           // When hit by laser, it stops so speed is nil
            _audioSource.Play();                  // Plays the explosion sound when enemy collides with the laser
            Destroy(this.gameObject, 2.8f);       // The enemy object gets destroyed as well
        }
    }
}
