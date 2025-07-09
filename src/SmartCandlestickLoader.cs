using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace MarketFib
{
    /// <summary>
    /// Creates a list of SmartCandlesticks from a .csv file.
    /// </summary>
    public class SmartCandlestickLoader : Loader<SmartCandlestick>
    {
        private DateTime _startDate; // date is stored when user sets the dates
        private DateTime _endDate; // date is stored when user sets the dates

        /// <summary>
        /// Initializes a new instance of <see cref="SmartCandlestickLoader"/>.
        /// </summary>
        /// <param name="filePath">A .csv filepath.</param>
        public SmartCandlestickLoader(string filePath)
            : base(filePath)
        {
            FindPositionTypes(); // finds the position types of all SmartCandlesticks
        }

        /// <summary>
        /// Get all SmartCandlesticks from the default start to end date.
        /// </summary>
        public List<SmartCandlestick> GetFilteredItems()
        {
            return GetFilteredItems(_startDate, _endDate);
        }

        /// <summary>
        /// Sets the dates used for filtering.
        /// </summary>
        /// <param name="startDate">Start cutoff date.</param>
        /// <param name="endDate">End cutoff date.</param>
        public void SetDates(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        /// <summary>
        /// Finds all possible valid waves.
        /// Returns all wave start candlesticks and all the associated end wave candlesticks.
        /// </summary>
        /// <returns>A tuple of 2 lists, one with all wave starts and the other with all possible wave ends for each start.</returns>
        public (List<SmartCandlestick>, Dictionary<SmartCandlestick, List<SmartCandlestick>>) GetWaves()
        {
            List<SmartCandlestick> swingCandlesticks = GetSwingCandlesticks(); // gets all peaks & valleys

            List<SmartCandlestick> validWaveStarts = new List<SmartCandlestick>(); // start wave candlestick list
            Dictionary<SmartCandlestick, List<SmartCandlestick>> validWaveEnds = new Dictionary<SmartCandlestick, List<SmartCandlestick>>(); // maps the wave starts to the wave end lists

            for (int i = 0; i < swingCandlesticks.Count - 1; i++) // start wave
            {
                // candlestick is a peak or valley, this var is used to differentiate between them
                bool iIsPeak = swingCandlesticks[i].IsPeak;

                List<SmartCandlestick> validCandlesticks = new List<SmartCandlestick>(); // holds valid wave ends

                for (int j = i + 1; j < swingCandlesticks.Count; j++) // possible end wave candlesticks
                {
                    // candlestick is a peak or valley, this var is used to differentiate between them
                    bool jIsPeak = swingCandlesticks[j].IsPeak;

                    // wave is broken if a candlestick is a greater peak
                    if ((iIsPeak && jIsPeak) && (swingCandlesticks[i].High < swingCandlesticks[j].High))
                    {
                        break;
                    }

                    // wave is broken is a candlestick is a lesser valley
                    else if ((!iIsPeak && !jIsPeak) && (swingCandlesticks[i].Low > swingCandlesticks[j].Low))
                    {
                        break;
                    }

                    // wave end can't be of the same type
                    if (iIsPeak == jIsPeak)
                    {
                        continue;
                    }

                    // first opposite swing candlestick is added no matter what
                    if (validCandlesticks.Count == 0)
                    {
                        validCandlesticks.Add(swingCandlesticks[j]);
                    }

                    // opposite swing candlestick is only added if it is valid compared to all the previous valid candlesticks
                    else if (IsValidComparedToList(validCandlesticks, swingCandlesticks[j]))
                    {
                        validCandlesticks.Add(swingCandlesticks[j]);
                    }
                }

                // adds the wave start and the end points to an array
                if (validCandlesticks.Count > 0)
                {
                    // inserts the start of the wave to the start of the list
                    validWaveStarts.Add(swingCandlesticks[i]);

                    validWaveEnds[swingCandlesticks[i]] = validCandlesticks; // maps the wave start to all possible wave ends
                }
            }

            return (validWaveStarts, validWaveEnds); // returns the start and end waves
        }

        /// <summary>
        /// Creates a <see cref="SmartCandlestick"/> instance from the given parameters.
        /// </summary>
        /// <returns>An instance of <see cref="SmartCandlestick"/>.</returns>
        protected override SmartCandlestick CreateItemFromData(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
        {
            SmartCandlestick smartCandle = new SmartCandlestick(date, open, high, low, close, volume);
            return smartCandle;
        }

        /// <summary>
        /// Sets the position type for all SmartCandlesticks.
        /// Position types depend on a SmartCandlestick's neighbors. 
        /// </summary>
        private void FindPositionTypes()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                SmartCandlestick before;
                SmartCandlestick after;

                // if the list is 1 then it has no neighbors
                if (Items.Count == 1)
                {
                    before = null;
                    after = null;
                }

                // if the index if 0 then it has no left neighbor
                else if (i == 0)
                {
                    before = null;
                    after = Items[i + 1];
                }

                // if the index is the end, then it has no right neighbor
                else if (i == Items.Count - 1)
                {
                    before = Items[i - 1];
                    after = null;
                }

                // if the index none of the above, then it has 2 neighbors
                else
                {
                    before = Items[i - 1];
                    after = Items[i + 1];
                }

                Items[i].FindPositionType(before, after); // finds the type depending on the neighbors
            }
        }

        /// <summary>
        /// Gets all swing candlesticks.
        /// </summary>
        /// <returns>A list of <see cref="SmartCandlestickLoader"/> which are all swing candlesticks.</returns>
        private List<SmartCandlestick> GetSwingCandlesticks()
        {
            List<SmartCandlestick> filteredItems = GetFilteredItems();
            List<SmartCandlestick> swingCandlesticks = new List<SmartCandlestick>(); // future list of all peaks and valleys
            for (int i = 0; i < filteredItems.Count; i++) // loops through all filtered items
            {
                if (filteredItems[i].IsPeak || filteredItems[i].IsValley) // adds to the list if it is a peak or valley
                {
                    swingCandlesticks.Add(filteredItems[i]); // adds to the list
                }
            }

            return swingCandlesticks; // returns all peaks & valleys
        }

        /// <summary>
        /// Checks if a given candlestick element is valid compared to every element in the list.
        /// </summary>
        private bool IsValidComparedToList(List<SmartCandlestick> candlesticks, SmartCandlestick checkCandlestick)
        {
            for (int i = 0; i < candlesticks.Count; i++)
            {
                // checks if the candlestick is lower than a previous candlestick
                if (checkCandlestick.IsPeak && (checkCandlestick.High < candlesticks[i].High))
                {
                    return false; // if it is not valid for 1 element, false is returned
                }

                // checks if the candlestick is higher than a previous candlestick
                else if (checkCandlestick.IsValley && (checkCandlestick.Low > candlesticks[i].Low))
                {
                    return false; // if it is not valid for 1 element, false is returned
                }
            }

            return true; // if it is valid for all elements, true is returned
        }
    }
}
