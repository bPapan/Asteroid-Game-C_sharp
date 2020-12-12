using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const int LifeTime = 2;
    Timer deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = LifeTime;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    public void ApplyForce(Vector2 direction)
    {
        const float magnitude = 20f;
        //transform.Rotate(Vector3.forward, rotate);
        GetComponent<Rigidbody2D>().AddForce(
            direction * magnitude,
            ForceMode2D.Impulse);
    }
}
