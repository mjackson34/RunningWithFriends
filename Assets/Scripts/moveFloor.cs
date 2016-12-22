using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveFloor : MonoBehaviour {

    public List<GameObject> floors;

	// Use this for initialization
	void Start () {
        moveObject(floors[0], floors[1]);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void moveObject(GameObject firstObj, GameObject secondObj)
    {
        secondObj.transform.position = new Vector3(firstObj.transform.position.x, firstObj.transform.position.y, firstObj.GetComponent<Renderer>().bounds.size.z);
        secondObj.transform.Rotate(-(Time.deltaTime * 100), 0f, 0f, Space.World);
        secondObj.transform.position = new Vector3(secondObj.transform.position.x, secondObj.GetComponent<Renderer>().bounds.max.y, secondObj.transform.position.z);
    }
}
