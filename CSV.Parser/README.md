# CSV.Parser
* It is CSV parser that should parse CSV files compatible with the standard for the CSV format defined by [RFC 4180](https://tools.ietf.org/html/rfc4180).

## Notes

### Encoding examples without BOM:

* Encoding.GetEncoding(852) - It works with polish characters in Console.Output
* Encoding.GetEncoding("Windows-1250")
* Encoding.ASCII
* new UTF8Encoding(false, true)
* new UnicodeEncoding(false, false, true)

## DoD / TODO

* Implement in "clean code" way.
* Add unit tests.
* Clean all TODO's and comments in solution (implement, fix or move it to readme or "backlog").
* Clean R# code issues.
* Remove useless references.

## Remarks / TODO

### CsvWriter
* REMARKS: It is not part of coding exercise but it can be usefull to generate some test csv files.

### TextWriterFactory
* REMARKS: GetFilePath - Change to some safe place to write output file and add some unique {Guid.NewGuid():N} or timestamp.

### CsvStreamReaderFactory and others
* REMARKS: Consider using DI Container - Autofac or Castle

### CsvReaderTests
* REMARKS: Read_Should_Parse_Csv_File - Returned IList<ICsvLine> can be asserted using FluentAssertions.

### CsvWriterTests
* REMARKS: Write_Should_Write_To_Csv_File - Check manually & clean results (physical file written on disc).