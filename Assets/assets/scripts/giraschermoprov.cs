using UnityEngine;
using System.Collections;


public class giraschermoprov : MonoBehaviour {
	
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		StampaScore ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {  
//			Application.LoadLevel ("menu"); 
			callLevel.meh.CallLevel("menu");
		}
	}

	public void Back(){
//		Application.LoadLevel ("menu");
		callLevel.meh.CallLevel ("menu");
	}

	public void StampaScore(){
		cella[] aux = salvataggio.meh.score;
		for (int i =0; i<aux.Length; i++) {
			if(aux[i].plays!=0){
				GameObject.Find((i+1)+"plays").GetComponent<TextMesh>().text=aux[i].plays.ToString();
				GameObject.Find((i+1)+"point").GetComponent<TextMesh>().text=aux[i].points.ToString();
			}
		}
	}
}
