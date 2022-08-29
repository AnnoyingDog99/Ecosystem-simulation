using System.Collections;
using System.Collections.Generic;
class CommandHistory
{
    private Stack<ICommand> commandUndoStack = new Stack<ICommand>();
    private Stack<ICommand> commandRedoStack = new Stack<ICommand>();

    public CommandHistory()
    {

    }

    public void Push(ICommand command)
    {
        this.commandUndoStack.Push(command);
        this.commandRedoStack.Clear();
    }

    public ICommand PopNextUndo()
    {
        ICommand command = this.commandUndoStack.Pop();
        this.commandRedoStack.Push(command);
        return command;
    }

    public ICommand PopNextRedo()
    {
        ICommand command = this.commandRedoStack.Pop();
        this.commandUndoStack.Push(command);
        return command;
    }

    public bool IsUndoStackEmpty()
    {
        return this.commandUndoStack.Count == 0;
    }

    public bool IsRedoStackEmpty()
    {
        return this.commandRedoStack.Count == 0;
    }
}