using UnityEngine;
using System.Collections;


public class Score : MonoBehaviour {
	public static Score meh;

	private int score;
	private int plays;
	

	public void Awake()
	{
		if (meh == null) {
			DontDestroyOnLoad (gameObject);
			meh = this;
		}
		else if (meh != this) {
			Destroy(gameObject);
		}
	}

	public void SetScore(int aux){
		score= score+aux;
		plays = plays+1;
		salvataggio.meh.inCorso = true;
		salvataggio.meh.punteggio = score;
		salvataggio.meh.giocate = plays;
		salvataggio.meh.Save ();
	}

	public void TakeSaves(){
		score = salvataggio.meh.punteggio;
		plays = salvataggio.meh.giocate;
	}
	
	public void Reset(){
		score = 0;
		plays = 0;
	}

	public int GetScore(){
		return score;
	}

	public int GetPlays(){
		return plays;
	}
}
