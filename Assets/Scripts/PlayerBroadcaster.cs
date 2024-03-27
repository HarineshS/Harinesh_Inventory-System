using UnityEngine;

public class PlayerBroadcaster : MonoBehaviour
{
    public delegate void PlayerUpdate(GameObject obj);
    public static event PlayerUpdate OnPlayerUpdate;

    void Update()
    {
        // Broadcast the player's position to any subscribed listeners
        if (OnPlayerUpdate != null)
        {
            OnPlayerUpdate(this.gameObject);
        }
    }
}
