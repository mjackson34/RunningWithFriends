using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour {

    public List<GameObject> obstacles;
    public List<GameObject> playerTypePrefabs; //where the player prefab goes
    public List<GameObject> players;
    public List<GameObject> crosshairs;
    public GameObject crosshairPrefab;
    public Canvas canvas;
    public GameObject lookPoint;
    public GameObject camera;
    public GameObject plane;
    public GameObject meshGroundLevel;
    public GameObject disableObstaclePlane;
    public float spawnTimer = 1.0f;
    public int maxNumObstacles = 10;

    private float planeWidth;
    private float planeLength;
    private List<GameObject> createdObstacles = new List<GameObject>();
    private float spawnClock = 0;
    private int currentActiveObstacles = 0;

    private int lastPositionInCurveVertices = 0; //starts at ZERO and is the last vertex 
    private Vector3 spawnObstacleOnMesh;
    private Vector3 rightBoundsSpawnObstacleOnMesh;
    private bool finishedPlayerSetup = false;
    //private List<Vector3> meshVectors;

	// Use this for initialization
	void Start () {
        planeWidth = plane.GetComponent<MeshRenderer>().bounds.size.x;
        planeLength = plane.GetComponent<MeshRenderer>().bounds.size.z;

        //set the player number
        for(int i = 0; i < players.Count; i++)
        {
            //create and name player
            players[i] = (GameObject)Instantiate(playerTypePrefabs[0], new Vector3(10f, 10f, 10f * i), Quaternion.identity);
            players[i].name = "Player " + (i + 1);
            players[i].GetComponent<characterMovement>().playerNumber = i + 1;
            //add player to gameManager object as child
            players[i].transform.parent = this.gameObject.transform;
            players[i].GetComponent<characterMovement>().lookPoint = lookPoint;
            //create crosshair for player
            crosshairs[i] = (GameObject)Instantiate(crosshairPrefab, new Vector2(canvas.GetComponent<RectTransform>().rect.width/2, canvas.GetComponent<RectTransform>().rect.height / 2), Quaternion.identity);
            crosshairs[i].name = "Crosshair " + (i + 1);
            ArmControl[] armcontrols = players[i].GetComponentsInChildren<ArmControl>();
            armcontrols[0].controllerCrosshairs = crosshairs[i];
            armcontrols[0].camera = camera.GetComponent<Camera>();
            armcontrols[1].controllerCrosshairs = crosshairs[i];
            armcontrols[1].camera = camera.GetComponent<Camera>();
            //add player number to crosshairMovment script otherwise the crosshair won't move
            crosshairs[i].GetComponent<crosshairMovement>().playerNumber = i + 1;
            //add crosshair to canvas
            crosshairs[i].transform.parent = canvas.GetComponent<RectTransform>();
            crosshairs[i].GetComponent<crosshairMovement>().canvasRectTransform = canvas.GetComponent<RectTransform>();
            //add player to lookPoint so it will move away from all players
            lookPoint.GetComponent<MoveTarget>().players.Add(players[i].transform);
            //add player to camera so the camera will follow all players
            camera.GetComponent<moveCamera>().players.Add(players[i].transform);
            disableObstaclePlane.GetComponent<moveCamera>().players.Add(players[i].transform);
            //StartCoroutine(Setup(i));
            Debug.Log("End setup for player " + (i + 1));

        }
        finishedPlayerSetup = true;
        //instantiateObstacle();
	}
	
	// Update is called once per frame
	void Update () {
        List<Vector3> list = meshGroundLevel.GetComponent<createLevel>().getCurveVertices();
        if(list.Count > 200)
        {
            //Spawning obstacles
            if (spawnClock > spawnTimer && currentActiveObstacles < maxNumObstacles)
            {
                //Going through the list of already created obstacles to see if there are any to activate
                foreach (GameObject obstacle in createdObstacles)
                {
                    if (obstacle.activeSelf == false)
                    {

                        spawnObstacleOnMesh = obstacleSpawnPoint(list);
                        rightBoundsSpawnObstacleOnMesh = list[lastPositionInCurveVertices - 1];

                        //This was for spawning on a plane
                        //obstacle.transform.position = new Vector3(Random.Range(-(planeWidth / 2), (planeWidth / 2)), 10f, Random.Range(0, 40));
                        //obstacle.transform.position = new Vector3(spawnObstacleOnMesh.x, 5f, Random.Range(spawnObstacleOnMesh.z, spawnObstacleOnMesh.z + 200));


                        Vector3 spawnLocation = new Vector3(spawnObstacleOnMesh.x, 5f, Random.Range(spawnObstacleOnMesh.z, rightBoundsSpawnObstacleOnMesh.z));
                        spawnLocation = meshGroundLevel.transform.TransformPoint(spawnLocation);
                        //Debug.Log("left side Z: " + spawnObstacleOnMesh.z + "right size Z: " + rightBoundsSpawnObstacleOnMesh.z + ", spawnObstacleOnMesh: " + 
                        //spawnObstacleOnMesh + ", rightBoundsSpawnObstacleOnMesh: " + rightBoundsSpawnObstacleOnMesh + ", spawnLocation: " + spawnLocation);
                        obstacle.transform.position = spawnLocation;
                        obstacle.SetActive(true);
                        spawnClock = 0;
                        currentActiveObstacles += 1;
                        break;
                    }
                }
                //If it gets through the list of already created objects, and has not actived one, then it creates on.
                if (spawnClock > spawnTimer && currentActiveObstacles < maxNumObstacles)
                {
                    spawnObstacleOnMesh = obstacleSpawnPoint(list);
                    rightBoundsSpawnObstacleOnMesh = list[lastPositionInCurveVertices + 1];
                    Vector3 spawnLocation = new Vector3(spawnObstacleOnMesh.x, 5f, Random.Range(spawnObstacleOnMesh.z, rightBoundsSpawnObstacleOnMesh.z));
                    //Vector3 spawnLocation = new Vector3(Random.Range(spawnObstacleOnMesh.x, rightBoundsSpawnObstacleOnMesh.x), 5f, spawnObstacleOnMesh.z);
                    spawnLocation = meshGroundLevel.transform.TransformPoint(spawnLocation);
                    //spawnLocation.z += -100f;
                    //Debug.Log("left side Z: "+ spawnObstacleOnMesh.z + "right size Z: " + rightBoundsSpawnObstacleOnMesh.z + ", spawnObstacleOnMesh: " + 
                    //spawnObstacleOnMesh + ", rightBoundsSpawnObstacleOnMesh: " + rightBoundsSpawnObstacleOnMesh + ", spawnLocation: " + spawnLocation);
                    instantiateObstacle(obstacles[Random.Range(0, obstacles.Count)], spawnLocation);
                    spawnClock = 0;
                }
            }
            else
            {
                spawnClock += Time.deltaTime;
            }
        } else
        {
            spawnClock += Time.deltaTime;
        }
        
	    
	}

    //create objects
    public void instantiateObstacle()
    {
        GameObject newObstacle = (GameObject)Instantiate(obstacles[1], new Vector3(Random.Range(-(planeWidth/2), (planeWidth/2)), 0, Random.Range(0, 40)), Quaternion.identity);
        newObstacle.transform.parent = this.gameObject.transform;
        createdObstacles.Add(newObstacle);
        currentActiveObstacles += 1;
    }

    public void instantiateObstacle(GameObject obstacle, Vector3 position)
    {
        GameObject newObstacle = (GameObject)Instantiate(obstacle, position, Quaternion.identity);
        newObstacle.transform.parent = meshGroundLevel.transform;
        //newObstacle.transform.parent = this.gameObject.transform;
        newObstacle.name = obstacle.name;
        createdObstacles.Add(newObstacle);
        currentActiveObstacles += 1;
    }

    public GameObject getDisableObjectPlane() { return disableObstaclePlane; }
    public void decrementCurrentActiveObstacles() { currentActiveObstacles -= 1; }
    public List<GameObject> getPlayers() { return this.players; } 
    public bool getFinishedPlayerSetup() { return this.finishedPlayerSetup; }

    public Vector3 obstacleSpawnPoint(List<Vector3>list)
    {
        Vector3 meshVertexPosition = Vector3.zero;
        while (meshVertexPosition.x < players[0].transform.position.x + 100f)
        {
            //increment up the left side of the mesh
            lastPositionInCurveVertices += 2;
            meshVertexPosition = list[lastPositionInCurveVertices];

        }
        return meshVertexPosition;
    }

    public IEnumerator Setup(int i)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Starting gameManager coroutine");
        players[i].GetComponent<characterMovement>().playerNumber = i + 1;
        crosshairs[i].GetComponent<crosshairMovement>().playerNumber = i + 1;
        Debug.Log("End gameManager coroutine");
        //yield return new WaitForSeconds(.1f);
    }
}
