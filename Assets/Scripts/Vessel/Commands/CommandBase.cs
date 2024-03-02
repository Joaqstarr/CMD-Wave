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

    private void Start()
    {
        if (_addByDefault)
        {
            GameObject.Find("Console").GetComponent<CommandLineManager>().AddCommand(this);
        }
    }
    public abstract string[] Execute(out CommandContext overrideContext, string arg =null);

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
