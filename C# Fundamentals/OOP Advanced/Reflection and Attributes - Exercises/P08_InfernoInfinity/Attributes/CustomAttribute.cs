using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class)]
public class CustomAttribute : Attribute
{
    private readonly string _author;
    private readonly int _revision;
    private readonly string _description;
    private readonly ICollection<string> _reviewers;

    public CustomAttribute(string author, int revision, string description, params string[] reviewers)
    {
        _author = author;
        _revision = revision;
        _description = description;
        _reviewers = reviewers;
    }

    public string Author => $"Author: {_author}";

    public string Revision => $"Revision: {_revision}";

    public string Description => $"Class description: {_description}";

    public string Reviewers => $"Reviewers: {string.Join(", ", _reviewers)}";
}