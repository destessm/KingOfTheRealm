using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody m_Rigidbody;

    [SerializeField] float movementSpeed = 10.0f;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * movementSpeed, 0.0f, moveVertical * movementSpeed);
        Vector3 rotationVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Quaternion rotation = Quaternion.LookRotation(rotationVector);

        m_Rigidbody.AddForce(movement);
        m_Rigidbody.MoveRotation(rotation);
    }
}
