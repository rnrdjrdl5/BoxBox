using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UVDrawData
{
    public float nowPivotX, nowPivotY, nextHeightX, nextHeightY;

    public void SetData(float px, float py, float nx, float ny)
    {
        nowPivotX = px;
        nowPivotY = py;
        nextHeightX = nx;
        nextHeightY = ny;
    }
}


public class Quad : MonoBehaviour {

    Vector3[] GetBoxVertex(Vector3 size)
    {
        List<Vector3> vec3 = new List<Vector3>();

        Vector3[] front = new Vector3[]
        {
            new Vector3(-size.x, -size.y, -size.z),
            new Vector3(-size.x, +size.y, -size.z),
            new Vector3(+size.x, +size.y, -size.z),
            new Vector3(+size.x, -size.y, -size.z)
        };

        Vector3[] top = new Vector3[]
        {
            new Vector3(-size.x,size.y,-size.z),
            new Vector3(-size.x,size.y,size.z),
            new Vector3(size.x,size.y,size.z),
            new Vector3(size.x,size.y,-size.z),
        };

        Vector3[] left = new Vector3[]
        {
            new Vector3(-size.x, -size.y, size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(-size.x, size.y, -size.z),
            new Vector3(-size.x, -size.y, -size.z)
        };

        Vector3[] right = new Vector3[]
        {
            new Vector3(size.x, -size.y, -size.z),
            new Vector3(size.x, size.y, -size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, -size.y, size.z),
        };

        Vector3[] back = new Vector3[]
        {
            new Vector3(size.x, -size.y, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(-size.x, -size.y, size.z),
        };

        Vector3[] bottom = new Vector3[]
        {
            new Vector3(-size.x, -size.y, size.z),
            new Vector3(-size.x, -size.y, -size.z),
            new Vector3(size.x, -size.y, -size.z),
            new Vector3(size.x, -size.y, size.z)
        };

        vec3.AddRange(front);
        vec3.AddRange(top);
        vec3.AddRange(left);
        vec3.AddRange(right);
        vec3.AddRange(back);
        vec3.AddRange(bottom);

        return vec3.ToArray();
    }
    
    Vector2[] GetUV(Vector2 frontPivot, Vector3 uvSize, UVType uVType)
    {
        Vector2[] uvVector2 = null;

        switch (uVType)
        {

            case UVType.FL:
                uvVector2 = GetUVFL(frontPivot, uvSize);
                break;
            case UVType.FB:
                uvVector2 = GetUVFB(frontPivot, uvSize);
                break;
        }

        if (uvVector2 == null) { Debug.LogWarning(" 처리하지 않은 사항"); }
        return uvVector2;
    }

    Vector2[] GetUVFL(Vector2 frontPivot, Vector3 uvSize)
    {

        UVDrawData uVDrawData = new UVDrawData();
        List<Vector2> uvs = new List<Vector2>();

        uVDrawData.SetData(frontPivot.x,
            frontPivot.y,
            uvSize.x, 
            uvSize.y);
        Vector2[] front = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x,
            frontPivot.y + uvSize.y, 
            uvSize.x,
            uvSize.z);
        Vector2[] top = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x + uvSize.x + uvSize.z,
            frontPivot.y,
            uvSize.x,
            uvSize.y);
        Vector2[] back = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x + uvSize.x,
            frontPivot.y,
            uvSize.z,
            uvSize.y);
        Vector2[] left = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x - uvSize.z,
         frontPivot.y,
           uvSize.z,
           uvSize.y);
        Vector2[] right = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x + uvSize.x,
            frontPivot.y + uvSize.y,
               uvSize.z,
               uvSize.x);
        Vector2[] bottom = GetUIElement(uVDrawData);


        uvs.AddRange(front);
        uvs.AddRange(top);
        uvs.AddRange(left);
        uvs.AddRange(right);
        uvs.AddRange(back);
        uvs.AddRange(bottom);

        return uvs.ToArray();
    }

    Vector2[] GetUVFB(Vector2 frontPivot, Vector3 uvSize)
    {
        UVDrawData uVDrawData = new UVDrawData();
        List<Vector2> uvs = new List<Vector2>();

        uVDrawData.SetData(frontPivot.x,
           frontPivot.y,
         uvSize.x,
           uvSize.y);
        Vector2[] front = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x,
            frontPivot.y + uvSize.y,
            uvSize.x,
            uvSize.z);
        Vector2[] top = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x + uvSize.x ,
            frontPivot.y,
            uvSize.x,
            uvSize.y);
        Vector2[] back = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x + uvSize.x *2 ,
            frontPivot.y,
            uvSize.z,
            uvSize.y);
        Vector2[] left = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x - uvSize.z,
         frontPivot.y,
           uvSize.z,
           uvSize.y);
        Vector2[] right = GetUIElement(uVDrawData);


        uVDrawData.SetData(frontPivot.x + uvSize.x,
            frontPivot.y + uvSize.y,
               uvSize.z,
               uvSize.x);
        Vector2[] bottom = GetUIElement(uVDrawData);


        uvs.AddRange(front);
        uvs.AddRange(top);
        uvs.AddRange(left);
        uvs.AddRange(right);
        uvs.AddRange(back);
        uvs.AddRange(bottom);

        return uvs.ToArray();
    }

    Vector2[] GetUIElement(UVDrawData uVDrawData)
    {
        return new Vector2[]
        {
            new Vector2(uVDrawData.nowPivotX + uVDrawData.nextHeightX,
            uVDrawData.nowPivotY),

            new Vector2(uVDrawData.nowPivotX + uVDrawData.nextHeightX,
            uVDrawData.nowPivotY + uVDrawData.nextHeightY),

            new Vector2(uVDrawData.nowPivotX,
            uVDrawData.nowPivotY + uVDrawData.nextHeightY),

            new Vector2(uVDrawData.nowPivotX, 
            uVDrawData.nowPivotY)

        };

    }

    int[] GetQuadTri(Vector3[] Verteices)
    {
        List<int> Datas = new List<int>();

        for (int i = 0; i < Verteices.Length / 4; i++)
        {
            Datas.AddRange(GetTri(i * 4));
        }
        return Datas.ToArray();
    }

    int[] GetTri(int Data)
    {
        return new int[]{
            Data,
            Data+1,
            Data+2,

            Data,
            Data+2,
            Data+3
        };




    }



    public Vector3 boxSize;

    public Vector2 uvStart;
    public Vector3 boxUV;

    // 전개도가 다른 타입별로 UV를 나누기 위해 사용
    public enum UVType { FL, FB }
    public UVType uvType;



    // Use this for initialization
    void Start()
    {

        MeshFilter mf = GetComponent<MeshFilter>();

        Mesh m = new Mesh
        {
            vertices = GetBoxVertex(boxSize),
            uv = GetUV(uvStart, boxUV, uvType)
        };

        m.triangles = GetQuadTri(m.vertices);

        m.RecalculateNormals();

        mf.mesh = m;




    }

}
