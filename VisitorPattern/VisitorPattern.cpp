#include <iostream>
#include <vector>

class TextFile;
class Spreadsheet;
class PresentationFile;

class IDocumentVisitor {
public:
    virtual ~IDocumentVisitor() = default;

    virtual void visit(TextFile* document) = 0;
    virtual void visit(Spreadsheet* document) = 0;
    virtual void visit(PresentationFile* document) = 0;
};

class IDocument {
public:
    virtual ~IDocument() = default;
    virtual void accept(IDocumentVisitor* visitor) = 0;
};

class TextFile : public IDocument {
public:
    
    void accept(IDocumentVisitor* visitor) override;
};

class Spreadsheet : public IDocument {
public:
    
    void accept(IDocumentVisitor* visitor) override;
};

class PresentationFile : public IDocument {
public:
    
    void accept(IDocumentVisitor* visitor) override;
};

class WordCounter : public IDocumentVisitor {
public:

    void visit(TextFile* document) override {
        std::cout << "WordCounter -> TextFile\n";
    }
    void visit(Spreadsheet* document) override {
        std::cout << "WordCounter -> Spreadsheet\n";
    }
    void visit(PresentationFile* document) override {
        std::cout << "WordCounter -> PresentationFile\n";
    }
};

class TextExtractor : public IDocumentVisitor {
public:

    void visit(TextFile* document) override {
        std::cout << "TextExtractor -> TextFile\n";
    }
    void visit(Spreadsheet* document) override {
        std::cout << "TextExtractor -> Spreadsheet\n";
    }
    void visit(PresentationFile* document) override {
        std::cout << "TextExtractor -> PresentationFile\n";
    }
};

class FormatAnalyzer : public IDocumentVisitor {
public:

    void visit(TextFile* document) override {
        std::cout << "FormatAnalyzer -> TextFile\n";
    }
    void visit(Spreadsheet* document) override {
        std::cout << "FormatAnalyzer -> Spreadsheet\n";
    }
    void visit(PresentationFile* document) override {
        std::cout << "FormatAnalyzer -> PresentationFile\n";
    }
};

void TextFile::accept(IDocumentVisitor* visitor) {
    visitor->visit(this);
}

void Spreadsheet::accept(IDocumentVisitor* visitor) {
    visitor->visit(this);
}

void PresentationFile::accept(IDocumentVisitor* visitor) {
    visitor->visit(this);
}

int main() {
    std::vector<IDocument*> documents = {
        new TextFile(),
        new Spreadsheet(),
        new PresentationFile()
    };

    WordCounter wordCounter;
    TextExtractor textExtractor;
    FormatAnalyzer formatAnalyzer;

    for (auto doc : documents) {
        doc->accept(&wordCounter);
    }

    for (auto doc : documents) {
        doc->accept(&textExtractor);
    }

    for (auto doc : documents) {
        doc->accept(&formatAnalyzer);
    }

    for (auto doc : documents) {
        delete doc;
    }

    return 0;
}
