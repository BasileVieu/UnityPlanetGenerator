public static class ExtensionMethods
{
    public static float Remap(this float _value, float _fromMin, float _toMin, float _fromMax, float _toMax) => _toMin + (_value - _fromMin) * (_toMax - _toMin) / (_fromMax - _fromMin);
}