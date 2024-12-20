using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Speed Variable of 8
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        // Translate the laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Check if the laser position is greater than 8 on the y 
        if (transform.position.y > 8f)
        {
            Destroy(this.gameObject);           // Destroys the laserobject by specifying this script's gameobject
            // Destroy(this.gameObject, 5f);   // Alternatively, you can use 5f with Destroy to destroy the object after 5 seconds from when the object was instantiated
        }
    }
}
