using UnityEngine;

[System.Serializable]
public struct Quaterniond
{
    public double x;
    public double y;
    public double z;
    public double w;

    public const double kEpsilon = 1E-06f;

    public static readonly Quaterniond identity = new Quaterniond(0.0f, 0.0f, 0.0f, 1.0f);

    public Quaterniond(double _x, double _y, double _z, double _w)
    {
        x = _x;
        y = _y;
        z = _z;
        w = _w;
    }

    public static Quaterniond Euler(double _yaw, double _pitch, double _roll)
    {
        _yaw *= Mathf.Deg2Rad;
        _pitch *= Mathf.Deg2Rad;
        _roll *= Mathf.Deg2Rad;

        double yawOver2 = _yaw * 0.5f;
        double cosYawOver2 = System.Math.Cos(yawOver2);
        double sinYawOver2 = System.Math.Sin(yawOver2);
        double pitchOver2 = _pitch * 0.5f;
        double cosPitchOver2 = System.Math.Cos(pitchOver2);
        double sinPitchOver2 = System.Math.Sin(pitchOver2);
        double rollOver2 = _roll * 0.5f;
        double cosRollOver2 = System.Math.Cos(rollOver2);
        double sinRollOver2 = System.Math.Sin(rollOver2);

        var result = new Quaterniond(0.0f, 0.0f, 0.0f, 0.0f)
        {
                w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2,
                x = sinYawOver2 * cosPitchOver2 * cosRollOver2 + cosYawOver2 * sinPitchOver2 * sinRollOver2,
                y = cosYawOver2 * sinPitchOver2 * cosRollOver2 - sinYawOver2 * cosPitchOver2 * sinRollOver2,
                z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2
        };

        return result;
    }

    public static Quaterniond operator *(Quaterniond _a, Quaterniond _b) => new Quaterniond(
     _a.w * _b.x + _a.x * _b.w + _a.y * _b.z - _a.z * _b.y,
     _a.w * _b.y + _a.y * _b.w + _a.z * _b.x - _a.x * _b.z,
     _a.w * _b.z + _a.z * _b.w + _a.x * _b.y - _a.y * _b.x,
     _a.w * _b.w - _a.x * _b.x - _a.y * _b.y - _a.z * _b.z);

    public static Vector3d operator *(Quaterniond _rotation, Vector3d _point)
    {
        double num1 = _rotation.x * 2.0f;
        double num2 = _rotation.y * 2.0f;
        double num3 = _rotation.z * 2.0f;
        double num4 = _rotation.x * num1;
        double num5 = _rotation.y * num2;
        double num6 = _rotation.z * num3;
        double num7 = _rotation.x * num2;
        double num8 = _rotation.x * num3;
        double num9 = _rotation.y * num3;
        double num10 = _rotation.w * num1;
        double num11 = _rotation.w * num2;
        double num12 = _rotation.w * num3;

        var result = new Vector3d
        {
                x = (1.0f - (num5 + num6)) * _point.x + (num7 - num12) * _point.y + (num8 + num11) * _point.z,
                y = (num7 + num12) * _point.x + (1.0f - (num4 + num6)) * _point.y + (num9 - num10) * _point.z,
                z = (num8 - num11) * _point.x + (num9 + num10) * _point.y + (1.0f - (num4 + num5)) * _point.z
        };

        return result;
    }

    public static bool operator ==(Quaterniond _a, Quaterniond _b) => Dot(_a, _b) > 0.999999f;

    public static bool operator !=(Quaterniond _a, Quaterniond _b) => !(_a == _b);

    public static explicit operator Quaternion(Quaterniond _a) =>
            new Quaternion((float) _a.x, (float) _a.y, (float) _a.z, (float) _a.w);

    public static explicit operator Quaterniond(Quaternion _a) => new Quaterniond(_a.x, _a.y, _a.z, _a.w);

    public static double Dot(Quaterniond _a, Quaterniond _b) => _a.x * _b.x + _a.y * _b.y + _a.z * _b.z + _a.w * _b.w;

    public static double Angle(Quaterniond _a, Quaterniond _b)
    {
        double f = Dot(_a, _b);

        return Mathd.Acos(Mathd.Min(Mathd.Abs(f), 1.0f)) * 2.0f * 57.295788f;
    }

    public override int GetHashCode() =>
            x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode() >> 1;

    public override bool Equals(object _other)
    {
        bool result;
        
        if (!(_other is Quaterniond))
        {
            result = false;
        }
        else
        {
            var quaternion = (Quaterniond) _other;

            result = x.Equals(quaternion.x) && y.Equals(quaternion.y) && z.Equals(quaternion.z) &&
                     w.Equals(quaternion.w);
        }

        return result;
    }

    public override string ToString() => $"({x:F1}, {y:F1}, {z:F1}, {w:F1})";

    public string ToString(string _format) =>
            $"({x.ToString(_format)}, {y.ToString(_format)}, {z.ToString(_format)}, {w.ToString(_format)})";
}