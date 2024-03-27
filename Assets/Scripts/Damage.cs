using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float DamagetoDeal;

    public float BulletRange = 50f;
    private Transform PlayerCamera;

    private void Start() {
        PlayerCamera = Camera.main.transform;
    }


    public void ShootGun()
    {
        Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.red, 1.0f);
        if(Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.Health -= DamagetoDeal;
            }
        }
    }
}
