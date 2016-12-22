using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveLevel : MonoBehaviour {

    public float speed = 5f;
    public GameObject disableObjectPlane;
    public GameObject movementObject;

    private List<Vector3> curveVertices = new List<Vector3>();
    private createLevel createLevelScript;
    private int lastPositionInCurveVertices = 2;
    private float movementTimer = 0f;
    private Vector3 moveMeshLeftPoint = Vector3.zero;
    private Vector3 moveMeshRightPoint = Vector3.zero;
    private Vector3 movePoint = Vector3.zero;

    // Use this for initialization
    void Start () {
        createLevelScript = this.gameObject.GetComponent<createLevel>();
    }
	
	// Update is called once per frame
	void Update () {
        curveVertices = createLevelScript.getCurveVertices();
        //Debug.Log(movementTimer);
        if(movementTimer > 1f)
        {
            moveMeshLeftPoint = moveObstaclePoint(curveVertices);
            moveMeshRightPoint = curveVertices[lastPositionInCurveVertices + 1];
            movePoint = new Vector3(moveMeshLeftPoint.x, moveMeshLeftPoint.y, (moveMeshLeftPoint.z + moveMeshRightPoint.z) / 2);
            //Debug.Log(movePoint);
            //movementObject.transform.position = movePoint;
            //movementObject.transform.RotateAround(transform.forward, movePoint, 20 * Time.deltaTime);

            movementTimer = 0f;
        } else
        {
            movementTimer += Time.deltaTime;
        }
        //movementObject.transform.RotateAround(movePoint, transform.right, 20 * Time.deltaTime);
        //transform.RotateAround(transform.forward, movePoint, 20 * Time.deltaTime);
        //this.gameObject.transform.position += -movementObject.transform.right * Time.deltaTime * speed;
        //this.transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    public Vector3 moveObstaclePoint(List<Vector3> list)
    {
        Vector3 meshVertexPosition = list[0];
        while (meshVertexPosition.x < disableObjectPlane.transform.position.x + 100f)
        {
            //increment up the left side of the mesh
            lastPositionInCurveVertices += 2;
            if(lastPositionInCurveVertices > list.Count)
            {
                lastPositionInCurveVertices = list.Count - 1;
            }

            meshVertexPosition = list[lastPositionInCurveVertices];
            //Debug.Log("list.count: " + list.Count + ", lastPositionInCurveVertices: " + lastPositionInCurveVertices + 
            //    ", meshVertexPosition: " + meshVertexPosition + ", disableObjectPlane: " + disableObjectPlane.transform.position.x);

        }
        return meshVertexPosition;
    }
}
