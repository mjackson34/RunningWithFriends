using UnityEngine;
using System.Collections;

public class ControllerEnabled : MonoBehaviour {

    public bool controllerEnabled = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isControllerEnabled() { return controllerEnabled; }
}
