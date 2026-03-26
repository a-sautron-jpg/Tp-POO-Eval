// Le fichier GameManager.cs - Une classe monolithique qui fait tout
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Metadata;

public class GameManager : MonoBehaviour
{
    [Header("Explosion")]
    public ExplosionManager explosionManager;

    // Variables publiques expos�es sans encapsulation
    public int score;
    public int lives;


    // Nouvelles variables pour les fonctionnalit�s demand�es
    [Header("Weapon Settings")]
    public int bulletCount = 1; // Nombre de projectiles tir�s simultan�ment
    public float bulletSpacing = 0.5f; // Espacement horizontal entre les projectiles
    public int maxBulletCount = 5; // Limite maximale de projectiles simultan�s

    public float gameTime = 0f; // Temps de jeu �coul�


    // UI references
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;
    public GameObject gameOverPanel;
    public TMPro.TMP_Text powerupMessageText; // Pour afficher les messages de powerup
    public TMPro.TMP_Text timeText; // Pour afficher le temps �coul�
    public GameObject playerDamageEffect; // Effet visuel quand un ennemi traverse

    private bool isGameOver = false;
    private float restartCountdown = 3.0f;
    public TMPro.TMP_Text countdownText;

    // Avant de remplacer le syst�me de collisions, il faut cr�er des classes pour g�rer les collisions
    // Ces classes seront attach�es aux objets du jeu concern�s

    // Voici les scripts � cr�er pour le syst�me de trigger/collision Unity
    // Note pour les �tudiants : Ces scripts devraient �tre dans des fichiers s�par�s pour respecter les principes SOLID

    void Start()
    {
        // Initialisation
        score = 0;
        lives = 3;
        bulletCount = 1;
        gameTime = 0f;
        UpdateUI();
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (powerupMessageText) powerupMessageText.gameObject.SetActive(false);
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
            // Fallback vers l'explosion originale
            //Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
        }

        // Destruction de l'ennemi
        Destroy(enemy, 0.1f); // Court d�lai pour permettre � l'explosion de commencer

        // Destruction de la balle
        Destroy(bullet);
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Augmentation du temps de jeu
            gameTime += Time.deltaTime;

            

            // Affichage du temps de jeu (optionnel)
            if (timeText != null)
            {
                int minutes = Mathf.FloorToInt(gameTime / 60);
                int seconds = Mathf.FloorToInt(gameTime % 60);
                timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
            }

            // Nous ne v�rifions plus les collisions manuellement
            // Les collisions sont maintenant g�r�es par les �v�nements OnTriggerEnter/OnCollisionEnter


            // Mise � jour de l'UI
            UpdateUI();
        }

        // Gestion du d�compte de red�marrage
        if (isGameOver)
        {
            restartCountdown -= Time.deltaTime;

            // Mise � jour du texte avec la valeur arrondie � l'entier sup�rieur
            if (countdownText != null)
            {
                countdownText.text = "Red�marrage dans: " + Mathf.Ceil(restartCountdown).ToString();
            }

            // Lorsque le d�compte atteint z�ro
            if (restartCountdown <= 0)
            {
                RestartGame();
            }
        }
    }

    public void ApplyPowerUp()
    {
        // Augmenter le nombre de projectiles pour tous les power-ups
        if (bulletCount < maxBulletCount)
        {
            bulletCount++;

            // Affichage d'un message temporaire pour informer le joueur
            StartCoroutine(ShowPowerupMessage("Weapon Upgraded! Bullets: " + bulletCount));
        }
        else
        {
            // Bonus de score si le joueur a d�j� le maximum de projectiles
            score += 200;
            StartCoroutine(ShowPowerupMessage("Max Weapon Level! +200 Score"));
        }
    }

    // Coroutine pour afficher un message temporaire
    IEnumerator ShowPowerupMessage(string message)
    {
        if (powerupMessageText != null)
        {
            powerupMessageText.text = message;
            powerupMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            powerupMessageText.gameObject.SetActive(false);
        }
        yield return null;
    }

    void UpdateUI()
    {
        // Mise � jour des textes de score et de vies
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    public void GameOver()
    {
        // Affichage du panel de game over
        gameOverPanel.SetActive(true);

        // Initialisation du compte � rebours
        isGameOver = true;
        restartCountdown = 3.0f;

        // Mise � jour initiale du texte de d�compte
        if (countdownText != null)
        {
            countdownText.text = "Red�marrage dans: " + Mathf.Ceil(restartCountdown).ToString();
            countdownText.gameObject.SetActive(true);
        }

        // Note: ne pas arr�ter le temps ici puisque nous voulons que le d�compte fonctionne
        // Time.timeScale = 0; -- retirez cette ligne s'il elle est pr�sente
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}