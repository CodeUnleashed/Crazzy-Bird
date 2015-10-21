using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoresScript : MonoBehaviour
{
    // The preface to read the score
    private const string PREFACE = "HighScore", EMPTY = "---EMPTY---";

    // The score labels
    public Text[] ScoreLabels;

	// Use this for initialization
	void Start ()
    {
        // Checking to see that the lables were entered
        if (ScoreLabels == null)
            return;

        // Getting the saved score
        for (int i = 0; i < ScoreLabels.Length; i++)
        {
            if (PlayerPrefs.HasKey(PREFACE + i))    // Getting the saved score
                ScoreLabels[i].text = PlayerPrefs.GetInt(PREFACE + i) + "";
            else                                    // Displaying no saved score
                ScoreLabels[i].text = EMPTY;
        }
	}

    public void AgainPressed()
    {
        Camera.main.GetComponent<AudioManager>().PlayButton();  // Playing the button sound
        Application.LoadLevel(1);
    }

    public void MenuPressed()
    {
        Camera.main.GetComponent<AudioManager>().PlayButton();  // Playing the button sound
        Application.LoadLevel(0);
    }
}
