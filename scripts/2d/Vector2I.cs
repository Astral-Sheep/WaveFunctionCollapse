using Com.Astral.WFC.Utils;
using System;
using GD = Godot;
using Mathf = Godot.Mathf;

namespace Com.Astral.WFC._2D
{
	/// <summary>
	/// Representation of a 2 dimensional vector of int.
	/// </summary>
	public struct Vector2I : IVectorI, IEquatable<Vector2I>
	{
		/// <summary>
		/// Vector with all values set to 0: (0, 0).
		/// </summary>
		public static Vector2I Zero => _zero;
		/// <summary>
		/// Vector with all values set to 1: (1, 1). 
		/// </summary>
		public static Vector2I One => _one;
		/// <summary>
		/// Vector with all values set to -1: (-1, -1).
		/// </summary>
		public static Vector2I NegOne => _negOne;
		/// <summary>
		/// Vector corresponding to the right direction: (1, 0).
		/// </summary>
		public static Vector2I Right => _right;
		/// <summary>
		/// Vector corresponding to the left direction: (-1, 0).
		/// </summary>
		public static Vector2I Left => _left;
		/// <summary>
		/// Vector corresponding to the up direction: (0, -1).
		/// </summary>
		public static Vector2I Up => _up;
		/// <summary>
		/// Vector corresponding to the down direction: (0, 1).
		/// </summary>
		public static Vector2I Down => _down;

		private static readonly Vector2I _zero = new Vector2I(0, 0);
		private static readonly Vector2I _one = new Vector2I(1, 1);
		private static readonly Vector2I _negOne = new Vector2I(-1, -1);
		private static readonly Vector2I _right = new Vector2I(1, 0);
		private static readonly Vector2I _left = new Vector2I(-1, 0);
		private static readonly Vector2I _up = new Vector2I(0, -1);
		private static readonly Vector2I _down = new Vector2I(0, 1);

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

		private int[] coordinates;

		public Vector2I(int x, int y)
		{
			coordinates = new int[2] { x, y };
		}

		public Vector2I(Vector2I vector)
		{
			coordinates = vector.coordinates;
		}

		public Vector2I(GD.Vector2I vector)
		{
			coordinates = new int[2] { vector.X, vector.Y };
		}

		/// <summary>
		/// Return the vector with all its values set to absolute values.
		/// </summary>
		public readonly Vector2I Abs()
		{
			return new Vector2I(Mathf.Abs(X), Mathf.Abs(Y));
		}

		/// <summary>
		/// Clamp the vector between the given minimum vector and the maximum vector.
		/// </summary>
		public readonly Vector2I Clamp(Vector2I min, Vector2I max)
		{
			return new Vector2I(Mathf.Clamp(X, min.X, max.X), Mathf.Clamp(Y, min.Y, max.Y));
		}

		/// <summary>
		/// Return the length of the vector. Use <see cref="LengthSquared"/> if you just need to compare lengths.
		/// </summary>
		public readonly float Length()
		{
			return Mathf.Sqrt(X * X + Y * Y);
		}

		/// <summary>
		/// Return the length squared of the vector.
		/// </summary>
		public readonly int LengthSquared()
		{
			return X * X + Y * Y;
		}

		/// <summary>
		/// Return a vector with its values set to the sign of this vector.
		/// </summary>
		public readonly Vector2I Sign()
		{
			return new Vector2I(Mathf.Sign(X), Mathf.Sign(Y));
		}

		#region OPERATORS

		public static Vector2I operator +(Vector2I lhs, Vector2I rhs)
		{
			return new Vector2I(lhs.X + rhs.X, lhs.Y + rhs.Y);
		}

		public static Vector2I operator -(Vector2I lhs, Vector2I rhs)
		{
			return new Vector2I(lhs.X - rhs.X, lhs.Y - rhs.Y);
		}

		public static Vector2I operator -(Vector2I vector)
		{
			return new Vector2I(-vector.X, -vector.Y);
		}

		public static Vector2I operator *(Vector2I vector, int scalar)
		{
			return new Vector2I(vector.X * scalar, vector.Y * scalar);
		}

		public static Vector2I operator *(int scalar, Vector2I vector)
		{
			return vector * scalar;
		}

		public static Vector2I operator *(Vector2I lhs, Vector2I rhs)
		{
			return new Vector2I(lhs.X * rhs.X, lhs.Y * rhs.Y);
		}

		public static Vector2I operator /(Vector2I vector, int scalar)
		{
			return new Vector2I(vector.X / scalar, vector.Y / scalar);
		}

		public static Vector2I operator /(Vector2I lhs, Vector2I rhs)
		{
			return new Vector2I(lhs.X / rhs.X, lhs.Y / rhs.Y);
		}

		public static Vector2I operator %(Vector2I vector, int scalar)
		{
			return new Vector2I(vector.X % scalar, vector.Y % scalar);
		}

		public static Vector2I operator %(Vector2I lhs, Vector2I rhs)
		{
			return new Vector2I(lhs.X % rhs.X, lhs.Y % rhs.Y);
		}

		public static bool operator ==(Vector2I lhs, Vector2I rhs)
		{
			return lhs.X == rhs.X && lhs.Y == rhs.Y;
		}

		public static bool operator !=(Vector2I lhs, Vector2I rhs)
		{
			return lhs.X != rhs.X || lhs.Y != rhs.Y;
		}

		public static implicit operator GD.Vector2I(Vector2I vector)
		{
			return new GD.Vector2I(vector.X, vector.Y);
		}

		public static explicit operator GD.Vector2(Vector2I vector)
		{
			return new GD.Vector2(vector.X, vector.Y);
		}

		public static explicit operator Vector2I(GD.Vector2 vector)
		{
			return new Vector2I(Mathf.RoundToInt(vector.X), Mathf.RoundToInt(vector.Y));
		}

		#endregion // OPERATORS

		public readonly bool Equals(Vector2I other)
		{
			return this == other;
		}

		public override readonly bool Equals(object obj)
		{
			if (obj == null || obj is not Vector2I)
				return false;

			return this == (Vector2I)obj;
		}

		public override readonly int GetHashCode()
		{
			return Y.GetHashCode() ^ X.GetHashCode();
		}

		public override readonly string ToString()
		{
			return $"({X}, {Y})";
		}
	}
}
