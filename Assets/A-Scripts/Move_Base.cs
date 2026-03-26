using UnityEngine;

public class Move_Base : MonoBehaviour
{
    public float Speed;

    protected GameManager gameManager;
    
    protected Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = FindAnyObjectByType<GameManager>();
    }

    protected virtual void Move()
    {

    }

    // Nouvelle m�thode pour configurer les composants de collision
}
