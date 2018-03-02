using System.Collections.Generic;

public interface ILeutenantGeneral : IPrivate
{
    IReadOnlyCollection<IPrivate> Privates { get; }
    void AddPrivate(IPrivate @private);
}