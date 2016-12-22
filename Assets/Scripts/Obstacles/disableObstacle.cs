using UnityEngine;
using System.Collections;

public class disableObstacle : MonoBehaviour {

    public GameObject disableObjectPlane;

    // Use this for initialization
    void Start () {
        StartCoroutine(wait());
    }
	
	// Update is called once per frame
	void Update () {
	    if(this.transform.position.y < -25)
        {
            disableObstacles(this.gameObject);

        }
        //Debug.Log(disableObjectPlane.transform.position.x - this.transform.position.x);
        /*
        if(disableObjectPlane.transform.position.x - this.transform.position.x < -50)
        {
            disableObstacles(this.gameObject);
        }
        */
	}

    //disable objects
    public void disableObstacles(GameObject obstacle)
    {
        obstacle.SetActive(false);
        gameObject.GetComponentInParent<gameManager>().decrementCurrentActiveObstacles();
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.name);
        if(collider.name == disableObjectPlane.name)
        {
            disableObstacles(gameObject);
            //gameObject.GetComponentInParent<gameManager>().decrementCurrentActiveObstacles();
        }
    }

    IEnumerator wait()
    {
        while (disableObjectPlane == null)
        {
            disableObjectPlane = gameObject.GetComponentInParent<gameManager>().getDisableObjectPlane();
            yield return new WaitForSeconds(1.0f);
        }

    }
}
