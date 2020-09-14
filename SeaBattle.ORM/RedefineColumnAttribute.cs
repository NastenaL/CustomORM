namespace SeaBattle.ORM
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class RedefineColumnAttribute : Attribute
    {
        public string newName;
        public RedefineColumnAttribute(string newName)
        {
            this.newName = newName;
        }
    }
}
