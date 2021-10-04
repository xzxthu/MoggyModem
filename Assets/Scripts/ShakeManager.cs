using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    [Header("过多久开始震动")]
    public float WaitForShakeTime = 3f;

    [Header("震多久")]
    public float ShakingTime = 2f;

    [Header("摇哪些东西")]
    public Transform ScreenShakeInNotSave;
    public Transform ScreenShakeAllTheWay;

    [Header("震动幅度")]
    public float amplitude = 1f;

    private Vector3 originSaveScreen;
    private Vector3 originAllScreen;

    private bool isStarted = false;
    private bool isSave = false;
    private bool isShaking = false;
    private float timer;

    private List<ShakeItem> l_shakeItems;
    private List<ShakeItem> r_shakeItems;
    private List<SaveItem> l_saveItems;
    private List<SaveItem> r_saveItems;

    public static ShakeManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        originSaveScreen = ScreenShakeInNotSave.position;
        originAllScreen = ScreenShakeAllTheWay.position;
        timer = 0;

    }

    private void Update()
    {
        if(isStarted)
        {
            if(CountTimer())
            {
                isShaking = true;
                
            }

            if(isShaking)
            {
                ShakeScreen();
            }
        }
    }

    public void UpdateShakeItems()
    {
        l_shakeItems = ItemManager.GetInstance().GetItemsList<ShakeItem>(LevelManager.Instance.LeftTile);
        r_shakeItems = ItemManager.GetInstance().GetItemsList<ShakeItem>(LevelManager.Instance.RightTile);
        l_saveItems = ItemManager.GetInstance().GetItemsList<SaveItem>(LevelManager.Instance.LeftTile);
        r_saveItems = ItemManager.GetInstance().GetItemsList<SaveItem>(LevelManager.Instance.RightTile);
    }


    private bool CountTimer()
    {
        Debug.Log(timer);
        timer += Time.deltaTime;
        if(timer>WaitForShakeTime)
        {
            timer = 0;
            StartCoroutine(EndShaking());
            
            return true;
        }
        return false;
    }

    private IEnumerator EndShaking()
    {
        yield return new WaitForSeconds(ShakingTime);
        ResetShake();
    }

    public void ShakeScreen()
    {
        if (!isSave)
        {
            WiggleScreen(ScreenShakeInNotSave, originSaveScreen);
        }

        WiggleScreen(ScreenShakeAllTheWay, originAllScreen);
        Debug.Log("shaking");
    }

    private void WiggleScreen(Transform screen, Vector3 originPos)
    {
        screen.position = originPos +
            Vector3.up * Random.Range(0.32f, amplitude) * Random.Range(-1f,1f) + 
            Vector3.right * Random.Range(0.32f, amplitude) * Random.Range(-1f, 1f);
    }

    public void StartShakeProcess()
    {
        isStarted = true;
        
        LevelManager.Instance.LeftTile.GetComponent<ItemController>().EnableItems<SaveItem>();
        LevelManager.Instance.RightTile.GetComponent<ItemController>().EnableItems<SaveItem>();

        // 调用开始动画 **
    }

    public void IsSave()
    {
        isSave = true;
        Debug.Log("is save");
    }

    public void IsNotSave()
    {
        isSave = false;
        Debug.Log("is not save");
    }

    public void ResetShake()
    {
        StopAllCoroutines();
        timer = 0;
        isStarted = false;
        isSave = false;
        isShaking = false;
        ScreenShakeInNotSave.position = originSaveScreen;
        ScreenShakeAllTheWay.position = originAllScreen;
        LevelManager.Instance.LeftTile.GetComponent<ItemController>().DisableItems<SaveItem>();
        LevelManager.Instance.RightTile.GetComponent<ItemController>().DisableItems<SaveItem>();
    }

}
