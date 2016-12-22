using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTarget : MonoBehaviour {

    //public GameObject lookPoint; //LookPoint Object
    public List<Transform> players; //player prefabs
    public createLevel createLevelScript; //GroundLevel Object
    public float distanceFromLookPoint = 30f;
    public GameObject gameManager;

    private List<Vector3> curveVertices = new List<Vector3>();
    private int lastPositionInCurveVertices = 2;
    private float movementTimer = 0f;
    private Vector3 moveMeshLeftPoint = Vector3.zero;
    private Vector3 moveMeshRightPoint = Vector3.zero;
    private Vector3 movePoint = Vector3.zero;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update()
    {

        //gets an updated list of the level vertices
        curveVertices = createLevelScript.getCurveVertices();

        //looks through all the players
        foreach (Transform player in players)
        {
            //Debug.Log("lookPoint position: " + transform.position + ", player position: " + player.position);
            //if a player is within a certain distance, move the lookPoint (target) foward
            if (transform.position.x < player.position.x + distanceFromLookPoint)
            {
                //Debug.Log("true");
                //gets the left point on the mesh
                moveMeshLeftPoint = moveObstaclePoint(curveVertices);
                //gets the right point on the mesh
                moveMeshRightPoint = curveVertices[lastPositionInCurveVertices + 1];
                //gets the middle point between the two vertices on the mesh
                movePoint = new Vector3(moveMeshLeftPoint.x, moveMeshLeftPoint.y, (moveMeshLeftPoint.z + moveMeshRightPoint.z) / 2);
                //this is used if you lerp the lookPoint
                //Vector3 oldPoint = new Vector3(transform.position.x, 0f, transform.position.y);
                //Debug.Log(movePoint);
                this.transform.position = movePoint;
                //lookPoint.transform.position = Vector3.Lerp(oldPoint, movePoint, Time.deltaTime * 2);
            }
            //Debug.Log("Finished MoveTarget foreach loop!");
        }

    }

    public Vector3 moveObstaclePoint(List<Vector3> list)
    {
        Vector3 meshVertexPosition = list[lastPositionInCurveVertices];
        while (meshVertexPosition.x < transform.position.x + distanceFromLookPoint)
        {
            //increment up the left side of the mesh
            lastPositionInCurveVertices += 2;
            if (lastPositionInCurveVertices > list.Count)
            {
                lastPositionInCurveVertices = list.Count - 1;
            }

            meshVertexPosition = list[lastPositionInCurveVertices];
            //Debug.Log("list.count: " + list.Count + ", lastPositionInCurveVertices: " + lastPositionInCurveVertices +
            //    ", meshVertexPosition: " + meshVertexPosition + ", disableObjectPlane: " + lookPoint.transform.position.x);

        }
        return meshVertexPosition;
    }

}
