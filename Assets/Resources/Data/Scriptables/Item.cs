using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable/Item")]
public class Item : ScriptableObject
{
    public int ID;
    public string nombre;
    [TextArea(3, 10)]
    public string descripcion; 
    public Sprite icono;
    [Range(1, 5)]
    public int rareza;
    public bool usable;
    public int maxCant = 999;
    public bool visible = true;



    public static Item NONE;
    public static Item DEGITERIO;
    public static Item OCCATERIO;
    public static Item SUERO_VITAL;
    public static Item SUERO_ENERGETICO;
    public static Item SUERO_FORTALECEDOR;
    public static Item SUERO_PROTECTOR;
    public static Item MINERAL_FRAGMENTADO;
    public static Item NEXOTEK;
    public static Item MINERAL_COMPACTO;
    public static Item PIEDRAS_DE_LAVA;
    public static Item ROCA_DE_MAGMA;
    public static Item SOLLOZOS_DEL_CREPUSCULO;
    public static Item TEMOR_DEL_CREPUSCULO;
    public static Item GOTAS_DE_SLIME;
    public static Item GOTAS_DE_SLIME_FURIOSO;
    public static Item GOTAS_DE_SLIME_DE_LAVA;
    public static Item MODULO_DE_SUPERSALTO_1;
    public static Item MODULO_DE_SUPERSALTO_2;
    public static Item MODULO_DE_SUPERSALTO_3;

    private static Item[] allItems;

    private void OnEnable()
    {
        NONE = Resources.Load<Item>("Data\\Item\\NONE");
        DEGITERIO = Resources.Load<Item>("Data\\Item\\Degiterio");
        OCCATERIO = Resources.Load<Item>("Data\\Item\\Occaterio");
        SUERO_VITAL = Resources.Load<Item>("Data\\Item\\Suero_vital");
        SUERO_ENERGETICO = Resources.Load<Item>("Data\\Item\\Suero_energetico");
        SUERO_FORTALECEDOR = Resources.Load<Item>("Data\\Item\\Suero_fortalecedor");
        SUERO_PROTECTOR = Resources.Load<Item>("Data\\Item\\Suero_protector");
        MINERAL_FRAGMENTADO = Resources.Load<Item>("Data\\Item\\Mineral_fragmentado");
        NEXOTEK = Resources.Load<Item>("Data\\Item\\Nexotek");
        MINERAL_COMPACTO = Resources.Load<Item>("Data\\Item\\Mineral_compacto");
        PIEDRAS_DE_LAVA = Resources.Load<Item>("Data\\Item\\Piedras_de_lava");
        ROCA_DE_MAGMA = Resources.Load<Item>("Data\\Item\\ROca_de_magma");
        SOLLOZOS_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\Sollozos_del_crepusculo");
        TEMOR_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\Temor_del_crepusculo");
        GOTAS_DE_SLIME = Resources.Load<Item>("Data\\Item\\Gotas_de_slime");
        GOTAS_DE_SLIME_FURIOSO = Resources.Load<Item>("Data\\Item\\Gotas_de_slime_furioso");
        GOTAS_DE_SLIME_DE_LAVA = Resources.Load<Item>("Data\\Item\\Gotas_de_slime_de_lava");
        MODULO_DE_SUPERSALTO_1 = Resources.Load<Item>("Data\\Modulo\\Modulo_de_Supersalto_1");
        MODULO_DE_SUPERSALTO_2 = Resources.Load<Item>("Data\\Modulo\\Modulo_de_Supersalto_2");
        MODULO_DE_SUPERSALTO_3 = Resources.Load<Item>("Data\\Modulo\\Modulo_de_Supersalto_3");

        allItems = new Item[]
        {
            NONE, DEGITERIO, OCCATERIO, SUERO_VITAL, SUERO_ENERGETICO, SUERO_FORTALECEDOR, SUERO_PROTECTOR, MINERAL_FRAGMENTADO,
            NEXOTEK, MINERAL_COMPACTO, PIEDRAS_DE_LAVA, ROCA_DE_MAGMA, SOLLOZOS_DEL_CREPUSCULO, TEMOR_DEL_CREPUSCULO,
            GOTAS_DE_SLIME, GOTAS_DE_SLIME_FURIOSO, GOTAS_DE_SLIME_DE_LAVA, null, null, null, null, null, null, null, null, 
            null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, MODULO_DE_SUPERSALTO_1, MODULO_DE_SUPERSALTO_2, MODULO_DE_SUPERSALTO_3
        };
    }

    public static Item getItemByID(int id)
    {
        return allItems[id];
    } 
}
