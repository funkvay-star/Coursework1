using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;
using TMPro;

public class UIController : MonoBehaviour
{
	public static UIController _instance;

	[SerializeField] private Image _heart1, _heart2, _heart3, _popup, _popupQuestion, _popupQuestionImage;

	[SerializeField] private Button _popupQuestionButton1, _popupQuestionButton2, _popupQuestionButton3, _popupQuestionButton4;

	[SerializeField] private Sprite _fullHeart, _halfHeart, _emptyHeart;

	[SerializeField] private Text _gemsText, _popupContent, _popupQuestionContent;

	[SerializeField] private TextMeshProUGUI _buttonText1, _buttonText2, _buttonText3, _buttonText4;

	private bool _button1, _button2, _button3, _button4;

	private int _conversationIndex;

	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		UpdateGems();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateHealthDisplay()
	{
		//Debug.Log(PlayerHealthController._instance.Health);
		switch (PlayerHealthController._instance.Health)
		{
			case 6:
				_heart1.sprite = _fullHeart;
				_heart2.sprite = _fullHeart;
				_heart3.sprite = _fullHeart;
				break;

			case 5:
				_heart1.sprite = _fullHeart;
				_heart2.sprite = _fullHeart;
				_heart3.sprite = _halfHeart;
				break;

			case 4:
				_heart1.sprite = _fullHeart;
				_heart2.sprite = _fullHeart;
				_heart3.sprite = _emptyHeart;
				break;

			case 3:
				_heart1.sprite = _fullHeart;
				_heart2.sprite = _halfHeart;
				_heart3.sprite = _emptyHeart;
				break;

			case 2:
				_heart1.sprite = _fullHeart;
				_heart2.sprite = _emptyHeart;
				_heart3.sprite = _emptyHeart;
				break;

			case 1:
				_heart1.sprite = _halfHeart;
				_heart2.sprite = _emptyHeart;
				_heart3.sprite = _emptyHeart;
				break;

			case 0:
				_heart1.sprite = _emptyHeart;
				_heart2.sprite = _emptyHeart;
				_heart3.sprite = _emptyHeart;
				break;
		}
	}

	public void UpdateGems()
	{
		_gemsText.text = LevelManager._instance.Gems.ToString();
	}

	private void Hide(Image img)
	{
		LevelManager._instance._gamePaused = false;
		img.gameObject.SetActive(false);
	}

	public void PopUp(List<string> conversation, bool dzenovOhan = false)
	{
		LevelManager._instance._gamePaused = true;
		_popup.gameObject.SetActive(true);

		_conversationIndex = 0;

		StartCoroutine(ShowNextText());

		IEnumerator ShowNextText()
		{
			_popupContent.text = conversation[_conversationIndex];
			yield return new WaitForSeconds(1f);

			while (true)
			{
				if (Input.GetKeyUp(KeyCode.Space))
				{
					break;
				}
				else
				{
					yield return null;
				}
			}

			++_conversationIndex;

			if (_conversationIndex < conversation.Count)
			{
				StartCoroutine(ShowNextText());
			}
			else
			{
				Hide(_popup);
				//PlayerMovement._instance.GiveSword();
				if (dzenovOhan)
				{
					PlayerMovement._instance.GiveSword();
				}
			}
		}
	}

	//public void PopUpQuestion(List<string> question, List<string> answers, int rightAnswer)
	//{
	//	LevelManager._instance._gamePaused = true;
	//	_popupQuestion.gameObject.SetActive(true);

	//	_conversationIndex = 0;

	//	StartCoroutine(ShowNextText());

	//	IEnumerator ShowNextText()
	//	{
	//		_popupQuestionContent.text = question[_conversationIndex];
	//		//Text buttonText1 = _popupQuestionButton1.GetComponentInChildren<Text>();
	//		//Text buttonText2 = _popupQuestionButton2.GetComponentInChildren<Text>();
	//		//Text buttonText3 = _popupQuestionButton3.GetComponentInChildren<Text>();
	//		//Text buttonText4 = _popupQuestionButton4.GetComponentInChildren<Text>();

	//		_buttonText1.text = answers[0];
	//		_buttonText2.text = answers[1];
	//		_buttonText3.text = answers[2];
	//		_buttonText4.text = answers[3];

	//		//buttonText1.text = answers[0];
	//		//buttonText2.text = answers[1];
	//		//buttonText3.text = answers[2];
	//		//buttonText4.text = answers[3];

	//		int playerAnswer = -1;

	//		yield return new WaitForSeconds(1f);

