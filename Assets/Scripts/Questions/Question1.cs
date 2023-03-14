using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question1 : MonoBehaviour
{
	//List<string> _question = new List<string> {"??? ??? ????? ???????"};
	//List<string> _answers = new List<string> { "???????", "??????? ????", "???? ?????", "??????? ????" };

	List<string> _question = new List<string> { "Who's Davids father?" };
	List<string> _answers = new List<string> { "Sanasar", "Mher junior", "Uncle Toros", "Mher senior" };
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
			UIController._instance.PopUpQuestion(_question, _answers, 3);
		}
	}
}
