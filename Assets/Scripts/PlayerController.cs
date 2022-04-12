using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("敌方坦克预设体")]
    public Object EnemyOriginal;
    [Header("敌方坦克数量")]
    [Range(40, 100)]
    public int EnemyMax = 50;

    private int EnemyNumber = 1;
    private Vector3 CameraDiff;
    private GameObject MainCamera;
    private TankController tank;
    // Start is called before the first frame update
    void Start()
    {
        tank = transform.GetComponent<TankController>();
        MainCamera = GameObject.FindWithTag("MainCamera");
        CameraDiff = MainCamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        //createEnemy();
    }
    void Fire()
    {
        if (Input.GetButtonDown("Fire"))
        {
            tank.Fire();
        }
    }
    void Move()
    {
        // 获取水平输入
        float hor = Input.GetAxis("Horizontal");
        // 获取垂直输入
        float ver = Input.GetAxis("Vertical");
        if (hor != 0)
        {
            tank.RotateTo(transform.right * hor);
        }
        // 向前后移动
        tank.MoveTo(transform.forward * ver);
        setCamera();
    }
    /// <summary>
    /// 设置摄像机位置，跟随玩家坦克
    /// </summary>
    void setCamera()
    {
        MainCamera.transform.position = transform.position + CameraDiff;
    }

    void createEnemy()
    {
        // 生成随机位置，检测以当前位置为中心，2米半径的球体检测范围内是否有 除 地板 之外的游戏对象
        // 如果有，则重新生成随机位置，如果无，则创建坦克
        if (EnemyNumber > EnemyMax) return;
        float x = 0;
        float z = 0;
        do
        {
            x = Random.Range(-45, 45);
            z = Random.Range(-45, 45);

        } while (Physics.CheckSphere(new Vector3(x, 0, z), 2f, ~(1 << 6)));
        Instantiate(EnemyOriginal, new Vector3(x, 0, z), Quaternion.identity);
        EnemyNumber++;
    }
}
