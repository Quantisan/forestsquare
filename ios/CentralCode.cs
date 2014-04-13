using UnityEngine;
using System.Collections;

public class CentralCode : MonoBehaviour {
	public GameObject topLine;
	public GameObject midLine;
	public GameObject bottomLine;

	public GameObject circleOutline;
	public GameObject circleHolder;
	public GameObject arrowsHolder;

	public UILabel treeHeight;
	public UILabel personHeight;
	int pHeightVal;
	float tHeightVal;
	public int myFeet;
	public int myInches;

	public GameObject mainMenu;
	public GameObject measureTree;

	public GameObject titleBar;

	bool pinching = false;
	float oldPinchDistance = 0;
	float currentScale = .1f;

	public void showMain(){
	//	titleBar.transform.position = new Vector3(0,300,0);
	//	TweenPosition.Begin(titleBar,1f, new Vector3(0,163,0)).method = UITweener.Method.BounceIn;
	}

	public void startLogging(){
		mainMenu.SetActive(false);
		measureTree.SetActive(true);
	}

	public void okayPressed(){
		arrowsHolder.SetActive(false);
		circleHolder.SetActive(true);
	}
	public void sendPressed(){
		circleHolder.SetActive(false);
		arrowsHolder.SetActive(true);
		measureTree.SetActive(false);
		mainMenu.SetActive(true);
		showMain();
		sendInfo();
	}

	public void sendInfo(){
		int heightInches = Mathf.RoundToInt(tHeightVal);
		float diameterInches = 10.5f;
		float radiusInches = diameterInches/2f;

		int treeID = 39204;
		string treeType = "burr_oak";
		//string url = "http://example.com/script.php?height="+heightInches+"&treeID=" + treeID + "&diameter=" + diameterInches + "&species=" + treeType;
	//	string url = "http://example.com/script.php";

		string url = "http://forestsquare-spaceappsboston.herokuapp.com/tree/";
//	http://forestsquare-spaceappsboston.herokuapp.com/tree/12?height=100&radius=1&locationX=200&locationY=300&species=SomeTree
		url += treeID + "?";
		url += "height="+heightInches+"&";
		url += "radius="+radiusInches+"&";
		url += "locationX="+1.2490f+"&";
		url += "locationY="+5.6490f+"&";
		url += "species=" + treeType;

		//	height=100&radius=1&locationX=200&locationY=300&species=SomeTree


		Debug.Log (url);

		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));


	//	WWWForm form = new WWWForm();
	//	form.AddField("height",heightInches+"");
	//	form.AddField("radius",radiusInches+"");
	//	form.AddField("locationX",1.03029848f+"");
	//	form.AddField("locationY",3.03705123f+"");
	//	form.AddField("treeID",treeID+"");
	//	form.AddField("species",treeType);

	//	WWW www = new WWW(url, form);
	//	Debug.Log (form);

	//	StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
			
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}
	} 

	public void writeItAll(){
		float bigDist = topLine.transform.position.y - bottomLine.transform.position.y;
		float smallDist = bottomLine.transform.position.y - midLine.transform.position.y;

		if(bigDist < 0){bigDist *= -1;}
		if(smallDist < 0){smallDist *= -1;}

		if(smallDist == 0){return;}

		tHeightVal = pHeightVal*bigDist/smallDist;
		int tHeightInt = Mathf.RoundToInt (tHeightVal);

		int feetVal = 0;
		int inchesVal = tHeightInt;

		while(inchesVal >= 12){
			inchesVal -= 12;
			feetVal += 1;
		}
		treeHeight.text = "Tree Height: " + feetVal + "\' " + inchesVal + "\"";
	}

	// Use this for initialization
	void Start () {

		mainMenu.SetActive(true);
		measureTree.SetActive(false);
		circleHolder.SetActive(false);
		arrowsHolder.SetActive(true);
		showMain();
		int feetVal = 0;
		int inchesVal = pHeightVal;
		
		while(inchesVal >= 12){
			inchesVal -= 12;
			feetVal += 1;
		}
		personHeight.text = "Your Height: " + myFeet + "\' " + myInches + "\"";
		pHeightVal = 12*myFeet + myInches;
	}
	
	// Update is called once per frame
	void Update () {
		if(arrowsHolder.activeSelf){
			writeItAll();
		}
		if(circleHolder.activeSelf){
			if(Input.touchCount >= 2){
				Vector2 touch0, touch1;
				float distance;
				touch0 = Input.GetTouch(0).position;
				touch1 = Input.GetTouch(1).position;
				
				distance = Vector2.Distance(touch0, touch1);
				
				if(!pinching){
					pinching = true;
					oldPinchDistance = distance;
					currentScale = circleOutline.transform.localScale.x;
				}
				
				circleOutline.transform.localScale = new Vector3(currentScale*distance/oldPinchDistance, currentScale*distance/oldPinchDistance,1);
			}
			else{
				pinching = false;
			}
		}
	}
}
