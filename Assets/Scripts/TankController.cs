using UnityEngine;

public class TankController : MonoBehaviour
{
    public float HP = 100;
    [Header("移动速度")]
    public float MoveSpeed = 3f;
    [Header("转向速度")]
    public float RotateSpeed = 1f;
    [Header("炮弹预设体")]
    public Rigidbody BulletOriginal;

    private AudioSource FireAudio;

    private void Start()
    {
        FireAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        checkIsDead();
    }
    /// <summary>
    /// 坦克移动到某个位置
    /// </summary>
    /// <param name="position">移动到的位置坐标</param>
    public void MoveTo(Vector3 position)
    {
        transform.position += position.normalized * Time.deltaTime * MoveSpeed;
        // 将物体移动速度设置为0，防止碰撞反弹
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    /// <summary>
    /// 坦克旋转至某个方向
    /// </summary>
    /// <param name="quaternion"></param>
    public void RotateTo(Vector3 vector)
    {
        Quaternion quaternion =  Quaternion.LookRotation(vector);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * RotateSpeed);
    }

    public void Fire()
    {
        Transform bulletTransform = transform.Find("Top/Gun/Bullet");
        Instantiate(BulletOriginal, bulletTransform.position, bulletTransform.rotation);
        Debug.Log("Fire");
        FireAudio.Play();
    }
    void checkIsDead()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
