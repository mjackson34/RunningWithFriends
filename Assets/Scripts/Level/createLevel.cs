using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class createLevel : MonoBehaviour {

    public MeshFilter filter;
    //public GameObject obstacleManager;
    public int resolution;
    public float ythickness = 0;
    public float xlength;
    public float zdistance;
    public float buildInterval = 0;

    public float speed = 1.0f;
    public float scale = 0.1f;
    public float width = 10f;
    public int curveResolution = 40;

    private bool ydirection = false; //false is up, true is down
    private float lastRandomY = 0;
    private float sinNumber = 0;
    private Vector3[] vertices;
    private List<Vector3> curveVertices = new List<Vector3>();
    private int[] triangles;

    void Start () {
        vertices = new Vector3[resolution * 8];
        //vertices = new Vector3[resolution * 6 * 2];
        //curveVertices = new Vector3[vertices.Length * 20];
        StartCoroutine(Generate());
        
	}
	
	void Update () {
	
	}
    
    //getters and setters
    public List<Vector3> getCurveVertices() { return curveVertices; }

    //creates the mesh
    public IEnumerator Generate()
    {
        int v = 0;

        for (int index = 0; index < resolution * 4; index++)
        {
            //zdistance += Mathf.Sin(Time.time * speed * zdistance) * scale;
            zdistance += Mathf.Sin(Time.time * speed + xlength + ythickness + zdistance) * scale;
            //xlength += Mathf.Sin(Time.time * speed + xlength + ythickness + zdistance) * scale;
            //builds in the X direction
            
            vertices[v++] = new Vector3((xlength * index), ythickness, zdistance);
            vertices[v++] = new Vector3((xlength * index), ythickness, -zdistance);
            
            //vertices[v++] = new Vector3(xlength, ythickness, (zdistance * index));
            //vertices[v++] = new Vector3(-xlength, ythickness, (zdistance * index));
        }

        //the below two show the extra piece that is being created
        //vertices[vertices.Length - 2] = new Vector3((xlength * (vertices.Length / 2 - 2)), ythickness, zdistance);
        //vertices[vertices.Length - 1] = new Vector3((xlength * (vertices.Length / 2 - 1)), ythickness, -zdistance);

        //https://nrj.io/procedural-curved-mesh-generation-in-unity-part-1/
        //http://www.theappguruz.com/blog/bezier-curve-in-games

        //Need to create a loop to take four points from vertices and create
        //the curve between them
        //0,1
        //2,3
        //4,5
        //6,7
        
        //Debug.Log("vertices.Length: " + vertices.Length);
        for(int i = 0; i < vertices.Length; )
        {

            for (int j = 0; j < curveResolution; j++)
            {
                float t = (float)j / (float)(curveResolution - 1); //20 is the resolution
                Vector3 p = CalculateBezierPoint(t, vertices[i], vertices[i + 2], vertices[i + 4], vertices[i + 6]);
                curveVertices.Add(p);
                //Vector3 p2 = CalculateBezierPoint(t, vertices[i + 1], vertices[i + 3], vertices[i + 5], vertices[i + 7]);
                //curveVertices.Add(p2);
                //X DIRECTION
                curveVertices.Add(new Vector3(p.x, p.y, p.z + width));

                //curveVertices.Add(new Vector3(p.x + 200, p.y, p.z));
                //curveVertices.Add(CalculateBezierPoint(t, vertices[i], vertices[i + 1], vertices[i + 2], vertices[i + 3]));
                //curveVertices.Add(CalculateBezierPoint(t, vertices[i + 4], vertices[i + 5], vertices[i + 6], vertices[i + 7]));


            }
            i += 8;
        }
        
        triangles = new int[curveVertices.Count * 6 - 6];
        //triangles = new int[resolution * 6 - 6];
        //Debug.Log("Number of triangles: " + triangles.Length);
        //for (int index = 0; index < resolution - 1; index++) //original
       //Debug.Log("curveVertices: " + curveVertices.Count/2);
        for (int index = 0; index < curveVertices.Count/2 - 1; index++)
        {
            //Debug.Log("index: " + index);
            //Debug.Log("location: " + curveVertices[index * 2]);
            triangles[index * 6] = index * 2 + 0;
            triangles[index * 6 + 1] = index * 2 + 1;
            triangles[index * 6 + 2] = index * 2 + 2;

            //ApplyMesh("Ground");

            //yield return new WaitForSeconds(buildInterval);
            triangles[index * 6 + 3] = index * 2 + 2;
            triangles[index * 6 + 4] = index * 2 + 1;
            triangles[index * 6 + 5] = index * 2 + 3;

            ApplyMesh("Ground");

            //yield return new WaitForSeconds(buildInterval);
            yield return null;
        }

        //Debug.Log("FINISHED!!!!!1!!!!");
    }


    //after creating the vertices and triangles this puts it all together
    void ApplyMesh(string meshName)
    {
        MeshCollider newMeshCollider = GetComponent<MeshCollider>();
        Mesh newMesh = new Mesh();
        //newMesh.Clear();
        newMesh.name = meshName;
        //newMesh.vertices = vertices;
        newMesh.vertices = curveVertices.ToArray();
        newMesh.triangles = triangles;

        newMesh.RecalculateNormals();
        newMeshCollider.sharedMesh = newMesh;

        filter.mesh = newMesh;
        //meshes.Add(newMesh);
        //Debug.Log(meshes.Count);
        
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //B(t) = [(1-t)^2*P0 + t * P1] + t[(1,t) * P1 + t * P2], 0 <= t <= 1
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        //Debug.Log(p);
        return p;
    }

}
