#include <iostream>
#include <string>
#include <stack>

using namespace std;

class Canvas;

class CanvasMemento {
protected:
    string color;
    string content;
    string border;

public:
    CanvasMemento(string col, string con, string bor) 
        : color(col), content(con), border(bor) {}

    friend class Canvas;
};

class Canvas {
private:
    string color;
    string content;
    string border;

public:
    Canvas(string col, string con, string bor) 
        : color(col), content(con), border(bor) {}

    CanvasMemento* save() {
        return new CanvasMemento(color, content, border);
    }

    void restore(const CanvasMemento* snapShot) {
        if (snapShot != nullptr) {
            color = snapShot->color;
            content = snapShot->content;
            border = snapShot->border;
        }
    }

    void updateAttributes(string col, string con, string bor) {
        color = col;
        content = con;
        border = bor;
    }

    void printState() {
        cout << "Current Canvas State -> Color: " << color 
             << ", Content: " << content 
             << ", Border: " << border << "\n";
    }
};

class HistoryManager {
private:
    stack<const CanvasMemento*> undoStack, redoStack;

public:
    HistoryManager() = default;

    ~HistoryManager() {
        while (!redoStack.empty()) {
            delete const_cast<CanvasMemento*>(redoStack.top());
            redoStack.pop();
        }
        while (!undoStack.empty()) {
            delete const_cast<CanvasMemento*>(undoStack.top());
            undoStack.pop();
        }
    }

    void saveState(const CanvasMemento* snapShot) {
        if (snapShot == nullptr) return;
        undoStack.push(snapShot);
        while (!redoStack.empty()) {
            delete const_cast<CanvasMemento*>(redoStack.top());
            redoStack.pop();
        }
    }

    const CanvasMemento* undo() {
        if (undoStack.size() <= 1)
            return nullptr;

        redoStack.push(undoStack.top());
        undoStack.pop();
        return undoStack.top();
    }

    const CanvasMemento* redo() {
        if (redoStack.empty())
            return nullptr;

        undoStack.push(redoStack.top());
        redoStack.pop();
        return undoStack.top();
    }
};

int main() {
    HistoryManager historyManager;

    cout << "--- 1. Initial State ---\n";
    Canvas canvas("green", "tree", "black");
    historyManager.saveState(canvas.save());
    canvas.printState();

    cout << "\n--- 2. Update Attributes ---\n";
    canvas.updateAttributes("gray", "wood", "none");        
    historyManager.saveState(canvas.save());
    canvas.printState();

    cout << "\n--- 3. First Undo ---\n";
    canvas.restore(historyManager.undo());
    canvas.printState();

    cout << "\n--- 4. Second Undo (Boundary Guard) ---\n";
    canvas.restore(historyManager.undo());
    canvas.printState();

    cout << "\n--- 5. First Redo ---\n";
    canvas.restore(historyManager.redo());
    canvas.printState();

    cout << "\n--- 6. Second Redo (Boundary Guard) ---\n";
    canvas.restore(historyManager.redo());
    canvas.printState();

    return 0;
}