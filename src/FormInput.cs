using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketFib
{
    public partial class FormInput : Form
    {
        /// <summary>
        /// Initializes an instance of the <see cref="FormInput"> class.
        /// Sets up UI components and the form icon.
        /// </summary>
        public FormInput()
        {
            this.Icon = Properties.Resources.app; // sets the form's icon

            InitializeComponent();
        }

        /// <summary>
        /// Opens a chart form and gives it parameters that the user specified.
        /// </summary>
        /// <param name="filepaths">An array of one or more filepaths that will be displayed in one or more windows.</param>
        /// <param name="isMultiStock">If the window contains multiple stocks.</param>
        /// Determines if one or multiple will be used to display the stocks. 
        private void InitializeFormChart(string[] filepaths, bool isMultiStock)
        {
            DateTime defaultStartDate = dtpDefaultStartDate.Value; // initial start date for Form_Chart
            DateTime defaultEndDate = dtpDefaultEndDate.Value; // initial end date for Form_Chart

            FormChart displayFromChart = new FormChart(filepaths, defaultStartDate, defaultEndDate, isMultiStock); // initialize form chart
            displayFromChart.Show(); // display form chart
        }

        /// <summary>
        /// Opens a chart form if the user selects .csv files.
        /// </summary>
        private void OfdSingleWindowLoad_FileOk(object sender, CancelEventArgs e)
        {
            string[] filepaths = ofdSingleWindowLoad.FileNames; // gets the file path of the loaded file

            InitializeFormChart(filepaths, true); // creates the form
        }

        /// <summary>
        /// Opens multiple chart forms if the user selects .csv files.
        /// </summary>
        private void OfdMultiWindowLoad_FileOk(object sender, CancelEventArgs e)
        {
            string[] filepaths = ofdMultiWindowLoad.FileNames; // gets the file path of the loaded file

            // opens a form chart for each file
            foreach (string filepath in filepaths)
            {
                InitializeFormChart(new string[] { filepath }, false); // creates the form
            }
        }

        /// <summary>
        /// Prompts the user with the file selector if they press the button.
        /// Open all files in the same window.
        /// </summary>
        private void BtnSingleWindow_Click(object sender, EventArgs e)
        {
            ofdSingleWindowLoad.ShowDialog(); // opens the file selector
        }

        /// <summary>
        /// Prompts the user with the file selector if they press the button.
        /// Opens each file in a separate window.
        /// </summary>
        private void BtnMultiWindow_Click(object sender, EventArgs e)
        {
            ofdMultiWindowLoad.ShowDialog(); // opens the file selector
        }
    }
}
