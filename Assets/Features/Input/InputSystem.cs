using Entitas;
using UnityEngine;

public sealed class InputSystem : IExecuteSystem, IInitializeSystem
{
    readonly Contexts contexts;
    private InputEntity inputEntity;
    private Vector3 prevMousePos;
    private bool isTouchInProgress;

    public InputSystem(Contexts contexts)
    {
        this.contexts = contexts;
    }
    
    public void Initialize()
    {
        contexts.input.isInput = true;
        inputEntity = contexts.input.inputEntity;
    }

    public void Execute()
    {
        setBurstMode();
        emitInput();
    }

    void setBurstMode()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            contexts.input.isBurstMode = !contexts.input.isBurstMode;
        }
    }

    void emitInput()
    {
        Vector3 pos = Input.mousePosition;
        
        pos = Camera.main.ScreenToWorldPoint(pos);

        
        if (Input.GetMouseButtonDown(0))
        {
            isTouchInProgress = true;
            inputEntity.AddMouseDown(new Features.Input.MouseDownComponent { value = pos });
        }

        if (prevMousePos != pos && isTouchInProgress)
        {
            inputEntity.ReplaceMouseDown(new Features.Input.MouseDownComponent { value = pos });
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isTouchInProgress = false;
            if(inputEntity.hasMouseDown)
                inputEntity.RemoveMouseDown();
            
            inputEntity.ReplaceMouseUp(new Features.Input.MouseUpComponent { value = pos });
        }
    }

   
}
