using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;

    private float timeSinceLastShot;

    private void Start()
    {
        
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= 1f / fireRate)
        {
            // Check for the closest visible enemy
            Transform closestEnemy = GetClosestEnemy();

            // Fire a projectile
            if (closestEnemy != null)
            {
                FireProjectile(closestEnemy);
                timeSinceLastShot = 0f;
            }
        }
    }

    private Transform GetClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f, enemyLayer);

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            Transform enemyTransform = hitCollider.transform;
            float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemyTransform;
            }
        }

        return closestEnemy;
    }

    private void FireProjectile(Transform target)
    {
        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calculate the direction to the target
        Vector3 direction = (target.position - firePoint.position).normalized;

        // Set the projectile's initial velocity
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
    }
}
