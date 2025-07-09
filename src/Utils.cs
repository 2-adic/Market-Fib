using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketFib
{
    public static class Utils
    {
        // chart properties
        public const int stockLabelAmountMax = 20; // maximum amount of date labels
        public const int stockLabelAngle = -25; // the angle of the date labels
        public const decimal stockChartSpacing = .03m; // percentage distance between top/bottom of line to the top/bottom of the chart

        // beauty analysis properties
        public const int beautyDataSize = 256; // amount of beauty graph data points
        public const decimal beautyAnalysisCutoff = .1m; // percentage difference between the initial and final x value of the beauty graph
        public const decimal beautyFibCutoff = .015m; // max percentage distance a data point can have to a fib level for it to count towards the beauty count
        public const decimal beautyChartSpacing = .1m; // percentage distance between top/bottom of line to the top/bottom of the chart

        private static readonly decimal[] _fibonacciNum = { 0m, 0.236m, 0.382m, 0.5m, 0.618m, 0.764m, 1m }; // fib percentage levels

        // line properties
        private static readonly int _waveLineWidth = 4;
        private static readonly int _levelLineWidth = 2;
        private static readonly Color _waveLineColor = Color.LightBlue;
        private static readonly Color _levelLineColor = Color.LightSalmon;
        
        /// <summary>
        /// Calculates Fibonacci retracement levels based on a start and end value.
        /// </summary>
        /// <param name="high">Highest value.</param>
        /// <param name="low">Lowest value.</param>
        /// <returns>A decimal array of 7 values.</returns>
        public static decimal[] GetLevels(decimal high, decimal low)
        {
            decimal[] results = new decimal[7];

            for (int i = 0; i < 7; i++)
            {
                results[i] = (_fibonacciNum[i] * (high - low)) + low;
            }

            return results; // returns all fib retracement levels
        }

        /// <summary>
        /// Returns a list of the Fibonacci retracement levels.
        /// </summary>
        /// <param name="start">Start candlestick.</param>
        /// <param name="end">End candlestick.</param>
        public static List<decimal> GetLevels(SmartCandlestick start, SmartCandlestick end)
        {
            decimal high;
            decimal low;

            // if start candlestick is a peak, then the high is the start high
            if (start.IsPeak)
            {
                high = start.High;
                low = end.Low;
            }

            // if start candlestick is a valley, then the low is the start low
            else
            {
                high = end.High;
                low = start.Low;
            }

            return GetLevels(high, low).ToList();
        }

        /// <summary>
        /// Adds a <see cref="StripLine"/> as a vertical line to the chart area.
        /// </summary>
        /// <param name="area">The <see cref="ChartArea"/> where the line will appear.</param>
        /// <param name="dateIndex">The X position of the line.</param>
        public static void CreateStripLine(ChartArea area, int dateIndex)
        {
            StripLine stripLine = new StripLine
            {
                // line properties
                BorderColor = _waveLineColor,
                BorderWidth = _waveLineWidth,
                BorderDashStyle = ChartDashStyle.Solid,

                IntervalOffset = dateIndex + 1, // x position
            };

            area.AxisX.StripLines.Add(stripLine); // adds line to the chart
        }

        /// <summary>
        /// Adds a <see cref="StripLine"/> as a horizontal line to the chart area.
        /// </summary>
        /// <param name="area">The <see cref="ChartArea"/> where the line will appear.</param>
        /// <param name="position">The Y position of the line.</param>
        public static void CreateStripLine(ChartArea area, Decimal position)
        {
            StripLine stripLine = new StripLine
            {
                // line properties
                BorderColor = _levelLineColor,
                BorderWidth = _levelLineWidth,
                BorderDashStyle = ChartDashStyle.Dash,

                IntervalOffset = (double)position, // x position
            };

            area.AxisY.StripLines.Add(stripLine); // adds line to the chart
        }

        /// <summary>
        /// Deletes all striplines on the chart.
        /// </summary>
        public static void DeleteStripLines(ChartArea area)
        {
            area.AxisX.StripLines.Clear();
            area.AxisY.StripLines.Clear();
        }

        /// <summary>
        /// Deletes all label annotations on the chart.
        /// </summary>
        public static void DeleteLabelAnnotations(Chart chart)
        {
            // loop through all annotations
            for (int i = chart.Annotations.Count - 1; i >= 0; i--)
            {
                if (chart.Annotations[i] is CalloutAnnotation)
                {
                    chart.Annotations.RemoveAt(i); // removes the label
                }
            }
        }
    }
}
