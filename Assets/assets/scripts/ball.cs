using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class ball : MonoBehaviour {
	public Rigidbody2D palla;
	public GameObject enemy;
	public GameObject arrow;
	public TextMesh testo;
	public TextMesh tap;
	public TextMesh punteggio;
	public TextMesh su;
	public TextMesh giocate;
	public eh punto;
	public velocity vel;
	public BlurOptimized ciao;
	public Camera gianni;
	public tapreset tapreset;

	private Color color;
	private bool blur=false;
	private int velocity;
	private GameObject cLight;
	private bool partito= false;
	private bool finito=true;
	private float angle;

	int x,y,x1,y2;
	float velx,vely;

	int Boh(int x){
		if (Random.Range (0, 2) == 0)
			return x;
		else
			return -x;
	}

	Vector2 Cal_Pos_Palla(){
		float xl = (GameObject.Find ("left").transform.position.x + GameObject.Find ("left").GetComponent<Renderer> ().bounds.extents.x)+2.0f;
		float xr = (GameObject.Find ("right").transform.position.x - GameObject.Find ("right").GetComponent<Renderer> ().bounds.extents.x)-2.0f;
		float yt = (GameObject.Find ("top").transform.position.y - GameObject.Find ("top").GetComponent<Renderer> ().bounds.extents.y)-2.0f;
		float yb = (GameObject.Find ("bottom").transform.position.y + GameObject.Find ("bottom").GetComponent<Renderer> ().bounds.extents.y)+2.0f;
//		print ("xl : " + xl);
//		print ("xr : " + xr);
//		print ("yt : " + yt);
//		print ("yb : " + yb);
		Vector2 pallarand = new Vector2 (Random.Range (xl, xr), Random.Range (yb, yt));
		Collider2D hitColliders = Physics2D.OverlapCircle(pallarand,0.6f);
		while (hitColliders!=null) {
			pallarand = new Vector2 (Random.Range (xl, xr), Random.Range (yb, yt));
			hitColliders = Physics2D.OverlapCircle(pallarand,0.6f);
		}
		return pallarand;
	}
	
	void Start () {

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		if (Screen.height == 480) {
			GameObject.Find ("Main Camera").GetComponent<Camera> ().orthographicSize = 7.43f;
			GameObject.Find ("Main Camera 1").GetComponent<Camera> ().orthographicSize = 7.43f;
		}
		cLight = GameObject.Find("2DLight");
		palla = GetComponent<Rigidbody2D> ();
		color = testo.color;
		salvataggio.meh.Load ();
		if (salvataggio.meh.partita) {
			vel.SetVelocity (salvataggio.meh.velocitaInz);
			palla.position = salvataggio.meh.pallaPos.V2;
			GameObject.Find ("target").transform.position = salvataggio.meh.puntatore.V3;
			Destroy(arrow);
			Destroy(GameObject.Find("cursore"));
			palla.velocity = salvataggio.meh.pallaVel.V2;
			partito = true;
		} else if (salvataggio.meh.setpalla) {
			x =(int) salvataggio.meh.pallaVel.x;
			y =(int) salvataggio.meh.pallaVel.y;
			print (salvataggio.meh.pallaVel.V2);
			print((int) salvataggio.meh.pallaVel.y);
			palla.position = salvataggio.meh.pallaPos.V2;
			arrow.transform.position = new Vector3 (palla.position.x, palla.position.y, -3.0f);
			angle = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
			arrow.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
			vel.SetVelocity (salvataggio.meh.velocitaInz);
		} else {
			velocity = Random.Range (6, 16);
			vel.SetVelocity (velocity);
			x = Random.Range (1, velocity);
			y = Boh (velocity - x);
			x = Boh (x);
			palla.position = Cal_Pos_Palla ();
			arrow.transform.position = new Vector3 (palla.position.x, palla.position.y, -3.0f);
			angle = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
			arrow.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
			salvataggio.meh.setpalla=true;
			salvataggio.meh.velocitaInz=velocity;
			salvataggio.meh.pallaPos.V2=palla.position;
			salvataggio.meh.pallaVel.V2=new Vector2(x,y);
			print (salvataggio.meh.pallaVel.V2);
			salvataggio.meh.Save();
		}
//		aux.AddForce(new Vector2(x,y), ForceMode2D.Impulse);
		cLight.transform.position = palla.position;
//		aux.AddForce(new Vector2(4,4), ForceMode2D.Impulse);
//		print ("ciao: "+aux.velocity);
//		y = Boh (Random.Range (1, 15));
//		x = Boh (Random.Range (1, 15));
//		aux.AddForce(new Vector2(x,y), ForceMode2D.Impulse);

//		aux.AddForce(new Vector2(-5.6f,-3.3f), ForceMode2D.Impulse);
		if(salvataggio.meh.inCorso)
			Score.meh.TakeSaves();
		else
			Score.meh.Reset ();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			salvataggio.meh.pallaPos.V2=palla.position;
			if(partito)
				salvataggio.meh.pallaVel.V2 =palla.velocity;
			salvataggio.meh.Save();
			callLevel.meh.CallLevel("menu");
		}
		if (partito) {
//			palla.AddForce (new Vector2 (-palla.velocity.x * 0.2f, -palla.velocity.y * 0.2f));
			cLight.transform.position = palla.position;
//		Instantiate(enemy,aux.position,Quaternion.identity);
//			print (palla.velocity);
			if((palla.velocity.y<0.1f && palla.velocity.y>-0.1f) && (palla.velocity.x<0.1f && palla.velocity.x>-0.1f)){
				palla.AddForce(new Vector2 (-palla.velocity.x, -palla.velocity.y ));
				if(finito)
					calcolapunteggio();
			}
			velx = palla.velocity.x;
			vely = palla.velocity.y;
//			print (palla.velocity);
		} 
		if (blur) {
			if(ciao.blurSize<3.0f){
				ciao.blurSize=ciao.blurSize+0.2f;
				color.a+=0.066f;
				testo.color=color;
				tap.color=color;
				punteggio.color=color;
				su.color=color;
				giocate.color=color;
			}
			else
				tapreset.enabled=true;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
//		print ("prima ("+velx+" , "+vely+")");
//		print ("dopo ("+palla.velocity.x+" , "+palla.velocity.y+")");

		if (col.gameObject.name == "top" || col.collider.name=="topc") {
			if(palla.velocity.y<0.1f && palla.velocity.y>-0.1f && (palla.velocity.x>0.1f || palla.velocity.x<-0.1f)){
				if(vely<0.2f)
					palla.AddForce (new Vector2 (0, -0.1f),ForceMode2D.Impulse);
				else
					palla.AddForce (new Vector2 (0, -(vely-0.08f)),ForceMode2D.Impulse);
//				print ("y: "+palla.velocity);
			}
		}

		if (col.gameObject.name == "bottom" || col.collider.name == "bottomc") {
			if(palla.velocity.y<0.1f && palla.velocity.y>-0.1f && (palla.velocity.x>0.1f || palla.velocity.x<-0.1f)){
				if(vely>-0.2f)
					palla.AddForce (new Vector2 (0, +0.1f),ForceMode2D.Impulse);
				else
					palla.AddForce (new Vector2 (0, -(vely+0.08f)),ForceMode2D.Impulse);
//				print ("y: "+palla.velocity);
			}
		}

		if (col.gameObject.name == "right" || col.collider.name == "rightc") {
			if (palla.velocity.x<0.1f && palla.velocity.x>-0.1f && (palla.velocity.y>0.1f || palla.velocity.y<-0.1f)) {
				if(velx<0.2f)
					palla.AddForce (new Vector2 (-0.1f, 0),ForceMode2D.Impulse);
				else
					palla.AddForce (new Vector2 (-(velx-0.08f), 0),ForceMode2D.Impulse);
//				print ("x: "+palla.velocity);
			}
		}

		if (col.gameObject.name == "left" || col.collider.name == "leftc") {
			if (palla.velocity.x<0.1f && palla.velocity.x>-0.1f && (palla.velocity.y>0.1f || palla.velocity.y<-0.1f)) {
				if(velx>-0.2f)
					palla.AddForce (new Vector2 (+0.1f, 0),ForceMode2D.Impulse);
				else
					palla.AddForce (new Vector2 (-(velx+0.08f), 0),ForceMode2D.Impulse);
//				print ("x: "+palla.velocity);
			}
		}
//		print ("dopo script ("+palla.velocity.x+" , "+palla.velocity.y+")");
	}

	public void StartBall(){
		palla.AddForce (new Vector2 (x, y), ForceMode2D.Impulse);
		partito = true;
		salvataggio.meh.pallaVel.V2 = new Vector2 (x, y);
		salvataggio.meh.puntatore.V3=GameObject.Find("target").transform.position;
		salvataggio.meh.pallaPos.V2 = palla.position;
		salvataggio.meh.partita = true;
		salvataggio.meh.Save ();
		Destroy(arrow);
	}

	void calcolapunteggio(){
		print ("?");
		Vector2 vabbe = GameObject.Find("target").transform.position;
//		print ("coordinate punto: " + vabbe);
//		print ("coordinate palla: " + palla.position);
		if (Vector3.Distance (vabbe, palla.position) < 4.42) {
			float aux=1000 / (Vector3.Distance (vabbe, palla.position) + 1.0f);
			int aux2 = (int)Mathf.CeilToInt (aux);
//			scores.SetScore (aux2);
//			punteggio.text=scores.GetScore().ToString()+" Points";
//			giocate.text=scores.GetPlays().ToString()+" Plays";
			print (aux2);
			Score.meh.SetScore (aux2);
			punteggio.text=Score.meh.GetScore().ToString()+" Points";
			giocate.text=Score.meh.GetPlays().ToString()+" Plays";
			testo.text = "You win";
		} 
		else {
			testo.text = "Meh!";
//			punteggio.text=scores.GetScore().ToString()+" Points";
//			giocate.text=scores.GetPlays().ToString()+" Plays";
			punteggio.text=Score.meh.GetScore().ToString()+" Points";
			giocate.text=Score.meh.GetPlays().ToString()+" Plays";
			Destroy(GameObject.Find("Score"));
			if(Score.meh.GetScore()!=0)
				salvataggio.meh.saveScore();
			salvataggio.meh.inCorso=false;
			salvataggio.meh.punteggio=0;
			salvataggio.meh.giocate=0;
			salvataggio.meh.Save();
		}
		salvataggio.meh.partita = false;
		salvataggio.meh.setpalla = false;
		salvataggio.meh.Save ();
		GameObject.Find ("meh").transform.position = new Vector2 (0, 0);
		ciao.enabled = true;
		blur = true;
		gianni.enabled = true;
		finito = false;
	}
}
