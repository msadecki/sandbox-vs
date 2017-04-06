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
* It is not part of coding exercise but it can be usefull to generate some test csv files.

### TextWriterFactory
* GetFilePath - Change to some safe place to write output file and add some unique {Guid.NewGuid():N} or timestamp.

### CsvStreamReaderFactory and others
* Consider using DI Container - Autofac or Castle

### CsvCharacterParser - TO VERIFY
* REMARKS: Empty lines should not be ignored (TODO: How to count lines in 1 column CSV - if all fields are empty then ..., if some are nonempty then ... ?)
* REMARKS: First line should determine fields count - use it to validate all other lines (use exception throwing or some validation result)
* REMARKS: ParseTail - Verify that it always works correctly. See doc "The last field in the record must not be followed by a comma").

### CsvFieldBuilder
* TODO: InitNewLine - Check fields count with first line on every InitNewLine (CSV.14.UTF8.BOM.FieldsCount.Invalid.txt, ...)

### CachedCsvReader
* TODO: Remove it - in test just decorate ICsvLineConsumer and mock ICsvLineConsumerFactory (move test cases from CachedCsvReaderTests to CsvReaderTests).

### CsvReaderTests
* TODO: Read_Should_Parse_Csv_File - Here we can assert returned IList<ICsvLine> using FluentAssertions.

### CsvWriterTests
* REMARKS: Write_Should_Write_To_Csv_File - Check manually & clean results (physical file written on disc).