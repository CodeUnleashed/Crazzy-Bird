using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
    // The number of blocks and background to be spawned at the start
    public int StartBlocks, StartBackground, StartClouds;

    // The types of blocks, backgrounds and clouds
    public GameObject blocks, background, clouds;

    // The position of the bird
    public Transform BirdPosition;

    // The properties of generating the background
    public float BackgroundPositionX, BackgroundSpacing;

    // The properties of generating the blocks
    public float BlockSpacing, BlockWidth, BlockRange;

    // The properties of generating the clouds
    public float CloudPositionX, CloudPositionY, CloudSpacing, CloudWidth, CloudRange;

    // The speed of the cloud to create a parallax backgroud
    public int CloudSpeed;

    // Whether or not spawning blocks, or gen new block
    private bool spawnBlocks, genNewBlock;

    // X position of block, ratio to decrease spacing
    private float blockX, blockRatio;

    // Use this for initialization
    void Start () {
        spawnBlocks = false;
        blockRatio = 1;
        genBackground(StartBackground);
        genCloud(StartClouds);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        if (other.name.Contains("Stack"))
        {
            if (genNewBlock)
            {
                genBlocks();
                genNewBlock = false;
            }
            else
                genNewBlock = true;
        }
        else if (other.name.Contains("Ground"))
            genBackground();
        else if (other.name.Contains("Cloud"))
            genCloud();
    }

    private void genCloud() { genCloud(1); }

    private void genCloud(int amount)
    {
        if (amount <= 0)
            return;

        Vector3 newPosition = clouds.transform.position;

        newPosition.x = CloudPositionX;
        newPosition.y = Random.Range(0, CloudRange) + CloudPositionY;

        clouds.transform.position = newPosition;
        clouds.GetComponent<CameraMovement>().HorizontalSpeed = CloudSpeed;

        Instantiate(clouds);

        CloudPositionX += Random.Range(0, CloudSpacing) + CloudWidth;

        genCloud(amount - 1);
    }

    private void genBackground() { genBackground(1); }

    private void genBackground(int amount)
    {
        if (amount <= 0)
            return;

        Vector3 newPosition = background.transform.position;

        newPosition.x = BackgroundPositionX;

        background.transform.position = newPosition;

        Instantiate(background);

        BackgroundPositionX += BackgroundSpacing;

        genBackground(amount - 1);
    }

    private void genBlocks() { genBlocks(1); }

    private void genBlocks(int amount)
    {
        if (amount <= 0)
            return;

        Vector3 newPosition = blocks.transform.position;

        newPosition.x = blockX;
        newPosition.y = Random.Range(0, BlockRange);

        blocks.transform.position = newPosition;

        Instantiate(blocks);

        blockX += BlockSpacing * blockRatio + BlockWidth;

        if (blockRatio > 0)
            blockRatio -= 0.05f;

        genBlocks(amount - 1);
    }

    public void EnableBlocks()
    {
        if (spawnBlocks)
            return;

        spawnBlocks = true;

        blockX = BirdPosition.position.x + 10;

        genBlocks(StartBlocks);
    }
}
