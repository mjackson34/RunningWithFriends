using UnityEngine;
using System.Collections;

public class ArmControl : MonoBehaviour {

    //2 is kind of slow
    public float armSpeed = 5;
    public float swingSpeed = 0.1f;
    public Camera camera;
    public float moveDistance = 0.5f;
    public ControllerEnabled controllerStatus; //from the gameManager object
    public GameObject controllerCrosshairs;

    private Vector3 localPoint;
    private bool controllerEnabled = false;

    // Use this for initialization
    void Start () {
        controllerEnabled = controllerStatus.isControllerEnabled();
    }
	
	// Update is called once per frame
	void Update () {
        controllerEnabled = controllerStatus.isControllerEnabled();

        //NEED TO CREATE A CURSOR ON THE SCREEN FOR CONTROLLER/GAMEPAD
        Ray ray;
        if(!controllerEnabled) { ray = camera.ScreenPointToRay(Input.mousePosition); }
        else { ray = camera.ScreenPointToRay(controllerCrosshairs.transform.position); }

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(localPoint == Vector3.zero) { localPoint = hit.point; }
            localPoint = Vector3.Lerp(localPoint, hit.point, Time.deltaTime * swingSpeed);
            //Debug.Log(localPoint);
            //mouse position
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            //hands moving toward this point
            Debug.DrawLine(this.transform.position, hit.point, Color.blue);
            //Debug.DrawRay(this.transform.position, (hit.point - this.transform.position).normalized, Color.red);
            //Ray armLength = new Ray()

            Vector3 idealPoint = this.transform.parent.position + (localPoint - this.transform.position).normalized * moveDistance;
            //Vector3 idealPoint = this.transform.position + (localPoint - this.transform.position).normalized * moveDistance;
            //Debug.Log(idealPoint);
            //Debug.Log(this.transform.position +", " + idealPoint);
            //Debug.DrawLine(this.transform.parent.position, idealPoint, Color.red);
            //Debug.DrawLine(this.transform.position, idealPoint, Color.red);
            this.transform.position = Vector3.MoveTowards(this.transform.position, idealPoint, armSpeed * Time.deltaTime);
            //Debug.Log(this.transform.position);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, hit.point, armSpeed * Time.deltaTime);



        }

    }

    public Vector3 getLocalPoint() { return localPoint; }

}
