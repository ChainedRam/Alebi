using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour {

    public UnityEvent onTrigger;
    public float time;

	void Start () {
        StartCoroutine(timer());
	}
	
    IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        onTrigger.Invoke();
    }
}
