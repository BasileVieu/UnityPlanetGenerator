public class MinMax
{
    public float Min
    {
        get;
        private set;
    }
    
    public float Max
    {
        get;
        private set;
    }

    public MinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    public void AddValue(float _value)
    {
        if (_value > Max)
        {
            Max = _value;
        }

        if (_value < Min)
        {
            Min = _value;
        }
    }
}