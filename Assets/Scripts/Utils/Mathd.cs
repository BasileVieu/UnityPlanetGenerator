using System;

public static class Mathd
{
    public const double pi = Math.PI;
    public const double piTimes2 = Math.PI * 2.0;
    public const double piOver2 = Math.PI * 0.5;
    
    public const double Deg2Rad = 180.0 / Math.PI / 180.0;
    public const double Rad2Deg = Math.PI / 180.0;
    public const double Epsilon = Double.Epsilon;

    public static double Floor(double _x) => Math.Floor(_x);

    public static double Ceil(double _x) => Math.Ceiling(_x);

    public static double Lerp(double _a, double _b, double _t) => _a + (_b - _a) * _t;

    public static double Exp(double _x) => Math.Exp(_x);

    public static double Pow(double _x, double _y) => Math.Pow(_x, _y);

    public static double Log(double _x) => Math.Log(_x);

    public static double Log(double _x, double _y) => Math.Log(_x, _y);

    public static double Sqrt(double _x) => Math.Sqrt(_x);

    public static double Clamp01(double _x) => _x < 0 ? 0 : (_x > 1 ? 1 : _x);

    public static double Clamp(double _x, double _low, double _high) => _x < _low ? _low : (_x > _high ? _high : _x);

    public static double Max(double _x, double _y) => _x > _y ? _x : _y;

    public static double Min(double _x, double _y) => _x < _y ? _x : _y;

    public static double Sign(double _x) => _x < 0 ? -1 : (_x > 0 ? 1 : 0);

    public static double Abs(double _x) => _x >= 0 ? _x : (_x * -1.0);

    public static double Cos(double _x) => Math.Cos(_x);

    public static double Sin(double _x) => Math.Sin(_x);

    public static double Tan(double _x) => Math.Tan(_x);

    public static double Acos(double _x) => Math.Acos(_x);

    public static double Asin(double _x) => Math.Asin(_x);

    public static double Atan(double _x) => Math.Atan(_x);

    public static double Atan2(double _y, double _x) => Math.Atan2(_y, _x);
}