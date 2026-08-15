#include <iostream>
#include <string>

using namespace std;

class Data {
public:
    string info;
    string format;
    int sizeInMB;
    bool containsPersonalInformation;

    Data(
        string info,
        string format,
        int sizeInMB,
        bool containsPersonalInformation
    ) {
        this->info = info;
        this->format = format;
        this->sizeInMB = sizeInMB;
        this->containsPersonalInformation = containsPersonalInformation;
    }
};

class IHandler {
public:
    virtual ~IHandler() = default;

    virtual IHandler* SetNext(IHandler* nextHandler) = 0;

    virtual bool Handle(const Data& data) = 0;
};

class BaseDataHandler : public IHandler {
private:
    IHandler* nextHandler = nullptr;

public:
    IHandler* SetNext(IHandler* nextHandler) override {
        this->nextHandler = nextHandler;
        return nextHandler;
    }

    bool Handle(const Data& data) override {
        if (nextHandler != nullptr) {
            return nextHandler->Handle(data);
        }

        return true;
    }
};

class ValidationChecks : public BaseDataHandler {
public:
    bool Handle(const Data& data) override {
        cout << "Running Validation Check..." << endl;

        if (data.info.empty()) {
            cout << "Validation failed: data is empty." << endl;
            return false;
        }

        cout << "Validation passed." << endl;
        return BaseDataHandler::Handle(data);
    }
};

class FormattingChecks : public BaseDataHandler {
public:
    bool Handle(const Data& data) override {
        cout << "Running Formatting Check..." << endl;

        if (data.format != "CSV" && data.format != "JSON") {
            cout << "Formatting failed: unsupported format." << endl;
            return false;
        }

        cout << "Formatting passed." << endl;
        return BaseDataHandler::Handle(data);
    }
};

class DataSizeCheck : public BaseDataHandler {
private:
    int maxSizeInMB;

public:
    DataSizeCheck(int maxSizeInMB) {
        this->maxSizeInMB = maxSizeInMB;
    }

    bool Handle(const Data& data) override {
        cout << "Running Data Size Check..." << endl;

        if (data.sizeInMB > maxSizeInMB) {
            cout << "Data size failed: data is too large." << endl;
            return false;
        }

        cout << "Data size passed." << endl;
        return BaseDataHandler::Handle(data);
    }
};

class PersonalInformationChecks : public BaseDataHandler {
public:
    bool Handle(const Data& data) override {
        cout << "Running Personal Information Check..." << endl;

        if (data.containsPersonalInformation) {
            cout << "Personal information check failed: sensitive data found." << endl;
            return false;
        }

        cout << "Personal information check passed." << endl;
        return BaseDataHandler::Handle(data);
    }
};

class BatchJob {
private:
    IHandler* pipeline;

public:
    BatchJob(IHandler* pipeline) {
        this->pipeline = pipeline;
    }

    void Process(const Data& data) {
        cout << "Starting batch job pipeline..." << endl;
        cout << "-----------------------------" << endl;

        bool passed = pipeline->Handle(data);

        cout << "-----------------------------" << endl;

        if (passed) {
            cout << "All checks passed. Processing data..." << endl;
        } else {
            cout << "Pipeline failed. Data processing stopped." << endl;
        }

        cout << endl;
    }
};

int main() {
    ValidationChecks validation;
    FormattingChecks formatting;
    DataSizeCheck dataSize(100);
    PersonalInformationChecks personalInfo;

    validation
        .SetNext(&formatting)
        ->SetNext(&dataSize)
        ->SetNext(&personalInfo);

    BatchJob batchJob(&validation);

    Data validData(
        "Big data content",
        "CSV",
        50,
        false
    );

    Data invalidFormatData(
        "Some content",
        "TXT",
        20,
        false
    );

    Data largeData(
        "Huge data content",
        "JSON",
        150,
        false
    );

    Data personalData(
        "Customer private data",
        "CSV",
        30,
        true
    );

    cout << "Test Case 1: Valid Data" << endl;
    batchJob.Process(validData);

    cout << "Test Case 2: Invalid Format Data" << endl;
    batchJob.Process(invalidFormatData);

    cout << "Test Case 3: Large Data" << endl;
    batchJob.Process(largeData);

    cout << "Test Case 4: Personal Information Data" << endl;
    batchJob.Process(personalData);

    return 0;
}