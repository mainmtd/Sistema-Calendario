using System.Collections.Generic;
using System.Windows.Media;
using Telerik.Windows.Controls;
using System.Linq;
using System.Windows.Data;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIGrid : RadGridView
    {
        public GIGrid()
        {
            this.DefaultStyleKey = typeof(GIGrid);
            this.IsFilteringAllowed = true;
            Windows8Palette.Palette.AccentColor = Colors.Red;
        }


        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);

            if (this.AutoGenerateColumns)
                this.AutoGenerateColumns = false;
        }

        protected override void OnAutoGeneratingColumn(GridViewAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            if (this.AutoGenerateColumns)
                this.AutoGenerateColumns = false;
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (this.Columns.Count > 0)
                Columns.Clear();

            List<GIGridAttributes> attrib = new List<GIGridAttributes>();
            bool alreadyAdded = false;

            foreach (var item in Items)
            {
                var props = item.GetType().GetProperties();

                foreach (var p in props)
                {
                    foreach (var a in p.GetCustomAttributes(true))
                    {
                        try
                        {
                            var w = a as GIGridAttributes;

                            if (w.ColumnName != p.Name)
                                w.ColumnName = p.Name;

                            attrib.Add(w);
                            alreadyAdded = true;
                        }
                        catch
                        {
                            alreadyAdded = false;
                        }
                    }

                    if (!alreadyAdded)
                    {
                        attrib.Add(new GIGridAttributes(p.Name, p.Name, true));
                        alreadyAdded = false;
                    }
                    
                }
                
            }

            foreach (GIGridAttributes at in attrib.Distinct().ToList())
            {
                var coluna = new GridViewDataColumn();
                coluna.Header = at.Header;
                coluna.IsVisible = at.IsColumnVisible;
                coluna.IsReadOnly = at.IsColumnReadOnly;
                coluna.IsFilterable = true;
                coluna.ShowFilterButton = at.IsFilterable;
                coluna.DataMemberBinding = new Binding(at.ColumnName);
                coluna.TextAlignment = at.TextAlignment;
                
                if(!at.IsColumnReadOnly)
                    coluna.EditTriggers = Telerik.Windows.Controls.GridView.GridViewEditTriggers.CellClick;

                this.Columns.Add(coluna);
            }
        }
    }
}
