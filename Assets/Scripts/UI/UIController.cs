using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
	public static UIController _instance;

	[SerializeField] private Image _heart1, _heart2, _heart3, _popup;

	[SerializeField] private Sprite _fullHeart, _halfHeart, _emptyHeart;

	[SerializeField] private Text _gemsText, _popupContent;

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

	private void Hide()
	{
		LevelManager._instance._gamePaused = false;
		_popup.gameObject.SetActive(false);
	}

	public void PopUp(List<string> conversation)
	{
		LevelManager._instance._gamePaused = true;
		_popup.gameObject.SetActive(true);

		_conversationIndex = 0;

		StartCoroutine(ShowNextText());

		IEnumerator ShowNextText()
		{
			_popupContent.text = conversation[_conversationIndex];
			yield return new WaitForSeconds(2f);

			while(true)
			{
				if(Input.GetKeyUp(KeyCode.Space))
				{
					break;
				}
				else 
				{
					yield return null;
				}
			}

			++_conversationIndex;

			if(_conversationIndex < conversation.Count)
			{
				StartCoroutine(ShowNextText());
			}
			else
			{
				Hide();
				PlayerMovement._instance.GiveSword();
			}
		}
	}
}
