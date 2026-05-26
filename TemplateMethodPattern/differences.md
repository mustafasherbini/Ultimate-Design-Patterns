# Template Method Pattern — C++ vs C# Key Differences

## 1. Abstract Class Definition

Both languages support abstract classes, but syntax differs.

C++:
    class VideoEditingPresets {
    public:
        virtual ~VideoEditingPresets() = default;
        virtual void renderVideo() = 0;
    };

C#:
    abstract class VideoEditingPresets
    {
        protected abstract void RenderVideo();
    }

---

## 2. Pure Virtual ( = 0 ) vs Abstract Methods

In C++, `= 0` makes the method pure virtual:
- The class becomes abstract
- Child classes MUST override it

C++:
    virtual void renderVideo() = 0;

In C#, use `abstract` keyword:

C#:
    protected abstract void RenderVideo();

---

## 3. Template Method Implementation

Both languages implement the Template Method similarly.

C++:
    void process() {
        enhanceVideoQuality();
        applyColorCorrection();
        enhanceAudioQuality();
        applyFilters();
        renderVideo();
    }

C#:
    public void Process()
    {
        EnhanceVideoQuality();
        ApplyColorCorrection();
        EnhanceAudioQuality();
        ApplyFilters();
        RenderVideo();
    }

---

## 4. Method Access Modifiers

C++ commonly uses `protected` section.

C++:
    protected:
        void applyFilters() {}

C# uses access modifiers directly on methods.

C#:
    protected void ApplyFilters() {}

---

## 5. Override Syntax

C++:
    void renderVideo() override {
    }

C#:
    protected override void RenderVideo()
    {
    }

---

## 6. Virtual Destructor

C++ requires a virtual destructor in polymorphic base classes:

C++:
    virtual ~VideoEditingPresets() = default;

Without it, deleting derived objects through base pointers may cause memory leaks.

C# does not need destructors for this purpose because:
- Memory is managed automatically
- Garbage Collector handles cleanup

---

## 7. Object Creation

C++:
    VideoEditingPresets* preset = new FHDVideoPresets();
    preset->process();

C#:
    VideoEditingPresets preset = new FHDVideoPresets();
    preset.Process();

---

## 8. Memory Management

C++:
- Manual memory management
- Uses `new/delete`

C#:
- Automatic memory management
- Uses Garbage Collector (GC)

---

## Summary Table

| Feature                     | C++                              | C#                              |
|-----------------------------|----------------------------------|---------------------------------|
| Abstract class              | class + pure virtual             | abstract class                  |
| Abstract method             | virtual void f() = 0             | abstract void F()               |
| Template method             | Regular member function          | Regular member function         |
| Override keyword            | override                         | override                        |
| Virtual destructor          | Required                         | Not needed                      |
| Memory management           | Manual (new/delete)              | Automatic (GC)                  |
| Object access               | Pointer (`->`)                   | Reference (`.`)                 |
| Access modifier style       | protected: section               | protected keyword per method    |