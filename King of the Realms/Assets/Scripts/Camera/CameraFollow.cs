using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject objectToFollow;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0.0f, 10.0f, -10.0f);

        if (objectToFollow)
        {
            transform.LookAt(objectToFollow.transform);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (objectToFollow)
        {
            transform.position = objectToFollow.transform.position + offset;
            transform.LookAt(objectToFollow.transform);
        }
    }

    public void SetObjectToFollow(GameObject toFollow)
    {
        objectToFollow = toFollow;
    }
}
