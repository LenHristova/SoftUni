﻿namespace TeamBuilder.App.Core.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
