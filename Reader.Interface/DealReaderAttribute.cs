using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace Reader.Interface
{
    public interface IDealReaderAttributeMetadata
    {
        string FormatType { get; }
    }

    public class DealReaderAttribute : ExportAttribute, IDealReaderAttributeMetadata
    {
        public DealReaderAttribute(string formattype, Type contractType): base(contractType)
        {
            this.FormatType = formattype;
        }

        public string FormatType { get; private set; }
    }
}
