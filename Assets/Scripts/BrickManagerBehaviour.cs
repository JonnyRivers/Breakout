using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManagerBehaviour : MonoBehaviour
{
    public GameObject BrickPrefab;

    public int NumActiveBricks => m_bricks.Count(b => b.activeInHierarchy);

    private List<GameObject> m_bricks;

    // Start is called before the first frame update
    void Start()
    {
        m_bricks = new List<GameObject>();

        for (int x = 0; x < 10; ++x)
        {
            for(int z = 0; z < 4; ++z)
            {
                float xPosition = -2f +(0.4f * x);
                float zPosition = 2f + (0.1f * z);

                GameObject brick = Instantiate(BrickPrefab, new Vector3(xPosition, 0.05f, zPosition), Quaternion.identity);
                m_bricks.Add(brick);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestoreAll()
    {
        foreach(GameObject brick in m_bricks)
        {
            brick.SetActive(true);
        }
    }
}
