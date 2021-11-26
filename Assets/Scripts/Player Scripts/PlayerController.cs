using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float rotateSpeed = 100, bulletSpeed = 100;

    private Transform handPos;
    private Transform firePos1, firePos2;

    private LineRenderer lineRenderer;

    public GameObject bullet;

    void Awake()
    {
        handPos = GameObject.Find("LeftArm").transform;
        firePos1 = GameObject.Find("FirePos1").transform;
        firePos2 = GameObject.Find("FirePos2").transform;
        lineRenderer = GameObject.Find("Gun").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Aim();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
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
    }


    private void Shoot()
    {
        lineRenderer.enabled = false;

        GameObject b = Instantiate(bullet, firePos1.position, Quaternion.identity);

        if (transform.localScale.x > 0)
            b.GetComponent<Rigidbody2D>().AddForce(firePos1.right * bulletSpeed, ForceMode2D.Impulse);
        else
            b.GetComponent<Rigidbody2D>().AddForce(-firePos1.right * bulletSpeed, ForceMode2D.Impulse);

        Destroy(b, 2);
    }
}
