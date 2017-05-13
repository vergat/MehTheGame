using UnityEngine;
using System.Collections;

public class go : MonoBehaviour {
	public ball meh;
	public bool verifica= true;

	bool one_click = false;
	bool timer_running;
	float timer_for_double_click=0.0f;
	
	float delay=0.8f;
	
	void Update(){
		if(verifica){
//			if (Input.GetMouseButtonDown (0)){
//				if(!one_click){
//					one_click = true;
//					timer_for_double_click = Time.time;
//				} 
//				else
//				{
//					one_click = false;
//					meh.StartBall ();
//					verifica = false;
//					GetComponent<eh> ().enabled = false;
//					GameObject.Find("target").transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,2.9f);
//					Destroy(gameObject);
//				}
//			}
			if(one_click){
				if((Time. time - timer_for_double_click) > delay)
					one_click = false;
				}
		}
	}

	void OnMouseDown(){
		if(!one_click){
			one_click = true;
			timer_for_double_click = Time.time;
		} 
		else
		{
			one_click = false;
			verifica = false;
			GetComponent<eh> ().enabled = false;
			GameObject.Find("target").transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,2.9f);
			meh.StartBall ();
			Destroy(gameObject);
		}
	}
}
