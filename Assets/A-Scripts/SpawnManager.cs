using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private GameManager gameManager;

    [Header("Difficulty Settings")]
    public float initialSpawnRate = 2.0f; // Taux de spawn initial
    public float minSpawnRate = 0.5f; // Taux de spawn minimal (plus difficile)
    public float spawnRateDifficulty = 0.1f; // R�duction du taux de spawn par minute

    // R�f�rence directe � tous les objets du jeu
    public GameObject playerShip;
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    public GameObject powerUpPrefab;
    public GameObject bulletPrefab;


    // Variables pour le timing
    private float nextSpawnTime;
    public float spawnRate = 2.0f;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        // S'assurer que le joueur a les composants n�cessaires pour les collisions
        SetupCollisionComponents(playerShip, true, false, "Player");

        // Ajouter le script de gestion de collision au joueur
        if (playerShip.GetComponent<PlayerCollider>() == null)
        {
            playerShip.AddComponent<PlayerCollider>();
        }

        spawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + spawnRate;
    }

    private void Update()
    {
        // Calcul du nouveau taux de spawn en fonction du temps �coul� (en minutes)
        float minutesPlayed = gameManager.gameTime / 2f;
        spawnRate = Mathf.Max(minSpawnRate, initialSpawnRate - (spawnRateDifficulty * minutesPlayed));

        // G�n�ration de nouveaux ennemis/ast�ro�des
        SpawnEnemiesAndAsteroids();

        // Tir
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    protected void SetupCollisionComponents(GameObject obj, bool hasRigidbody, bool isTrigger, string tag)
    {
        // Ajouter ou configurer le collider si n�cessaire
        Collider collider = obj.GetComponent<Collider>();
        if (collider == null)
        {
            // Ajouter un BoxCollider par d�faut
            collider = obj.AddComponent<BoxCollider>();

            // Ajuster la taille du collider en fonction du tag
            BoxCollider boxCollider = (BoxCollider)collider;
            if (tag == "Bullet")
            {
                // Collider plus petit pour les balles
                boxCollider.size = new Vector3(0.3f, 0.3f, 0.5f);
            }
            else if (tag == "PowerUp")
            {
                // Collider plus grand pour les power-ups pour faciliter leur collecte
                boxCollider.size = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }

        // Configurer le collider comme trigger ou non
        collider.isTrigger = isTrigger;

        // Ajouter un Rigidbody si n�cessaire
        if (hasRigidbody && obj.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = false; // D�sactiver la gravit� pour un jeu spatial
            rb.isKinematic = false; // Ne pas rendre kin�matique pour permettre les collisions physiques
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; // Figer certains axes
            rb.interpolation = RigidbodyInterpolation.Extrapolate;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        // D�finir le tag
        obj.tag = tag;
    }

    void SpawnEnemiesAndAsteroids()
    {
        if (Time.time > nextSpawnTime)
        {
            if (UnityEngine.Random.value < 0.3f)
            {
                // Spawn d'un ennemi
                float randomX = UnityEngine.Random.Range(-8f, 8f);
                // Position de spawn sur l'axe Z au lieu de Y
                Vector3 spawnPosition = new Vector3(randomX, 0, 9);
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Configuration des composants de collision pour l'ennemi
                SetupCollisionComponents(enemy, true, false, "Enemy");

                // Ajouter le script de gestion de collision � l'ennemi
                enemy.AddComponent<EnemyCollider>();
            }
            else
            {
                // Spawn d'un ast�ro�de
                float randomX = UnityEngine.Random.Range(-8f, 8f);
                // Position de spawn sur l'axe Z au lieu de Y
                Vector3 spawnPosition = new Vector3(randomX, 0, 9);
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                // Configuration des composants de collision pour l'ast�ro�de
                SetupCollisionComponents(asteroid, true, false, "Asteroid");

                // Ajouter le script de gestion de collision � l'ast�ro�de
                asteroid.AddComponent<AsteroidCollider>();
            }

            nextSpawnTime = Time.time + spawnRate;
        }
    }

    public void SpawnPowerUp(Vector3 position)
    {
        GameObject powerUp = Instantiate(powerUpPrefab, position, Quaternion.identity);

        // Configuration des composants de collision pour le power-up
        SetupCollisionComponents(powerUp, true, false, "PowerUp");

        // Ajouter le script de gestion de collision au power-up
        powerUp.AddComponent<PowerUpCollider>();
    }

    void FireBullet()
    {
        // Calcul de la position de d�part pour centrer les projectiles
        float startX = -((gameManager.bulletCount - 1) * gameManager.bulletSpacing) / 2;

        // Cr�ation de plusieurs balles c�te � c�te
        for (int i = 0; i < gameManager.bulletCount; i++)
        {
            // Calcule la position avec l'offset horizontal
            Vector3 bulletOffset = new Vector3(startX + (i * gameManager.bulletSpacing), -0.5f, 0.5f);
            Vector3 spawnPosition = transform.position + bulletOffset;

            // Instanciation du projectile
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // Configuration des composants de collision pour la balle
            // Les projectiles doivent avoir un Rigidbody pour les collisions
            SetupCollisionComponents(bullet, true, false, "Bullet");

            // Ajouter le script de gestion de collision � la balle
            bullet.AddComponent<BulletCollider>();
        }

        // Son de tir
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}