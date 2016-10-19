using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interface : MonoBehaviour {

	//general variables
	public GameObject _player;
	GameObject _gameManager;
	GameManager _gm;
	PlayerStats _playerStats;

	//touch related variables
	Vector3 _touchPosition;
	Touch _touch;

	//context item related variables
	public GameObject _item;
	Vector3 _defaultItemPosition;
	bool _canMove;

	public Text _happinessIcon, _hungerIcon, _energyIcon, _coinText;

	void Awake()
	{
		_defaultItemPosition = _item.transform.position;
		_player = GameObject.FindWithTag ("Player");
		_gameManager = GameObject.FindWithTag ("GameManager");
		_gm = _gameManager.GetComponent<GameManager> ();
		_coinText.text = PlayerStats._currentSoftCurrency.ToString("");
	}

	void Update ()
	{
		if (Input.touchCount > 0) 
		{
			_touch = Input.GetTouch (0);
			_touchPosition = Camera.main.ScreenToWorldPoint (_touch.position);

			ItemInteraction ();
		}
	}

	public void RefreshStatsUI()
	{
		_happinessIcon.text = "H: " +PlayerStats._currentHappiness.ToString ("0");
		_hungerIcon.text = "F: " + PlayerStats._currentHunger.ToString ("0");
		_energyIcon.text = "E: " + PlayerStats._currentEnergy.ToString ("0");
		_coinText.text = PlayerStats._currentSoftCurrency.ToString("0");
	}

	//Interaction with context item 
	void ItemInteraction()
	{
		if (_item.GetComponent<Collider2D> ().OverlapPoint (_touchPosition)) 
		{	
			_canMove = true;
		}

		if (_canMove) 
		{
			if (_touch.phase == TouchPhase.Moved) 
			{
				_item.transform.position = new Vector3 (_touchPosition.x, _touchPosition.y, 0f);
			}
			if (_touch.phase == TouchPhase.Ended) 
			{
				if (_player.GetComponent<Collider2D>().OverlapPoint(_touchPosition))
				{
					Destroy (_item);
					GameObject newItem = GameObject.Instantiate (_item, _defaultItemPosition, Quaternion.identity) as GameObject;
					newItem.GetComponent<Collider2D> ().enabled = true;
					_item = newItem;
				}
				else
				{
					_item.transform.position = _defaultItemPosition;
				}
				_canMove = false;
			} 	
		}
	}

	public void OpenShopInterface()
	{
		//open shop
	}
}