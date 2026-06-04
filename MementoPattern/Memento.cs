using System;
using System.Collections.Generic;

public interface IMemento 
{
}

public class Canvas {
    private string color;
    private string content;
    private string border;

    public Canvas(string col, string con, string bor) => (color, content, border) = (col, con, bor);

    public IMemento save() {
        return new CanvasMemento(color, content, border);
    }

    public void restore(IMemento snapShot) {
        if (snapShot is CanvasMemento concreteMemento) {
            color = concreteMemento.color;
            content = concreteMemento.content;
            border = concreteMemento.border; 
        }
    }

    public void updateAttributes(string col, string con, string bor) {
        color = col;
        content = con;
        border = bor;
    }

    public void printState() {
        Console.WriteLine($"Current Canvas State -> Color: {color}, Content: {content}, Border: {border}");
    }

    private class CanvasMemento : IMemento {
        public string color { get; }
        public string content { get; }
        public string border { get; }

        public CanvasMemento(string col, string con, string bor) => (color, content, border) = (col, con, bor);
    }
}

public class HistoryManager {
    private Stack<IMemento> undoStack, redoStack;

    public HistoryManager() => (undoStack, redoStack) = (new Stack<IMemento>(), new Stack<IMemento>());

    public void saveState(IMemento snapShot) {
        undoStack.Push(snapShot);
        redoStack.Clear(); 
    }

    public IMemento undo() {
        if (undoStack.Count <= 1)
            return null; 

        redoStack.Push(undoStack.Pop());
        return undoStack.Peek();
    }

    public IMemento redo() {
        if (redoStack.Count == 0)
            return null; 

        undoStack.Push(redoStack.Pop());
        return undoStack.Peek();
    }
}

class Program {
    static void Main() {
        HistoryManager historyManager = new HistoryManager();

        Console.WriteLine("--- 1. Initial State ---");
        Canvas canvas = new Canvas("green", "tree", "black");
        historyManager.saveState(canvas.save());
        canvas.printState();

        Console.WriteLine("\n--- 2. Update Attributes ---");
        canvas.updateAttributes("gray", "wood", "none");        
        historyManager.saveState(canvas.save());
        canvas.printState();

        Console.WriteLine("\n--- 3. First Undo ---");
        canvas.restore(historyManager.undo());
        canvas.printState();

        Console.WriteLine("\n--- 4. Second Undo (Boundary Guard) ---");
        canvas.restore(historyManager.undo());
        canvas.printState();

        Console.WriteLine("\n--- 5. First Redo ---");
        canvas.restore(historyManager.redo());
        canvas.printState();

        Console.WriteLine("\n--- 6. Second Redo (Boundary Guard) ---");
        canvas.restore(historyManager.redo());
        canvas.printState();
    }
}