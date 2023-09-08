using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anime;
    PlayerMovement pm;
    SpriteRenderer sr;

    void Start()
    {
        anime = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChangeAnimeStatus();
    }

    void ChangeAnimeStatus()
    {
        if (pm.moveDirection.x != 0)
        {
            anime.SetBool("MoveHorizontal", true);
            FaceDirection();
        }
        else
        {
            anime.SetBool("MoveHorizontal", false);
            FaceDirection();
        }

        if (pm.moveDirection.y > 0)
        {
            anime.SetBool("MoveUp", true);
        }
        else
        {
            anime.SetBool("MoveUp", false);
        }

        if (pm.moveDirection.y < 0)
        {
            anime.SetBool("MoveDown", true);
        }
        else
        {
            anime.SetBool("MoveDown", false);
        }

    }

    void FaceDirection()
    {
        if (pm.moveDirection.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
