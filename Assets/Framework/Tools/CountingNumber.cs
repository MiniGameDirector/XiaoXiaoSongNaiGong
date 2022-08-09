using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class CountingNumber : MonoBehaviour
{
    public ChangeType changeType = ChangeType.Linear;
    Text mText;
    int startNum = 100;
    int currTime;
    int targetNum = 0;
    float duration = 0;
    bool isStartChange = false;
    float time = 0;
    ChangeTypeBase changeText;


    private void Start()
    {
        mText = transform.GetComponent<Text>();
        if (IsInt(mText.text))
            startNum = int.Parse(mText.text);

    }

    public void ChangeTo(int targetNum, float duration)
    {
        mText.fontSize = 60;
        this.targetNum = targetNum;
        currTime = startNum;
        this.duration = duration;
        time = 0;
        isStartChange = true;


        switch (changeType)
        {
            case ChangeType.Linear:
                changeText = new LinearType(startNum, targetNum, duration);
                break;
            case ChangeType.Easein:
                changeText = new EaseinType(startNum, targetNum, duration);
                break;
            case ChangeType.Easeout:
                changeText = new EaseoutType(startNum, targetNum, duration);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        UpdateText();
    }


    void UpdateText()
    {
        if (!isStartChange)
            return;
        time += Time.deltaTime;
        if (time >= duration)
        {
            mText.text = targetNum.ToString();
            startNum = targetNum;
            changeText = null;
            time = 0;
            isStartChange = false;
            mText.fontSize = 50;
            return;
        }
        int cacheNum = startNum + changeText.ChangeText(time);
        if (cacheNum == currTime)
            return;
        currTime = cacheNum;
        mText.text = currTime.ToString();
    }

    public bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*$");
    }
}


public abstract class ChangeTypeBase
{
    protected int startNum;
    protected int targetNum;
    protected float duration;
    public ChangeTypeBase(int startNum, int targetNum, float duration)
    {
        this.startNum = startNum;
        this.targetNum = targetNum;
        this.duration = duration;
    }

    public abstract int ChangeText(float time);
}


public class LinearType : ChangeTypeBase
{
    public LinearType(int startNum, int targetNum, float duration) : base(startNum, targetNum, duration)
    {
    }
    public override int ChangeText(float time)
    {
        return (int)((targetNum - startNum) * (time / duration));
    }
}


public class EaseinType : ChangeTypeBase
{
    public EaseinType(int startNum, int targetNum, float duration) : base(startNum, targetNum, duration)
    {
    }
    public override int ChangeText(float time)
    {
        return (int)((targetNum - startNum) * (time / duration) * (time / duration));
    }
}

public class EaseoutType : ChangeTypeBase
{
    public EaseoutType(int startNum, int targetNum, float duration) : base(startNum, targetNum, duration)
    {
    }
    public override int ChangeText(float time)
    {
        return (int)((targetNum - startNum) * Math.Sin(Math.PI / 2 * (time / duration)));
    }
}





public enum ChangeType
{
    Linear,
    Easein,
    Easeout,
}