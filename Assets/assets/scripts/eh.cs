using UnityEngine;
using System.Collections;

public class eh : MonoBehaviour {
//	Vector3 inputPosition = Input.mousePosition; 
//	Vector3 ray = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - inputPosition.x, Screen.height - inputPosition.y, Camera.main.transform.position.z - 2f)); 
	// Update is called once per frame
	public float distance = 5.206312f;
	bool culo=false;

	private float xl;
	private float xr;
	private float yt;
	private float yb;
	private float offSet=0.6f;

	void Start(){
		xl = (GameObject.Find ("left").transform.position.x + GameObject.Find ("left").GetComponent<Renderer> ().bounds.extents.x) + offSet;
		xr = (GameObject.Find ("right").transform.position.x - GameObject.Find ("right").GetComponent<Renderer> ().bounds.extents.x) - offSet;
		yt = (GameObject.Find ("top").transform.position.y - GameObject.Find ("top").GetComponent<Renderer> ().bounds.extents.y) - offSet;
		yb = (GameObject.Find ("bottom").transform.position.y + GameObject.Find ("bottom").GetComponent<Renderer> ().bounds.extents.y) + offSet;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) 
			culo=true;
		if(Input.GetMouseButtonUp(0))
			culo=false;
		if(culo){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		Vector3 point = ray.origin + (ray.direction * distance);
		point=new Vector3(point.x,point.y,2.54f);
//		Debug.Log( "World point " + point );
		if(InArea(point))
			gameObject.transform.position=point;
		}
	
	}

	public bool InArea(Vector3 point){
		return ((xl<point.x && xr>point.x)&&(yb<point.y && yt>point.y)); 
	}
	public Vector2 GetPos(){
		return gameObject.transform.position;
	}
}
