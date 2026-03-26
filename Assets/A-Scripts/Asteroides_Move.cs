using UnityEngine;

public class Asteroides_Move : Move_Base
{
    void Start()
    {
        Speed = 2.0f;
    }

    protected override void Move()
    {
        // Direction al�atoire pour chaque ast�ro�de
        float randomX = Random.Range(-0.5f, 0.5f);

        if (rb != null)
        {
            // Appliquer directement une v�locit� au Rigidbody
            rb.linearVelocity = new Vector3(randomX, 0, -1) * Speed;

            // Appliquer une rotation
            transform.Rotate(0, 30 * Time.deltaTime, 0);
        }
        else
        {
            // Fallback au mouvement par transform si pas de Rigidbody
            Vector3 movement = new Vector3(randomX, 0, -1) * Speed * Time.deltaTime;
            transform.position += movement;
            transform.Rotate(0, 30 * Time.deltaTime, 0);
        } 

        base.Move();
    }
    
}
