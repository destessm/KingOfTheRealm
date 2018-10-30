using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody m_Rigidbody;

    public float movementSpeed = 10.0f;

    public GameObject currentRealm;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 18.0f;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInputs();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * movementSpeed, 0.0f, moveVertical * movementSpeed);
        Vector3 rotationVector = new Vector3(moveHorizontal, 0.0f, moveVertical);

        m_Rigidbody.AddForce(movement);

        if(moveHorizontal != 0 || moveVertical != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(rotationVector);
            m_Rigidbody.MoveRotation(rotation);
        }
    }

    private void HandleInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapRealms();
        }
    }

    private void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    private void SwapRealms()
    {
        var realms = GameObject.FindGameObjectsWithTag("Realm");
        Debug.Log(realms);

        foreach(GameObject realm in realms)
        {
            Debug.Log(realm);
            if (!GameObject.ReferenceEquals(realm, currentRealm))
            {
                var spawnLocation = realm.GetComponent<RealmSpawnManager>().GetSpawnLocation();
                currentRealm = realm;
                transform.position = spawnLocation;
                break;
            }
        }
    }
}
