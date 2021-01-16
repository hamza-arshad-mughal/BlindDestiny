using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : Bolt.EntityBehaviour<IPlayerStateForAll>
{
    public Animator charAnimator;
    public Rigidbody2D charRb;
    // Start is called before the first frame update
    

    // Update is called once per frame
    public override void SimulateOwner()
    {
        
        if(charRb.velocity.x > 0 || charRb.velocity.x < 0)
        {
            state.running = true;
            
        }
        else if(charRb.velocity.x == 0)
        {
            
            state.running = false;
           
        }
    }
    private void Update()
    {
        if (state.running)
        {
            charAnimator.SetBool("running", true);
        }
        else
        {
            charAnimator.SetBool("running", false);
        }
    }
}
