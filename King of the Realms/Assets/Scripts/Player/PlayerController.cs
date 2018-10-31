using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
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

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        Camera.main.GetComponent<CameraFollow>().SetObjectToFollow(gameObject);
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        HandleInputs();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
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
            CmdFire();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapRealms();
        }
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Spawn the bullet on all Clients
        NetworkServer.Spawn(bullet);

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
