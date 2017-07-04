using System;
using System.Collections.Generic;
namespace ADND
{
    public class WriteSheetConsole :IWriteSheet
    {

        public void WriteSheet(IList<string> parsedSheet)
        {
			foreach (string entry in parsedSheet)
				Console.WriteLine(entry);
        }

    }
}
