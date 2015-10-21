using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BirdMovemet : MonoBehaviour {
    // The adcontrol to display the ad
    public AdControl AdToDisplay;

    // The medals to be awarded
    public Sprite SadBird, HappyBird;

    // The menu for when the bird dies
    public Canvas EndMenu, PointsSystem;

    // The text for the points
    public Text PointsText, ScoreText, BestText;

    // The image for new best score
    public Image newImage, medalImage;

    // The terrain generator
    public TerrainGenerator Generator;

    // The gravity and flap forces
    public Vector3 Flap, Gravity;

    // The maximum height of the bird
    public float MaxHeight;
    
    // Whether or not tapped and flapped and dead and grounded
    private bool tapped, flapped, dead, grounded;

    // The velocity vector
    private Vector3 velocity;

    // The audio manager to play the sounds
    private AudioManager audioManager;

    // The points
    private int points;

    // Use this for initialization
	void Start ()
    {
        audioManager = Camera.main.GetComponent<AudioManager>();    // Getting the sound manager

        AdToDisplay.RequestInterstitial();          // Requesting an ad

        points = 0;                                 // Resetting the points to 0
        grounded = dead = tapped = flapped = false; // Resetting start booleans to false
        changeMenuVisibility(false);                // Displaying the bird and points

        medalImage.sprite = SadBird;                // Setting a default medal for the user
        newImage.enabled = false;                   // Hiding the medal
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            // Indicating that the bird flapped
            tapped = flapped = true;
            // Indicating that the next image should be shown
            GetComponent<Animator>().SetBool("Tapped", true);
            // Indicating flapped
            GetComponent<Animator>().SetBool("Flapped", true);
            // Generating the obsticles
            Generator.EnableBlocks();

            // Playing the flap sound
            audioManager.PlayFlap();
        }
    }

    void FixedUpdate()
    {
        if (!tapped || grounded)
            return;

        velocity -= Gravity * Time.deltaTime;    // Adding gravity to the bird
        
        if (flapped && !dead)
        {
            velocity.y  = 0;
            velocity    += Flap;
            flapped     = false;       
                 
            // Indicating flapped finished
            GetComponent<Animator>().SetBool("Flapped", false);
        }

        velocity = Vector3.ClampMagnitude(velocity, MaxHeight);

        rotateBird();

        transform.position  += velocity * Time.deltaTime;   // Setting the new bird position
    }

    private void rotateBird()
    {
        float angle = 25;

        // Rotating the bird when falling
        if (velocity.y < 0)
            angle += Mathf.Lerp(0, -90, -velocity.y / Flap.y);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Point"))
        {
            audioManager.PlayScore();

            points++;

            PointsText.text = points + "";
        }
        else if (other.name.Contains("Stack") || other.name.Contains("Roof"))
        {
            if (dead)
                return;

            killBird();

            if (velocity.y > 0)
                velocity.y = 0;

            Gravity.y *= 2;

            audioManager.PlayFall();
        }
        else if (other.name.Contains("Ground"))
        {
            grounded = true;

            // Indicating grounded
            GetComponent<Animator>().SetBool("Grounded", true);

            if (!dead)
                killBird();

            changeMenuVisibility(true);

            AdToDisplay.ShowInterstitial();

            loadScores();
        }
    }

    private void loadScores()
    {
        int best = 0;

        if (PlayerPrefs.HasKey("HighScore0"))
            best = PlayerPrefs.GetInt("HighScore0");

        if(best < points)
        {
            best = points;
            newImage.enabled = true;
            medalImage.sprite = HappyBird;
        }

        saveScore();
        
        ScoreText.text  = points + "";
        BestText.text   = best + "";
    }

    private void saveScore()
    {
        int currScore = points;
        int tempScore = 0;

        for (int i = 0; i < 10; i++)
        {
            if (!PlayerPrefs.HasKey("HighScore" + i)){
                PlayerPrefs.SetInt("HighScore" + i, currScore);
                currScore = tempScore;

                break;
            }
            else if((tempScore = PlayerPrefs.GetInt("HighScore" + i)) < currScore){
                PlayerPrefs.SetInt("HighScore" + i, currScore);
                currScore = tempScore;
            }
        }
    }

    private void killBird()
    {
        audioManager.PlayHit();

        dead = true;

        GetComponent<Animator>().SetBool("Dead", true);
        Camera.main.GetComponent<CameraMovement>().Stop();
    }

    private void changeMenuVisibility(bool isVisible)
    {
        EndMenu.enabled         = isVisible;
        PointsSystem.enabled    = !isVisible;
    }
}
