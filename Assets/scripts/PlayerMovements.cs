using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5f;  // Adjust the speed as needed
    public float rotationSpeed = 200f;  // Adjust the rotation speed as needed
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public float fireRate = 0.5f; // Adjust the fire rate as needed
    private float nextFireTime = 0f;

    private void Update()
    {
        // Handle input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Calculate mouse direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = (mousePosition - transform.position).normalized;

        // Apply movement directly using Transform
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Apply rotation towards mouse direction
        if (mouseDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Shooting logic
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = firePoint.right * projectileSpeed;

        Destroy(projectile, 2f);
    }
}
