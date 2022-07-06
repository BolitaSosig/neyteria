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

    public static Color RAREZA_1 = new Color(0.7f, 0.7f, 0.7f);
    public static Color RAREZA_2 = new Color(0.5f, 0.9f, 0.9f);
    public static Color RAREZA_3 = new Color(0.5f, 0.9f, 0.5f);
    public static Color RAREZA_4 = new Color(0.9f, 0.5f, 0.9f);
    public static Color RAREZA_5 = new Color(0.9f, 0.9f, 0.5f);

    public static Color GetRarezaColor(int r)
    {
        switch(r)
        {
            case 1:
                return RAREZA_1;
            case 2:
                return RAREZA_2;
            case 3:
                return RAREZA_3;
            case 4:
                return RAREZA_4;
            case 5:
                return RAREZA_5;
        }
        return new Color();
    }

    public static Item NONE;
    public static Item DEGITERIO;
    public static Item OCCATERIO;
    public static Item SUERO_VITAL;
    public static Item SUERO_ENERGETICO;
    public static Item SUERO_FORTALECEDOR;
    public static Item SUERO_PROTECTOR;
    public static Item MINERAL_FRAGMENTADO;
    public static Item NEXOTERIO;
    public static Item MINERAL_COMPACTO;
    public static Item PIEDRAS_DE_LAVA;
    public static Item ROCA_DE_MAGMA;
    public static Item SOLLOZOS_DEL_CREPUSCULO;
    public static Item TEMOR_DEL_CREPUSCULO;
    public static Item GOTAS_DE_SLIME;
    public static Item GOTAS_DE_SLIME_FURIOSO;
    public static Item GOTAS_DE_SLIME_DE_LAVA;
    public static Item TROZO_DE_HUESO;
    public static Item TROZO_DE_HUESO_EXTRANO;
    public static Item HUESO_CONTAMINADO;

    public static Modulo MODULO_DE_SUPERSALTO_1;
    public static Modulo MODULO_DE_SUPERSALTO_2;
    public static Modulo MODULO_DE_SUPERSALTO_3;
    public static Modulo MODULO_DE_INVENCIBILIDAD_1;
    public static Modulo MODULO_DE_AGUANTE_1;
    public static Modulo MODULO_DE_RUMARH_1;
    public static Modulo MODULO_DE_RUMARH_2;
    public static Modulo MODULO_DE_RUMARH_3;
    public static Modulo MODULO_DE_CONVERSION;
    public static Modulo MODULO_DE_LA_FURIA;
    public static Modulo MODULO_NULO;

    public static Arma ESPADA_CORTA;
    public static Arma MAZA;
    public static Arma CANON_LASER;
    public static Arma SABLE_PICAPIEDRA;
    public static Arma PORRA_DEMOLEROCAS;
    public static Arma PISTOLA_ROMPEMUROS;

    public static Traje TUNICA_PROTECTORA;
    public static Traje TRAJE_DE_VITALIDAD;
    public static Traje MANTO_DE_LA_FURIA;
    public static Traje ROPA_DE_LOS_RAUVNIR;
    public static Traje TUNICA_DEL_MENSAJERO;
    public static Traje TUNICA_LIGERA;
    public static Traje VESTIDO_DEL_COSECHADOR;
    public static Traje CONJUNTO_VOLADOR;
    public static Traje ARMADURA_DE_FTREQIS;
    public static Traje SABANA_DEL_TIEMPO;
    public static Traje BENDICION_DE_LAS_NUBES;
    public static Traje MANTO_DE_LA_LOCURA;

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
        NEXOTERIO = Resources.Load<Item>("Data\\Item\\008-Nexoterio");
        MINERAL_COMPACTO = Resources.Load<Item>("Data\\Item\\009-Mineral_compacto");
        PIEDRAS_DE_LAVA = Resources.Load<Item>("Data\\Item\\010-Piedras_de_lava");
        ROCA_DE_MAGMA = Resources.Load<Item>("Data\\Item\\011-Roca_de_magma");
        SOLLOZOS_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\012-Sollozos_del_crepusculo");
        TEMOR_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\013-Temor_del_crepusculo");
        GOTAS_DE_SLIME = Resources.Load<Item>("Data\\Item\\014-Gotas_de_slime");
        GOTAS_DE_SLIME_FURIOSO = Resources.Load<Item>("Data\\Item\\015-Gotas_de_slime_furioso");
        GOTAS_DE_SLIME_DE_LAVA = Resources.Load<Item>("Data\\Item\\016-Gotas_de_slime_de_lava");
        TROZO_DE_HUESO = Resources.Load<Item>("Data\\Item\\017-Trozo_de_hueso");
        TROZO_DE_HUESO_EXTRANO = Resources.Load<Item>("Data\\Item\\018-Trozo_de_hueso_extrano");
        HUESO_CONTAMINADO = Resources.Load<Item>("Data\\Item\\019-Hueso_contaminado");

        MODULO_DE_SUPERSALTO_1 = Resources.Load<Modulo>("Data\\Modulo\\100-Modulo_de_supersalto_1");
        MODULO_DE_SUPERSALTO_2 = Resources.Load<Modulo>("Data\\Modulo\\101-Modulo_de_supersalto_2");
        MODULO_DE_SUPERSALTO_3 = Resources.Load<Modulo>("Data\\Modulo\\102-Modulo_de_supersalto_3");
        MODULO_DE_INVENCIBILIDAD_1 = Resources.Load<Modulo>("Data\\Modulo\\103-Modulo_de_invencibilidad_1");
        MODULO_DE_AGUANTE_1 = Resources.Load<Modulo>("Data\\Modulo\\104-Modulo_de_aguante_1");
        MODULO_DE_RUMARH_1 = Resources.Load<Modulo>("Data\\Modulo\\105-Modulo_de_Rumarh_1");
        MODULO_DE_RUMARH_2 = Resources.Load<Modulo>("Data\\Modulo\\106-Modulo_de_Rumarh_2");
        MODULO_DE_RUMARH_3 = Resources.Load<Modulo>("Data\\Modulo\\107-Modulo_de_Rumarh_3");
        MODULO_DE_CONVERSION = Resources.Load<Modulo>("Data\\Modulo\\108-Modulo_de_conversion");
        MODULO_DE_LA_FURIA  = Resources.Load<Modulo>("Data\\Modulo\\109-Modulo_de_la_furia");
        MODULO_NULO  = Resources.Load<Modulo>("Data\\Modulo\\199-Modulo_nulo");

        ESPADA_CORTA = Resources.Load<Arma>("Data\\Arma\\200-Espada_corta");
        MAZA = Resources.Load<Arma>("Data\\Arma\\201-Maza");
        CANON_LASER = Resources.Load<Arma>("Data\\Arma\\202-Cañon_laser");
        SABLE_PICAPIEDRA = Resources.Load<Arma>("Data\\Arma\\203-Sable_picapiedra");
        PORRA_DEMOLEROCAS = Resources.Load<Arma>("Data\\Arma\\204-Porra_demolerocas");
        PISTOLA_ROMPEMUROS = Resources.Load<Arma>("Data\\Arma\\205-Pistola_rompemuros");

        TUNICA_PROTECTORA = Resources.Load<Traje>("Data\\Traje\\250-Tunica_protectora");
        TRAJE_DE_VITALIDAD = Resources.Load<Traje>("Data\\Traje\\251-Traje_de_vitalidad");
        MANTO_DE_LA_FURIA = Resources.Load<Traje>("Data\\Traje\\252-Manto_de_la_furia");
        ROPA_DE_LOS_RAUVNIR = Resources.Load<Traje>("Data\\Traje\\253-Ropa_de_los_rauvnir");
        TUNICA_DEL_MENSAJERO = Resources.Load<Traje>("Data\\Traje\\254-Tunica_del_mensajero");
        TUNICA_LIGERA = Resources.Load<Traje>("Data\\Traje\\255-Tunica_ligera");
        VESTIDO_DEL_COSECHADOR = Resources.Load<Traje>("Data\\Traje\\256-Vestido_del_cosechador");
        CONJUNTO_VOLADOR = Resources.Load<Traje>("Data\\Traje\\257-Conjunto_volador");
        ARMADURA_DE_FTREQIS = Resources.Load<Traje>("Data\\Traje\\258-Armadura_de_ftreqis");
        SABANA_DEL_TIEMPO = Resources.Load<Traje>("Data\\Traje\\259-Sabana_del_tiempo");
        BENDICION_DE_LAS_NUBES = Resources.Load<Traje>("Data\\Traje\\260-Bendicion_de_las_nubes");
        MANTO_DE_LA_LOCURA = Resources.Load<Traje>("Data\\Traje\\261-Manto_de_la_locura");

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
            NEXOTERIO, 
            MINERAL_COMPACTO, 
            PIEDRAS_DE_LAVA, 
            ROCA_DE_MAGMA, 
            SOLLOZOS_DEL_CREPUSCULO, 
            TEMOR_DEL_CREPUSCULO,
            GOTAS_DE_SLIME,
            GOTAS_DE_SLIME_FURIOSO,
            GOTAS_DE_SLIME_DE_LAVA, 
            TROZO_DE_HUESO, 
            TROZO_DE_HUESO_EXTRANO, 
            HUESO_CONTAMINADO, 
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
            MODULO_NULO,
            ESPADA_CORTA,
            MAZA,
            CANON_LASER,
            SABLE_PICAPIEDRA,
            PORRA_DEMOLEROCAS,
            PISTOLA_ROMPEMUROS,
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
            TUNICA_PROTECTORA,
            TRAJE_DE_VITALIDAD,
            MANTO_DE_LA_FURIA,
            ROPA_DE_LOS_RAUVNIR,
            TUNICA_DEL_MENSAJERO,
            TUNICA_LIGERA,
            VESTIDO_DEL_COSECHADOR,
            CONJUNTO_VOLADOR,
            ARMADURA_DE_FTREQIS,
            SABANA_DEL_TIEMPO,
            BENDICION_DE_LAS_NUBES,
            MANTO_DE_LA_LOCURA
        };
    }

    public static Item getItemByID(int id)
    {
        return allItems[id];
    }
}
