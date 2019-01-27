using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeBallLayer : MonoBehaviour {

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable
	
    public enum TriggerType { Start,Exit,Options};
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
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

                break;

            case TriggerType.Options:
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

                break;
            case TriggerType.Exit:
                Application.Quit();
                break;
            default: break;
        }

        yield return null;
    }
}