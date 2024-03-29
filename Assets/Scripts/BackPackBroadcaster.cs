
using UnityEngine;

public class BackPackBroadcaster : MonoBehaviour
{
    public delegate void BackPackUpdate(GameObject obj);
    public static event BackPackUpdate OnBackPackUpdate;
    void Update()
    {
        // Broadcast the Backppack game object to any subscribed listeners
        if (OnBackPackUpdate != null)
        {
            OnBackPackUpdate(this.gameObject);
        }
    }
}
