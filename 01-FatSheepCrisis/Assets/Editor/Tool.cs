using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Tool
{
    public static string Excel = "Config";//excel表名字

    [MenuItem("Tool/Build Scriptable Data")]
    public static void ExcuteBuildLevel()
    {

        PackageItem holder = ScriptableObject.CreateInstance<PackageItem>();

        //查询excel表中数据，赋值给asset文件
        holder.weapons = Tool._01SelectMenuLevel("WeaponConfig");
        holder.enemyes = Tool._02SelectMenuLevel("EnemyConfig");
        holder.tickets = Tool._03SelectMenuLevel("TicketConfig");
        holder.professions = Tool._04SelectMenuLevel("ProfessionConfig");
        //生成文件路径
        string path = "Assets/Resources/" + Excel + ".asset";

        AssetDatabase.CreateAsset(holder, path);
        AssetDatabase.Refresh();
        Debug.Log(Excel + " " + "BuildAsset Success!");
    }
    //查询数据表
    public static List<WeaponData> _01SelectMenuLevel(string excel)
    {
        string excelName = excel + ".xlsx";
        DataRowCollection collect = Tool.ReadExcel(excelName, 0);

        List<WeaponData> menuArray = new List<WeaponData>();
        for (int i = 1; i < collect.Count; i++)
        {
            if (collect[i][1].ToString() == "") continue;

            WeaponData level = new WeaponData
            {
                Id = collect[i][0].ToString(),
                Name = collect[i][1].ToString(),
                Type = collect[i][2].ToString(),
                Aggressivity = collect[i][3].ToString(),
                AttackInterval=collect[i][4].ToString(),
                CritChance = collect[i][5].ToString(),
                CritDamage =collect[i][6].ToString(),
                RepelNum=collect[i][7].ToString(),
                ProjectilesNum=collect[i][8].ToString(),
                AttackRange=collect[i][9].ToString(),
                SkillDescription=collect[i][10].ToString(),
                Quality= collect[i][11].ToString()
            };
            menuArray.Add(level);
        }
        return menuArray;
    }
    public static List<EnemyData> _02SelectMenuLevel(string excel)
    {
        string excelName = excel + ".xlsx";
        DataRowCollection collect = Tool.ReadExcel(excelName, 0);

        List<EnemyData> menuArray = new List<EnemyData>();
        for (int i = 1; i < collect.Count; i++)
        {
            if (collect[i][1].ToString() == "") continue;

            EnemyData level = new EnemyData
            {
                Id = collect[i][0].ToString(),
                Name = collect[i][1].ToString(),
                Aggressivity = collect[i][2].ToString(),
                Armor = collect[i][3].ToString(),
                Max_Hp = collect[i][4].ToString(),
                MoveSpeed = collect[i][5].ToString(),
                DefenseRepelNum = collect[i][6].ToString(),
            };
            menuArray.Add(level);
        }
        return menuArray;
    }
    public static List<TicketData> _03SelectMenuLevel(string excel)
    {
        string excelName = excel + ".xlsx";
        DataRowCollection collect = Tool.ReadExcel(excelName, 0);

        List<TicketData> menuArray = new List<TicketData>();
        for (int i = 1; i < collect.Count; i++)
        {
            if (collect[i][1].ToString() == "") continue;

            TicketData level = new TicketData
            {
                Id = collect[i][0].ToString(),
                Name = collect[i][1].ToString(),
                Probability = collect[i][2].ToString(),
                Quality = collect[i][3].ToString(),
            };
            menuArray.Add(level);
        }
        return menuArray;
    }
    public static List<ProfessionData> _04SelectMenuLevel(string excel)
    {
        string excelName = excel + ".xlsx";
        DataRowCollection collect = Tool.ReadExcel(excelName, 0);

        List<ProfessionData> menuArray = new List<ProfessionData>();
        for (int i = 1; i < collect.Count; i++)
        {
            if (collect[i][1].ToString() == "") continue;

            ProfessionData level = new ProfessionData
            {
                Id = collect[i][0].ToString(),
                Name = collect[i][1].ToString(),
                Max_Hp = collect[i][2].ToString(),
                Re_Hp = collect[i][3].ToString(),
                Armor = collect[i][4].ToString(),
                MoveSpeed = collect[i][5].ToString(),
                AttackSpeed = collect[i][6].ToString(),
                CritChance = collect[i][7].ToString(),
                CritDamage = collect[i][8].ToString(),
                PickUpRange = collect[i][9].ToString(),
                Exp_GainRate = collect[i][10].ToString(),
                Gold_GainRate = collect[i][11].ToString(),
                ProjectilesNum = collect[i][12].ToString(),
                FinalDamage = collect[i][13].ToString(),
                ExtraDamage = collect[i][14].ToString(),
                AdditionalDamage = collect[i][15].ToString(),
                WeaponType = collect[i][16].ToString(),
                Introduce = collect[i][17].ToString(),
                Weapon = collect[i][18].ToString()
            };
            menuArray.Add(level);
        }
        return menuArray;
    }

    /// <summary>
    /// 读取 Excel ; 需要添加 Excel.dll; System.Data.dll;
    /// </summary>
    /// <param name="excelName">excel文件名</param>
    /// <param name="sheetName">sheet名称</param>
    /// <returns>DataRow的集合</returns>
    public static DataRowCollection ReadExcel(string excelName, int sheetName)
    {
        string path = Application.dataPath + "/Excel/" + excelName;
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        //int columns = result.Tables[0].Columns.Count;
        //int rows = result.Tables[0].Rows.Count;

        //tables可以按照sheet名获取，也可以按照sheet索引获取
        // return result.Tables[3].Rows;
        return result.Tables[sheetName].Rows;
    }
}


