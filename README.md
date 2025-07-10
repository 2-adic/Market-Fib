# Market-Fib

### About:

Market-Fib is a Windows Forms application designed for the analysis of Fibonacci retracement levels within stock price data. It provides an interactive approach to wave analysis, allowing users to manually select wave structures. The alignment scores for these selected wave structures are shown visually, which lets users see which wave structures are historically significant.

### Info:

Market-Fib was developed using C# in Microsoft Visual Studio. As a Windows Form application, it is only compatible with Windows systems.

This project was independently developed as part of a Software System Design course. It is published for educational and portfolio purposes only. Do not use this code for academic coursework.

> [!WARNING]
> This application is provided "as-is" for informational purposes only. It is not intended to be a source of financial advice. You are solely responsible for the accuracy of the data you provide and for any financial decisions you make based on the output of this software.

# How to Use

### Setup:

1. Download the latest release and sample stock data.

2. Decompress the `.zip` file(s).

3. Run `MarketFib.exe`.

### Usage:

When launched, the application displays a window prompting the user user to select CSV files.

Selector Window:

- Adjust the start/end dates, and then choose the window type.

- Select the `.csv` file(s) to analyze.
> [!IMPORTANT]
> CSV files must be formatted correctly.<br>
> Refer to the [File Format](#File-Format) section below for detailed requirements.

Once the CSV files are chosen, one or more windows will open to display the stock data using candlestick charts.

Stock Analyzer Window:

- Adjust the start/end dates if needed.

- Select the wave start/end. Alignment scores will be displayed for the selected wave.

- Choose a pattern type from the dropdown menu to display candlestick patterns.

- In single-window mode, switch the stock and period via the corresponding dropdowns.

# File Format

All files must be in the `.csv` file format.

### Filenames:

It is recommended for filenames to be in the format: "Stock-Period.csv". While not required, using this format allows the application to separate the stock ticker from the sample period, making it easier to organize and select data.

### Headers:

Headers in CSV files are optional. If used, headers can span multiple lines.

### Delimiters:

The following delimiters are supported: `,`, `;`, `\t` (tab), `|`, and `^`.

**Spaces are not valid delimiters.**

### CSV Data:

- Empty data rows are ignored.

- CSVs are required to have data in the following order: date, open, high, low, close, volume.

- Date data must be in one of the following formats:

    - Date and time:
        - yyyy-MM-dd HH:mm:ss
        - yyyy-MM-dd HH:mm
        - yyyy-MM-dd HH

    - Date only:
        - yyyy-MM-dd
        - yyyy/MM/dd
        - yyyyMMdd
        - MM/dd/yyyy
        - dd-MM-yyyy
        - M/d/yyyy
        - d-M-yyyy

- Dates may be enclosed in quotation marks `"`, but other data fields cannot.

- Open, high, low, and close data may be provided with high decimal precision.

- Volume data must be integer values.

### Sample Data:

Sample stock data is available in the `sample-data` directory which can be downloaded download from the Releases page.

The included "AAPL", "AMZN", "MSFT", and "NVDA" CSV files contain real stock data from 2020 to 2021. These stocks can be used for wave analysis. They are examples of datasets that do not include time information (e.g., hours/minutes/seconds).

The "EXAMPLE" CSV files contains artificial data and therefore should not be used for wave analysis due to not reflecting realistic market behavior. They are examples of datasets with time information. 

# How to Build

Download the **Visual Studio 2022 (Community Edition)** on the [official Visual Studio website](https://visualstudio.microsoft.com/vs/).

Select the **.NET desktop development** workload and click the install button.

Download or clone this repository, then open the solution by double-clicking the `MarketFib.sln` file.
