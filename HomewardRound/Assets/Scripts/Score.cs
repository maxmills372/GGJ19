using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    int score_multiplier;
    public int points = 50;
    bool multiplier_enabled;
    float count;
    public Text score_text;

	// Use this for initialization
	void Start ()
    {
        score = 0;
        score_multiplier = 1;
        count = 0.0f;
        multiplier_enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (multiplier_enabled == true)
        {
            count += Time.deltaTime;
            if (count >= 5.0f)
            {
                Disable_Multiplier();
                count = 0.0f;
            }
        }
        score_text.text = "" + score;
    }

    public void Update_Score()
    {
        score = score + (points * score_multiplier);
	}

    public void Enable_Multiplier()
    {
        score_multiplier += 1;
        multiplier_enabled = true;
        score_text.color = Color.yellow;
    }
    
    void Disable_Multiplier()
    {
        score_multiplier = 1;
        multiplier_enabled = false;
        score_text.color = Color.black;
    }
}
