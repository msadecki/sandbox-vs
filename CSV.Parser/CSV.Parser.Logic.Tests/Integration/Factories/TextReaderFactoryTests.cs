using System.Collections.Generic;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Configurations;
using CSV.Parser.Logic.Abstractions.Interfaces.Factories;
using CSV.Parser.Logic.Factories;
using Moq;
using Xunit;

namespace CSV.Parser.Logic.Tests.Integration.Factories
{
    public class TextReaderFactoryTests
    {
        private readonly ITextReaderFactory _textReaderFactory;
        private readonly Mock<IEncodingConfiguration> _encodingConfigurationMock = new Mock<IEncodingConfiguration>();

        public TextReaderFactoryTests()
        {
            _textReaderFactory = new TextReaderFactory();
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Create_Should_Return_TextReader_That_Read_FileContent_Correctly(
            string filePath,
            Encoding defaultFileInputEncoding,
            string expectedFileContent)
        {
            // Arrange
            _encodingConfigurationMock
                .SetupGet(x => x.DefaultFileInputEncoding)
                .Returns(defaultFileInputEncoding);

            // Act
            string actualFileContent;
            using (var textReader = _textReaderFactory.Create(filePath, _encodingConfigurationMock.Object))
            {
                actualFileContent = textReader.ReadToEnd();
            }

            // Assert
            Assert.Equal(expectedFileContent, actualFileContent);

            _encodingConfigurationMock
                .VerifyGet(x => x.DefaultFileInputEncoding, Times.Once);
        }

        private static IEnumerable<object[]> GetTestCases()
        {
            const string expectedFileContent2Lines =
@"""Polish Accented Characters: [πÊÍ≥ÒÛúüø], [•∆ £—”åèØ]""
Second Line";

            const string expectedFileContent4WindowsEndOfLines =
@"



";

            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.01.UTF8.No.BOM.2.Lines.txt", Encoding.UTF8, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.02.UTF8.BOM.2.Lines.txt", Encoding.UTF8, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.02.UTF8.BOM.2.Lines.txt", Encoding.UTF32, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.03.UTF16BE.BOM.2.Lines.txt", Encoding.BigEndianUnicode, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.03.UTF16BE.BOM.2.Lines.txt", Encoding.Unicode, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.04.UTF16LE.BOM.2.Lines.txt", Encoding.Unicode, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.04.UTF16LE.BOM.2.Lines.txt", Encoding.BigEndianUnicode, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.05.UTF32.BOM.2.Lines.txt", Encoding.UTF32, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.05.UTF32.BOM.2.Lines.txt", Encoding.UTF8, expectedFileContent2Lines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.06.UTF8.BOM.Empty.txt", Encoding.UTF8, string.Empty };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.06.UTF8.BOM.Empty.txt", Encoding.UTF32, string.Empty };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.07.No.BOM.Empty.txt", Encoding.UTF8, string.Empty };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.08.UTF8.BOM.4.CRLF.txt", Encoding.UTF8, expectedFileContent4WindowsEndOfLines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.08.UTF8.BOM.4.CRLF.txt", Encoding.UTF32, expectedFileContent4WindowsEndOfLines };
            yield return new object[] { @".\Integration\TestCases\TextReaderFactory\TRF.09.ANSII.Windows-1250.No.BOM.2.Lines.txt", Encoding.GetEncoding("Windows-1250"), expectedFileContent2Lines };
        }
    }
}