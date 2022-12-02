using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject tear;
    [SerializeField]
    private PlayerStats playerStats;

    private float tears = 3f;
    private float damage = 3.5f;
    private float range = 5f;
    private float shotSpeed = 2f;
    private float luck = 0f;

    private float eyeHeight = 0.55f;

    //private Vector2 fireDirection;
    [SerializeField]
    private Transform leftEye;
    [SerializeField]
    private Transform rightEye;
    
    private enum FireState { LeftEye, RightEye };
    private FireState fireState = FireState.LeftEye;
    private Animator headAnimator;

    private float cooldown = 0f;

    void Awake()
    {
        UpdateStats();
        GameObject playerHead = playerStats.playerHead;
        headAnimator = playerHead.GetComponent<Animator>();
    }

    void UpdateStats()
    {
        tears = playerStats.tears;
        damage = playerStats.damage;
        range = playerStats.range;
        shotSpeed = playerStats.shotSpeed;
        luck = playerStats.luck;
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateStats();
        Vector2 fireDirection;
        fireDirection.x = Input.GetAxisRaw("TearsH");
        //if(fireDirection.x > 0 || fireDirection.x < 0 || Input.GetAxisRaw("Vertical") != 0)
        //{
        //    fireDirection.x = 0;
        //}
        fireDirection.y = Input.GetAxisRaw("TearsV");
        headAnimator.SetBool("isFiring", false);
        if (fireDirection.magnitude > 0)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= 1 / tears)
            {
                cooldown = 0;
                FireTear(fireDirection);
            }
            headAnimator.SetBool("isFiring", true);
            headAnimator.SetFloat("FiringX", fireDirection.x);
            headAnimator.SetFloat("FiringY", fireDirection.y);
        }
    }

    void FireTear(Vector2 direction)
    {
        GameObject newTear;
        if (fireState == FireState.LeftEye)
        {
            Vector3 vec = new Vector3(leftEye.position.x, leftEye.position.y - eyeHeight, leftEye.position.z);
            newTear = Instantiate(tear, vec, Quaternion.identity);
            fireState = FireState.RightEye;    
        } else
        {
            Vector3 vec = new Vector3(rightEye.position.x, rightEye.position.y - eyeHeight, rightEye.position.z);
            newTear = Instantiate(tear, vec, Quaternion.identity);
            fireState = FireState.LeftEye;
        }
        Tear tc = newTear.GetComponent<Tear>();
        tc.damage = damage;
        tc.range = range;
        tc.shotSpeed = shotSpeed;
        tc.direction = direction;
        tc.eyeHeight = eyeHeight;
        Destroy(newTear, 10);
    }
}
