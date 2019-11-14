using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerController>() == GameManager.singleton.player)
        {
            GameManager.singleton.EndLevel(true);
        }
    }
}
