using System;
using System.Collections.Generic;
using System.Linq;
using GDataDB;
using GDataDB.Impl;
using Google.GData.Spreadsheets;
using UnityEngine;

public static class ReadGoogleSheetHelper {
    /// <summary>
    /// Get list data from google do
    /// </summary>
    /// <param name="spreadSheetName">Google drive sheet name</param>
    /// <param name="workSheetName">Sheet name</param>
    /// <returns></returns>
    public static List<CellEntry> DoCellQuery(string spreadSheetName, string workSheetName) {
        var client = new DatabaseClient("", "");
        if (string.IsNullOrEmpty(spreadSheetName))
            return null;
        if (string.IsNullOrEmpty(workSheetName))
            return null;
        var error = string.Empty;
        var db = client.GetDatabase(spreadSheetName, ref error);
        if (db == null) {
            Debug.Log("Db null");
            return null;
        }

        var worksheet = ((Database)db).GetWorksheetEntry(workSheetName);
        var cellQuery = new CellQuery(worksheet.CellFeedLink);
        var cellFeed = client.SpreadsheetService.Query(cellQuery);

        return cellFeed.Entries.Cast<CellEntry>().ToList();
    }

    public static List<CellEntry> GetListRow(this List<CellEntry> data, int row) {
        return data.FindAll(s => s.Row == row);
    }

    public static IEnumerable<CellEntry[]> GroupByRows(this List<CellEntry> data) {
        IEnumerable<IGrouping<uint, CellEntry>> groups = data.GroupBy(c => c.Row);

        foreach (IGrouping<uint, CellEntry> group in groups) {
            yield return group.ToArray();
        }
    }

    public static List<CellEntry> GetListRow(this List<CellEntry> data, int row, int colmunFrom, int colmunTo) {
        return data.FindAll(s => s.Row == row && s.Column <= colmunTo && s.Column >= colmunFrom);
    }

    public static List<CellEntry> GetListColumn(this List<CellEntry> data, int column, Func<CellEntry, bool> condition = null) {
        return data.FindAll(s => s.Column == column && (condition != null ? condition.Invoke(s) : true));
    }

    public static List<CellEntry> GetListColumn(this List<CellEntry> data, int column, int rowFrom, int rowTo) {
        return data.FindAll(s => s.Column == column && s.Row <= rowTo && s.Row >= rowFrom);
    }

    public static int GetMaxRow(this List<CellEntry> data, int column) {
        var listColumn = data.GetListColumn(column);

        return listColumn.Count <= 0 ? 0 : (int)listColumn[listColumn.Count - 1].Row;
    }

    public static int GetMaxColumn(this List<CellEntry> data, int row) {
        var listColumn = data.GetListRow(row);

        return (int)listColumn[listColumn.Count - 1].Column;
    }

    public static string GetStringCell(this List<CellEntry> data, int row, int col) {
        var x = data.Find(s => s.Row == row && s.Column == col);
        return x != null ? x.Value : string.Empty;
    }

    public static int GetIntFromCell(this List<CellEntry> data, int row, int col) {
        var x = data.Find(s => s.Row == row && s.Column == col);
        return x == null ? 0 : int.Parse(x.Value);
    }

    public static float GetFloatFromCell(this List<CellEntry> data, int row, int col) {
        var x = data.Find(s => s.Row == row && s.Column == col);
        return x == null ? 0f : float.Parse(x.Value);
    }

    public static int GetInt(this CellEntry data) {
        if (data != null && int.TryParse(data.Value, out int result)) {
            return result;
        }
        return 0;
    }

    public static string GetString(this CellEntry data) {
        return data != null ? data.Value : string.Empty;
    }

    public static float GetFloat(this CellEntry data) {
        if (data != null && float.TryParse(data.Value, out float result)) {
            return result;
        }
        return 0;
    }
}