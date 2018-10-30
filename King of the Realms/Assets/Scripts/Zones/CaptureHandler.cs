using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureHandler : MonoBehaviour {

    [SerializeField] float timeToCapture = 10.0f;

    private float m_captureProgress = 0.0f;
    private float m_playerCount = 0;
    static float m_timeCounter = 0.0f;
    private bool m_contested = false;
    private bool m_captured = false;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        m_playerCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        m_playerCount--;
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
        if (m_captureProgress < 100.0f)
        {
            m_captureProgress += (0.2f * Time.deltaTime) * 100.0f / timeToCapture;
            m_timeCounter += 0.5f * Time.deltaTime;
            CaptureStatusManager.captureStatus = "Capturing..." + m_captureProgress.ToString("F0") + "%";
        }
        else if (m_captureProgress >= 100.0f)
        {
            m_captured = true;
            CaptureStatusManager.captureStatus = "Zone Captured";
            ScoreManager.incrementScore = true;
        }
    }

    private void FixedUpdate()
    {
        if (m_playerCount == 0 && !m_contested && !m_captured && m_captureProgress >= 0)
        {
            m_captureProgress -= (0.2f * Time.deltaTime) * 100.0f / timeToCapture;
            m_timeCounter += 0.5f * Time.deltaTime;
            CaptureStatusManager.captureStatus = "Capturing..." + m_captureProgress.ToString("F0") + "%";
        }
    }
}
