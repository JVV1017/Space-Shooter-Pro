using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;          // Variable for the rotation speed
    [SerializeField]
    private GameObject _explosionPrefab;        // Variable used to instantiate the explosion through its prefab
    private SpawnManager _spawnManager;         // Variable used to access the spawn manager in the Start Function

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Gets the components of Spawn Manager
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        // Null checks Spawn Manager
        if (_spawnManager == null)
            Debug.LogError("The Spawn Manager is NULL.");
    }

    // Update is called once per frame
    void Update()
    {
        // rotate object on the z axis by 3 meters if fast, choose best for u
        // with a given set speed 
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
    // Collider trigger when the asteroid collides with the laser (both destroyed and explosion created)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);     // Creates an explosion animation through the prefab

            Destroy(other.gameObject);              // Destroys laser

            _spawnManager.StartSpawning();          // Before destroying asteroid, we start spawning the enemies and powerups by calling the StartSpawning public function from SpawnManager

            Destroy(this.gameObject, 0.25f);        // Destroys the asteroid after .25 seconds
        }
    }
}
