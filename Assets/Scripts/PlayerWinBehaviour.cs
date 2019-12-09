using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinBehaviour : MonoBehaviour
{
    public GameObject BrickManager;
    public GameObject Bullet;
    public GameObject Paddle;

    private BrickManagerBehaviour m_brickManagerBehaviour;
    private BulletBehaviour m_bulletBehaviour;
    private Rigidbody m_bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_brickManagerBehaviour = BrickManager.GetComponent<BrickManagerBehaviour>();
        m_bulletBehaviour = Bullet.GetComponent<BulletBehaviour>();
        m_bulletRigidbody = Bullet.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_brickManagerBehaviour.NumActiveBricks == 0)
        {
            // TODO: this is shared with BallLostBehaviour
            Bullet.transform.position = new Vector3(
                Paddle.transform.position.x,
                Paddle.transform.position.y,
                Paddle.transform.position.z + 0.1f// TODO - derive size of paddle
            );

            m_bulletRigidbody.velocity = new Vector3(0, 0, 0);
            m_bulletBehaviour.IsStuck = true;

            m_brickManagerBehaviour.RestoreAll();
        }
    }
}
