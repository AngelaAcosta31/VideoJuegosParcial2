using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;
	public int selectedCharacter = 0;
	
	public PlayerInput playerInput;

	public int ID;

	public Camera camara;

	public TextMeshProUGUI NameTxt;
	public TextMeshProUGUI ReadyTxt;

	public bool Ready = false;


	void Start()
    {
        playerInput = GetComponent<PlayerInput>();

		playerInput.actions["Accept"].performed += ctx => OnAccepttReleased();

		playerInput.actions["Next"].performed += ctx => OnNextReleased();
		playerInput.actions["Prev"].performed += ctx => OnPrevReleased();
		NameTxt.text = $"Jugador {playerInput.playerIndex + 1}";
		PlayerPrefs.SetInt("CountPlayers", playerInput.playerIndex+1);

    }
	void OnAccepttReleased()
    {
        Ready = !Ready;
		if (Ready){
			PlayerPrefs.SetInt($"selectedCharacter{playerInput.playerIndex}", selectedCharacter);
			ReadyTxt.enabled = true;
		}else{
			ReadyTxt.enabled = false;
		}
    }

    void OnNextReleased()
    {
		if (!Ready){
        	NextCharacter();
		}
    }

    void OnPrevReleased()
    {
		if (!Ready){
        	PreviousCharacter();
		}
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }
}
