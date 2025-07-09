using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MarketFib
{
    public class SmartCandlestick : Candlestick
    {
        // properties
        public decimal Range { get; private set; }
        public decimal BodyRange { get; private set; }
        public decimal TopPrice { get; private set; }
        public decimal BottomPrice { get; private set; }
        public decimal UpperTail { get; private set; }
        public decimal LowerTail { get; private set; }

        // type
        public bool IsBullish { get; private set; }
        public bool IsBearish { get; private set; }
        public bool IsNeutral { get; private set; }
        public bool IsMarubozu { get; private set; }
        public bool IsHammer { get; private set; }
        public bool IsDoji { get; private set; }
        public bool IsDragonflyDoji { get; private set; }
        public bool IsGravestoneDoji { get; private set; }

        // position type (false by default)
        public bool IsPeak { get; private set; } = false;
        public bool IsValley { get; private set; } = false;

        public SmartCandlestick() : base() { }

        // constructor with Candlestick as a parameter
        /// <summary>
        /// Initializes a new instance of <see cref="SmartCandlestick"/>.
        /// </summary>
        public SmartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
            : base(date, open, high, low, close, volume)
        {
            FillData();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SmartCandlestick"/>.
        /// </summary>
        public SmartCandlestick(Candlestick candlestick)
            : base(candlestick.Date, candlestick.Open, candlestick.High, candlestick.Low, candlestick.Close, candlestick.Volume)
        {
            FillData();
        }

        /// <summary>
        /// Calculates properties and the type of candlestick.
        /// </summary>
        private void FillData()
        {
            // calculates properties based on the given data
            Range = High - Low;
            BodyRange = Math.Abs(Close - Open);
            TopPrice = Math.Max(Open, Close);
            BottomPrice = Math.Min(Open, Close);
            UpperTail = High - TopPrice;
            LowerTail = BottomPrice - Low;

            // calculates boolean values for the following types
            IsBullish = Close > Open; // close is greater than the open
            IsBearish = Close < Open; // close if less than the open
            IsNeutral = Math.Abs(Open - Close) <= Open * 0.01m; // open and close are within 1%
            IsMarubozu = UpperTail == 0 && LowerTail == 0; // candlestick has no tail
            IsHammer = BodyRange <= Range * 0.25m && LowerTail >= BodyRange * 2; // low is a lot lower than its high
            IsDoji = BodyRange <= Range * 0.1m; // open and close are very close
            IsDragonflyDoji = IsDoji && UpperTail <= Range * 0.05m && LowerTail > 0; // lower tail is long and body is small
            IsGravestoneDoji = IsDoji && LowerTail <= Range * 0.05m && UpperTail > 0; // upper tail is long and the body is small
        }

        /// <summary>
        /// Checks if candlestick is valley or peak.
        /// (Must be compared to it's neighbors.)
        /// </summary>
        public void FindPositionType(SmartCandlestick before, SmartCandlestick after)
        {
            bool isBeforeNull = before == null;
            bool isAfterNull = after == null;

            // case: candlestick is surrounded
            if (!isBeforeNull && !isAfterNull) 
            {
                if (this.Low < before.Low && this.Low < after.Low)
                {
                    IsValley = true; // is a valley if it is lower than its neighbors
                }

                else if (this.High > before.High && this.High > after.High)
                {
                    IsPeak = true; // is a peak if it is higher than its neighbors
                }
            }

            // case: candlestick is alone
            else if (isBeforeNull && isAfterNull) 
            {
                // it is both if it is alone with no neighbors
                IsValley = true;
                IsPeak = true;
            }

            // case: candlestick left is empty
            else if (isBeforeNull && !isAfterNull)
            {
                if (this.Low < after.Low)
                {
                    IsValley = true; // is a valley if its only neighbor is higher
                }

                else if (this.High > after.High)
                {
                    IsPeak = true; // is a peak if its only neighbor is lower
                }
            }

            // case: candlestick right is empty
            else if (!isBeforeNull && isAfterNull)
            {
                if (this.Low < before.Low)
                {
                    IsValley = true; // is a valley if its only neighbor is higher
                }

                else if (this.High > before.High)
                {
                    IsPeak = true; // is a peak if its only neighbor is lower
                }
            }
        }

        /// <summary>
        /// Checks if the candlestick is of the given type. The string must be one of the following:
        /// </summary>
        /// <returns><b>true</b> if the candlestick matches the specified type; otherwise, <b>false</b>.</returns>
        /// <remarks>
        /// Valid types: "Peak", "Valley", "Bullish", "Bearish", "Neutral", "Marubozu", "Hammer", "Doji", "DragonflyDoji", or "GravestoneDoji".
        /// </remarks>
        public bool IsType(string type)
        {
            if (type == "Peak")
            {
                return IsPeak; // returns the value for the given type
            }
            else if (type == "Valley")
            {
                return IsValley; // returns the value for the given type
            }
            else if (type == "Bullish")
            {
                return IsBullish; // returns the value for the given type
            }
            else if (type == "Bearish")
            {
                return IsBearish; // returns the value for the given type
            }
            else if (type == "Neutral")
            {
                return IsNeutral; // returns the value for the given type
            }
            else if (type == "Marubozu")
            {
                return IsMarubozu; // returns the value for the given type
            }
            else if (type == "Hammer")
            {
                return IsHammer; // returns the value for the given type
            }
            else if (type == "Doji")
            {
                return IsDoji; // returns the value for the given type
            }
            else if (type == "DragonflyDoji")
            {
                return IsDragonflyDoji; // returns the value for the given type
            }
            else if (type == "GravestoneDoji")
            {
                return IsGravestoneDoji; // returns the value for the given type
            }

            return false; // the candlestick has no type
        }

        /// <summary>
        /// Print method for testing purposes.
        /// </summary>
        public override string ToString()
        {
            // returns a string of all values formatted to be in organized columns
            const int columnSpacing = 4; // gives some space between the columns
            return $"Date: {Date,-(21 + columnSpacing)}Open: {Open,-(12 + columnSpacing)}High: {High,-(12 + columnSpacing)}Low: {Low,-(12 + columnSpacing)}Close: {Close,-(12 + columnSpacing)}Volume: {Volume}\n" +
                   $"Range: {Range}\tBodyRange: {BodyRange}\tTopPrice: {TopPrice}\tBottomPrice: {BottomPrice}\tUpperTail: {UpperTail}\tLowerTail: {LowerTail}\n" +
                   $"IsBullish: {IsBullish}\tIsBearish: {IsBearish}\tIsNeutral: {IsNeutral}\tIsMarubozu: {IsMarubozu}\tIsHammer: {IsHammer}\tIsDoji: {IsDoji}\tIsDragonflyDoji: {IsDragonflyDoji}\tIsGravestoneDoji: {IsGravestoneDoji}\n" +
                   $"IsPeak: {IsPeak}\tIsValley: {IsValley}\n\n";
        }
    }
}
