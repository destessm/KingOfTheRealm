using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField] string prefixText = "Current score: ";
    [SerializeField] float scoreIncrementTimer = 2.0f;

    public static int score = 0;
    public static bool incrementScore = false;
    private float updatePeriod = 0.0f;
    Text m_text;

	// Use this for initialization
	void Start () {
        m_text = GetComponent<Text>();
        m_text.text = prefixText + score;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (incrementScore)
        {
            if (updatePeriod >= scoreIncrementTimer)
            {
                score++;
                updatePeriod = 0;
            }
            updatePeriod += Time.deltaTime;
        }

        m_text.text = prefixText + score;
    }
}
