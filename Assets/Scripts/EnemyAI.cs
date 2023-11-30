using UnityEngine;

public enum EnemyMovementType
{
    Normal,
    Jumping
}

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleLayer;
    public float moveSpeed = 3f;
    public float stoppingDistance = 1f;
    public float jumpForce = 5f;
    public EnemyMovementType movementType = EnemyMovementType.Normal;

    private Rigidbody enemyRigidbody;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned to the EnemyAI script!");
        }

        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Avoid obstacles in the path
            AvoidObstacles(ref direction);

            // Choose movement type
            if (movementType == EnemyMovementType.Normal)
            {
                MoveTowardsPlayer(direction);
                RotateTowardsPlayer(direction);
            }
            else if (movementType == EnemyMovementType.Jumping)
            {
                JumpTowardsPlayer();
            }
        }
    }

    private void AvoidObstacles(ref Vector3 direction)
    {
       
        // Calculate a new direction to avoid the obstacle
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, obstacleLayer))
        {
            Vector3 avoidanceDirection = Vector3.Cross(Vector3.up, hit.normal);
            direction += avoidanceDirection * 2f;
        }
    }

    private void MoveTowardsPlayer(Vector3 direction)
    {
       
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > stoppingDistance)
        {
            enemyRigidbody.velocity = direction * moveSpeed;
        }
        else
        {
            enemyRigidbody.velocity = Vector3.zero;
        }
    }

    private void RotateTowardsPlayer(Vector3 direction)
    {
 
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 5f);
    }

    private void JumpTowardsPlayer()
    {
   
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

  
        if (distanceToPlayer < stoppingDistance)
        {
  
            enemyRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
