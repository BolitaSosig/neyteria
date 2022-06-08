using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDrop", menuName = "Scriptable/Drop Enemigo")]
public class EnemyDrop : ScriptableObject
{
    public Item[] drop;
    public int[] cant;
    [Range(0, 1)]
    public float[] rate;
    public bool[] asegurado;

    private List<(Item, int)> CalculateDrops()
    {
        List<(Item, int)> drops = new List<(Item, int)>();
        for(int i = 0; i < drop.Length; i++)
        {
            int cantidad = asegurado[i] ? 1 : 0;
            for (int j = cantidad; j < cant[i]; j++)
            {
                float prob = Random.value;
                cantidad += prob <= rate[i] ? 1 : 0;
            }
            if (cantidad > 0)
            {
                drops.Add((drop[i], cantidad));
            }
        }
        return drops;
    }

    public void GetDrops(Transform t, GameObject itemOrbPrefab)
    {
        PlayerItems pi = FindObjectOfType<PlayerItems>();
        foreach ((Item i, int c) in CalculateDrops())
        {
            GameObject go = Instantiate(itemOrbPrefab, t.position, new Quaternion());
            go.GetComponent<ItemOrbController>().Create(i, c);
        }
    }
}
