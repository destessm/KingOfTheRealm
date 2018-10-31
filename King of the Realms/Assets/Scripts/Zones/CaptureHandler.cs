using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureHandler : MonoBehaviour {

    // Nested class
    private class UserInfo
    {
        private int mTeamID;
        private int mPlayerCountInZone;

        public UserInfo()
        {
            mTeamID = -1;
            mPlayerCountInZone = 0;
        }

        public UserInfo(int tID)
        {
            mTeamID = tID;
            mPlayerCountInZone = 1; // We initialize when we add one, so skip that step
        }

        public UserInfo(int tID, int pCnt)
        {
            mTeamID = tID;
            mPlayerCountInZone = pCnt;
        }

        public int GetTeamID() { return mTeamID; }
        public void SetTeamID(int tID) { mTeamID = tID; }
        public int GetPlayerCountInZone() { return mPlayerCountInZone; }
        public void IncrementPlayerCount() { mPlayerCountInZone++; }
        public void DecrementPlayerCount()
        {
            mPlayerCountInZone--;
            if (mPlayerCountInZone < 0)
            {
                mPlayerCountInZone = 0;
            }
        }
    };

    // Fields
    [SerializeField]
    private float timeToCapture = 10.0f;

    [SerializeField]
    private float mCaptureProgress = 0.0f;

    private bool mCaptured = false;
    private List<UserInfo> mListOfUsers;
    private int mZoneTeam = -1;

    static float mTimeCounter = 0.0f;

    void Start()
    {
        mListOfUsers = new List<UserInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int playerTeam = other.GetComponent<PlayerTeam>().GetTeam();
            // check if this user's team exists in the list of Users
            bool foundInList = false;
            for (int i = 0; i < mListOfUsers.Count; i++)
            {
                if (playerTeam == mListOfUsers[i].GetTeamID())
                {
                    mListOfUsers[i].IncrementPlayerCount();
                    foundInList = true;
                }
            }
            // if not found, add a new entry
            if(!foundInList)
            {
                mListOfUsers.Add(new UserInfo(playerTeam));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            int teamID = other.GetComponent<PlayerTeam>().GetTeam();

            for (int i = 0; i < mListOfUsers.Count; i++)
            {
                if (teamID == mListOfUsers[i].GetTeamID())
                {
                    mListOfUsers[i].DecrementPlayerCount();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            HandleCapture(other.gameObject);
        }
    }


    private void HandleCapture(GameObject capturer)
    {
        if (mCaptureProgress < 100.0f && !IsZoneContested())
        {
            mCaptureProgress += (0.2f * Time.deltaTime) * 100.0f / timeToCapture;
            mTimeCounter += 0.5f * Time.deltaTime;
            CaptureStatusManager.captureStatus = "Capturing..." + mCaptureProgress.ToString("F0") + "%";
        }
        else if (mCaptureProgress >= 100.0f)
        {
            mCaptured = true;
            mZoneTeam = capturer.GetComponent<PlayerTeam>().GetTeam();
            CaptureStatusManager.captureStatus = "Zone Captured";
            ScoreManager.incrementScore = true; // Needs to be modified to count up for the correct team
        }
    }

    private void FixedUpdate()
    {
        if (GetNumTeamsInZone() == 0 && !mCaptured && mCaptureProgress >= 0)
        {
            mCaptureProgress -= (0.2f * Time.deltaTime) * 100.0f / timeToCapture;
            mTimeCounter += 0.5f * Time.deltaTime;
            CaptureStatusManager.captureStatus = "Capturing..." + mCaptureProgress.ToString("F0") + "%";
        }
    }

    private bool IsZoneContested()
    {
        return (GetNumTeamsInZone() > 1);
    }

    private int GetNumTeamsInZone()
    {
        int numTeamsInZone = 0;
        for (int i = 0; i < mListOfUsers.Count; i++)
        {
            if (mListOfUsers[i].GetPlayerCountInZone() > 0)
            {
                numTeamsInZone++;
            }
        }
        return numTeamsInZone;

    }
}