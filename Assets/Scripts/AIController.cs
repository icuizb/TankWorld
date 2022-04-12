using UnityEngine;

public class AIController : MonoBehaviour
{
    // 坦克控制器
    private TankController tank;
    // 巡逻周期时间
    private float PatrolTimer = 0;
    // 巡逻路径
    private Vector3 PatrolPath = Vector3.zero;
    // 装弹时间
    private float LoadTimer = 0;


    private void Start()
    {
        tank = GetComponent<TankController>();
    }

    void Update()
    {
        checkPlayer();
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    // 检查玩家是否在检测范围内
    void checkPlayer()
    {
        // 检测到玩家
        if (Physics.CheckSphere(transform.position, 10f, 1 << 3))
        {
            GameObject player = GameObject.FindWithTag("Player");
            // 转向玩家
            tank.RotateTo(player.transform.position - transform.position - transform.forward);
            Shoot();
        }
        // 没有检测到玩家
        else
        {
            PatrolOrWait();
        }
    }
    void Shoot()
    {
        Transform bulletTransform = transform.Find("Top/Gun/Bullet");
        if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, 10f, 1 << 3))
        {
            LoadTimer += Time.deltaTime;
            if(LoadTimer > 3)
            {
                tank.Fire();
                LoadTimer = 0;
            }
            
        }
        else
        {
            Debug.Log("无法射击到玩家");
        }
    }
    void PatrolOrWait()
    {
        PatrolTimer += Time.deltaTime;
        if (PatrolTimer > 3)
        {
            PatrolTimer = 0;
            if (Random.Range(0f, 1f) > 0.5)
            {
                // 生成新的随机位置
                float x = Random.Range(-10, 10);
                float z = Random.Range(-10, 10);
                PatrolPath = new Vector3(x, 0, z);
            }
            else
            {
                // 原地等待
                PatrolPath = Vector3.zero;
            }
            
        }
        if (PatrolPath != Vector3.zero)
        {
            tank.RotateTo(PatrolPath);
            tank.MoveTo(PatrolPath);
        }
    }
}
