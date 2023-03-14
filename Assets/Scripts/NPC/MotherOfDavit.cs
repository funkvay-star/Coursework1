using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherOfDavit : MonoBehaviour
{
	List<string> _conversation = new List<string> { "m1", "m2", "m3", "m4" };
	bool _firstTriggerEnter;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !_firstTriggerEnter)
		{
			_firstTriggerEnter = true;
			UIController._instance.PopUp(_conversation);
			//UIController._instance.PopUpQuestion(_conversation);
		}
	}
}
