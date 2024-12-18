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
    [SerializeField]
    public float _speed = 3.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

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
}