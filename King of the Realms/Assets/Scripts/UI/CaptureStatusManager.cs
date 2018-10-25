using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureStatusManager : MonoBehaviour {

    [SerializeField] string prefixText = "Uncaptured";

    public static GameObject capturingPlayer;
    public static string captureStatus = "Uncaptured";
    Text m_text;

    // Use this for initialization
    void Start()
    {
        m_text = GetComponent<Text>();
        m_text.text = captureStatus;
    }

    // Update is called once per frame
    void Update()
    {
        m_text.text = captureStatus;
    }
}
