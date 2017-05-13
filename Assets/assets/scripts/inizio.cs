using UnityEngine;
using System.Collections;

public class inizio : MonoBehaviour {
	
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		callLevel.meh.CallLevel ("menu");
	}

}
