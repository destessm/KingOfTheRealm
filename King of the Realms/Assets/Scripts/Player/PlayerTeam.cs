using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour {

    public bool freeForAll;

    [SerializeField]
    private int mTeam;

    static private int sPlayerCountFFA = 0;

    // Use this for initialization
    void Start()
    {
        if (freeForAll)
        {
            mTeam = sPlayerCountFFA;
            sPlayerCountFFA++;
        }
    }

    public void SetTeam(int t)
    {
        mTeam = t;
    }

    public int GetTeam()
    {
        return mTeam;
    }

	void Update ()
    {	
	}
}
