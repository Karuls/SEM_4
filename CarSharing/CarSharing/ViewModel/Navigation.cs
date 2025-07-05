using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

public static class Navigation
{
    private static Stack<Window> undoStack = new Stack<Window>();
    private static Stack<Window> redoStack = new Stack<Window>();
    private static Window currentWindow;

    public static void OpenWindow(Window newWindow)
    {
        if (newWindow == null)
            throw new ArgumentNullException(nameof(newWindow));

        //MessageBox.Show($"Before OpenWindow: UndoStack={undoStack.Count}, RedoStack={redoStack.Count}");

        if (currentWindow != null)
        {
            undoStack.Push(currentWindow);
            //MessageBox.Show($"Pushed to undo: {currentWindow.Title}");
            currentWindow.Close(); 
        }

        currentWindow = newWindow;
        currentWindow.Show();
        redoStack.Clear();

        //MessageBox.Show($"After OpenWindow: UndoStack={undoStack.Count}, RedoStack={redoStack.Count}");
    }

    public static void Undo()
    {
       // MessageBox.Show($"Undo called. CanUndo: {undoStack.Count > 0}");

        if (undoStack.Count == 0)
        {
            //MessageBox.Show("UndoStack is empty!");
            return;
        }

        if (currentWindow != null)
        {
            redoStack.Push(currentWindow);
            currentWindow.Close();
        }

        var previousWindowType = undoStack.Pop().GetType();
        currentWindow = (Window)Activator.CreateInstance(previousWindowType);
        currentWindow.Show();

        //MessageBox.Show($"After Undo: UndoStack={undoStack.Count}, RedoStack={redoStack.Count}");
    }

    public static void Redo()
    {
        //MessageBox.Show($"Redo called. CanRedo: {redoStack.Count > 0}");

        if (redoStack.Count == 0)
        {
           // MessageBox.Show("RedoStack is empty!");
            return;
        }

        if (currentWindow != null)
        {
            undoStack.Push(currentWindow);
            currentWindow.Close();
        }

        var previousWindowType = redoStack.Pop().GetType();
        currentWindow = (Window)Activator.CreateInstance(previousWindowType);
        currentWindow.Show();

        //MessageBox.Show($"After Redo: UndoStack={undoStack.Count}, RedoStack={redoStack.Count}");
    }

    public static bool CanUndo => undoStack.Count > 0;
    public static bool CanRedo => redoStack.Count > 0;
}