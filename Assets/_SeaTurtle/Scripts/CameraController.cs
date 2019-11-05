using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        relativePosition = player.transform.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.TransformPoint(relativePosition);
        transform.forward = player.transform.position - transform.position;
    }
}
