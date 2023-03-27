using Com.Astral.WFC._3D;
using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;
using Vector3I = Com.Astral.WFC._3D.Vector3I;

namespace Com.Astral.WFC.Rendering
{
	public partial class Cell3D : Node3D, ICell<Pattern3D, Vector3I>
	{
		protected const string MESH_PATH = "res://assets/meshes/cell_part.obj";
		protected const float MESH_SIZE = 1f;
		protected List<MeshInstance3D> meshes;

		public void Render(Pattern3D pPattern)
		{
			if (meshes != null || pPattern.Entropy > 0)
				return;

			meshes = new List<MeshInstance3D>();
			int lState = pPattern.GetState();

			for (sbyte i = 0; i < 3; i++)
			{
				if (Data3D.GetStateOnAxis(lState, (Axis)(1 << i)) > 0)
				{
					CreateMesh(new Vector3(i == 0 ? 1 : 0, i == 1 ? 1 : 0, i == 2 ? 1 : 0));
				}

				if (Data3D.GetStateOnAxis(lState, (Axis)~(1 << i)) > 0)
				{
					CreateMesh(new Vector3(i == 0 ? -1 : 0, i == 1 ? -1 : 0, i == 2 ? -1 : 0));
				}
			}

			if (meshes.Count > 0)
			{
				CreateMesh(Vector3.Zero);
			}
		}

		public void Reset()
		{
			if (meshes == null)
				return;

			for (int i = meshes.Count - 1; i >= 0; i--)
			{
				meshes[i].QueueFree();
				meshes.RemoveAt(i);
			}

			meshes = null;
		}

		protected void CreateMesh(Vector3 pPosition)
		{
			MeshInstance3D lMesh = new MeshInstance3D()
			{
				Mesh = GD.Load<Mesh>(MESH_PATH)
			};

			AddChild(lMesh);
			lMesh.Position = pPosition * MESH_SIZE;
			meshes.Add(lMesh);
		}
	}
}