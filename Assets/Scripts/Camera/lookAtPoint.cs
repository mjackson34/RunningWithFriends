using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lookAtPoint : MonoBehaviour {

    public GameObject lookPoint; //LookPoint Object
    public createLevel createLevelScript; //GroundLevel Object
    public float distanceFromLookPoint = 30f;

    private List<Vector3> curveVertices = new List<Vector3>();
    private int lastPositionInCurveVertices = 2;
    private float movementTimer = 0f;
    private Vector3 moveMeshLeftPoint = Vector3.zero;
    private Vector3 moveMeshRightPoint = Vector3.zero;
    private Vector3 movePoint = Vector3.zero;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        curveVertices = createLevelScript.getCurveVertices();

        if (lookPoint.transform.position.x < transform.position.x + distanceFromLookPoint)
        {
            moveMeshLeftPoint = moveObstaclePoint(curveVertices);
            moveMeshRightPoint = curveVertices[lastPositionInCurveVertices + 1];
            //Debug.Log(moveMeshLeftPoint.z + ", " + moveMeshRightPoint.z);
            movePoint = new Vector3(moveMeshLeftPoint.x, moveMeshLeftPoint.y, (moveMeshLeftPoint.z + moveMeshRightPoint.z) / 2);
            //Vector3 oldPoint = new Vector3(lookPoint.transform.position.x, 0f, lookPoint.transform.position.z);

            lookPoint.transform.position = movePoint;
            //lookPoint.transform.position = Vector3.Lerp(oldPoint, movePoint, Time.deltaTime * 2);
        }

        Vector3 relativePos = movePoint - transform.position;
        //Debug.Log(lookPoint.transform.position);
        Quaternion newRotation = Quaternion.LookRotation(relativePos.normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime / 2);
        //transform.rotation = newRotation;
        //transform.rotation = Quaternion.LookRotation(relativePos);
        //transform.LookAt(lookPoint.transform);
    }

    //NEED TO ADD IN MOVING THE OBJECT IN FRONT OF THE CHARACTER AND HAVE THE CHARACTER ROTATE THAT WAY
    public Vector3 moveObstaclePoint(List<Vector3> list)
    {
        Vector3 meshVertexPosition = list[lastPositionInCurveVertices];
        while (meshVertexPosition.x < lookPoint.transform.position.x + distanceFromLookPoint)
        {
            //increment up the left side of the mesh
            lastPositionInCurveVertices += 2;
            if (lastPositionInCurveVertices > list.Count)
            {
                lastPositionInCurveVertices = list.Count - 1;
            }

            meshVertexPosition = list[lastPositionInCurveVertices];

        }
        return meshVertexPosition;
    }

}
