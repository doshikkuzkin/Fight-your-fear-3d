using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;
    private float shotTime;
    private Camera mailCamera;
    public float shotOffset;
    bool GunBought;
    public GameObject gun, improvedGun;

    private void Start()
    {
        if (PlayerPrefs.GetString("GunBought", "false") == "true")
        {
            GunBought = true;
        }
        else
        {
            GunBought = false;
        }
        if (GunBought)
        {
            improvedGun.SetActive(true);
        }
        else
        {
            gun.SetActive(true);
        }
        mailCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (DataHolder.player && !DataHolder.MenuShown)
        {
            Ray cameraRay = mailCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            if (Input.GetMouseButton(0))
            {
                if (Time.time >= shotTime)
                {
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    if (GunBought)
                    {
                       Instantiate(projectile, new Vector3(shotPoint.position.x - shotOffset, shotPoint.position.y, shotPoint.position.z - shotOffset), transform.rotation);
                       Instantiate(projectile, new Vector3(shotPoint.position.x + shotOffset, shotPoint.position.y, shotPoint.position.z + shotOffset), transform.rotation);
                    }
                    shotTime = Time.time + timeBetweenShots;
                }
            }
        }
    }
}
