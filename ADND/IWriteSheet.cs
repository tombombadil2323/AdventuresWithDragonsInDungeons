using System;
using System.Collections.Generic;
namespace ADND
{
    public interface IWriteSheet
    {
        void WriteSheet(IList<string> parsedSheet);
    }
}
