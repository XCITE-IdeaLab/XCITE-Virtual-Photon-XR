using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonMovement : MonoBehaviour
{
    private Vector3 direction;
    private float velocity;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void SetVelocity(float newVelocity)
    {
        velocity = newVelocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * velocity);
    }
}
