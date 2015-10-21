using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {
    // The menus
    public Canvas MainMenu, QuitMenu;
    
    // The audio manager playing the button sound
    private AudioManager manager;
	
    // Use this for initialization
	void Start () {
        manager = Camera.main.GetComponent<AudioManager>(); // Getting the audio manager from the main camera
        changeVisibility(true);     // Showing the main menu and hiding the quit menu
	}

    /// <summary>
    /// Handling the Play button press
    /// </summary>
    public void PlayClicked()
    {
        manager.PlayButton();       // Playing the button sound
        Application.LoadLevel(1);   // Going to the game scene
    }

    /// <summary>
    /// Handling Rank button press
    /// </summary>
    public void RankClicked()
    {
        manager.PlayButton();       // Playing the button sound
        Application.LoadLevel(2);   // Going to the ranks scene
    }

    /// <summary>
    /// Handling the Back button press
    /// </summary>
    public void BackClicked()
    {
        manager.PlayButton();       // Playing the button sound
        changeVisibility(false);    // Showing the quit menu and hiding the main menu
    }

    /// <summary>
    /// Handling the Rate button press
    /// </summary>
    public void RateClicked()
    {
        manager.PlayButton();       // Playing the button sound
#if UNITY_ANDROID
     Application.OpenURL("market://details?id=com.QCoder101.CrazyBird");
#elif UNITY_IPHONE
     Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_APP_ID");
#else
    Application.OpenURL("http://windowsphone.com/s?appId=9d682bb5-0fe6-4439-9fde-39f49f674e8b");
#endif
    }

    public void YesClicked()
    {
        manager.PlayButton();       // Playing the button sound
        Application.Quit();
    }

    public void NoClicked()
    {
        manager.PlayButton();       // Playing the button sound
        changeVisibility(true);     // Showing the main menu and hiding the quit menu
    }

    private void changeVisibility(bool showMain)
    {
        MainMenu.enabled = showMain;
        QuitMenu.enabled = !showMain;
    }
}
