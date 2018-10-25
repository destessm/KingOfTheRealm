using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureHandler : MonoBehaviour {

    [SerializeField] float timeToCapture = 10.0f;

    private float captureProgress = 0.0f;

    static float timeCounter = 0.0f;


    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(captureProgress < 100.0f)
        {
            captureProgress += (0.2f * Time.deltaTime) * 100.0f / timeToCapture;
            timeCounter += 0.5f * Time.deltaTime;
            CaptureStatusManager.captureStatus = "Capturing..." + captureProgress.ToString("F0") + "%";
        }
        else if(captureProgress >= 100.0f)
        {
            CaptureStatusManager.captureStatus = "Zone Captured";
            ScoreManager.incrementScore = true;
        }
    }
}
