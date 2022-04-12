using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("炮弹初始速度")]
    public float Speed = 15f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // || other.transform.root.tag == "Player"
        if (other.transform.root.tag == "Tank" )
        {
            other.transform.root.GetComponent<TankController>().HP -= 10;
        }
        Destroy(gameObject);
    }
}
