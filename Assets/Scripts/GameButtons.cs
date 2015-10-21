using UnityEngine;
using System.Collections;

public class GameButtons : MonoBehaviour {
    
    public void AgainClicked()
    {
        Camera.main.GetComponent<AudioManager>().PlayButton();

        Application.LoadLevel(1);
    }

    public void MenuClicked()
    {
        Camera.main.GetComponent<AudioManager>().PlayButton();

        Application.LoadLevel(0);
    }
}
