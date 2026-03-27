using UnityEngine;

public class Move_Base : MonoBehaviour
{
    public float Speed;

    protected GameManager gameManager;

    [Header("Explosion")]
    public ExplosionManager explosionManager;
    public GameObject explosionPrefab;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = FindAnyObjectByType<GameManager>();

        explosionManager = FindAnyObjectByType<ExplosionManager>();

        explosionPrefab = (GameObject)Resources.Load("Prefabs\\Explode");
    }

    protected virtual void Move()
    {

    }
}
