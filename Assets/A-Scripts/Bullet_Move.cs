using UnityEngine;

public class Bullet_Move : Move_Base
{
    private SpawnManager spawnManager;
    void Start()
    {
        Speed = 10.0f;
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (rb != null)
        {
            // R�initialiser la v�locit� et appliquer une nouvelle force
            rb.linearVelocity = Vector3.forward * Speed;
        }
        else
        {
            // Fallback au mouvement par transform si pas de Rigidbody
            transform.position += Vector3.forward * Speed * Time.deltaTime;
        }

        // Suppression des balles qui sortent de l'�cran
        if (transform.position.z > 9) // Chang� de y � z
        {
            Destroy(this);
        }
    }

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        gameManager = FindAnyObjectByType<GameManager>();

        if (collision.gameObject.CompareTag("Enemy"))
        {

            // Balle touche ennemi
            HandleBulletEnemyCollision(gameObject, collision.gameObject);
            gameManager.score += 100;

            // Chance de générer un power-up
            if (Random.value < 0.5f)
            {
                spawnManager = FindAnyObjectByType<SpawnManager>();

            }
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Balle touche astéroïde
            HandleBulletEnemyCollision(gameObject, collision.gameObject);
            gameManager.score += 50;
        }
    }

    public void HandleBulletEnemyCollision(GameObject bullet, GameObject enemy)
    {
        // Explosion avec effet de fragmentation
        if (explosionManager != null)
        {
            explosionManager.ExplodeObject(enemy);
        }
        else
        {
            explosionPrefab = (GameObject)Resources.Load("Prefabs\\Explode");
            explosionManager = FindAnyObjectByType<ExplosionManager>();
            // Fallback vers l'explosion originale
            Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
        }

        // Destruction de l'ennemi
        Destroy(enemy, 0.1f); // Court d�lai pour permettre � l'explosion de commencer

        // Destruction de la balle
        Destroy(bullet);
    }
}
