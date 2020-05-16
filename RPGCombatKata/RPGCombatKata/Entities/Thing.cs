namespace RPGCombatKata.Entities
{
    public class Thing
    {
        public Thing(int id, string name, int health)
        {
            Id = id;
            Name = name;
            Health = health;
            Destroyed = false;
        }
        public int Id { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public bool Destroyed { get; set; }
    }
}
