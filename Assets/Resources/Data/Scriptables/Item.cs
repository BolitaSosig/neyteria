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
    public static Item MODULO_DE_INVENCIBILIDAD_1;
    public static Item MODULO_DE_AGUANTE_1;
    public static Item MODULO_DE_RUMARH_1;
    public static Item MODULO_DE_RUMARH_2;
    public static Item MODULO_DE_RUMARH_3;
    public static Item MODULO_DE_CONVERSION;
    public static Item MODULO_DE_LA_FURIA;

    public static Item ESPADA_CORTA;
    public static Item MAZA;
    public static Item CANON_LASER;
    public static Item SABLE_PICAPIEDRA;
    public static Item PORRA_DEMOLEROCAS;
    public static Item PISTOLA_ROMPEMUROS;



    private static Item[] allItems;

    private void OnEnable()
    {
        NONE = Resources.Load<Item>("Data\\Item\\000-NONE");
        DEGITERIO = Resources.Load<Item>("Data\\Item\\001-Degiterio");
        OCCATERIO = Resources.Load<Item>("Data\\Item\\002-Occaterio");
        SUERO_VITAL = Resources.Load<Item>("Data\\Item\\003-Suero_vital");
        SUERO_ENERGETICO = Resources.Load<Item>("Data\\Item\\004-Suero_energetico");
        SUERO_FORTALECEDOR = Resources.Load<Item>("Data\\Item\\005-Suero_fortalecedor");
        SUERO_PROTECTOR = Resources.Load<Item>("Data\\Item\\006-Suero_protector");
        MINERAL_FRAGMENTADO = Resources.Load<Item>("Data\\Item\\007-Mineral_fragmentado");
        NEXOTEK = Resources.Load<Item>("Data\\Item\\008-Nexotek");
        MINERAL_COMPACTO = Resources.Load<Item>("Data\\Item\\009-Mineral_compacto");
        PIEDRAS_DE_LAVA = Resources.Load<Item>("Data\\Item\\010-Piedras_de_lava");
        ROCA_DE_MAGMA = Resources.Load<Item>("Data\\Item\\011-Roca_de_magma");
        SOLLOZOS_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\012-Sollozos_del_crepusculo");
        TEMOR_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\013-Temor_del_crepusculo");
        GOTAS_DE_SLIME = Resources.Load<Item>("Data\\Item\\014-Gotas_de_slime");
        GOTAS_DE_SLIME_FURIOSO = Resources.Load<Item>("Data\\Item\\015-Gotas_de_slime_furioso");
        GOTAS_DE_SLIME_DE_LAVA = Resources.Load<Item>("Data\\Item\\016-Gotas_de_slime_de_lava");

        MODULO_DE_SUPERSALTO_1 = Resources.Load<Item>("Data\\Modulo\\100-Modulo_de_supersalto_1");
        MODULO_DE_SUPERSALTO_2 = Resources.Load<Item>("Data\\Modulo\\101-Modulo_de_supersalto_2");
        MODULO_DE_SUPERSALTO_3 = Resources.Load<Item>("Data\\Modulo\\102-Modulo_de_supersalto_3");
        MODULO_DE_INVENCIBILIDAD_1 = Resources.Load<Item>("Data\\Modulo\\103-Modulo_de_invencibilidad_1");
        MODULO_DE_AGUANTE_1 = Resources.Load<Item>("Data\\Modulo\\104-Modulo_de_aguante_1");
        MODULO_DE_RUMARH_1 = Resources.Load<Item>("Data\\Modulo\\105-Modulo_de_Rumarh_1");
        MODULO_DE_RUMARH_2 = Resources.Load<Item>("Data\\Modulo\\106-Modulo_de_Rumarh_2");
        MODULO_DE_RUMARH_3 = Resources.Load<Item>("Data\\Modulo\\107-Modulo_de_Rumarh_3");
        MODULO_DE_CONVERSION = Resources.Load<Item>("Data\\Modulo\\108-Modulo_de_conversion");
        MODULO_DE_LA_FURIA  = Resources.Load<Item>("Data\\Modulo\\109-Modulo_de_la_furia");

        ESPADA_CORTA = Resources.Load<Item>("Data\\Arma\\200-Espada_corta");
        MAZA = Resources.Load<Item>("Data\\Arma\\201-Maza");
        CANON_LASER = Resources.Load<Item>("Data\\Arma\\202-Cañon_laser");
        SABLE_PICAPIEDRA = Resources.Load<Item>("Data\\Arma\\203-Sable_picapiedra");
        PORRA_DEMOLEROCAS = Resources.Load<Item>("Data\\Arma\\204-Porra_demolerocas");
        PISTOLA_ROMPEMUROS = Resources.Load<Item>("Data\\Arma\\205-Pistola_rompemuros");

        allItems = new Item[]
        {
            NONE, 
            DEGITERIO, 
            OCCATERIO, 
            SUERO_VITAL, 
            SUERO_ENERGETICO, 
            SUERO_FORTALECEDOR, 
            SUERO_PROTECTOR, 
            MINERAL_FRAGMENTADO,
            NEXOTEK, 
            MINERAL_COMPACTO, 
            PIEDRAS_DE_LAVA, 
            ROCA_DE_MAGMA, 
            SOLLOZOS_DEL_CREPUSCULO, 
            TEMOR_DEL_CREPUSCULO,
            GOTAS_DE_SLIME,
            GOTAS_DE_SLIME_FURIOSO,
            GOTAS_DE_SLIME_DE_LAVA, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null,
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null,
            null, 
            null, 
            null, 
            null,
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null,
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null,
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            null,
            null, 
            null, 
            null, 
            null, 
            null, 
            null, 
            MODULO_DE_SUPERSALTO_1, 
            MODULO_DE_SUPERSALTO_2, 
            MODULO_DE_SUPERSALTO_3,
            MODULO_DE_INVENCIBILIDAD_1,
            MODULO_DE_AGUANTE_1,
            MODULO_DE_RUMARH_1,
            MODULO_DE_RUMARH_2,
            MODULO_DE_RUMARH_3,
            MODULO_DE_CONVERSION,
            MODULO_DE_LA_FURIA,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            ESPADA_CORTA,
            MAZA,
            CANON_LASER,
            SABLE_PICAPIEDRA,
            PORRA_DEMOLEROCAS,
            PISTOLA_ROMPEMUROS
        };
    }

    public static Item getItemByID(int id)
    {
        return allItems[id];
    }
}
