using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NL.GI.ComponentesWPF.Cliente
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class GIGridAttributes : Attribute
    {
        public virtual string Header { get; set; }
        public virtual string ColumnName { get; set; }
        public virtual bool IsColumnVisible { get; set; }
        public virtual bool IsFilterable { get; set; }
        public virtual bool IsColumnReadOnly { get; set; }
        public virtual TextAlignment TextAlignment { get; set; }

        public GIGridAttributes(string header, string columnName, bool isColumnVisible = true) : this(header, columnName, isColumnVisible, true)
        { 
        }

        public GIGridAttributes(string header, string columnName, bool isColumnVisible, bool isFilterable, bool isColumnReadOnly = true, TextAlignment alignment = System.Windows.TextAlignment.Left)
        {
            this.Header = header;
            this.ColumnName = columnName;
            this.IsColumnVisible = isColumnVisible;
            this.IsFilterable = IsFilterable;
            this.IsColumnReadOnly = isColumnReadOnly;
            this.TextAlignment = alignment;
        }
    }
}
