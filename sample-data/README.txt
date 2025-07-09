Market-Fib Example Stock Data
=============================

This folder contains example stock data for use within the Market-Fib application.
These files can be used to test the software's functionality, such as candlestick pattern recognition, wave selection, and Fibonacci alignment scoring.

The included "AAPL", "AMZN", "MSFT", and "NVDA" CSV files contain real stock data from 2020 to 2021. These stocks can be used for wave analysis.
The "EXAMPLE" CSV files contains artificial data and therefore should not be used for wave analysis due to not reflecting realistic market behavior.

File Formatting:

    Files must be in the CSV format (.csv extension).

    Filenames:
        It is recommended for filenames to be in the format: "Stock-Period.csv".
        While not required, using this format allows the application to separate the stock ticker from the sample period, making it easier to organize and select data.

    Headers:
        Headers in CSV files are optional. If used, headers can span multiple lines.

    Delimiters:
        The following delimiters are supported: ',', ';', '\t' (tab), '|', and '^'.
        
        Spaces (' ') are not valid delimiters.

    CSV Data:
        - The "AAPL", "AMZN", "MSFT", and "NVDA" CSV files are examples of datasets that do not include time information (e.g., hours/minutes/seconds).

        - The "EXAMPLE" CSV files are examples of datasets with time information. 

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

        - Dates may be enclosed in quotes ("), but other data fields cannot.

        - Open, high, low, and close data can be provided with high decimal precision.

        - Volume data must be integer values.