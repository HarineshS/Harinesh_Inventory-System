using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxRange = 100f; // Specify the maximum range of the ray
    public LayerMask layerMask; // Specify which layers the ray should interact with

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            // Cast a ray from the center of the screen
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            // Declare a RaycastHit variable to store information about the collision
            RaycastHit hit;

            // Check if the ray hits something within the specified range
            if (Physics.Raycast(ray, out hit, maxRange, layerMask))
            {
                // Print the name of the object hit by the ray
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                // Do something with the object hit by the ray (e.g., apply damage, trigger an event, etc.)
                // hit.collider.gameObject.SendMessage("ApplyDamage", damageAmount);
            }
            else
            {
                // If the ray doesn't hit anything within the specified range, you can handle this case here
                Debug.Log("No object hit within the specified range.");
            }
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxRange, Color.red, 0.1f);
        }
    }
}
