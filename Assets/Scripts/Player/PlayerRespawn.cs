using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Sound that will play when player reaches a checkpoint
    private Transform currentCheckpoint; //store the last checkpoint here
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
    }

    public void CheckRespawn()
    {
        //Check if checkpoint is available
        if(currentCheckpoint == null)
        {
            //Show game over menu
            uiManager.GameOver();
            return;
        }

        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();

        //Move camera to checkpoint room
        Camera.main.GetComponent<CameraController>().moveToNewRoom(currentCheckpoint.parent);
    }

    //Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //Save the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deacivate collider of activated checkpoint
            collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger the appear animation of checkpoint
        }
    }
}
