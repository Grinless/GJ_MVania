using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public enum BossState
{
    GROWL,
    NORMAL,
    DROP,
    COUGH,
    DEAD
}

public enum BossDropState
{
    NONE,
    DROPPING,
    DROPPED,
    RISING
}

[System.Serializable]
public class BossData
{
    public float health;
    public float damage;
    public float droppedTime;
    public float iframes;
}

public class Boss_Mass_Fight : MonoBehaviour
{
    public BossData data = new BossData();
    public BossState state = BossState.GROWL;
    public BossDropState dropState = BossDropState.NONE;
    public IframeManager iframeManager;
    public Animator animator;
    public bool growlComplete = false;
    public bool droppingComplete = false;
    public GameObject bullet;

    public void Start() => InitaliseBoss();

    public void OnEnable() => InitaliseBoss();

    public void Update() => UpdateState();

    private void InitaliseBoss()
    {
        iframeManager.iframes = data.iframes;
        animator.SetBool("Awake", true);
        state = BossState.GROWL;
    }

    private void UpdateState()
    {
        switch (state)
        {
            case BossState.GROWL:
                State_Growl();
                break;
            case BossState.NORMAL:
                State_Normal();
                break;
            case BossState.COUGH:
                State_Cough();
                break;
            case BossState.DEAD:
                State_Dead();
                break;
            case BossState.DROP:
                State_Drop();
                break;
            default:
                break;
        }
    }

    public void State_Growl()
    {
        if (growlComplete)
        {
            state = BossState.NORMAL;
        }
    }

    public void State_Normal()
    {
        //Generate a decision over which attack to launch.
        state = GetAttackType();
    }

    public void State_Cough()
    {
        float randAngle = UnityEngine.Random.Range(20f, 60f) * Mathf.Deg2Rad;
        float angle = Vector2.Angle(Vector2.down, Vector2.down * 2) * Mathf.Deg2Rad;
        Vector2[] angles = new Vector2[]
        {
            new Vector2(MathF.Cos(angle) * Mathf.Rad2Deg, MathF.Sin(angle) * Mathf.Rad2Deg),
            new Vector2(MathF.Cos( randAngle + angle) * Mathf.Rad2Deg, MathF.Sin( randAngle + angle) * Mathf.Rad2Deg),
            new Vector2(MathF.Cos( randAngle - angle) * Mathf.Rad2Deg, MathF.Sin( randAngle - angle) * Mathf.Rad2Deg)
        };

        foreach (Vector2 direction in angles)
        {
            GameObject obj = GameObject.Instantiate(bullet);
            obj.transform.transform.position = gameObject.transform.position;
            obj.GetComponent<Rigidbody2D>().velocity = direction * 10;
        }
    }

    public void State_Drop()
    {
        switch (dropState)
        {
            case BossDropState.NONE:
                dropState = BossDropState.DROPPING;
                animator.SetBool("Drop", true);                
                break;
            case BossDropState.DROPPING:
                State_Dropping();
                break;
            case BossDropState.DROPPED:
                State_Dropped();
                break;
            case BossDropState.RISING:
                State_Rising();
                break;
            default:
                break;
        }
    }   

    private void State_Dead()
    {
        gameObject.SetActive(false);
    }

    #region Generate Attack Method. 
    private int[] randNumberArr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private bool dropTimerActive;

    private BossState GetAttackType()
    {
        int rand = RandomiseArray();
        if (rand <= 5)
        {
            return BossState.COUGH;
        }

        if (rand >= 5)
        {
            return BossState.DROP;
        }
        return BossState.NORMAL;
    }

    private int RandomiseArray()
    {
        int[] arr = new int[randNumberArr.Length];
        randNumberArr.CopyTo(arr, 0);
        System.Random random = new System.Random();
        arr = arr.OrderBy(x => random.Next()).ToArray();
        return arr[new System.Random().Next(arr.Length - 1)];
    }
    #endregion

    #region Drop Substate.
    public void State_Dropping()
    {
        if (droppingComplete)
        {
            dropState = BossDropState.DROPPED;
        }
    }

    public void State_Dropped()
    {
        if (droppingComplete && !dropTimerActive)
        {
            dropTimerActive = true;
            StartCoroutine(DroppedTimer());
        }
    }

    private IEnumerator DroppedTimer()
    {
        yield return new WaitForSeconds(data.droppedTime);
        State_Rising();
    }


    public void State_Rising()
    {
        animator.SetBool("Rising", true);
    }
    #endregion

    #region Health Methods. 
    public void ApplyDamage(float dmg)
    {
        if (data.health > 0)
        {
            data.health -= dmg;
            Mathf.Floor(data.health);
        }

        if (data.health <= 0)
            state = BossState.DEAD;
        else
            iframeManager.ActivateIframes();
    }
    #endregion
}
