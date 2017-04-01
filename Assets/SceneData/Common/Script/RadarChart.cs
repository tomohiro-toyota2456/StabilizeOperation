namespace Common
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  //****************************************
  //レーダーチャートを表現するクラス
  //****************************************
  public class RadarChart : Graphic
  {
    [SerializeField]
    int elementNum;//頂点数 3以上を推奨
    [SerializeField]
    float[] elementVal;//頂点の長さ任意で決める
    [SerializeField]
    Color centerCol;//中心カラー
    [SerializeField]
    Color color;//頂点カラー

    public void SetValues(float[] _values)
    {
      elementVal = _values;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      UIVertex basevertex = new UIVertex();
      basevertex.position.x = 0.0f;
      basevertex.position.y = 0.0f;
      basevertex.color = centerCol;

      //１週分の角度を分割する
      float oneRad = (2 * Mathf.PI) / elementNum;

      List<UIVertex> vList = new List<UIVertex>();

      vList.Add(basevertex);

      //要素数だけ頂点を作る
      for(int i = 0; i < elementNum; i++)
      {
        float val = 0.0f;

        if(i < elementVal.Length)
        {
          val = elementVal[i];
        }

        UIVertex vertex = new UIVertex();
        vertex.position.x = Mathf.Sin(oneRad * i)*val;
        vertex.position.y = Mathf.Cos(oneRad * i)*val;
        vertex.color = color;

        vList.Add(vertex);
      }

      //頂点インデックスリスト作成
      List<int> iList = new List<int>();
      for(int  i = 0; i < elementNum; i++)
      {
        int one = 0;
        int two = i + 1;
        int three = i + 2;

        if(three > elementNum)
        {
          three = 1;
        }

        iList.Add(one);
        iList.Add(two);
        iList.Add(three);
      }

      vh.AddUIVertexStream(vList, iList);
    }
  }
}
