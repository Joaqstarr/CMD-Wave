using DG.Tweening;

[System.Serializable]
public struct TweenData
{
    public float Duration;
    public float Strength;
    public Ease Ease;

    public TweenData(float duration, float strength, Ease ease)
    {
        Duration = duration;
        Strength = strength;
        Ease = ease;
    }
    public TweenData(float duration, float strength)
    {
        Duration = duration;
        Strength = strength;
        Ease = Ease.Linear;
    }

}
