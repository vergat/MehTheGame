using UnityEngine;
using System.Collections;

public class callLevel : MonoBehaviour {
	public static callLevel meh;

	void Start(){
		meh = this;
	}

	public void CallLevel(string level){
		if (Screen.height == 1200) {
			Application.LoadLevel (level + "1200");
		}
//		if (Screen.height == 1080 || Screen.height == 720) {
		else{
			Application.LoadLevel (level + "1080");
		}
	}
}
