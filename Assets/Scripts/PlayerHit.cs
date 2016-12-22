using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {

    public float duration = 0.25f;
    public float force = 50;
    public Rigidbody playerRigidBody;

    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = gameObject.GetComponent<CharacterController>();
        playerRigidBody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void impact(Vector3 direction, float force)
    {
        //get the start time for the impact
        /*
        float startTime = Time.time;
        while(Time.time < (startTime + duration))
        {
            controller.SimpleMove(direction * force);
        }
        */
        Debug.Log("Hit backwards!");
        playerRigidBody.AddForce(direction, ForceMode.Impulse);

    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.name == "pickupObject" || collider.name == "pickupObjectBlue" || collider.name == "pickupObjectRed"
            || collider.name == "multiPickUpObject" || collider.name == "multiPickUpObjectChildOne" || collider.name == "multiPickUpObjectChildTwo"
            || collider.name == "small_rock_1" || collider.name == "stick_asset")
        {
            Debug.Log("Hit by: " + collider.name);
            impact(Vector3.back, force);
        }
    }
    /*
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("HIT!" + hit.transform.name);
        if (hit.collider.name == "pickupObject" || hit.collider.name == "pickupObjectBlue" || hit.collider.name == "pickupObjectRed"
        || hit.collider.name == "multiPickUpObject" || hit.collider.name == "multiPickUpObjectChildOne" || hit.collider.name == "multiPickUpObjectChildTwo"
        || hit.collider.name == "small_rock_1" || hit.collider.name == "stick_asset") {
            Debug.Log("Hit by: " + hit.collider.name);
            impact(Vector3.back, force);
        }

    }
    */
}
