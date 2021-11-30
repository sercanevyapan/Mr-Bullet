using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public float rotateSpeed = 100, bulletSpeed = 100;
    public int ammo = 4;

    public Transform handPos;
    public Transform firePos1, firePos2;

    public LineRenderer lineRenderer;

    public GameObject bullet;

    public GameObject crosshair;

    public AudioClip gunShot;

    void Awake()
    {
        crosshair.SetActive(false);
        lineRenderer.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsMouseOverUI())
        {
            if (Input.GetMouseButton(0))
            {

                Aim();
        
            }
            if (Input.GetMouseButtonUp(0))
            {
               
                if (ammo > 0)
                {
                    Shoot();
                  
                }
                  
                else
                {
                    lineRenderer.enabled = false;
                    crosshair.SetActive(false);
                }

               
            }
        
        }
        
    } 

    private void Aim()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        handPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePos1.position);
        lineRenderer.SetPosition(1, firePos2.position);

        crosshair.SetActive(true);
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }


    private void Shoot()
    {
        crosshair.SetActive(false);
        lineRenderer.enabled = false;

        GameObject b = Instantiate(bullet, firePos1.position, Quaternion.identity);

        if (transform.localScale.x > 0)
            b.GetComponent<Rigidbody2D>().AddForce(firePos1.right * bulletSpeed, ForceMode2D.Impulse);
        else
            b.GetComponent<Rigidbody2D>().AddForce(-firePos1.right * bulletSpeed, ForceMode2D.Impulse);

        ammo--;
        FindObjectOfType<GameManager>().CheckBullets();
        SoundManager.instance.PlaySoundFX(gunShot,0.3f);


        Destroy(b, 2);
    }


    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
