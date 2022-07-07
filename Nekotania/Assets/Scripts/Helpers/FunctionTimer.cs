using System;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer Create(Action action, float timer)
    {
        GameObject go = new GameObject("FunctionTimer", typeof(MonoBehaviorHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, go);
        go.GetComponent<MonoBehaviorHook>().onUpdate = functionTimer.Update;
        return functionTimer;
    }
    private class MonoBehaviorHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update() { if (onUpdate != null) onUpdate(); }
    }
    private Action action;
    private float timer;
    private bool isDestroyed;
    private GameObject gameObject;
    public FunctionTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        isDestroyed = false;
        this.gameObject = gameObject;
    }
    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                action();
                DestroySelf();
            }
        }
    }
    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}
