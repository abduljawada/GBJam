using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] Animator anim;

    // Update is called once per frame
    void Update()
    {
        Vector2 movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movementDirection != Vector2.zero)
        {
            anim.SetFloat("X", Mathf.Round(movementDirection.x));
            anim.SetFloat("Y", Mathf.Round(movementDirection.y));
        }

        rb.velocity = movementDirection.normalized * movementSpeed;
    }
}
