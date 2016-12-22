using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class characterMovement : MonoBehaviour {

    public float keyboardMovementSpeed = 10f;
    public float controllerMovementSpeed = 100f;
    public float runSpeed = 100f; //750f works on desktop pretty well
    public float sprintSpeedMultiplier = 2;
    public GameObject lookPoint;
    public createLevel createLevelScript; //Add the object that has the createLevel script on it (currently that is GroundLevel)
    public ControllerEnabled controllerStatus; //from the gameManager object
    public int playerNumber;

    private List<Vector3> curveVertices = new List<Vector3>();
    private int lastPositionInCurveVertices = 2;
    private float movementTimer = 0f;
    private Vector3 moveMeshLeftPoint = Vector3.zero;
    private Vector3 moveMeshRightPoint = Vector3.zero;
    private Vector3 movePoint = Vector3.zero;
    private bool controllerEnabled = false;

    // Use this for initialization
    void Start () {
        //StartCoroutine(Setup());
        controllerEnabled = controllerStatus.isControllerEnabled();
    }
	
	// Update is called once per frame
	void Update () {
        controllerEnabled = controllerStatus.isControllerEnabled();
        curveVertices = createLevelScript.getCurveVertices();

        //always move the player towards the lookPoint
        //Debug.Log(lookPoint.transform.position);
        Vector3 moveForward = (lookPoint.transform.position - transform.position).normalized;

        gameObject.GetComponent<CharacterController>().SimpleMove(moveForward * runSpeed * Time.deltaTime);

        //controller
        Vector3 controllerVector = Vector3.zero;
        Vector3 keyboardVector = Vector3.zero;
        //Debug.Log("controllerEnabled: " + controllerEnabled);
        //mouse and keyboard
        if (!controllerEnabled)
        {
            //mouse and keyboard
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                keyboardVector.x = Input.GetAxis("Horizontal");
                keyboardVector = Vector3.forward;
                float newSpeed = -Input.GetAxis("Horizontal") * keyboardMovementSpeed;
                //
                gameObject.GetComponent<CharacterController>().SimpleMove(Vector3.forward * newSpeed * Time.deltaTime);
            }
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                keyboardVector.z = Input.GetAxis("Vertical");
                keyboardVector = Vector3.right;
                float newSpeed = Input.GetAxis("Vertical") * keyboardMovementSpeed;
                gameObject.GetComponent<CharacterController>().SimpleMove(Vector3.right * newSpeed * Time.deltaTime);
            }
        } else
        {
            if (Mathf.Abs(Input.GetAxis("RightJoystickHorizontal" + playerNumber)) > 0.1f)
            {
                controllerVector.z = -Input.GetAxis("RightJoystickHorizontal" + playerNumber);
                gameObject.GetComponent<CharacterController>().Move(controllerVector * controllerMovementSpeed * Time.deltaTime);
            }
            if (Mathf.Abs(Input.GetAxis("RightJoystickVertical" + playerNumber)) > 0.1f)
            {
                controllerVector.x = Input.GetAxis("RightJoystickVertical" + playerNumber);
                gameObject.GetComponent<CharacterController>().Move(controllerVector * controllerMovementSpeed * Time.deltaTime);
            }
        }

        //Rotate towards the lookPoint
        Vector3 relativePos = lookPoint.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime/2);
        
    }

    public IEnumerator Setup()
    {
        Debug.Log("player number: " + playerNumber + ", " + (controllerStatus != null));
        while (lookPoint == null && createLevelScript == null && controllerStatus == null && playerNumber == 0)
        {
            lookPoint = gameObject.transform.Find("lookPoint").gameObject;
            createLevelScript = gameObject.transform.Find("GroundLevel").gameObject.GetComponentInParent<createLevel>();
            controllerStatus = gameObject.transform.Find("Game Manager").gameObject.GetComponentInParent<ControllerEnabled>();
            playerNumber = gameObject.GetComponentInParent<characterMovement>().playerNumber;
            new WaitForSeconds(0.5f);
        }

        yield return null;

    }

}
