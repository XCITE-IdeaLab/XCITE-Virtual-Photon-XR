using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron1Movement : MonoBehaviour
{

    private float speed = 30.0f;
    Vector3 vector = new Vector3(1, 1, 0);
    Vector3 spawnPos1 = new Vector3(250, 10, 0);

    public GameObject photon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vector * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        vector = new Vector3(-1, 1, 0);
        Debug.Log("E1: Hit");
        Instantiate(photon, spawnPos1, photon.transform.rotation);

 /*       if (other)
        {

        }*/
    }

}
