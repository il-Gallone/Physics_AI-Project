using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRadRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float rotation;
    float speed;
    void Start()
    {
        rotation = Random.Range(0, 360);
        speed = Random.Range(-10, 10);

        transform.Rotate(Vector3.forward, rotation); // Initial Rotation
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, speed * 10 * Time.deltaTime); // Increasr rotation
    }
}
