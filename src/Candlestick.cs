using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace MarketFib
{
    /// <summary>
    /// Keeps track of individual stock entries.
    /// </summary>
    public class Candlestick
    {
        // candlestick data
        public DateTime Date { get; private set; }
        public decimal Open { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Close { get; private set; }
        public ulong Volume { get; private set; }

        public Candlestick() { }

        // constructor
        public Candlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
        {
            // sets given values to the class variables
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        // print method
        public override string ToString()
        {
            // returns a string of all values formatted to be in organized columns
            const int columnSpacing = 4; // gives some space between the columns
            return $"Date: {Date,-(21 + columnSpacing)}Open: {Open,-(12 + columnSpacing)}High: {High,-(12 + columnSpacing)}Low: {Low,-(12 + columnSpacing)}Close: {Close,-(12 + columnSpacing)}Volume: {Volume}";
        }
    }
}


