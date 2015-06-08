using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Caliburn.Micro;
using Monitor_kl2.Models;

namespace Monitor_kl2.ViewModels
{
    public class PrintViewModel : Screen
    {
        private FlowDocument _statsTable;

        public FlowDocument Stats
        {
            get { return _statsTable; }
            set { _statsTable = value;
                NotifyOfPropertyChange(() => Stats);
            }
        }


        private BindableCollection<Host> _hostList;

        public PrintViewModel(BindableCollection<Host> hl)
        {
            _hostList = hl;
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();
            GeneratePrint();
        }

        private void GeneratePrint()
        {
            Table statsTable = new Table();
            statsTable.BorderBrush = Brushes.Black;
            statsTable.BorderThickness = new Thickness(1, 1, 1, 1);
            statsTable.Background = Brushes.White;

            TableRowGroup headerRowGroup = new TableRowGroup();
            statsTable.RowGroups.Add(headerRowGroup);
            TableRow headerRow = new TableRow();
            headerRow.FontSize = 12;
            headerRow.FontWeight = FontWeights.Bold;
            headerRowGroup.Rows.Add(headerRow);

            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("ID"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Nazwa"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("IP"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("RAM"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("CPU"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Godzina"))));

            TableRowGroup dataRowGroup = new TableRowGroup();
            statsTable.RowGroups.Add(dataRowGroup);
            dataRowGroup.FontSize = 10;
            dataRowGroup.FontWeight = FontWeights.Normal;

            bool color = false ;

           // var rows = _hostList.Select(host => GenerateRow(host));

            foreach (Host h in _hostList)
            {
                color = !color;
                TableRow tableRow = new TableRow();
                dataRowGroup.Rows.Add(tableRow);

                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(h.Id.ToString()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(h.Name))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(h.IpAddr))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(h.RamUsage.ToString()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(h.CpuUsege.ToString()))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(h.Hour.ToLongTimeString()))));

                if (color)
                    tableRow.Background = Brushes.LightGray;
                else
                    tableRow.Background = Brushes.White;

            }

            Stats = new FlowDocument();
            Stats.Blocks.Add(statsTable);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }
    }
}
