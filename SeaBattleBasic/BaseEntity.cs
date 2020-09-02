namespace SeaBattleBasic
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        [Key]
        public Int64 Id { get; set; }
        public string IP { get; set; }
    }
}
