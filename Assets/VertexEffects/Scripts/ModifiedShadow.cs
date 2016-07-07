using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The behaviour of this class is almost the same as the original except:
/// 1. It absorbs version differences.
/// 2. It corrects the calculation of vertex list capacity (Unity 5.3 or older).
/// </summary>
public class ModifiedShadow : Shadow
{
#if !UNITY_5_4_OR_NEWER
    protected new void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
    {
        UIVertex vt;

        // The capacity calculation of the original version seems wrong.
        var neededCpacity = verts.Count + (end - start);
        if (verts.Capacity < neededCpacity)
            verts.Capacity = neededCpacity;

        for (int i = start; i < end; ++i)
        {
            vt = verts[i];
            verts.Add(vt);

            Vector3 v = vt.position;
            v.x += x;
            v.y += y;
            vt.position = v;
            var newColor = color;
            if (useGraphicAlpha)
                newColor.a = (byte)((newColor.a * verts[i].color.a) / 255);
            vt.color = newColor;
            verts[i] = vt;
        }
    }
#endif

#if UNITY_5_2 && !UNITY_5_2_1pX
    public override void ModifyMesh(Mesh mesh)
    {
        if (!this.IsActive())
            return;

        using (var vh = new VertexHelper(mesh))
        {
            ModifyMesh(vh);
            vh.FillMesh(mesh);
        }
    }
#endif

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
#if UNITY_5_2_1pX || UNITY_5_3_OR_NEWER
    public override void ModifyMesh(VertexHelper vh)
#else
    public void ModifyMesh(VertexHelper vh)
#endif
    {
        if (!this.IsActive())
            return;

        var list = ListPool<UIVertex>.Get();
        vh.GetUIVertexStream(list);

        ModifyVertices(list);

#if UNITY_5_2_1pX || UNITY_5_3_OR_NEWER
        vh.Clear();
#endif
        vh.AddUIVertexTriangleStream(list);
        ListPool<UIVertex>.Release(list);
    }

    public virtual void ModifyVertices(List<UIVertex> verts)
    {
    }
#endif
}
