using Com.Astral.WFC.Utils;
using System;
using GD = Godot;
using Mathf = Godot.Mathf;

namespace Com.Astral.WFC._3D
{
	/// <summary>
	/// Representation of a 3 dimensional vector of int.
	/// </summary>
	public struct Vector3I : IVectorI, IEquatable<Vector3I>
	{
		/// <summary>
		/// Vector with all values set to 0: (0, 0, 0).
		/// </summary>
		public static Vector3I Zero => _zero;
		/// <summary>
		/// Vector with all values set to 1: (1, 1, 1).
		/// </summary>
		public static Vector3I One => _one;
		/// <summary>
		/// Vector with all values set to -1: (-1, -1, -1).
		/// </summary>
		public static Vector3I NegOne => _negOne;
		/// <summary>
		/// Vector corresponding to the right direction: (1, 0, 0).
		/// </summary>
		public static Vector3I Right => _right;
		/// <summary>
		/// Vector corresponding to the left direction: (-1, 0, 0).
		/// </summary>
		public static Vector3I Left => _left;
		/// <summary>
		/// Vector corresponding to the up direction: (0, 1, 0).
		/// </summary>
		public static Vector3I Up => _up;
		/// <summary>
		/// Vector corresponding to the down direction: (0, -1, 0).
		/// </summary>
		public static Vector3I Down => _down;
		/// <summary>
		/// Vector corresponding to the forward direction: (0, 0, -1).
		/// </summary>
		public static Vector3I Forward => _forward;
		/// <summary>
		/// Vector corresponding to the back direction: (0, 0, 1).
		/// </summary>
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

		/// <summary>
		/// The coordinate on the X axis.
		/// </summary>
		public int X
		{
			readonly get => coordinates[0];
			set => coordinates[0] = value;
		}

		/// <summary>
		/// The coordinate on the Y axis.
		/// </summary>
		public int Y
		{
			readonly get => coordinates[1];
			set => coordinates[1] = value;
		}

		/// <summary>
		/// The coordinate on the Z axis.
		/// </summary>
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

		/// <summary>
		/// Return the vector with all its values set to absolute values.
		/// </summary>
		public readonly Vector3I Abs()
		{
			return new Vector3I(Mathf.Abs(X), Mathf.Abs(Y), Mathf.Abs(Z));
		}

		/// <summary>
		/// Clamp the vector between the given minimum vector and the maximum vector.
		/// </summary>
		public readonly Vector3I Clamp(Vector3I min, Vector3I max)
		{
			return new Vector3I(Mathf.Clamp(X, min.X, max.X), Mathf.Clamp(Y, min.Y, max.Y), Mathf.Clamp(Z, min.Z, max.Z));
		}

		/// <summary>
		/// Return the length of the vector. Use <see cref="LengthSquared"/> if you just need to compare lengths.
		/// </summary>
		public readonly float Length()
		{
			return Mathf.Sqrt(X * X + Y * Y + Z * Z);
		}

		/// <summary>
		/// Return the length squared of the vector.
		/// </summary>
		public readonly int LengthSquared()
		{
			return X * X + Y * Y + Z * Z;
		}

		/// <summary>
		/// Return a vector with its values set to the sign of this vector.
		/// </summary>
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
