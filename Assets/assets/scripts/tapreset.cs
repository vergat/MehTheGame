using UnityEngine;
using System.Collections;

public class tapreset : MonoBehaviour {
	
	void Update(){
		if(Input.GetMouseButtonUp(0))
			callLevel.meh.CallLevel ("play");
	}

}
