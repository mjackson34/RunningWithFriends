using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class createHedge : MonoBehaviour {

    public createLevel createLevelScript;
    public List<GameObject> hedgePrefabs;

    private List<Vector3> curveVertices = new List<Vector3>();
    private int lastPositionInCurveVertices = 2;
    private float movementTimer = 0f;
    private Vector3 moveMeshLeftPoint = Vector3.zero;
    private Vector3 moveMeshRightPoint = Vector3.zero;

    private bool stop = true;

    // Use this for initialization
    void Start () {
        Debug.Log("hedge size: "+hedgePrefabs[0].GetComponent<BoxCollider>().bounds.min.x);
	}
	
	// Update is called once per frame
	void Update () {

        curveVertices = createLevelScript.getCurveVertices();
        //Debug.Log(curveVertices.Count);
        if(curveVertices.Count > 0 && stop)
        {
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log(getMeshEdgePoint(curveVertices, 10f));
                Vector3 rightHedge = new Vector3(curveVertices[lastPositionInCurveVertices].x, 5f, curveVertices[lastPositionInCurveVertices].z+10f);
                Vector3 leftHedge = new Vector3(curveVertices[lastPositionInCurveVertices+1].x, 5f, curveVertices[lastPositionInCurveVertices+1].z);
                instantiateHedge(hedgePrefabs[0], rightHedge);
                instantiateHedge(hedgePrefabs[0], leftHedge);
                Debug.Log(rightHedge.x + 30f);
                rightHedge = getMeshEdgePoint(curveVertices, rightHedge.x+5);
                leftHedge = getMeshEdgePoint(curveVertices, leftHedge.x+5);
                rightHedge = new Vector3(curveVertices[lastPositionInCurveVertices].x, 5f, curveVertices[lastPositionInCurveVertices].z + 10f);
                leftHedge = new Vector3(curveVertices[lastPositionInCurveVertices + 1].x, 5f, curveVertices[lastPositionInCurveVertices + 1].z);
                instantiateHedge(hedgePrefabs[0], rightHedge);
                instantiateHedge(hedgePrefabs[0], leftHedge);

                stop = false;
            }
        }



    }

    public void instantiateHedge(GameObject hedge, Vector3 position)
    {
        GameObject newHedge = (GameObject)Instantiate(hedge, position, Quaternion.identity);
        newHedge.transform.parent = createLevelScript.transform;
        //newObstacle.transform.parent = this.gameObject.transform;
        newHedge.name = hedge.name;
    }

    public Vector3 getMeshEdgePoint(List<Vector3> list, float lastXCoord)
    {
        Vector3 meshVertexPosition = list[lastPositionInCurveVertices];

        //increment up the left side of the mesh
        while (lastXCoord < meshVertexPosition.x)
        {
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
