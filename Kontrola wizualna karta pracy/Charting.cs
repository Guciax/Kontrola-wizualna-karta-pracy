using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Kontrola_wizualna_karta_pracy
{
    class Charting
    {
        public static void DrawChartWasteReasons(List<WasteDataStructure> inspectionData, Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();
            chart.Annotations.Clear();

            Dictionary<string, int> wastePerReasonDict = new Dictionary<string, int>();

            foreach (var inspectionRecord in inspectionData)
            {
                foreach (var wasteEntry in inspectionRecord.WastePerReason)
                {
                    if(!wastePerReasonDict.ContainsKey(wasteEntry.Key))
                    {
                        wastePerReasonDict.Add(wasteEntry.Key, 0);
                    }
                    wastePerReasonDict[wasteEntry.Key] += wasteEntry.Value;
                }
            }

            var myList = wastePerReasonDict.ToList();

            myList.Sort((pair1, pair2) =>  -1*pair1.Value.CompareTo(pair2.Value));
            myList = myList.Select(i => i).Take(5).ToList();

            Series sr = new Series();
            sr.ChartType = SeriesChartType.Bar;
            sr.SmartLabelStyle.MovingDirection = LabelAlignmentStyles.Left;
            sr.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Partial;
            sr.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            sr.Color = Color.Orange;
            sr.BorderColor = Color.Red;

            ChartArea ar = new ChartArea();
            ar.BackColor = Color.Transparent;
            ar.AxisX.IsMarginVisible = false;
            ar.AxisY.IsMarginVisible = false;
            ar.AxisX.LabelStyle.Enabled = false;
            ar.AxisX.IsReversed = true;
            ar.AxisX.MajorGrid.Enabled = false;
            ar.AxisY.MajorGrid.Enabled = false;
            ar.Position.X = 0;
            ar.Position.Y = 0;
            ar.Position.Height = 100;
            ar.Position.Width = 100;
            
            

            foreach (var waste in myList)
            {
                DataPoint point = new DataPoint();
                point.SetValueXY(waste.Key, waste.Value);
                //point.Label = waste.Key;

                RectangleAnnotation ta = new RectangleAnnotation();
                ta.AnchorDataPoint = point;
                ta.AnchorX = 0;
                //ta.AnchorOffsetX = -100;     // *
                
                ta.Font = new Font("Arial Narrow", 9, FontStyle.Bold);
                ta.AnchorOffsetY = 0;     // *
                ta.AnchorAlignment = ContentAlignment.MiddleLeft;
                ta.Text = waste.Value + " - " + waste.Key;
                ta.BackColor = Color.Transparent;
                ta.LineWidth = 0;
                chart.Annotations.Add(ta);

                sr.Points.Add(point);
            }

            chart.ChartAreas.Add(ar);
            chart.Series.Add(sr);
        }
    }
}
