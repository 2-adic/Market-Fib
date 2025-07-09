using MarketFib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MarketFib
{
    public partial class FormChart : Form
    {
        // stores the filepaths for the .csv files
        private readonly string[] _filepaths;

        // keeps track if the form was loaded to display multiple stocks
        private readonly bool _isMultiStock;

        // initial dates from FormInput
        private readonly DateTime _initialStartDate;
        private readonly DateTime _initialEndDate;

        // prevents the form from being updated before it is fully initialized
        private readonly bool _updateToggle = false;

        private string _filenameCurr = null; // keeps track of the current selected CSV
        private int _stockPrev = -1; // keeps track of the previous stock

        // initializes a dictionary used to store each stock symbol with its periods 
        private readonly Dictionary<string, List<string>> _csvFilenames = new Dictionary<string, List<string>>();

        // list of valid waves for the current displayed candlesticks
        private List<SmartCandlestick> _waveStarts = new List<SmartCandlestick>();
        private Dictionary<SmartCandlestick, List<SmartCandlestick>> _waveEnds = new Dictionary<SmartCandlestick, List<SmartCandlestick>>();

        // stores selected waves for each .csv
        private readonly Dictionary<string, (SmartCandlestick, SmartCandlestick)> _waveSave = new Dictionary<string, (SmartCandlestick, SmartCandlestick)>();

        // stores selected period for each stock
        private readonly Dictionary<int, int> _periodSave = new Dictionary<int, int>();

        // list of the current candlesticks being graphed
        private List<SmartCandlestick> _filteredCurr = new List<SmartCandlestick>();

        // initializes a dictionary used to store SmartCandlestickLoader for each .csv
        private readonly Dictionary<string, SmartCandlestickLoader> _csvCandlestickLoaders = new Dictionary<string, SmartCandlestickLoader>();

        /// <summary>
        /// Initializes an instance of the <see cref="FormChart"> class.
        /// Sets up UI components, the form icon, and data from the CSV files.
        /// </summary>
        /// <param name="filepaths">A list of all CSV filepaths.</param>
        /// <param name="initialStartDate">The initial selected start date.</param>
        /// <param name="initialEndDate">The initial selected end date.</param>
        /// <param name="isMultiStock">If the window contains multiple stocks.</param>
        public FormChart(string[] filepaths, DateTime initialStartDate, DateTime initialEndDate, bool isMultiStock)
        {
            // sets variables to class variables
            _filepaths = filepaths;
            _isMultiStock = isMultiStock;
            _initialStartDate = initialStartDate;
            _initialEndDate = initialEndDate;

            this.Icon = Properties.Resources.app; // sets the form's icon

            InitializeComponent(); // initializes the form
            InitializeDefaults(); // initializes default values

            InitializeData(); // gets all filenames and populates csvCandlestickLoaders
            InitializeComboBoxes(); // initializes entries for the ComboBoxes

            UpdateDisplay(); // initializes the elements within the display
            _updateToggle = true; // allows the form to be updated via a change in the ComboBoxes or DateTimePicker
        }

        /// <summary>
        /// Sets the defaults given from the form chart.
        /// </summary>
        private void InitializeDefaults()
        {
            dtpStartDate.Value = _initialStartDate; // sets the start DateTimePicker to the default date
            dtpEndDate.Value = _initialEndDate; // sets the end DateTimePicker to the default date

            cmbPattern.SelectedIndex = 0; // sets the default ComboBox value to "None"

            // updates the name of the window
            if (!_isMultiStock) // form contains one stock
            {
                this.Text = Path.GetFileName(_filepaths[0]); // sets the form name
                HideStockSelector(); // hides the stock selector cmb and period cmb
            }

            chtStockDisplay.ChartAreas[0].AxisX.LabelStyle.Angle = Utils.stockLabelAngle; // sets the stock date label angle
            chtStockDisplay.ChartAreas[0].AxisY.LabelStyle.Format = "F2"; // sets the amount of decimals the beauty chart x axis displays
            chtStockDisplay.ChartAreas[1].AxisX.LabelStyle.Format = "F2"; // sets the amount of decimals the beauty chart x axis displays
        }

        /// <summary>
        /// Loads data from all csv files.
        /// </summary>
        private void InitializeData()
        {
            for (int i = 0; i < _filepaths.Length; i++)
            {
                // copies the filepath's filename to be added to a dictionary later
                string filename = Path.GetFileName(_filepaths[i]);
                filename = filename.Replace(".csv", ""); // removes the .csv extension from the string

                if (!filename.Contains('-'))
                {
                    filename += '-'; // adds on '-' to the end of the string
                }

                if (filename.EndsWith("-"))
                {
                    filename += "N/A"; // adds on "N/A" to the end of the string to prevent errors
                }

                // creates a SmartCandlestickLoader for each csv
                _csvCandlestickLoaders.Add(filename, new SmartCandlestickLoader(_filepaths[i]));

                // splits the string from '-'
                string[] parts = filename.Split(new char[] { '-' }, 2);

                // checks if the mapped string list was initialized yet
                if (!_csvFilenames.ContainsKey(parts[0]))
                {
                    // initializes list for each stock
                    _csvFilenames[parts[0]] = new List<string>();
                }

                // maps each stock symbol to its files
                _csvFilenames[parts[0]].Add(parts[1]);
            }
        }

        /// <summary>
        /// Fills the ComboBoxes with the stock symbol and the period.
        /// </summary>
        private void InitializeComboBoxes()
        {
            // populates the first ComboBox with keys from the dictionary
            cmbStockSymbol.DataSource = new BindingSource(_csvFilenames, null);
            cmbStockSymbol.DisplayMember = "Key"; // displays the dictionary keys

            UpdateCmbPeriod(); // initializes the period ComboBox
        }

        /// <summary>
        /// Updates the form if the update button was clicked.
        /// </summary>
        private void BtnUpdateDateRange_Click(object sender, EventArgs e)
        {
            if (_updateToggle)
            {
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Handles changes to the selected index of the ComboBox.
        /// Updates the display base on selected pattern.
        /// </summary>
        private void CmbPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updateToggle)
            {
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Handles changes to the selected index of the ComboBox.
        /// Updates the end wave ComboBox based on the start wave ComboBox.
        /// </summary>
        private void CmbWaveStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeWaveStart();
        }

        /// <summary>
        /// Handles changes to the selected index of the ComboBox.
        /// Updates the beauty chart and Fibonacci levels based on the new end wave.
        /// </summary>
        private void CmbWaveEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeWaveEnd();
        }

        /// <summary>
        /// Handles changes to the selected index of the ComboBox.
        /// Updates the period ComboBox based on the selected stock.
        /// </summary>
        private void CmbStockSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            SavePeriod(); // save current period
            _stockPrev = cmbStockSymbol.SelectedIndex; // updates stock after the previous stock info was used

            // changes the period to the previously saved period without raising the event
            cmbPeriod.SelectedIndexChanged -= CmbPeriod_SelectedIndexChanged; // disable event
            UpdateCmbPeriod(); // updates the period ComboBox
            LoadPeriod(); // loads the saved period
            cmbPeriod.SelectedIndexChanged += CmbPeriod_SelectedIndexChanged; // enable event

            CmbPeriod_SelectedIndexChanged(this, EventArgs.Empty); // raises the event to update the display
        }

        /// <summary>
        /// Handles changes to the selected index of the ComboBox.
        /// Updates the form based on the selected period.
        /// </summary>
        private void CmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // prevents the form from updating if the toggle is false, or if the cmbPeriod value is currently being changed
            if (_updateToggle && cmbPeriod.SelectedValue != null)
            {
                UpdateDisplay(); // updates the chart
            }
        }

        /// <summary>
        /// Updates the chart with the new values and dates.
        /// </summary>
        private void UpdateDisplay()
        {
            SaveWave(); // saves the current wave start/end

            // the user selected filename based on the two ComboBoxes
            _filenameCurr = ((KeyValuePair<string, List<string>>)cmbStockSymbol.SelectedItem).Key + '-' + cmbPeriod.SelectedValue.ToString();

            DateTime startDate = dtpStartDate.Value; // start date from the start DateTimePicker
            DateTime endDate = dtpEndDate.Value; // end date from the end DateTimePicker

            // gets the SmartCandlesticks from the dictionary and filters them based on the given dates
            _csvCandlestickLoaders[_filenameCurr].SetDates(startDate, endDate);
            List<SmartCandlestick> filteredCandlesticks = _csvCandlestickLoaders[_filenameCurr].GetFilteredItems();
            _filteredCurr = filteredCandlesticks;

            chtStockDisplay.DataSource = filteredCandlesticks; // displays data to chart
            chtStockDisplay.DataBind(); // binds the current data to the chart and updates its display

            UpdateWarningVisibility(); // display warning if no candlesticks exist for the selected date range

            ChartNormalize(filteredCandlesticks); // normalizes the chart
            DrawLabels(filteredCandlesticks); // draws lines and textboxes

            // gets all valid wave starts and ends
            (_waveStarts, _waveEnds) = _csvCandlestickLoaders[_filenameCurr].GetWaves();

            SetupWaveComboBoxes();
            LoadWave(); // updates selected wave to the previously saved wave

            UpdateLabelFormat();
        }

        /// <summary>
        /// Updates the period ComboBox depending on what the cmbStockSymbol's value is.
        /// </summary>
        private void UpdateCmbPeriod()
        {
            // gets the selected value from the first ComboBox
            if (cmbStockSymbol.SelectedItem is KeyValuePair<string, List<string>> selectedPair)
            {
                // updates the DataSource of the period ComboBox with the selected list
                cmbPeriod.DataSource = selectedPair.Value; // sets the new data source
            }
            else
            {
                // if there is no selection, sets data source to null
                cmbPeriod.DataSource = null;
            }
        }

        /// <summary>
        /// Changes the interval of date tick marks of the stock display chart depending on the amount of data points.
        /// </summary>
        private void UpdateInterval()
        {
            int dataSize = chtStockDisplay.Series[0].Points.Count;
            int interval = (int)Math.Ceiling((double)(dataSize / Utils.stockLabelAmountMax)) + 1;

            chtStockDisplay.ChartAreas[0].AxisX.Interval = interval;
        }

        /// <summary>
        /// Changes the label format for the stock data chart time data.
        /// </summary>
        private void UpdateLabelFormat()
        {
            List<DateTime> timestamps = _filteredCurr.Select(c => c.Date).ToList(); // gets all time data from the candlesticks

            string format;
            if (timestamps.Any(dt => dt.Second != 0)) 
                format = "yyyy-MM-dd HH:mm:ss"; // data includes seconds
            else if (timestamps.Any(dt => dt.Minute != 0 || dt.Second != 0))
                format = "yyyy-MM-dd HH:mm"; // data includes minutes
            else if (timestamps.Any(dt => dt.Hour != 0 || dt.Minute != 0 || dt.Second != 0))
                format = "yyyy-MM-dd HH"; // data includes hours
            else
                format = "yyyy-MM-dd";

            chtStockDisplay.ChartAreas[0].AxisX.LabelStyle.Format = format; // sets the stock chart's labels to show the minimum amount of data possible
        }

        private void UpdateWarningVisibility()
        {
            if (_filteredCurr.Count == 0)
                lblWarning.Visible = true;
            else
                lblWarning.Visible = false;
        }

        /// <summary>
        /// Removes the visibility of the stock and period selectors if the window only shows one stock.
        /// </summary>
        private void HideStockSelector()
        {
            if (!_isMultiStock)
            {
                lblStockSymbol.Visible = false;
                lblPeriod.Visible = false;
                cmbStockSymbol.Visible = false;
                cmbPeriod.Visible = false;
            }
        }

        /// <summary>
        /// Initializes the start wave cmb.
        /// </summary>
        private void SetupWaveComboBoxes()
        {
            cmbWaveStart.DataSource = null; // clears old data
            cmbWaveStart.DataSource = _waveStarts; // sets new values
            cmbWaveStart.DisplayMember = "Date"; // sets the ComboBox to only display the candlestick's date
        }

        /// <summary>
        /// Saves the current period.
        /// </summary>
        private void SavePeriod()
        {
            if (_stockPrev != -1)
            {
                _periodSave[_stockPrev] = cmbPeriod.SelectedIndex; // save period
            }
        }

        /// <summary>
        /// Loads the previously saved period.
        /// </summary>
        private void LoadPeriod()
        {
            if (_stockPrev != -1 && _periodSave.ContainsKey(_stockPrev))
            {
                cmbPeriod.SelectedIndex = _periodSave[_stockPrev];
            }
        }

        /// <summary>
        /// Saves the current selected wave values.
        /// </summary>
        void SaveWave()
        {
            SmartCandlestick start = (SmartCandlestick)cmbWaveStart.SelectedItem; // cmb selected start candlestick
            SmartCandlestick end = (SmartCandlestick)cmbWaveEnd.SelectedItem; // cmb selected end candlestick

            if (start == null || end == null)
            {
                return;
            }

            _waveSave[_filenameCurr] = (start, end);
        }

        /// <summary>
        /// Changes the wave selected value to the previously saved values.
        /// </summary>
        void LoadWave()
        {
            // exit if no saved value was found
            if (!_waveSave.TryGetValue(_filenameCurr, out (SmartCandlestick, SmartCandlestick) wave))
            {
                return;
            }

            (SmartCandlestick start, SmartCandlestick end) = wave;

            // exit if the saved wave is no longer within the list
            if (!_waveStarts.Contains(start) || !_waveEnds[start].Contains(end))
            {
                return;
            }

            cmbWaveStart.SelectedIndexChanged -= CmbWaveStart_SelectedIndexChanged; // disable start wave cmb change event
            cmbWaveEnd.SelectedIndexChanged -= CmbWaveEnd_SelectedIndexChanged; // disable end wave cmb change event

            cmbWaveStart.SelectedIndex = _waveStarts.FindIndex(item => item == start);
            ChangeWaveStart();

            cmbWaveEnd.SelectedIndex = _waveEnds[start].FindIndex(item => item == end);
            ChangeWaveEnd();

            cmbWaveEnd.SelectedIndexChanged += CmbWaveEnd_SelectedIndexChanged; // enable end wave cmb change event
            cmbWaveStart.SelectedIndexChanged += CmbWaveStart_SelectedIndexChanged; // enable start wave cmb change event
        }

        /// <summary>
        /// Updates the end wave cmb based on the selected wave start.
        /// </summary>
        void ChangeWaveStart()
        {
            SmartCandlestick start = (SmartCandlestick)cmbWaveStart.SelectedItem; // cmb selected start candlestick

            if (start == null)
            {
                cmbWaveEnd.DataSource = null;

                chtStockDisplay.Series[1].Points.Clear(); // clears the beauty chart

                return;
            }

            if (!_waveEnds.ContainsKey(start)) // invalid indices are not allowed
            {
                throw new KeyNotFoundException($"Wave start was not found inside dictionary: '{start}'.");
            }

            cmbWaveEnd.DataSource = null; // clears old data

            cmbWaveEnd.DataSource = _waveEnds[start]; // sets the ComboBox's elements to the end waves for the given start wave
            cmbWaveEnd.DisplayMember = "Date"; // sets ComboBox to only display the candlestick's date
        }

        /// <summary>
        /// Draws striplines based on the selected wave.
        /// </summary>
        void ChangeWaveEnd()
        {
            SmartCandlestick end = (SmartCandlestick)cmbWaveEnd.SelectedItem; // cmb selected end candlestick

            if (end == null)
            {
                return;
            }

            UpdateInterval();
            DrawWaveLines();
        }

        /// <summary>
        /// Changes the max and min y values of both chart areas to center the data.
        /// </summary>
        /// <param name="candlesticks">All candlesticks that are shown in the chart.</param>
        private void ChartNormalize(List<SmartCandlestick> candlesticks)
        {
            if (candlesticks.Count < 2) // exit if list is already normalized
            {
                return;
            }

            decimal min = candlesticks[0].Low;
            foreach (SmartCandlestick candlestick in candlesticks) // loops through all candlesticks to find the max 
            {
                if (candlestick.Low < min) // checks if the current value is still the max
                {
                    min = candlestick.Low; // updates the max value
                }
            }

            decimal max = candlesticks[0].High;
            foreach (SmartCandlestick candlestick in candlesticks) // loops through all candlesticks to find the max 
            {
                if (candlestick.High > max) // checks if the current value is still the max
                {
                    max = candlestick.High; // updates the max value
                }
            }

            // gives the stock chart a bit of spacing above and below
            decimal range = max - min;
            chtStockDisplay.ChartAreas[0].AxisY.Minimum = (double)Math.Floor(min - (range * Utils.stockChartSpacing)); // sets the min value
            chtStockDisplay.ChartAreas[0].AxisY.Maximum = (double)(Math.Ceiling(max + (range * Utils.stockChartSpacing)) + 0.001m); // sets the max value (adds 0.001 to prevent tickmark issues)
        }

        /// <summary>
        /// Draws the fib levels, and graph the beauty levels.
        /// </summary>
        /// <param name="start">Start candlestick.</param>
        /// <param name="end">End candlestick</param>
        private void FibonacciLogic(SmartCandlestick start, SmartCandlestick end)
        {
            List<decimal> drawLevels = Utils.GetLevels(start, end); // gets the initial fib retracement levels

            foreach (decimal level in drawLevels) // loops through all levels in the list
            {
                Utils.CreateStripLine(chtStockDisplay.ChartAreas[0], level); // draws the current level
            }

            GraphBeauty(start, end); // graphs the beauty levels
        }

        /// <summary>
        /// Graphs the beauty levels.
        /// </summary>
        /// <param name="start">Start candlestick.</param>
        /// <param name="end">End candlestick.</param>
        private void GraphBeauty(SmartCandlestick start, SmartCandlestick end)
        {
            int waveIndexStart = _filteredCurr.IndexOf(start); // finds the index of the start candlestick
            int waveIndexEnd = _filteredCurr.IndexOf(end); // finds the index of the end candlestick
            List<SmartCandlestick> waveCandlesticks = _filteredCurr.GetRange(waveIndexStart, (waveIndexEnd - waveIndexStart + 1)); // gets the list of all candlesticks between the start and end waves

            decimal valueStart;
            decimal valueEnd;
            decimal valueInitial;
            decimal valueFinal;

            decimal range;
            if (start.IsPeak) // if the wave start is a peak, then the beauty levels are calculated slightly below the end valley
            {
                valueStart = start.High;
                valueEnd = end.Low;
                range = valueStart - valueEnd;

                valueInitial = end.Low + (range * Utils.beautyAnalysisCutoff); // slightly above the low
                valueFinal = end.Low - (range * Utils.beautyAnalysisCutoff); // slightly below the low
            }

            else // if the wave start is a valley, then the beauty levels are calculated slightly above the end peak
            {
                valueStart = start.Low;
                valueEnd = end.High;
                range = valueEnd - valueStart;

                valueInitial = end.High - (range * Utils.beautyAnalysisCutoff); // slightly below the high
                valueFinal = end.High + (range * Utils.beautyAnalysisCutoff); // slightly above the high
            }

            List<decimal> valueSave = new List<decimal>();
            List<int> beautySave = new List<int>();
            decimal valueStep = (valueFinal - valueInitial) / Utils.beautyDataSize; // calculates the step size used for each consecutive level
            for (int i = 0; i < Utils.beautyDataSize; i++) // loops for each data point
            {
                decimal valueCurrent = valueInitial + (i * valueStep); // current value is the difference between the max/min and the next step

                decimal[] fibLevels = Utils.GetLevels(valueStart, valueCurrent); // gets the fib levels for the current step
                decimal[,] fibRange = new decimal[7, 2];
                for (int r = 0; r < 7; r++) // finds the ranges of all fib levels
                {
                    fibRange[r, 0] = fibLevels[r] - (range * Utils.beautyFibCutoff); // min range value
                    fibRange[r, 1] = fibLevels[r] + (range * Utils.beautyFibCutoff); // max range value
                }

                int beautyCount = 0; // keeps track of the beauty count
                for (int j = 0; j < 7; j++) // loops through all fib retracement levels
                {
                    for (int k = 0; k < waveCandlesticks.Count; k++) // each datapoint for all candlesticks in the wave are checked to see if they add to the beauty count
                    {
                        // candlestick high needs to be slightly within the level
                        if ((fibRange[j, 0] < waveCandlesticks[k].High) && (waveCandlesticks[k].High < fibRange[j, 1]))
                            beautyCount++;

                        // candlestick low needs to be slightly within the level
                        if ((fibRange[j, 0] < waveCandlesticks[k].Low) && (waveCandlesticks[k].Low < fibRange[j, 1]))
                            beautyCount++;

                        // candlestick open needs to be slightly within the level
                        if ((fibRange[j, 0] < waveCandlesticks[k].Open) && (waveCandlesticks[k].Open < fibRange[j, 1]))
                            beautyCount++;

                        // candlestick close needs to be slightly within the level
                        if ((fibRange[j, 0] < waveCandlesticks[k].Close) && (waveCandlesticks[k].Close < fibRange[j, 1]))
                            beautyCount++;
                    }
                }

                valueSave.Add(valueCurrent); // saves the current value
                beautySave.Add(beautyCount); // saves the beauty level
            }

            // normalize list
            if (valueSave[0] > valueSave[1])
            {
                valueSave.Reverse();
                beautySave.Reverse();
            }

            chtStockDisplay.Series[1].Points.DataBindXY(valueSave, beautySave); // graph all beauty values

            // create spacing between top/bottom of the chart and line
            int beautyMin = beautySave.Min();
            int beautyMax = beautySave.Max();
            int beautyRange = beautyMax - beautyMin;
            chtStockDisplay.ChartAreas[1].AxisY.Minimum = beautyMin - (int)(beautyRange * Utils.beautyChartSpacing);
            chtStockDisplay.ChartAreas[1].AxisY.Maximum = beautyMax + (int)(beautyRange * Utils.beautyChartSpacing);

            // prevents min and max from being equal
            if (chtStockDisplay.ChartAreas[1].AxisY.Minimum == chtStockDisplay.ChartAreas[1].AxisY.Maximum)
                chtStockDisplay.ChartAreas[1].AxisY.Maximum += 1;

            Utils.CreateStripLine(chtStockDisplay.ChartAreas[1], Utils.beautyDataSize / 2); // creates marking to signify valueEnd
        }

        /// <summary>
        /// Draws striplines based on the current wave start/end values, and fib levels.
        /// </summary>
        void DrawWaveLines()
        {
            // deletes old strip lines
            Utils.DeleteStripLines(chtStockDisplay.ChartAreas[0]);
            Utils.DeleteStripLines(chtStockDisplay.ChartAreas[1]);

            SmartCandlestick start = (SmartCandlestick)cmbWaveStart.SelectedItem; // cmb selected start candlestick
            SmartCandlestick end = (SmartCandlestick)cmbWaveEnd.SelectedItem; // cmb selected end candlestick

            Utils.CreateStripLine(chtStockDisplay.ChartAreas[0], _filteredCurr.IndexOf(start)); // draws a vertical line at the wave start
            Utils.CreateStripLine(chtStockDisplay.ChartAreas[0], _filteredCurr.IndexOf(end)); // draws a vertical line at the wave end

            FibonacciLogic(start, end); // draws the Fibonacci lines and calculates the beauty chart based on those lines
        }

        /// <summary>
        /// Draws lines and labels on the chart.
        /// </summary>
        /// <param name="filteredCandlesticks">The candlesticks that are shown on the chart.</param>
        private void DrawLabels(List<SmartCandlestick> filteredCandlesticks)
        {
            Utils.DeleteLabelAnnotations(chtStockDisplay); // deletes old annotations

            // loops through all candlesticks, creates a label if the candlestick has the selected pattern
            for (int i = 0; i < filteredCandlesticks.Count; i++)
            {
                SmartCandlestick candlestick = filteredCandlesticks[i]; // gets the next candlestick

                string selectedPattern = cmbPattern.SelectedItem.ToString();

                // creates the pattern labels if the text is selected within the ComboBox
                if (candlestick.IsType(selectedPattern))
                {
                    Color textColor;
                    if (selectedPattern == "Peak" && candlestick.IsPeak) // green text color if peak
                        textColor = Color.Green;
                    else if (selectedPattern == "Valley" && candlestick.IsValley) // red text color if valley
                        textColor = Color.Red;
                    else // black text color otherwise
                        textColor = Color.Black;

                    // creates the label
                    var patternAnnotation = new CalloutAnnotation // creates a new object to be used as a textbox
                    {
                        Text = selectedPattern, // sets the text to the selected pattern

                        AnchorDataPoint = chtStockDisplay.Series[0].Points[i],
                        AnchorY = (double)(candlestick.Low + candlestick.High) / 2, // points to the middle of the candlestick
                        AnchorAlignment = ContentAlignment.BottomCenter, // alignment
                        CalloutStyle = CalloutStyle.Rectangle, // alignment
                        ClipToChartArea = chtStockDisplay.ChartAreas[0].Name, // chart area
                        Font = new Font("Arial", 8, FontStyle.Bold), // sets the font

                        ForeColor = textColor, // text color
                        LineColor = Color.Black, // box perimeter color
                    };
                    chtStockDisplay.Annotations.Add(patternAnnotation); // adds the textbox
                }
            }
        }

    }
}
