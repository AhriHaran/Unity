using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public POOL_INDEX m_eItemType;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<PlayerScript>().DropItem(m_eItemType, 25, gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = transform.position;
        if(Pos.y >= 0.5)
        {
            Pos.y -= 0.1f;
            transform.position = Pos;
        }
    }
}
