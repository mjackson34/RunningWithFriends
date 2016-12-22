using UnityEngine;
using System.Collections;

public class crosshairMovement : MonoBehaviour {

    public float speed = 100f;
    public RectTransform rectTransform;
    public RectTransform canvasRectTransform;
    public int playerNumber;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 crosshairs = Vector3.zero;
        if (Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal" + playerNumber)) > 0.1f || Mathf.Abs(Input.GetAxis("LeftJoystickVertical" + playerNumber)) > 0.1f)
        {
            crosshairs.x = Input.GetAxis("LeftJoystickHorizontal" + playerNumber);
            crosshairs.y = Input.GetAxis("LeftJoystickVertical" + playerNumber);
            //Debug.Log("X: " + (transform.position.x < 0) + " Y: " + (transform.position.y < 0));
            //ZERO and Y
            if(rectTransform.anchoredPosition.y > 0) { rectTransform.anchoredPosition3D += crosshairs * speed * Time.deltaTime; }
            else { rectTransform.anchoredPosition3D = new Vector3(rectTransform.anchoredPosition3D.x, 0f, rectTransform.anchoredPosition3D.z); }
            //ZERO and X
            if(rectTransform.anchoredPosition.x > 0) { rectTransform.anchoredPosition3D += crosshairs * speed * Time.deltaTime;}
            else { rectTransform.anchoredPosition3D = new Vector3(0f, rectTransform.anchoredPosition3D.y, rectTransform.anchoredPosition3D.z); }
            //HEIGHT and Y
            if (rectTransform.anchoredPosition.y < canvasRectTransform.rect.height) { rectTransform.anchoredPosition3D += crosshairs * speed * Time.deltaTime; }
            else { rectTransform.anchoredPosition3D = new Vector3(rectTransform.anchoredPosition3D.x, canvasRectTransform.rect.height-10f, rectTransform.anchoredPosition3D.z); }
            //WIDTH and X
            if (rectTransform.anchoredPosition.x < canvasRectTransform.rect.width) { rectTransform.anchoredPosition3D += crosshairs * speed * Time.deltaTime; }
            else { rectTransform.anchoredPosition3D = new Vector3(canvasRectTransform.rect.width-10f, rectTransform.anchoredPosition3D.y, rectTransform.anchoredPosition3D.z); }
        }

    }
}
