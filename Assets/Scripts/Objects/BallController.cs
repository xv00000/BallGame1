using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] float moveForce = 8f; // 移动推力
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.drag = 0.2f; // 空气阻力（优化手感
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A=-1，D=1（X轴）
        float vertical = Input.GetAxisRaw("Vertical"); // S=-1，W=1（Z轴）
        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized; // 归一化避免斜向过快
        if (moveDir.magnitude > 0.1f)
        {
            rb.AddForce(moveDir * moveForce, ForceMode.Force);
        }
    }
}
