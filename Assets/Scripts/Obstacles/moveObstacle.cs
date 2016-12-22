using UnityEngine;
using System.Collections;

public class moveObstacle : MonoBehaviour {

    public float speed = 2f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(gameObject.GetComponent<CharacterJoint>());
        if(gameObject.GetComponent<CharacterJoint>() == null)
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

	}
}
