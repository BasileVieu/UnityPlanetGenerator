using UnityEngine;

[System.Serializable]
public struct Vector3d
{
    public double x;
    public double y;
    public double z;

    public Vector3d(double _x, double _y, double _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public Vector3d(double _x, double _y)
    {
        x = _x;
        y = _y;
        z = 0.0;
    }

    public static readonly Vector3d zero = new Vector3d(0f, 0f, 0f);

    public static readonly Vector3d one = new Vector3d(1f, 1f, 1f);

    public static readonly Vector3d up = new Vector3d(0f, 1f, 0f);

    public static readonly Vector3d down = new Vector3d(0f, -1f, 0f);

    public static readonly Vector3d left = new Vector3d(-1f, 0f, 0f);

    public static readonly Vector3d right = new Vector3d(1f, 0f, 0f);

    public static readonly Vector3d forward = new Vector3d(0f, 0f, 1f);

    public static readonly Vector3d back = new Vector3d(0f, 0f, -1f);

    public static readonly Vector3d positiveInfinity = new Vector3d(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

    public static readonly Vector3d negativeInfinity = new Vector3d(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

    public static double Magnitude(Vector3d _vector) => Mathf.Sqrt((float)(_vector.x * _vector.x + _vector.y * _vector.y + _vector.z * _vector.z));

    public static double SqrMagnitude(Vector3d _vector) => _vector.x * _vector.x + _vector.y * _vector.y + _vector.z * _vector.z;

    public static Vector3d Normalize(Vector3d _value)
    {
        double num = Magnitude(_value);

        Vector3d result;

        if (num > 1E-05f)
        {
            result = _value / num;
        }
        else
        {
            result = zero;
        }

        return result;
    }

    public void Normalize()
    {
        double num = Magnitude(this);

        if (num > 1E-05f)
        {
            this /= num;
        }
        else
        {
            this = zero;
        }
    }

    public static Vector3d Cross(Vector3d _a, Vector3d _b) =>
            new Vector3d(_a.y * _b.z - _a.z * _b.y, _a.z * _b.x - _a.x * _b.z, _a.x * _b.y - _a.y * _b.x);

    public static double Distance(Vector3d _a, Vector3d _b) =>
            Mathd.Sqrt((_b.x - _a.x) * (_b.x - _a.x) + (_b.y - _a.y) * (_b.y - _a.y) + (_b.z - _a.z) * (_b.z - _a.z));

    public Vector3d normalized => Normalize(this);

    public static Vector3d operator +(Vector3d _a, Vector3d _b) => new Vector3d(_a.x + _b.x, _a.x + _b.x, _a.x + _b.x);

    public static Vector3d operator -(Vector3d _a, Vector3d _b) => new Vector3d(_a.x - _b.x, _a.x - _b.x, _a.x - _b.x);

    public static Vector3d operator -(Vector3d _a) => new Vector3d(-_a.x, -_a.x, -_a.x);

    public static Vector3d operator *(Vector3d _a, int _b) => new Vector3d(_a.x * _b, _a.y * _b, _a.z * _b);

    public static Vector3d operator *(Vector3d _a, float _b) => new Vector3d(_a.x * _b, _a.y * _b, _a.z * _b);

    public static Vector3d operator *(Vector3d _a, double _b) => new Vector3d(_a.x * _b, _a.y * _b, _a.z * _b);

    public static Vector3d operator *(int _b, Vector3d _a) => new Vector3d(_a.x * _b, _a.y * _b, _a.z * _b);

    public static Vector3d operator *(float _b, Vector3d _a) => new Vector3d(_a.x * _b, _a.y * _b, _a.z * _b);

    public static Vector3d operator *(double _b, Vector3d _a) => new Vector3d(_a.x * _b, _a.y * _b, _a.z * _b);

    public static Vector3d operator /(Vector3d _a, int _b) => new Vector3d(_a.x / _b, _a.y / _b, _a.z / _b);

    public static Vector3d operator /(Vector3d _a, float _b) => new Vector3d(_a.x / _b, _a.y / _b, _a.z / _b);

    public static Vector3d operator /(Vector3d _a, double _b) => new Vector3d(_a.x / _b, _a.y / _b, _a.z / _b);

    public static Vector3d operator /(int _b, Vector3d _a) => new Vector3d(_a.x / _b, _a.y / _b, _a.z / _b);

    public static Vector3d operator /(float _b, Vector3d _a) => new Vector3d(_a.x / _b, _a.y / _b, _a.z / _b);

    public static Vector3d operator /(double _b, Vector3d _a) => new Vector3d(_a.x / _b, _a.y / _b, _a.z / _b);

    public static bool operator ==(Vector3d _left, Vector3d _right) => SqrMagnitude(_left - _right) < 9.99999944E-11f;

    public static bool operator !=(Vector3d _left, Vector3d _right) => !(_left == _right);

    public static explicit operator Vector3(Vector3d _a) => new Vector3((float) _a.x, (float) _a.y, (float) _a.z);

    public static explicit operator Vector3d(Vector3 _a) => new Vector3d(_a.x, _a.y, _a.z);

    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;

    public override bool Equals(object _other)
    {
        bool result;
        
        if (!(_other is Vector3d))
        {
            result = false;
        }
        else
        {
            var vector3d = (Vector3d) _other;

            result = x.Equals(vector3d.x) && y.Equals(vector3d.y) && z.Equals(vector3d.z);
        }

        return result;
    }

    public override string ToString()
    {
        return string.Format("({0:F2},  {1:F2},  {2:F2})", new object[]
        {
                x,
                y,
                z
        });
    }
    
    public string ToString(string _format) {
        return string.Format("({0},  {1},  {2})", new object[]
        {
                x.ToString(_format),
                y.ToString(_format),
                z.ToString(_format)
        });
    }
}