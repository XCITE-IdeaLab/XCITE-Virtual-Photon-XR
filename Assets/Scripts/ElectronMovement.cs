using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMovement : MonoBehaviour
{
    ///Variables
    public float speed = 30.0f;
    public bool collisionFlag = false; //Flag used to make sure collisions don't get triggered twice
    //vars to change electron direction
    Vector3 e1Direction = new Vector3(1, 1, 0);
    Vector3 e2Direction = new Vector3(-1, 1, 0);
    Vector3 direction = new Vector3(0, 1, 0);

    public GameObject photon;

    // Start is called before the first frame update
    void Start()
    {
        //check object tag and set appropriate direction
        if(gameObject.tag == "Electron1")
        {
            direction = e1Direction;
        }
        else if (gameObject.tag == "Electron2")
        {
            direction = e2Direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move electron with a set direction and speed
        transform.Translate(direction * Time.deltaTime * speed);
    }

    //Couroutine to reset flags to false some short time after collison
    IEnumerator ResetCollisionFlags(float delay, Collider other)
    {
        yield return new WaitForSeconds(delay);
        collisionFlag = false;
        other.GetComponent<ElectronMovement>().collisionFlag = false;
    }
    void OnTriggerEnter(Collider other)
    {
        //update direction of electron after collision. Switch out their directions to recreate them bouncing off each other. 
        if (gameObject.tag == "Electron1")
        {
            direction = e2Direction;
        }
        else if (gameObject.tag == "Electron2")
        {
            direction = e1Direction;
        }

        //check flags. if all false, then no collision yet
        if (!collisionFlag && !other.GetComponent<ElectronMovement>().collisionFlag)
        {
            //get collision angle
            Vector3 collisionNormal = (transform.position - other.transform.position).normalized;
            Vector3 reflectDirection = Vector3.Reflect(direction, collisionNormal);

            // Create photon at collision point
            GameObject newPhoton = Instantiate(photon, transform.position, Quaternion.identity);
            PhotonMovement photonMovement = newPhoton.GetComponent<PhotonMovement>();

            // Set photon direction and velocity based on collision
            photonMovement.SetDirection(reflectDirection);
            photonMovement.SetVelocity(speed);

            //update flags to true to avoid dobule collision/ creation
            collisionFlag = true;
            other.GetComponent<ElectronMovement>().collisionFlag = true;

            Destroy(other.gameObject);
            Destroy(gameObject);

            //reset flags after delay by using coroutine
            //StartCoroutine(ResetCollisionFlags(0.5f, other));
        }

    }

}
