using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet : Bullet
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.tag == "Shield"))
        {
            base.OnCollisionEnter2D(collision);
        }
        else
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
