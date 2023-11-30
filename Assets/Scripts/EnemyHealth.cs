using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Color flashColor = Color.white; 
    [HideInInspector]
    public Color EnemyColor; 
    public float flashDuration = 0.5f;   
    public Animator DieAnim;    
   

    private Renderer enemyRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRenderer = GetComponent<Renderer>();
        EnemyColor = enemyRenderer.material.color;
        DieAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(20); 
            StartCoroutine(FlashEnemy());
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Enemy Dieing
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation
        DieAnim.SetBool("Die", true);
        // Destroy the enemy 
        Destroy(gameObject, 1f);
    }

    IEnumerator FlashEnemy()
    {
        // Change color to flashColor
        enemyRenderer.material.color = flashColor;

        
        yield return new WaitForSeconds(flashDuration);

        // Return to the original color
        enemyRenderer.material.color = EnemyColor; // You may need to adjust this depending on the original color
    }
}
