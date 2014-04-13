using UnityEngine;
using System.Collections;

public class MovingArrow : MonoBehaviour {
	public GameObject arrowHolder;
	public Camera theCam;

	bool following = false;
	float baseY = 0;
	public void startDragging(){
		following = true;
		baseY = Input.mousePosition.y - arrowHolder.transform.localPosition.y;
	}
	public void stopDragging(){
		following = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(following){
			float yPos = Input.mousePosition.y - baseY;
			arrowHolder.transform.localPosition = new Vector3(arrowHolder.transform.localPosition.x, yPos,  arrowHolder.transform.localPosition.z);
		}
	}
}
