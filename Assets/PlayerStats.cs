using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public static float _currentHappiness;
	public static float _currentHunger;
	public static float _currentEnergy;
	public static int _currentSoftCurrency;

	GameObject _gm;
	Interface _interface;

	// Use this for initialization
	void Start () 
	{
		_gm = GameObject.FindWithTag ("GameManager");
		_interface = _gm.GetComponent<Interface> ();
		_currentSoftCurrency = 450;
		_currentHunger = 100f;
		_currentEnergy = 100f;
	}
		
	public void ChangeHappiness(float toAdd)
	{
		if(_currentHappiness <= 0)
		{
			_currentHappiness = 0;
		}
		_currentHappiness += toAdd;
		_interface.RefreshStatsUI ();
	}

	public void ChangeHunger(float toAdd)
	{
		_currentHunger += toAdd;
		_interface.RefreshStatsUI ();
	}

	public void ChangeEnergy(float toAdd)
	{
		_currentEnergy += toAdd;
		_interface.RefreshStatsUI ();
	}

	public void ChangeSoftCurrency(int adj)
	{
		_currentSoftCurrency += adj;
		_interface.RefreshStatsUI ();
	}
}
