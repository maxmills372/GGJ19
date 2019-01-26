using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score;
    int score_multiplier;
    public int points = 50;
    bool multiplier_enabled;
    float count;

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
    }

    public void Update_Score()
    {
        score = score + (points * score_multiplier);
	}

    public void Enable_Multiplier()
    {
        score_multiplier += 1;
        multiplier_enabled = true;
    }
    
    void Disable_Multiplier()
    {
        score_multiplier = 1;
        multiplier_enabled = true;
    }
}
