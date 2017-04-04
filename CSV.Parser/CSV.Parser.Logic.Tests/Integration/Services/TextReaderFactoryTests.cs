using System.Collections.Generic;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Configurations;
using CSV.Parser.Logic.Factories;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Services
{
    public class TextReaderFactoryTests
    {
        private static readonly IEncodingConfiguration EncodingConfiguration = new EncodingConfiguration();

        private readonly ITextReaderFactory _textReaderFactory;

        public TextReaderFactoryTests()
        {
            _textReaderFactory = new TextReaderFactory(EncodingConfiguration);
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void GetCurrentEncoding_Should_Return_Expected_Encoding(string filePath, Encoding expectedEncoding)
        {
            // Arrange

            // Act
            var actualResult = _textReaderFactory.GetCurrentEncoding(filePath);

            // Assert
            Assert.Equal(expectedEncoding, actualResult);
        }

        private static IEnumerable<object[]> GetTestCases()
        {
            var defaultEncoding = EncodingConfiguration.DefaultInputEncoding;

            yield return new object[] { @".\Integration\TestCases\Test.00.UTF8.No.BOM.txt", defaultEncoding };
            yield return new object[] { @".\Integration\TestCases\Test.01.UTF8.BOM.txt", Encoding.UTF8 };
            yield return new object[] { @".\Integration\TestCases\Test.02.UTF8.BOM.FieldWithCrLf.txt", Encoding.UTF8 };
            yield return new object[] { @".\Integration\TestCases\Test.03.Empty.No.BOM.txt", defaultEncoding };
            yield return new object[] { @".\Integration\TestCases\Test.04.UTF16BE.BOM.txt", Encoding.BigEndianUnicode };
            yield return new object[] { @".\Integration\TestCases\Test.05.UTF16LE.BOM.txt", Encoding.Unicode };
            yield return new object[] { @".\Integration\TestCases\Test.06.ANSII.Windows-1250.No.BOM.txt", defaultEncoding };
            yield return new object[] { @".\Integration\TestCases\Test.07.UTF32.BOM.txt", Encoding.UTF32 };
            yield return new object[] { @".\Integration\TestCases\Test.08.UTF8.BOM.EmptyLinesOnly.txt", Encoding.UTF8 };
        }
    }
}