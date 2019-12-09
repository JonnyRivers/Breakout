using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLostBehaviour : MonoBehaviour
{
    private BulletBehaviour m_bulletBehaviour;
    private Rigidbody m_bulletRigidbody;

    public GameObject Bullet;
    public GameObject Paddle;

    // Start is called before the first frame update
    void Start()
    {
        m_bulletBehaviour = Bullet.GetComponent<BulletBehaviour>();
        m_bulletRigidbody = Bullet.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ballIsLost = Bullet.transform.position.z < -5;// TODO - derive size of world

        if (ballIsLost)
        {
            // TODO: this is shared with PlayerWinBehaviour
            Bullet.transform.position = new Vector3(
                Paddle.transform.position.x,
                Paddle.transform.position.y,
                Paddle.transform.position.z + 0.1f// TODO - derive size of paddle
            );

            m_bulletRigidbody.velocity = new Vector3(0, 0, 0);
            m_bulletBehaviour.IsStuck = true;
        }
    }
}
