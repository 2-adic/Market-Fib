using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace MarketFib
{
    /// <summary>
    /// A generic base class for loading CSV file data into candlestick objects.
    /// </summary>
    /// <typeparam name="T">
    /// The concrete type of candlestick to be created. This type must be derived from the <see cref="Candlestick"/> class and must have a public, parameterless constructor.
    /// </typeparam>
    public abstract class Loader<T> where T : Candlestick, new()
    {
        private readonly string _filePath; // CSV filepath

        private readonly string[] _dateFormats = new[] { // acceptable CSV date formats
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm",
            "yyyy-MM-dd HH",
            "yyyy-MM-dd",
            "yyyy/MM/dd",
            "yyyyMMdd",
            "MM/dd/yyyy",
            "dd-MM-yyyy",
            "M/d/yyyy",
            "d-M-yyyy",
        };

        private readonly char[] _delimiters = { '"', ',', ';', '\t', '|', '^' }; // acceptable delimiters (includes '"' for data surrounded in that char, it is not meant to be used as a delimiter)

        protected List<T> Items = new List<T>();

        /// <summary>
        /// Initializes a new instance of <see cref="Loader{T}"/>.
        /// </summary>
        /// <param name="filePath">A CSV filepath.</param>
        public Loader(string filePath)
        {
            _filePath = filePath;

            LoadData(); // creates the list of items from the CSV
            NormalizeData(); // reverses the data if it is in the wrong order
        }

        /// <summary>
        /// Replaces the items list with a new list.
        /// </summary>
        /// <param name="newItems">The list of items to replace the existing list.</param>
        public void SetItems(List<T> newItems)
        {
            Items = newItems;
        }

        /// <summary>
        /// Filters the items list.
        /// </summary>
        /// <param name="startDate">The oldest date allowed.</param>
        /// <param name="endDate">The newest date allowed.</param>
        /// <returns>
        /// Returns a list of items between the start and end date.
        /// </returns>
        public List<T> GetFilteredItems(DateTime startDate, DateTime endDate)
        {
            List<T> filteredItems = new List<T>(); // initialize filtered list

            // loop through the unfiltered items
            foreach (var item in Items)
            {
                // ends loop if the items are after the end date
                if (item.Date > endDate)
                {
                    break;
                }

                // checks if the item is within the range (no need to check end date since if it was past it, then the loop would have ended)
                if (item.Date >= startDate)
                {
                    filteredItems.Add(item); // adds the item if it in within the range
                }
            }

            return filteredItems; // returns all found items that were within the range
        }

        // used to create an instance of the 
        /// <summary>
        /// When implemented in a derived class, creates a new instance of <typeparamref name="T"/> from the given parameters.
        /// </summary>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <typeparam name="T">The concrete type of the object to create, such as a <see cref="Candlestick"/>.</typeparam>
        protected abstract T CreateItemFromData(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume);

        /// <summary>
        /// Creates a list of candlestick items from a CSV file.
        /// </summary>
        private void LoadData()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    // counts the amount of headers
                    int headerCount = 0;
                    while(IsHeader(reader.ReadLine())) {
                        headerCount++;
                    }

                    // resets the reader
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    reader.DiscardBufferedData();

                    // jump past the headers
                    for (int i = 0; i < headerCount; i++)
                    {
                        reader.ReadLine();
                    }

                    // processes each line in the CSV
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine(); // gets a line of data
                        if (line == null) continue; // goes to the next line if it was empty

                        // splits the data into multiple parts based on the delimiters
                        var fields = line.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

                        if (fields.Length < 6) // skips invalid lines
                        {
                            Console.WriteLine($"Warning: A row contains insufficient data, skipping line. File: {_filePath}");
                            continue; 
                        }

                        // assumes the CSV columns are in the following order: date, open, high, low, close, volume
                        var dateString = fields[0].Trim('"');
                        bool dateParsed = DateTime.TryParseExact(
                            dateString,
                            _dateFormats.ToArray(),
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime date
                            );

                        if (!dateParsed) // skip line if invalid date format
                        {
                            Console.WriteLine($"Warning: Unable to parse date '{dateString}', skipping line. File: {_filePath}");
                            continue;
                        }

                        var open = decimal.Parse(fields[1], CultureInfo.InvariantCulture);
                        var high = decimal.Parse(fields[2], CultureInfo.InvariantCulture);
                        var low = decimal.Parse(fields[3], CultureInfo.InvariantCulture);
                        var close = decimal.Parse(fields[4], CultureInfo.InvariantCulture);
                        var volume = ulong.Parse(fields[5], CultureInfo.InvariantCulture);

                        // adds the item to a list
                        Items.Add(CreateItemFromData(date, open, high, low, close, volume));
                    }
                }
            }

            // gives error if data was not able to be loaded
            catch (Exception ex)
            {
                // tells the user the specific error
                Console.WriteLine($"Error loading candlestick data: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if a given line is a header.
        /// </summary>
        private bool IsHeader(string line)
        {
            if (line == null) return true; // blank line is treated as a header

            string[] fields = line.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (fields.Length < 6)
                return true; // not a row of data if it is an invalid line

            // fields 1-4 must be decimals
            for (int i = 1; i < 5; i++)
            {
                if (!decimal.TryParse(fields[i], out _))
                    return true; // not data if any decimal field fails
            }

            // fields 1-4 must be a ulong
            if (!ulong.TryParse(fields[5], out _))
                return true; // not data if volume is invalid

            return false; // not a header if all checks passed
        }

        /// <summary>
        /// <para>Reverses a list if it is in the wrong order.</para>
        /// <para>Assumes given list is sorted.</para>
        /// </summary>
        private void NormalizeData()
        {
            // returns if it is too small
            if (Items.Count < 2)
            {
                return;
            }

            // reverses the list if the dates are the wrong order
            if (Items[0].Date > Items[1].Date)
            {
                // reverses the list
                Items.Reverse();
            }
        }
    }
}
