using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    CommandHistory history = new CommandHistory();

    public CommandInvoker()
    {

    }

    public void Execute(ICommand command)
    {
        command.Execute();
        this.history.Push(command);
    }

    public void Undo()
    {
        if (this.history.IsUndoStackEmpty()) return;
        this.history.PopNextUndo().Undo();
    }

    public void Redo()
    {
        if (this.history.IsRedoStackEmpty()) return;
        this.history.PopNextRedo().Redo();
    }
}