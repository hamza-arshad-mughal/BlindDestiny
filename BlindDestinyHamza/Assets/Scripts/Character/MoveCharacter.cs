using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
public class MoveCharacter : Bolt.EntityBehaviour<IPlayerStateForAll>
{
    public AudioSource Player;
    public AudioClip jumpSound;
    public AudioClip AcidSound;

    public Rigidbody2D rb;
    public SpriteRenderer spchar;
    public Joystick joystick;
    
    public float jumpForce;
    public float moveSpeed;
    public float speedLimit;

    private bool inair = false;

    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.TransformPlayer, transform);
        if (entity.IsOwner)
        {
            joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
        }
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        if(rb.velocity.x > speedLimit)
        {
            Vector2 preV = rb.velocity;
            preV.x = speedLimit;
            rb.velocity = preV;
        }
        else if(rb.velocity.x < -speedLimit)
        {
            Vector2 preV = rb.velocity;
            preV.x = -speedLimit;
            rb.velocity = preV;
        }


        if (joystick.Vertical > 0.5f && inair == false && rb.velocity.y == 0)
        {
            Player.clip = jumpSound;
            Player.Play();
            rb.AddForce(new Vector2(0f, jumpForce));
            inair = true;
        }
        else if (joystick.Horizontal < -0.5f)
        {
            
            state.isFlipped = true;
            Vector2 preV = rb.velocity;
            preV.x = preV.x - 0.1f * moveSpeed;
            rb.velocity = preV;
           
        }
        else if (joystick.Horizontal > 0.5f)
        {
           
            state.isFlipped = false;
            Vector2 preV = rb.velocity;
            preV.x = preV.x + 0.1f * moveSpeed;
            rb.velocity = preV;
           
        }
        else
        {
            Vector2 preV = rb.velocity;
            preV.x = 0;
            rb.velocity = preV;

        }
       
    }

    private void Update()
    {
        if (state.isFlipped)
        {
            spchar.flipX = true;
        }
        else
        {
            spchar.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.ToString().Contains("_Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.collider);
        }
        if (col.gameObject.CompareTag("Acid"))
        {
            Player.clip = AcidSound;
            Player.Play();
            Player.enabled = false;
        }
        inair = false;
    }

}
