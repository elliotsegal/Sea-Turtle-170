using UnityEngine;

public class Level1EndTrigger : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter()
    {
        gameManager.CompleteLevel();


        
    }
}
