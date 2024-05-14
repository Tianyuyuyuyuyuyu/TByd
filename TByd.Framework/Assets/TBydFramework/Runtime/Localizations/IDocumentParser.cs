﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TBydFramework.Runtime.Localizations
{
    public interface IDocumentParser
    {
        Dictionary<string, object> Parse(Stream input, CultureInfo cultureInfo);
        
    }
}
