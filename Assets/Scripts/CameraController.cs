using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform player;
    
    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private float zOffset = 5f;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + xOffset, 
            player.position.y + yOffset, player.position.z + zOffset);
    }
}
