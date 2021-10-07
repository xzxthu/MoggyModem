using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    [Header("过多久开始震动")]
    public float WaitForShakeTime = 1f;

    [Header("震多久")]
    public float ShakingTime = 0.32f;

    [Header("摇哪些东西")]
    public Transform[] ScreenShakeInNotSave;
    public Transform[] ScreenShakeAllTheWay;

    [Header("震动幅度")]
    public float amplitude = 1f;

    private Vector3[] originSaveScreen;
    private Vector3[] originAllScreen;

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
        originSaveScreen = new Vector3[ScreenShakeInNotSave.Length];
        originAllScreen = new Vector3[ScreenShakeAllTheWay.Length];

        for (int i = 0;i<ScreenShakeInNotSave.Length;i++)
        {
            originSaveScreen[i] = ScreenShakeInNotSave[i].position;
        }

        for (int i = 0; i < ScreenShakeAllTheWay.Length; i++)
        {
            originAllScreen[i] = ScreenShakeAllTheWay[i].position;
        }

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
        //Debug.Log(timer);
        timer += Time.deltaTime;
        if(timer>WaitForShakeTime)
        {
            timer = 0;
            StartCoroutine(EndShaking());
            
            if(isSave) CatAnimationMgr.Instance.StartHolding();
            FatAnimationMgr.Instance.SetShaking();

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
            for (int i = 0; i < ScreenShakeInNotSave.Length; i++)
            {
                WiggleScreen(ScreenShakeInNotSave[i], originSaveScreen[i]);
            }
            
        }
        

        for (int i = 0; i < ScreenShakeAllTheWay.Length; i++)
        {
            WiggleScreen(ScreenShakeAllTheWay[i], originAllScreen[i]);
        }
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
        LevelManager.Instance.LeftTile.GetComponent<ItemController>().DisableItems<ShakeItem>();
        LevelManager.Instance.RightTile.GetComponent<ItemController>().DisableItems<ShakeItem>();

        StartCoroutine(LateCatWarning());
        PopupManager.Instance.ShakeWarning();
    }

    private IEnumerator LateCatWarning()
    {
        yield return new WaitForSeconds(0.5f);
        CatAnimationMgr.Instance.SetWarning();
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

        for (int i = 0; i < ScreenShakeInNotSave.Length; i++)
        {
            ScreenShakeInNotSave[i].position = originSaveScreen[i];
        }

        for (int i = 0; i < ScreenShakeAllTheWay.Length; i++)
        {
            ScreenShakeAllTheWay[i].position = originAllScreen[i];
        }

        LevelManager.Instance.LeftTile.GetComponent<ItemController>().DisableItems<SaveItem>();
        LevelManager.Instance.RightTile.GetComponent<ItemController>().DisableItems<SaveItem>();

        CatAnimationMgr.Instance.EndHolding();
        FatAnimationMgr.Instance.SetIdle();
    }

}
