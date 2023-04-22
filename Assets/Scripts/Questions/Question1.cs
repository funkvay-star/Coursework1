using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question1 : MonoBehaviour
{
	//List<string> _question = new List<string> {"Дратути"};
	//List<string> _answers = new List<string> { "???????", "??????? ????", "???? ?????", "??????? ????" };

	List<string> _question = new List<string> { "Кто был отцом Давида?" };
	List<string> _answers = new List<string> { "Санасар", "Младший Мгер", "Дядя Торос", "Старший Мгер" };
	bool _firstTriggerEnter;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !_firstTriggerEnter)
		{
			_firstTriggerEnter = true;
			UIController._instance.PopUpQuestion(_question, _answers, 3);
		}
	}
}
