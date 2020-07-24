using Entitas;
using UnityEngine;

public sealed class InputSystem : IExecuteSystem, IInitializeSystem
{
    readonly Contexts _contexts;
    private InputEntity _inputEntity;
    private Vector3 prevMousePos;
    private bool isTouchInProgress;

    public InputSystem(Contexts contexts)
    {
        _contexts = contexts;
    }
    
    public void Initialize()
    {
        _contexts.input.isInput = true;
        _inputEntity = _contexts.input.inputEntity;
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
            _contexts.input.isBurstMode = !_contexts.input.isBurstMode;
        }
    }

    void emitInput()
    {
        Vector3 pos = Input.mousePosition;
        
        pos = Camera.main.ScreenToWorldPoint(pos);

        
        if (Input.GetMouseButtonDown(0))
        {
            isTouchInProgress = true;
            _inputEntity.AddMouseDown(new Features.Input.MouseDownComponent { value = pos });
        }

        if (prevMousePos != pos && isTouchInProgress)
        {
            _inputEntity.ReplaceMouseDown(new Features.Input.MouseDownComponent { value = pos });
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isTouchInProgress = false;
            if(_inputEntity.hasMouseDown)
                _inputEntity.RemoveMouseDown();
            
            _inputEntity.ReplaceMouseUp(new Features.Input.MouseUpComponent { value = pos });
        }
    }

   
}
