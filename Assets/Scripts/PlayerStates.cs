using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {

	Rigidbody2D myRigidbody;
	Animator myAnim;
	enum States {idle, wander, jump};
	States currentState;
	public GameObject eye1, eye2;

	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		//myAnim = GetComponent<Animator> ();
		currentState = States.idle;
		Idle ();
	}
	
	// Update is called once per frame
	void Update ()
	{	
		Wander ();
	}

	void ChangeState()
	{
		switch(currentState)
		{
		case States.idle:
			Idle ();
			break;
		case States.wander:
			Wander ();
			break;
		case States.jump:
			Jump ();
			break;
		}
	}

	void Idle()
	{
		InvokeRepeating ("Blink", 1f, 3f);

	}

	void Blink()
	{
		StartCoroutine ("BlinkAnim");
		//Jump ();
	}

	IEnumerator BlinkAnim()
	{
		float rand = Random.Range (2f, 4f);
		yield return new WaitForSeconds (rand);
		eye1.SetActive (false);
		eye2.SetActive (false);
		yield return new WaitForSeconds (.15f);
		eye1.SetActive (true);
		eye2.SetActive (true);
		//myAnim.SetTrigger ("Blink");
	}

	void Wander()
	{
//		if(transform.position.x < 4 || transform.position.x > -4)
//		{
//			myRigidbody.velocity = new Vector2 (1f, 0f);
//		}

	}

	public void Jump()
	{
		myRigidbody.AddForce (transform.up * 300f);
		Debug.Log ("JUMPED");
	}
}
