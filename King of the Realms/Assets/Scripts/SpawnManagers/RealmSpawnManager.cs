using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmSpawnManager : MonoBehaviour {

    public Vector3[] spawnLocations;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetSpawnLocation()
    {
        return spawnLocations[0];
    }
}
