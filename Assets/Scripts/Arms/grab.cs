using UnityEngine;
using System.Collections;

public class grab : MonoBehaviour {

    public int mouseButton = 0;
    public bool controllerEnabled = false;
    public ControllerEnabled controllerStatus; //from the gameManager object
    public GameObject armControlObject;
    public int playerNumber;

    private GameObject pickedUpObject;
    private CharacterJoint pickedUpObjectComponent;
    private bool holdingAnObject = false;
    private string hand;

	// Use this for initialization
	void Start () {
        StartCoroutine(Setup());
        controllerEnabled = controllerStatus.isControllerEnabled();
    }
	
	// Update is called once per frame
	void Update () {
        if(!controllerEnabled)
        {
            if (Input.GetMouseButtonUp(mouseButton))
            {
                if (pickedUpObject != null)
                {
                    pickedUpObject.GetComponent<Rigidbody>().mass = 1.0f;
                    Destroy(pickedUpObjectComponent);
                    //this.gameObject.GetComponent<ArmControl>().armSpeed = 5;
                    holdingAnObject = false;
                }
            }
        } else
        {
            
            if (!Input.GetButton("R2Button" + playerNumber) && hand == "right_hand")
            {
                if (pickedUpObject != null)
                {
                    pickedUpObject.GetComponent<Rigidbody>().mass = 5.0f;
                    Destroy(pickedUpObjectComponent);
                    //Vector3 armControlPoint = armControlObject.GetComponent<ArmControl>().getLocalPoint();
                    //Vector3 newDirection = transform.position - armControlObject.GetComponent<ArmControl>().getLocalPoint();
                    pickedUpObject.GetComponent<Rigidbody>().AddForce((armControlObject.GetComponent<ArmControl>().getLocalPoint() - transform.position) * 100f);
                    //this.gameObject.GetComponent<ArmControl>().armSpeed = 5;
                    holdingAnObject = false;
                }
            }
            if (!Input.GetButton("L2Button" + playerNumber) && hand == "left_hand")
            {
                if (pickedUpObject != null)
                {
                    pickedUpObject.GetComponent<Rigidbody>().mass = 5.0f;
                    Destroy(pickedUpObjectComponent);
                    //Vector3 armControlPoint = armControlObject.GetComponent<ArmControl>().getLocalPoint();
                    //Vector3 newDirection = transform.position - armControlObject.GetComponent<ArmControl>().getLocalPoint();
                    pickedUpObject.GetComponent<Rigidbody>().AddForce((armControlObject.GetComponent<ArmControl>().getLocalPoint() - transform.position) * 100f);
                    //this.gameObject.GetComponent<ArmControl>().armSpeed = 5;
                    holdingAnObject = false;
                }
            }

        }
        //if(Input.GetButton("R2Button")) { Debug.Log(Input.GetButton("R2Button")); }
        //if (!Input.GetButton("R2Button")) { Debug.Log(Input.GetButton("R2Button")); }
        //Debug.Log(holdingAnObject);

    }

    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision.collider.name);
        //Make sure all the objects are named here
        //Debug.Log(controllerEnabled);
        if (!controllerEnabled)
        {
            if ((collision.name == "pickupObject" || collision.name == "pickupObjectBlue" || collision.name == "pickupObjectRed"
            || collision.name == "multiPickUpObject" || collision.name == "multiPickUpObjectChildOne" || collision.name == "multiPickUpObjectChildTwo"
            || collision.name == "small_rock_1" || collision.name == "stick_asset")
            && Input.GetMouseButton(mouseButton))
            {
                Debug.Log(collision.name);
                createJoint(collision);
            }
        }
        else
        {
            if ((collision.name == "pickupObject" || collision.name == "pickupObjectBlue" || collision.name == "pickupObjectRed"
            || collision.name == "multiPickUpObject" || collision.name == "multiPickUpObjectChildOne" || collision.name == "multiPickUpObjectChildTwo"
            || collision.name == "small_rock_1" || collision.name == "stick_asset")
            && (Input.GetButton("R2Button" + playerNumber) || Input.GetButton("L2Button" + playerNumber)))
            {
                Debug.Log(collision.name);
                createJoint(collision);

            }
        }

    }


    private void createJoint(Collider collision)
    {
        //Debug.Log(collision.gameObject.GetComponent<CharacterJoint>() == null);
        //Debug.Log(holdingAnObject);
        if (collision.gameObject.GetComponent<CharacterJoint>() == null && !holdingAnObject)
        {
            pickedUpObject = collision.gameObject;
            pickedUpObject.AddComponent<CharacterJoint>();
            pickedUpObjectComponent = pickedUpObject.GetComponent<CharacterJoint>();
            pickedUpObject.GetComponent<CharacterJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            pickedUpObject.GetComponent<Rigidbody>().mass = 0.1f;
            holdingAnObject = true;
        }
        else if (collision.gameObject.GetComponent<CharacterJoint>() != null && !holdingAnObject)
        {

            pickedUpObject = collision.gameObject;
            //pickedUpObject.GetComponent<CharacterJoint>().enabled = false;
            //pickedUpObject.AddComponent<CharacterJoint>();
            pickedUpObjectComponent = pickedUpObject.GetComponent<CharacterJoint>();
            pickedUpObject.GetComponent<CharacterJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            pickedUpObject.GetComponent<Rigidbody>().mass = 0.1f;
            holdingAnObject = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        //Make sure all the objects are named here
        //Debug.Log(controllerEnabled);
        if (!controllerEnabled)
        {
            if ((collision.collider.name == "pickupObject" || collision.collider.name == "pickupObjectBlue" || collision.collider.name == "pickupObjectRed"
            || collision.collider.name == "multiPickUpObject" || collision.collider.name == "multiPickUpObjectChildOne" || collision.collider.name == "multiPickUpObjectChildTwo"
            || collision.collider.name == "small_rock_1" || collision.collider.name == "stick_asset")
            && Input.GetMouseButton(mouseButton))
            {
                Debug.Log(collision.collider.name);
                createJoint(collision);
            }
        } else
        {
            if ((collision.collider.name == "pickupObject" || collision.collider.name == "pickupObjectBlue" || collision.collider.name == "pickupObjectRed"
            || collision.collider.name == "multiPickUpObject" || collision.collider.name == "multiPickUpObjectChildOne" || collision.collider.name == "multiPickUpObjectChildTwo"
            || collision.collider.name == "small_rock_1" || collision.collider.name == "stick_asset")
            && Input.GetButton("R2Button" + playerNumber))
            {
                Debug.Log(collision.collider.name);
                createJoint(collision);

            }
        }
        
    }

    private void createJoint(Collision collision)
    {
        //Debug.Log(collision.gameObject.GetComponent<CharacterJoint>() == null);
        //Debug.Log(holdingAnObject);
        if (collision.gameObject.GetComponent<CharacterJoint>() == null && !holdingAnObject)
        {
            pickedUpObject = collision.gameObject;
            pickedUpObject.AddComponent<CharacterJoint>();
            pickedUpObjectComponent = pickedUpObject.GetComponent<CharacterJoint>();
            pickedUpObject.GetComponent<CharacterJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            pickedUpObject.GetComponent<Rigidbody>().mass = 0.1f;
            holdingAnObject = true;
        }
        else if (collision.gameObject.GetComponent<CharacterJoint>() != null && !holdingAnObject)
        {

            pickedUpObject = collision.gameObject;
            //pickedUpObject.GetComponent<CharacterJoint>().enabled = false;
            //pickedUpObject.AddComponent<CharacterJoint>();
            pickedUpObjectComponent = pickedUpObject.GetComponent<CharacterJoint>();
            pickedUpObject.GetComponent<CharacterJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            pickedUpObject.GetComponent<Rigidbody>().mass = 0.1f;
            holdingAnObject = true;
        }
    }

    public void setPlayerNumber(int playerNumber) { this.playerNumber = playerNumber; }
    public int getPlayerNumber() { return this.playerNumber; }

    public IEnumerator Setup()
    {
        while(playerNumber == 0)
        {
            playerNumber = gameObject.GetComponentInParent<characterMovement>().playerNumber;
            hand = armControlObject.name;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
