using UnityEngine;

public class Spaceship_Move : Move_Base
{
    void Start()
    {
        Speed = 3.0f;
    }
    protected override void Move()
    {
        if (rb != null)
        {
            // Appliquer directement une v�locit� au Rigidbody
            rb.linearVelocity = Vector3.back * Speed;
        }
        else
        {
            // Fallback au mouvement par transform si pas de Rigidbody
            transform.position += Vector3.back * Speed * Time.deltaTime;
        }

        base.Move();
    }
}
