using System;
using System.Collections.Generic;

namespace VisitorPattern
{
    public interface IDocumentVisitor
    {
        void Visit(TextFile document);
        void Visit(Spreadsheet document);
        void Visit(PresentationFile document);
    }

    public interface IDocument
    {
        void Accept(IDocumentVisitor visitor);
    }

    public class TextFile : IDocument
    {
        public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
    }

    public class Spreadsheet : IDocument
    {
        public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
    }

    public class PresentationFile : IDocument
    {
        public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
    }

    public class WordCounter : IDocumentVisitor
    {
        public void Visit(TextFile document) => Console.WriteLine("WordCounter -> TextFile");
        public void Visit(Spreadsheet document) => Console.WriteLine("WordCounter -> Spreadsheet");
        public void Visit(PresentationFile document) => Console.WriteLine("WordCounter -> PresentationFile");
    }

    public class TextExtractor : IDocumentVisitor
    {
        public void Visit(TextFile document) => Console.WriteLine("TextExtractor -> TextFile");
        public void Visit(Spreadsheet document) => Console.WriteLine("TextExtractor -> Spreadsheet");
        public void Visit(PresentationFile document) => Console.WriteLine("TextExtractor -> PresentationFile");
    }

    public class FormatAnalyzer : IDocumentVisitor
    {
        public void Visit(TextFile document) => Console.WriteLine("FormatAnalyzer -> TextFile");
        public void Visit(Spreadsheet document) => Console.WriteLine("FormatAnalyzer -> Spreadsheet");
        public void Visit(PresentationFile document) => Console.WriteLine("FormatAnalyzer -> PresentationFile");
    }

    class Program
    {
        static void Main()
        {
            List<IDocument> documents = new List<IDocument>
            {
                new TextFile(),
                new Spreadsheet(),
                new PresentationFile()
            };

            IDocumentVisitor wordCounter = new WordCounter();
            IDocumentVisitor textExtractor = new TextExtractor();
            IDocumentVisitor formatAnalyzer = new FormatAnalyzer();

            foreach (var doc in documents) doc.Accept(wordCounter);
            foreach (var doc in documents) doc.Accept(textExtractor);
            foreach (var doc in documents) doc.Accept(formatAnalyzer);
        }
    }
}