	//		while (true)
	//		{
	//			// Check if any of the buttons were pressed
	//			if (_button1)
	//			{
	//				// Do something with button 1
	//				Debug.Log("Button 1 was pressed");
	//				playerAnswer = 1;
	//				_button1 = false; // reset the button flag

	//				CheckAnswer(playerAnswer, rightAnswer);
	//				break;
	//			}
	//			else if (_button2)
	//			{
	//				// Do something with button 2
	//				Debug.Log("Button 2 was pressed");
	//				playerAnswer = 2;
	//				_button2 = false; // reset the button flag
	//				CheckAnswer(playerAnswer, rightAnswer);
	//				break;
	//			}
	//			else if (_button3)
	//			{
	//				// Do something with button 3
	//				Debug.Log("Button 3 was pressed");
	//				playerAnswer = 3;
	//				_button3 = false; // reset the button flag
	//				CheckAnswer(playerAnswer, rightAnswer);
	//				break;
	//			}
	//			else if (_button4)
	//			{
	//				// Do something with button 4
	//				Debug.Log("Button 4 was pressed");
	//				playerAnswer = 4;
	//				_button4 = false; // reset the button flag
	//				CheckAnswer(playerAnswer, rightAnswer);
	//				break;
	//			}

	//			yield return new WaitForSeconds(1f);

	//			yield return null;
	//		}

	//		++_conversationIndex;

	//		if (_conversationIndex < question.Count)
	//		{
	//			StartCoroutine(ShowNextText());
	//		}
	//		else
	//		{
	//			Hide(_popupQuestion);
	//		}
	//	}

	//	// Add button event listeners
	//	_popupQuestionButton1.onClick.AddListener(() => _button1 = true);
	//	_popupQuestionButton2.onClick.AddListener(() => _button2 = true);
	//	_popupQuestionButton3.onClick.AddListener(() => _button3 = true);
	//	_popupQuestionButton4.onClick.AddListener(() => _button4 = true);
	//}

	[SerializeField] private Button _submitAnswerButton;

	private int _playerAnswer = -1;
	private bool _submitButtonPressed = false;

	public void PopUpQuestion(List<string> question, List<string> answers, int rightAnswer)
	{
		_popupQuestionContent.transform.parent.gameObject.SetActive(true);

		_conversationIndex = 0;

		StartCoroutine(ShowNextText());

		// Add button event listeners
		_popupQuestionButton1.onClick.AddListener(() => OnButtonClicked(1));
		_popupQuestionButton2.onClick.AddListener(() => OnButtonClicked(2));
		_popupQuestionButton3.onClick.AddListener(() => OnButtonClicked(3));
		_popupQuestionButton4.onClick.AddListener(() => OnButtonClicked(4));
		_submitAnswerButton.onClick.AddListener(() => _submitButtonPressed = true);

		_submitAnswerButton.interactable = false;

		IEnumerator ShowNextText()
		{
			_popupQuestionContent.text = question[_conversationIndex];

			_buttonText1.text = answers[0];
			_buttonText2.text = answers[1];
			_buttonText3.text = answers[2];
			_buttonText4.text = answers[3];

			yield return new WaitForSeconds(1f);

			while (true)
			{
				// Check if the submit button was pressed
				if (_submitButtonPressed)
				{
					_submitButtonPressed = false; // reset the submit button flag
					CheckAnswer(_playerAnswer, rightAnswer);
					break;
				}

				//yield return new WaitForSeconds(1f);

				yield return null;
			}

			++_conversationIndex;

			if (_conversationIndex < question.Count)
			{
				StartCoroutine(ShowNextText());
			}
			else
			{
				Hide(_popupQuestion);
			}
		}
	}

	private void OnButtonClicked(int answer)
	{
		_playerAnswer = answer;

		// Enable all buttons
		_popupQuestionButton1.interactable = true;
		_popupQuestionButton2.interactable = true;
		_popupQuestionButton3.interactable = true;
		_popupQuestionButton4.interactable = true;
		_submitAnswerButton.interactable = true;

		// Disable the clicked button
		switch (answer)
		{
			case 1:
				_popupQuestionButton1.interactable = false;
				break;
			case 2:
				_popupQuestionButton2.interactable = false;
				break;
			case 3:
				_popupQuestionButton3.interactable = false;
				break;
			case 4:
				_popupQuestionButton4.interactable = false;
				break;
		}
	}

	private void CheckAnswer(int playerAnswer, int rightAnswer)
	{
		// Perform your answer checking logic here

		// Reset buttons for the next question
		_popupQuestionButton1.interactable = true;
		_popupQuestionButton2.interactable = true;
		_popupQuestionButton3.interactable = true;
		_popupQuestionButton4.interactable = true;
	}

}