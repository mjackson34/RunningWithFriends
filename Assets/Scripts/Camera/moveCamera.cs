using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveCamera : MonoBehaviour {

    public List<Transform> players = new List<Transform>(); //a list of all the players in the game
    public float cameraDistance = 100f;
    public GameObject gameManager;
    public bool freezeCamera = false;
    //public float speed = 10f;

    private Transform startPosition;
    private Transform newPosition;
    private Vector3 newCameraPosition;
    // Use this for initialization
    void Start()
    {
        startPosition = transform;
        newPosition = players[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(newPosition == null)
        {
            newPosition = players[0];
        }
        //set the startPostion as the current position of the camera
        startPosition = transform;
        //loop through all the players
        if(!freezeCamera)
        {
            foreach (Transform player in players)
            {
                if ((player.position.x - transform.position.x) > cameraDistance)
                {
                    newPosition = player;
                }
            }
            newCameraPosition = new Vector3(newPosition.position.x, startPosition.position.y, newPosition.position.z);
            transform.position = Vector3.Lerp(startPosition.position, newCameraPosition, Time.deltaTime / 2);
        }


    }

}
