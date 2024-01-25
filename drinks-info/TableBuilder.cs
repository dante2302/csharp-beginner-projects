using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks_info
{
    internal class TableBuilder
    {
        public static void PrintTable<T>(List<T> list, [AllowNull] string tableName) 
            where T : class
        {
            if (tableName == null)
                tableName = "";

            ConsoleTableBuilder
                .From(list)
                .WithTitle(tableName)
                .ExportAndWriteLine();
        }
    }
}
