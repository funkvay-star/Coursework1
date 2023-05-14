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

	[SerializeField] private Text _lifeCount;

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
		UpdateLifeCount();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateLifeCount()
	{
		_lifeCount.text = PlayerHealthController._instance.Life.ToString() + "x";
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