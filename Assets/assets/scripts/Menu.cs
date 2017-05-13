using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	bool one_click = false;
	bool timer_running;
	float timer_for_double_click=0.0f;
	float delay=0.5f;
	public callLevel boh;

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Update(){
		if(!GameObject.Find("continue").GetComponent<Button>().interactable)
			if (salvataggio.meh.inCorso) 
				GameObject.Find("continue").GetComponent<Button>().interactable=true;

		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!one_click){
				one_click = true;
				timer_for_double_click = Time.time;
			} 
			else
			{
				one_click = false;
				Application.Quit();
			}
		}
		if(one_click){
			if((Time. time - timer_for_double_click) > delay)
				one_click = false;
		}
	}

	public void Play () {
		salvataggio.meh.saveScore ();
		salvataggio.meh.inCorso = false;
		salvataggio.meh.punteggio = 0;
		salvataggio.meh.giocate = 0;
		salvataggio.meh.partita=false;
		salvataggio.meh.setpalla=false;
		salvataggio.meh.Save ();
		callLevel.meh.CallLevel ("play");
	}

	public void Continue(){
//		Application.LoadLevel ("1");
		callLevel.meh.CallLevel ("play");
	}

	public void Score(){
//		Application.LoadLevel ("score");
		callLevel.meh.CallLevel ("score");
	}
}
