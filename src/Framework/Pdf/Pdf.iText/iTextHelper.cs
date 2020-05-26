using System.IO;
using iText.Kernel.Pdf;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Pdf.iText
{
    [Singleton]
    // ReSharper disable once InconsistentNaming
    public class iTextHelper
    {
        #region ==OpenRead==

        public PdfDocument OpenRead(string filePath)
        {
            CheckPath(filePath);

            var reader = new PdfReader(filePath);
            return new PdfDocument(reader);
        }

        public PdfDocument OpenRead(string filePath, ReaderProperties readerProperties)
        {
            CheckPath(filePath);
            Check.NotNull(readerProperties, nameof(readerProperties), "readerProperties is null");

            var reader = new PdfReader(filePath, readerProperties);
            return new PdfDocument(reader);
        }

        public PdfDocument OpenRead(Stream stream)
        {
            Check.NotNull(stream, nameof(stream), "stream is null");

            var reader = new PdfReader(stream);
            return new PdfDocument(reader);
        }

        public PdfDocument OpenRead(Stream stream, ReaderProperties readerProperties)
        {
            Check.NotNull(stream, nameof(stream), "stream is null");
            Check.NotNull(readerProperties, nameof(readerProperties), "readerProperties is null");

            var reader = new PdfReader(stream, readerProperties);
            return new PdfDocument(reader);
        }


        #endregion

        #region ==OpenWriter==

        public PdfDocument OpenWriter(string filePath)
        {
            CheckPath(filePath);

            var writer = new PdfWriter(filePath);
            return new PdfDocument(writer);
        }

        public PdfDocument OpenWriter(string filePath, WriterProperties writerProperties)
        {
            CheckPath(filePath);
            Check.NotNull(writerProperties, nameof(writerProperties), "writerProperties is null");

            var writer = new PdfWriter(filePath, writerProperties);
            return new PdfDocument(writer);
        }

        public PdfDocument OpenWriter(Stream stream)
        {
            var writer = new PdfWriter(stream);
            return new PdfDocument(writer);
        }

        public PdfDocument OpenWriter(Stream stream, WriterProperties writerProperties)
        {
            Check.NotNull(writerProperties, nameof(writerProperties), "writerProperties is null");

            var writer = new PdfWriter(stream, writerProperties);
            return new PdfDocument(writer);
        }

        #endregion

        /// <summary>
        /// 监测文件路径
        /// </summary>
        /// <param name="filePath"></param>
        private void CheckPath(string filePath)
        {
            Check.NotNull(filePath, nameof(filePath), "file path is null");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("file not found", filePath);
        }
    }
}
