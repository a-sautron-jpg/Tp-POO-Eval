using UnityEngine;

public class Move_Base : MonoBehaviour
{
    public float Speed;
    public float spawnRate = 2.0f;

    public GameObject playerDamageEffect;

    protected GameManager gameManager;
    
    protected Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = FindAnyObjectByType<GameManager>();
    }

    protected virtual void Move()
    {
        if (transform.position.z < -12)
        {
            //lives--;

            // Effet visuel pour montrer que l'ennemi a travers�
            if (playerDamageEffect != null)
            {
                Instantiate(playerDamageEffect, transform.position, Quaternion.identity);
            }

            // Destruction de l'ennemi
            Destroy(this);

            // V�rifier si le joueur n'a plus de vies

            //if (lives <= 0)
            //{
            //    GameOver();
            //}
        }
    }
}
