using UnityEngine;

public abstract class CommandBase : MonoBehaviour
{
    [SerializeField]
    protected string _commandName;
    [SerializeField]
    protected string _commandDescription;
    [SerializeField]
    protected bool _shouldClear;
    [SerializeField]
    private bool _addByDefault;
    [SerializeField]
    protected AudioClip _soundWhenExecuted;

    private void Start()
    {
        if (_addByDefault && CommandLineManager.Instance != null)
        {
            CommandLineManager.Instance.AddCommand(this);
        }
    }
    public abstract string[] Execute(out CommandContext overrideContext, out AudioClip sfx, string arg =null);

    public virtual bool CheckCommand(string commandName)
    {
        return _commandName.ToLower() == commandName.ToLower();
    }

    public bool ShouldClear{

        get { return _shouldClear; }

        }
    public virtual string GetHelpLine()
    {
        return _commandName.ToUpper() + ": " + _commandDescription;
    }
}
