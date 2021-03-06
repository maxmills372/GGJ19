﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeBallLayer : MonoBehaviour {

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable
	
    public enum TriggerType { Start,Exit,Options, Back };
    public TriggerType trigger_type; 

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.layer = LayerOnEnter;
            StartCoroutine("StartGame");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = LayerOnExit;
        }

    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2.0f);
        switch(trigger_type)
        {
            case TriggerType.Start:
                SceneManager.UnloadSceneAsync(0);
                SceneManager.LoadScene(1);

                break;

            case TriggerType.Options:
                SceneManager.UnloadSceneAsync(0);

                SceneManager.LoadScene(2);

                break;
            case TriggerType.Exit:
                Application.Quit();
                break;
            case TriggerType.Back:
                SceneManager.UnloadSceneAsync(2);

                SceneManager.LoadScene(0);
                break;
            default: break;
        }

        yield return null;
    }
}