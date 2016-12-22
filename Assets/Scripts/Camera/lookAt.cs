using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lookAt : MonoBehaviour {

    public GameObject lookPoint; //LookPoint Object
    //public float runSpeed = 1000f;


    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        Vector3 relativePos = lookPoint.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime);
        //Vector3 runDirection = transform.position - lookPoint.transform.position;
        //gameObject.GetComponent<CharacterController>().SimpleMove(runDirection * runSpeed * Time.deltaTime);
        //transform.LookAt(lookPoint.transform.position);
    }

}
