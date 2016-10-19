using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//public float _currentHappiness, _currentHunger;
	float _currentTime, timer = 5f;

	GameObject _gm;
	Interface _interface;
	PlayerStats _playerStats;

	public SpriteRenderer _sRenderer;
	public Sprite yard_sprite, kitchen_sprite, treehouse_sprite;
	public Text _locationText;

	bool isPaused = false;


	void Awake () 
	{
		_gm = GameObject.FindWithTag ("GameManager");
		_interface = _gm.GetComponent<Interface> ();
		_playerStats = _gm.GetComponent<PlayerStats> ();
		_locationText.text = _currentLocation.ToString ("");
	}

	void Update () 
	{
		Debug.Log ("Game is running");
		_currentTime += Time.deltaTime;
		if(_currentTime >= timer)
		{
			_playerStats.ChangeHappiness (-1);
			_playerStats.ChangeHunger (-1);
			_currentTime = 0f;
		}
	}

	float paused_currentHappiness;
	float paused_currentHunger;
	int _paused_currentSoftCurrency;
	DateTime pausedTimeStamp;

	public void OnApplicationPause (bool pausedStatus)
	{
		isPaused = !pausedStatus;
		DateTime oldDate;

		if (!isPaused) 
		{
			paused_currentHappiness = PlayerStats._currentHappiness;
			paused_currentHunger = PlayerStats._currentHunger;
			_paused_currentSoftCurrency = PlayerStats._currentSoftCurrency;
			PlayerPrefs.SetFloat ("Paused_currentHappiness", paused_currentHappiness);
			PlayerPrefs.SetFloat ("Paused_currentHunger", paused_currentHappiness);
			PlayerPrefs.SetFloat ("Paused_currentSoftCurrency", paused_currentHappiness);
			PlayerPrefs.SetString ("PausedTimeStamp", System.DateTime.Now.ToBinary ().ToString ());
		}
	    else 
	    {
			if(PlayerPrefs.HasKey("PausedTimeStamp"))
			{
				long temp = Convert.ToInt64(PlayerPrefs.GetString("PausedTimeStamp"));
				oldDate = DateTime.FromBinary(temp);
				paused_currentHappiness = PlayerPrefs.GetFloat ("Paused_currentHappiness");
			}
			else
			{ //this should only run the first time the app is started, due to PausedTimeStamp not existing, until app is minimized/slept
				paused_currentHappiness = 100f;
				oldDate = DateTime.Now;
			}
        	//Use the Subtract method and store the result as a timespan variable
			TimeSpan lapsedTime = DateTime.Now.Subtract(oldDate); //lapsed time works properly!
			//Debug.Log ("Lapsed Time: " +(float)lapsedTime.TotalSeconds);
			PlayerStats._currentHappiness = paused_currentHappiness - (float)lapsedTime.TotalSeconds; //new _currentHappiness is equal to _currentHappiness at paused minus the lapsed time when the app was paused
			PlayerPrefs.SetFloat ("saved_currentHappiness", PlayerStats._currentHappiness);
	    }
		PlayerPrefs.Save ();
	}
		
	enum Locations 
	{
		Yard,
		Treehouse,
		Kitchen
	}

	Locations _currentLocation = Locations.Yard;
	public int _currentlocationIndex = 0;

	public void ChangeLocation(int adjust)
	{
		int _newLocationIndex = 0;

		//need to loop the number

		_newLocationIndex = _currentlocationIndex += adjust;
		_currentLocation = (Locations)_newLocationIndex; //cast the enum to the int, to access the index

		_locationText.text = _currentLocation.ToString ("");

		if(_currentLocation == Locations.Yard)
		{
			_sRenderer.sprite = yard_sprite;
		}
		else if(_currentLocation == Locations.Kitchen)
		{
			_sRenderer.sprite = kitchen_sprite;
		}
		else
		{
			_sRenderer.sprite = treehouse_sprite;	
		}
	}
}
