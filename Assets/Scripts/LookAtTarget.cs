using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

    public Transform targetPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 relativePos = targetPoint.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime / 2);

    }
}
