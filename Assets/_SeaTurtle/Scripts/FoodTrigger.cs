using UnityEngine;

public class FoodTrigger : MonoBehaviour
{
    public GameObject eatFX;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player == GameManager.singleton.player)
        {
            Instantiate(eatFX, transform.position, player.transform.rotation);
            Destroy(gameObject);
            player.OnFoodEaten();
        }
    }
}
