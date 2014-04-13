using UnityEngine;
using System.Collections;

public class moveCircle : MonoBehaviour {
	public GameObject circleHolder;
	public Camera theCam;
	
	bool following = false;
	float baseY = 0;
	float baseX = 0;
	public void startDragging(){
		following = true;
		baseY = Input.mousePosition.y - circleHolder.transform.localPosition.y;
		baseX = Input.mousePosition.x - circleHolder.transform.localPosition.x;
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
			float xPos = Input.mousePosition.x - baseX;
			circleHolder.transform.localPosition = new Vector3(xPos, yPos,  circleHolder.transform.localPosition.z);			
		}
	}
}
