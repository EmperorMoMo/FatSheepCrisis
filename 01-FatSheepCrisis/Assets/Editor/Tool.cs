using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Tool
{
    [MenuItem("BuildAsset/Build Scriptable Data")]
    public static void ExcuteBuildLevel()
    {
        PackageItem holder = ScriptableObject.CreateInstance<PackageItem>();

        //��ѯexcel�������ݣ���ֵ��asset�ļ�
        holder.items = Tool.SelectMenuLevel();
        //�����ļ�·��
        string path = "Assets/Resources/WeaponConfig.asset";

        AssetDatabase.CreateAsset(holder, path);
        AssetDatabase.Refresh();
        Debug.Log("BuildAsset Success!");
    }

    public static string Excel = "WeaponConfig";//excel������
    //��ѯ���ݱ�
    public static List<ItemData> SelectMenuLevel()
    {
        string excelName = Excel + ".xlsx";
        DataRowCollection collect = Tool.ReadExcel(excelName, 0);

        List<ItemData> menuArray = new List<ItemData>();
        for (int i = 1; i < collect.Count; i++)
        {
            if (collect[i][1].ToString() == "") continue;

            ItemData level = new ItemData
            {
                Id = collect[i][0].ToString(),
                Name = collect[i][1].ToString(),
                Aggressivity = collect[i][2].ToString(),
                AttackSpeed=collect[i][3].ToString(),
                CritChance = collect[i][4].ToString(),
                CritDamage =collect[i][5].ToString(),
                RepelNum=collect[i][6].ToString(),
                ProjectilesNum=collect[i][7].ToString(),
                AttackRange=collect[i][8].ToString(),
                SkillDescription=collect[i][9].ToString()
            };
            menuArray.Add(level);
        }
        return menuArray;
    }
    /// <summary>
    /// ��ȡ Excel ; ��Ҫ��� Excel.dll; System.Data.dll;
    /// </summary>
    /// <param name="excelName">excel�ļ���</param>
    /// <param name="sheetName">sheet����</param>
    /// <returns>DataRow�ļ���</returns>
    static DataRowCollection ReadExcel(string excelName, int sheetName)
    {
        string path = Application.dataPath + "/Excel/" + excelName;
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        //int columns = result.Tables[0].Columns.Count;
        //int rows = result.Tables[0].Rows.Count;

        //tables���԰���sheet����ȡ��Ҳ���԰���sheet������ȡ
        // return result.Tables[3].Rows;
        return result.Tables[sheetName].Rows;
    }
}

