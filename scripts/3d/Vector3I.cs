using Com.Astral.WFC.Utils;
using System;
using GD = Godot;
using Mathf = Godot.Mathf;

namespace Com.Astral.WFC._3D
{
	public struct Vector3I : IVectorI, IEquatable<Vector3I>
	{
		public static Vector3I Zero => _zero;
		public static Vector3I One => _one;
		public static Vector3I NegOne => _negOne;
		public static Vector3I Right => _right;
		public static Vector3I Left => _left;
		public static Vector3I Up => _up;
		public static Vector3I Down => _down;
		public static Vector3I Forward => _forward;
		public static Vector3I Back => _back;

		private static readonly Vector3I _zero = new Vector3I(0, 0, 0);
		private static readonly Vector3I _one = new Vector3I(1, 1, 1);
		private static readonly Vector3I _negOne = new Vector3I(-1, -1, -1);
		private static readonly Vector3I _right = new Vector3I(1, 0, 0);
		private static readonly Vector3I _left = new Vector3I(-1, 0, 0);
		private static readonly Vector3I _up = new Vector3I(0, 1, 0);
		private static readonly Vector3I _down = new Vector3I(0, -1, 0);
		private static readonly Vector3I _forward = new Vector3I(0, 0, -1);
		private static readonly Vector3I _back = new Vector3I(0, 0, 1);

		public readonly int Size => coordinates.Length;
		public int this[int index]
		{
			readonly get => coordinates[index];
			set => coordinates[index] = value;
		}

		public int X
		{
			readonly get => coordinates[0];
			set => coordinates[0] = value;
		}

		public int Y
		{
			readonly get => coordinates[1];
			set => coordinates[1] = value;
		}

		public int Z
		{
			readonly get => coordinates[2];
			set => coordinates[2] = value;
		}

		private int[] coordinates;

		public Vector3I(int x, int y, int z)
		{
			coordinates = new int[3] { x, y, z };
		}

		public Vector3I(Vector3I vector)
		{
			coordinates = vector.coordinates;
		}

		public Vector3I(GD.Vector3I vector)
		{
			coordinates = new int[3] { vector.X, vector.Y, vector.Z };
		}

		public readonly Vector3I Abs()
		{
			return new Vector3I(Mathf.Abs(X), Mathf.Abs(Y), Mathf.Abs(Z));
		}

		public readonly Vector3I Clamp(Vector3I min, Vector3I max)
		{
			return new Vector3I(Mathf.Clamp(X, min.X, max.X), Mathf.Clamp(Y, min.Y, max.Y), Mathf.Clamp(Z, min.Z, max.Z));
		}

		public readonly float Length()
		{
			return Mathf.Sqrt(X * X + Y * Y + Z * Z);
		}

		public readonly int LengthSquared()
		{
			return X * X + Y * Y + Z * Z;
		}

		public readonly Vector3I Sign()
		{
			return new Vector3I(Mathf.Sign(X), Mathf.Sign(Y), Mathf.Sign(Z));
		}

		#region OPERATORS

		public static Vector3I operator+(Vector3I lhs, Vector3I rhs)
		{
			return new Vector3I(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
		}

		public static Vector3I operator-(Vector3I lhs, Vector3I rhs)
		{
			return new Vector3I(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
		}

		public static Vector3I operator-(Vector3I vector)
		{
			return new Vector3I(-vector.X, -vector.Y, -vector.Z);
		}

		public static Vector3I operator*(Vector3I vector, int scalar)
		{
			return new Vector3I(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}

		public static Vector3I operator*(int scalar, Vector3I vector)
		{
			return vector * scalar;
		}

		public static Vector3I operator*(Vector3I lhs, Vector3I rhs)
		{
			return new Vector3I(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
		}

		public static Vector3I operator/(Vector3I vector, int scalar)
		{
			return new Vector3I(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}

		public static Vector3I operator/(Vector3I lhs, Vector3I rhs)
		{
			return new Vector3I(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z);
		}

		public static Vector3I operator%(Vector3I vector, int scalar)
		{
			return new Vector3I(vector.X % scalar, vector.Y % scalar, vector.Z % scalar);
		}

		public static Vector3I operator%(Vector3I lhs, Vector3I rhs)
		{
			return new Vector3I(lhs.X % rhs.X, lhs.Y % rhs.Y, lhs.Z % rhs.Z);
		}

		public static bool operator==(Vector3I lhs, Vector3I rhs)
		{
			return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
		}

		public static bool operator!=(Vector3I lhs, Vector3I rhs)
		{
			return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
		}

		public static implicit operator GD.Vector3I(Vector3I vector)
		{
			return new GD.Vector3I(vector.X, vector.Y, vector.Z);
		}

		public static explicit operator GD.Vector3(Vector3I vector)
		{
			return new GD.Vector3(vector.X, vector.Y, vector.Z);
		}

		public static explicit operator Vector3I(GD.Vector3 vector)
		{
			return new Vector3I(Mathf.RoundToInt(vector.X), Mathf.RoundToInt(vector.Y), Mathf.RoundToInt(vector.Z));
		}

		#endregion OPERATORS

		public readonly bool Equals(Vector3I other)
		{
			return this == other;
		}

		public override readonly bool Equals(object obj)
		{
			if (obj == null || obj is not Vector3I)
				return false;

			return this == (Vector3I)obj;
		}

		public override readonly int GetHashCode()
		{
			return Y.GetHashCode() ^ X.GetHashCode() ^ Z.GetHashCode();
		}

		public override readonly string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}
	}
}
