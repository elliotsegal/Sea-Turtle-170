using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    void OnTriggerEnter()
    {
        GameManager.singleton.EndLevel(true);
    }
}
