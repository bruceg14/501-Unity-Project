using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {
    public GameObject Countdown;

	// Use this for initialization
	void Start () {
        StartCoroutine("StartDelay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float pauzeTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauzeTime)
            yield return 0;
        Countdown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
